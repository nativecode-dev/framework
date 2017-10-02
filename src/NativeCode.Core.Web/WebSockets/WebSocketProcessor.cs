namespace NativeCode.Core.Web.WebSockets
{
    using System;
    using System.Threading.Tasks;
    using Commands;
    using Core.Extensions;
    using Microsoft.Extensions.Logging;
    using Platform.Serialization;
    using Types;

    public class WebSocketProcessor : IWebSocketProcessor
    {
        public WebSocketProcessor(ILoggerFactory factory, IObjectSerializer objects, IStringSerializer strings)
        {
            this.Logger = factory.CreateLogger<WebSocketProcessor>();
            this.ObjectSerializer = objects;
            this.StringSerializer = strings;
        }

        protected ILogger Logger { get; }

        protected IObjectSerializer ObjectSerializer { get; }

        protected IStringSerializer StringSerializer { get; }

        public async Task Start(WebSocketConnection connection)
        {
            while (connection.IsReadable)
            {
                try
                {
                    var request = await connection.GetNext<WebSocketRequest>(connection.Token).NoCapture();
                    var message = await this.BindServerMessage(connection, request).NoCapture();
                    var response = await this.GetRequestResponse(message).NoCapture();

                    if (response != null)
                    {
                        await this.HandleResponse(connection, response).NoCapture();
                    }
                }
                catch (Exception ex)
                {
                    this.Logger.Exception(ex);
                }
            }
        }

        protected virtual Task<WebSocketServerMessage> BindServerMessage(WebSocketConnection connection, WebSocketRequest request)
        {
            return Task.FromResult(WebSocketServerMessage.Bind(connection, request));
        }

        protected virtual Task<WebSocketResponse> GetRequestResponse(WebSocketServerMessage message)
        {
            this.Logger.JsonInfo(message, this.StringSerializer);

            var command = new Command
            {
                Name = "initialize",
                Parameters =
                {
                    { "hubid", message.Connection.HubId.ToString() }
                }
            };

            var response = this.ObjectSerializer.Structure(command);
            this.Logger.JsonInfo(message, this.StringSerializer);
            return message.CreateResponseTask(response);
        }

        protected virtual Task HandleResponse(WebSocketConnection connection, WebSocketResponse response)
        {
            return connection.Send(response, connection.Token);
        }
    }
}