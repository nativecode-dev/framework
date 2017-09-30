namespace NativeCode.Core.Web.Extensions
{
    using Core.Extensions;
    using JetBrains.Annotations;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using WebSockets;
    using WebSockets.Management;
    using WebSockets.Types;

    public static class WebSocketExtensions
    {
        private const string HubId = "hubid";

        public static IServiceCollection AddNativeCodeWebSockets([NotNull] this IServiceCollection services)
        {
            services.AddSingleton<IHubManager, HubManager>();
            services.AddTransient<IWebSocketProcessor, WebSocketProcessor>();

            services.AddMediatR(typeof(WebSocketExtensions).Assembly);

            services.AddMemoryCache(options =>
            {
                options.CompactionPercentage = 0.02;
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(10);
            });

            return services;
        }

        public static void UseNativeCodeWebSockets([NotNull] this IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(WebSocketExtensions.HandleWebSocketRequest);
        }

        private static async Task HandleWebSocketRequest(HttpContext context, Func<Task> next)
        {
            try
            {
                // NOTE: We have to run all of the other middleware before we try to upgrade
                // the connection. There didn't seem to be a good spot to ensure this is called
                // only before closing the websocket, so we do it here first. Otherwise, we end
                // up with an ugly branching situation. Something might come along that changes
                // the validity of this assumption.
                await next().NoCapture();

                if (context.WebSockets.IsWebSocketRequest == false || context.Request.Path.HasValue == false)
                {
                    return;
                }

                if (context.Request.Path.Value.StartsWith("/ws") == false)
                {
                    return;
                }

                await WebSocketExtensions.DispatchWebSocket(context).NoCapture();
            }
            catch (Exception ex)
            {
                var factory = context.RequestServices.GetService<ILoggerFactory>();
                var logger = factory.CreateLogger(nameof(WebSocketExtensions));
                logger.Exception(ex);
            }
        }

        private static async Task DispatchWebSocket(HttpContext context)
        {
            var manager = context.RequestServices.GetService<IHubManager>();
            var socket = await context.WebSockets.AcceptWebSocketAsync().NoCapture();

            IHub hub;

            if (context.Request.Query.ContainsKey(WebSocketExtensions.HubId))
            {
                var value = context.Request.Query[WebSocketExtensions.HubId];

                if (Guid.TryParse(value, out var hubId) == false)
                {
                    throw new InvalidOperationException($"{value} is not a valid Guid.");
                }

                hub = await manager.GetHub(hubId).NoCapture();
            }
            else
            {
                hub = await manager.GetHub(Guid.NewGuid()).NoCapture();
            }

            var connection = await hub.Add(context, socket).NoCapture();

            try
            {
                var handler = context.RequestServices.GetService<IWebSocketProcessor>();
                await handler.Start(connection).NoCapture();
            }
            finally
            {
                await hub.Remove(connection).NoCapture();
            }
        }

        public static IEnumerable<Task> Broadcast<T>(this IEnumerable<WebSocketConnection> sockets, T data,
            CancellationToken token)
        {
            return sockets.Select(socket => socket.SendAsync(data, token));
        }

        public static IEnumerable<Task> BroadcastText(this IEnumerable<WebSocketConnection> sockets, string data,
            CancellationToken token)
        {
            return sockets.Select(socket => socket.SendTextAsync(data, token));
        }
    }
}