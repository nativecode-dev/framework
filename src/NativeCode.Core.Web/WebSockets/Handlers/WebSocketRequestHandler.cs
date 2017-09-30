namespace NativeCode.Core.Web.WebSockets.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Commands;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using Platform.Serialization;
    using Types;

    public class WebSocketRequestHandler : ICancellableAsyncRequestHandler<WebSocketServerMessage, WebSocketResponse>
    {
        private readonly ILogger logger;

        private readonly IStringSerializer serializer;

        public WebSocketRequestHandler(ILoggerFactory factory, IStringSerializer serializer)
        {
            this.logger = factory.CreateLogger<WebSocketRequestHandler>();
            this.serializer = serializer;
        }

        public virtual Task<WebSocketResponse> Handle(WebSocketServerMessage message, CancellationToken cancellationToken)
        {
            this.logger.LogInformation(this.serializer.Serialize(message));

            var command = new Command
            {
                Name = "initialize",
                Parameters =
                {
                    { "hubid", message.Connection.HubId.ToString() }
                }
            };

            return message.CreateResponseTask(JObject.FromObject(command));
        }
    }
}