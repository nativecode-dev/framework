﻿namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Collections.Concurrent;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Platform.Serialization;
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

        public virtual IMessageQueue<TMessage> Create<TMessage>(Uri connection) where TMessage : class, new()
        {
            return this.Create<TMessage>(new RabbitMessageQueueOptions(connection) { QueueName = typeof(TMessage).Name.ToLowerScore('.') });
        }

        public virtual IMessageQueue<TMessage> Create<TMessage>(RabbitMessageQueueOptions options) where TMessage : class, new()
        {
            var connection = this.GetRabbitConnection(options.Uri);

            return Retry.Until(() => new RabbitMessageQueue<TMessage>(this.Logger, this.Serializer, connection, options), 5);
        }

        protected virtual RabbitConnection GetRabbitConnection(RabbitUri uri)
        {
            if (Connections.ContainsKey(uri) == false)
            {
                return Connections.AddOrUpdate(uri, key => new RabbitConnection(uri, this.Logger), (k, v) => v);
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