namespace NativeCode.Core.Web.WebSockets
{
    using System;
    using System.Threading.Tasks;
    using Core.Extensions;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Types;

    public class WebSocketProcessor : IWebSocketProcessor
    {
        private readonly ILogger logger;

        private readonly IMediator mediator;

        public WebSocketProcessor(ILoggerFactory factory, IMediator mediator)
        {
            this.logger = factory.CreateLogger<WebSocketProcessor>();
            this.mediator = mediator;
        }

        public async Task Start(WebSocketConnection connection)
        {
            while (connection.IsReadable)
            {
                try
                {
                    var request = await connection.GetNextAsync<WebSocketRequest>(connection.Token).NoCapture();
                    var package = new WebSocketServerMessage(connection, request);
                    var response = await this.mediator.Send(package, connection.Token).NoCapture();

                    if (response != null)
                    {
                        await connection.SendAsync(response, connection.Token).NoCapture();
                    }
                }
                catch (Exception ex)
                {
                    this.logger.Exception(ex);
                }
            }
        }
    }
}