namespace NativeCode.Core.Web.WebSockets.Management
{
    using System;
    using System.Collections.Concurrent;
    using System.Net.WebSockets;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Types;

    internal class Hub : IHub
    {
        private readonly ConcurrentDictionary<Guid, WebSocketConnection> connections;

        public Hub(Guid id)
        {
            this.connections = new ConcurrentDictionary<Guid, WebSocketConnection>();
            this.HubId = id;
        }

        public Guid HubId { get; }

        public Task<WebSocketConnection> Add(HttpContext context, WebSocket socket)
        {
            var connection = new WebSocketConnection(context, socket, this);

            if (this.connections.TryAdd(connection.HubId, connection))
            {
                return Task.FromResult(connection);
            }

            throw new InvalidOperationException("Failed to add connection.");
        }

        public Task Remove(WebSocketConnection connection)
        {
            if (this.connections.TryRemove(connection.HubId, out var disposable))
            {
                disposable.Dispose();
            }

            return Task.CompletedTask;
        }
    }
}