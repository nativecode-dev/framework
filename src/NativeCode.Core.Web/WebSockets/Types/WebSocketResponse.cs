namespace NativeCode.Core.Web.WebSockets.Types
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;

    [DataContract]
    public class WebSocketResponse : WebSocketMessage
    {
        public WebSocketResponse()
        {
        }

        public WebSocketResponse(JObject data)
        {
            this.Data = data;
        }
    }
}