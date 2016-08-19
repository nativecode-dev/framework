namespace NativeCode.Core.Packages.Rabbit
{
    using System;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Messaging.MessageQueues;
    using NativeCode.Core.Types;

    using RabbitMQ.Client;

    public class RabbitMessageQueueProvider : DisposableManager, IMessageQueueProvider
    {
        protected internal RabbitMessageQueueProvider(RabbitConnection connection, MessageQueueType type)
        {
            this.Connection = connection;
            this.Type = type;

            // Create the queue after the properties are assigned.
            this.Queue = this.CreateQueue(connection, type);

            this.EnsureDisposed(this.Connection);
            this.EnsureDisposed(this.Queue);
        }

        public string QueueName => $"{this.Connection.Uri.VirtualHost}-{Environment.MachineName}-{this.Type}".ToLowerScore('.');

        protected RabbitConnection Connection { get; }

        protected IModel Queue { get; }

        protected MessageQueueType Type { get; }

        public void Acknowledge(QueueMessage message)
        {
            ulong identifier;

            if (ulong.TryParse(message.Identifier, out identifier))
            {
                this.Queue.BasicAck(identifier, false);
            }
        }

        public byte[] Consume()
        {
            var result = this.Queue.BasicGet(this.QueueName, true);
            return result?.Body;
        }

        public QueueMessage Next()
        {
            var result = this.Queue.BasicGet(this.QueueName, false);

            return new QueueMessage(result.DeliveryTag.ToString(), result.Body);
        }

        public void Publish(byte[] message)
        {
            this.Queue.BasicPublish(string.Empty, string.Empty, null, message);
        }

        private IModel CreateQueue(RabbitConnection connection, MessageQueueType type)
        {
            switch (type)
            {
                case MessageQueueType.Publisher:
                case MessageQueueType.Subscriber:
                    return connection.CreatePubSubQueue(this.QueueName, string.Empty);

                default:
                    return connection.CreateQueue(this.QueueName, string.Empty);
            }
        }
    }
}