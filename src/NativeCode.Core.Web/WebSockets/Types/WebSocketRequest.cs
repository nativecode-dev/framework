namespace NativeCode.Core.Web.WebSockets.Types
{
    using System;
    using System.Runtime.Serialization;
    using MediatR;

    [DataContract]
    public class WebSocketRequest : WebSocketMessage, IRequest<WebSocketResponse>
    {
        [DataMember]
        public Guid HubId { get; set; }
    }
}