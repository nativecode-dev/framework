namespace NativeCode.Core.Web.WebSockets.Management
{
    using Microsoft.AspNetCore.Http;
    using Reliability;
    using System;
    using System.Collections.Concurrent;
    using System.Net.WebSockets;
    using System.Threading.Tasks;
    using Types;

    internal class Hub : Disposable, IHub
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

        protected override void ReleaseManaged()
        {
            foreach (var connection in this.connections.Values)
            {
                connection.Dispose();
            }
            
            this.connections.Clear();
        }
    }
}