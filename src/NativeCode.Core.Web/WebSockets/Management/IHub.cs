namespace NativeCode.Core.Web.WebSockets.Management
{
    using System;
    using System.Collections.Generic;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Types;

    public interface IHub : IDisposable
    {
        Guid HubId { get; }

        Task<WebSocketConnection> Add(HttpContext context, WebSocket socket);

        IEnumerable<Task> Broadcast<T>(T data, CancellationToken token);

        IEnumerable<Task> BroadcastText(string data, CancellationToken token);

        Task Remove(WebSocketConnection connection);
    }
}