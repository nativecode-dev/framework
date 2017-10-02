namespace NativeCode.Core.Web.WebSockets.Middleware
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Management;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class WebSocketMiddleware : IWebSocketMiddleware
    {
        private const string HubId = "hubid";

        public WebSocketMiddleware(ILoggerFactory factory, IHubManager hubs, IWebSocketProcessor processor)
        {
            this.HubManager = hubs;
            this.Logger = factory.CreateLogger<WebSocketMiddleware>();
            this.Processor = processor;
        }

        protected IHubManager HubManager { get; }

        protected ILogger Logger { get; }

        protected IWebSocketProcessor Processor { get; }

        public virtual async Task Invoke(HttpContext context, Func<Task> next)
        {
            // NOTE: We have to run all of the other middleware before we try to upgrade
            // the connection. There didn't seem to be a good spot to ensure this is called
            // only before closing the websocket, so we do it here first. Otherwise, we end
            // up with an ugly branching situation. Something might come along that changes
            // the validity of this assumption, which is the error you get for trying to
            // modify the response status code.
            await next().NoCapture();

            if (context.WebSockets.IsWebSocketRequest == false || context.Request.Path.HasValue == false)
            {
                return;
            }

            if (context.Request.Path.Value.StartsWith("/ws") == false)
            {
                return;
            }

            try
            {
                await this.DispatchWebSocket(context).NoCapture();
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
            }
        }

        protected virtual async Task DispatchWebSocket(HttpContext context)
        {
            var socket = await context.WebSockets.AcceptWebSocketAsync().NoCapture();

            var hub = await this.GetWebSocketHub(context).NoCapture();
            var connection = await hub.Add(context, socket).NoCapture();

            try
            {
                await this.Processor.Start(connection).NoCapture();
            }
            finally
            {
                await hub.Remove(connection).NoCapture();
            }
        }

        protected virtual async Task<IHub> GetWebSocketHub(HttpContext context)
        {
            if (context.Request.Query.TryGetValue(WebSocketMiddleware.HubId, out var values) == false)
            {
                return await this.HubManager.CreateHub(Guid.NewGuid()).NoCapture();
            }

            if (values.Any() == false || Guid.TryParse(values.First(), out var hubId) == false)
            {
                return await this.HubManager.CreateHub(Guid.NewGuid()).NoCapture();
            }

            if (await this.HubManager.HubExists(hubId).NoCapture())
            {
                return await this.HubManager.GetOrCreateHub(hubId).NoCapture();
            }

            return await this.HubManager.CreateHub(Guid.NewGuid()).NoCapture();
        }
    }
}