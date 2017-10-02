namespace NativeCode.Core.Web.WebSockets.Types
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class WebSocketRequest : WebSocketMessage
    {
        [DataMember]
        public Guid HubId { get; set; }
    }
}