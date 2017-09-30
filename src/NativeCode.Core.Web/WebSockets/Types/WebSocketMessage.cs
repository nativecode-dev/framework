namespace NativeCode.Core.Web.WebSockets.Types
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;

    [DataContract]
    public abstract class WebSocketMessage
    {
        [DataMember]
        public JObject Data { get; set; }

        [DataMember]
        public IDictionary<string, string> Properties { get; protected set; } = new Dictionary<string, string>();
    }
}