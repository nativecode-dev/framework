namespace NativeCode.Core.Web.Extensions
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using WebSockets;
    using WebSockets.Management;
    using WebSockets.Middleware;

    public static class WebSocketExtensions
    {
        public static IServiceCollection AddNativeCodeWebSockets([NotNull] this IServiceCollection services)
        {
            services.AddSingleton<IHubManager, HubManager>();
            services.AddTransient<IWebSocketMiddleware, WebSocketMiddleware>();
            services.AddTransient<IWebSocketProcessor, WebSocketProcessor>();

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

        private static Task HandleWebSocketRequest(HttpContext context, Func<Task> next)
        {
            return context.RequestServices.GetService<IWebSocketMiddleware>().Invoke(context, next);
        }
    }
}