namespace NativeCode.Core.Web.WebSockets.Types
{
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public class WebSocketServerMessage : WebSocketRequest
    {
        internal WebSocketServerMessage(WebSocketConnection connection, WebSocketRequest request)
        {
            this.Connection = connection;
            this.Request = request;
        }

        public WebSocketConnection Connection { get; }

        public WebSocketRequest Request { get; }

        public WebSocketResponse CreateResponse()
        {
            return this.CreateResponse(this.Data);
        }

        public WebSocketResponse CreateResponse(JObject data)
        {
            return new WebSocketResponse(data);
        }

        public Task<WebSocketResponse> CreateResponseTask()
        {
            return Task.FromResult(this.CreateResponse());
        }

        public Task<WebSocketResponse> CreateResponseTask(JObject data)
        {
            return Task.FromResult(this.CreateResponse(data));
        }
    }
}