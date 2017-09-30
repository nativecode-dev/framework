namespace NativeCode.Core.Web.WebSockets.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Commands;
    using Core.Extensions;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Platform.Serialization;
    using Types;

    public class WebSocketRequestHandler : ICancellableAsyncRequestHandler<WebSocketServerMessage, WebSocketResponse>
    {
        private readonly ILogger logger;

        private readonly IObjectSerializer objects;

        private readonly IStringSerializer strings;

        public WebSocketRequestHandler(ILoggerFactory factory, IObjectSerializer objects, IStringSerializer strings)
        {
            this.logger = factory.CreateLogger<WebSocketRequestHandler>();
            this.objects = objects;
            this.strings = strings;
        }

        public virtual Task<WebSocketResponse> Handle(WebSocketServerMessage message, CancellationToken cancellationToken)
        {
            this.logger.JsonInfo(message, this.strings);

            var command = new Command
            {
                Name = "initialize",
                Parameters =
                {
                    { "hubid", message.Connection.HubId.ToString() }
                }
            };

            var response = this.objects.Structure(command);
            this.logger.JsonInfo(message, this.strings);
            return message.CreateResponseTask(response);
        }
    }
}