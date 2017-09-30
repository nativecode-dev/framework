namespace NativeCode.Core.Web.WebSockets.Management
{
    using System;
    using System.Net.WebSockets;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Types;

    public interface IHub
    {
        Guid HubId { get; }

        Task<WebSocketConnection> Add(HttpContext context, WebSocket socket);

        Task Remove(WebSocketConnection connection);
    }
}