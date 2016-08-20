namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Collections.Concurrent;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Platform.Serialization;

    using RabbitMQ.Client;

    public class RabbitMessageQueueAdapter : IMessageQueueAdapter
    {
        private readonly ConcurrentDictionary<RabbitUri, RabbitConnection> connections = new ConcurrentDictionary<RabbitUri, RabbitConnection>();

        public RabbitMessageQueueAdapter(ILogger logger, IStringSerializer serializer)
        {
            this.Logger = logger;
            this.Serializer = serializer;
        }

        protected ILogger Logger { get; }

        protected IStringSerializer Serializer { get; }

        public IMessageQueueProvider Connect(Type messageType, Uri uri, MessageQueueType queueType)
        {
            var connection = this.GetConnection(uri);
            var exchange = messageType.Name.ToLowerScore('.');
            var queue = GetQueueName(messageType, queueType);
            var model = GetQueue(connection, queue, exchange, queueType);

            return new RabbitMessageQueueProvider(queue, exchange, string.Empty, model);
        }

        public IMessageQueueProvider<TMessage> Connect<TMessage>(Uri uri, MessageQueueType queueType) where TMessage : class, new()
        {
            var type = typeof(TMessage);
            var connection = this.GetConnection(uri);
            var exchange = type.Name.ToLowerScore('.');
            var queue = GetQueueName(type, queueType);
            var model = GetQueue(connection, queue, exchange, queueType);

            return new RabbitMessageQueueProvider<TMessage>(queue, exchange, string.Empty, model, this.Serializer);
        }

        private RabbitConnection GetConnection(RabbitUri uri)
        {
            if (this.connections.ContainsKey(uri))
            {
                return this.connections[uri];
            }

            var connection = new RabbitConnection(uri, this.Logger);

            if (this.connections.TryAdd(uri, connection) == false)
            {
                connection.Dispose();
                throw new InvalidOperationException("Failed to add new connection.");
            }

            return connection;
        }

        private static IModel GetQueue(RabbitConnection connection, string queueName, string exchangeName, MessageQueueType queueType)
        {
            switch (queueType)
            {
                case MessageQueueType.Publisher:
                case MessageQueueType.Subscriber:
                    return connection.CreatePubSubQueue(queueName, exchangeName);

                default:
                    return connection.CreateQueue(queueName, exchangeName);
            }
        }

        private static string GetQueueName(Type type, MessageQueueType queueType)
        {
            var queueName = type.Name.ToLowerScore('.');

            switch (queueType)
            {
                case MessageQueueType.Publisher:
                case MessageQueueType.Subscriber:
                    return $"{queueName}@{Environment.MachineName.ToLower()}:publications";

                default:
                    return $"{queueName}@{Environment.MachineName.ToLower()}:inbox";
            }
        }
    }
}
