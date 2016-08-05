namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Collections.Concurrent;

    using NativeCode.Core.Logging;
    using NativeCode.Core.Messaging.Queuing;
    using NativeCode.Core.Serialization;
    using NativeCode.Core.Types;

    public class RabbitMessageQueueFactory : Disposable, IMessageQueueFactory<RabbitMessageQueueOptions>
    {
        private static readonly ConcurrentDictionary<string, RabbitConnection> Connections = new ConcurrentDictionary<string, RabbitConnection>();

        public RabbitMessageQueueFactory(ILogger logger, IStringSerializer serializer)
        {
            this.Logger = logger;
            this.Serializer = serializer;
        }

        protected ILogger Logger { get; }

        protected IStringSerializer Serializer { get; }

        public IMessageQueue<TMessage> Create<TMessage>(Uri connection) where TMessage : class, new()
        {
            return this.Create<TMessage>(new RabbitMessageQueueOptions(connection));
        }

        public IMessageQueue<TMessage> Create<TMessage>(RabbitMessageQueueOptions options) where TMessage : class, new()
        {
            var connection = this.GetRabbitConnection(options.Uri);

            return new RabbitMessageQueue<TMessage>(this.Serializer, this.Logger, connection.CreateModel(), options);
        }

        protected virtual RabbitConnection GetRabbitConnection(RabbitUri uri)
        {
            if (Connections.ContainsKey(uri) == false)
            {
                return Connections.AddOrUpdate(uri, key => new RabbitConnection(uri), (k, v) => v);
            }

            return Connections[uri];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                foreach (var connection in Connections.Values)
                {
                    connection.Dispose();
                }

                Connections.Clear();
            }

            base.Dispose(disposing);
        }
    }
}