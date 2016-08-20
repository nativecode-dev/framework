namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Platform.Serialization;
    using NativeCode.Core.Types;

    using RabbitMQ.Client;

    public class RabbitMessageQueueProvider : DisposableManager, IMessageQueueProvider
    {
        protected internal RabbitMessageQueueProvider(string queue, string exchange, string route, IModel model)
        {
            this.ExchangeName = exchange;
            this.QueueName = queue;
            this.RouteName = route;

            this.Queue = model;
            this.EnsureDisposed(this.Queue);
        }

        public string ExchangeName { get; }

        public string QueueName { get; }

        public string RouteName { get; }

        protected IModel Queue { get; }

        public void Acknowledge(MessageQueueResult result)
        {
            ulong identifier;

            if (ulong.TryParse(result.Identifier, out identifier))
            {
                this.Queue.BasicAck(identifier, false);
            }
        }

        public byte[] Consume()
        {
            var result = this.Queue.BasicGet(this.QueueName, true);
            return result?.Body;
        }

        public MessageQueueResult Next()
        {
            var result = this.Queue.BasicGet(this.QueueName, false);

            if (result == null)
            {
                return default(MessageQueueResult);
            }

            return new MessageQueueResult(result.DeliveryTag.ToString(), result.Body);
        }

        public void Publish(byte[] message)
        {
            this.Queue.BasicPublish(this.ExchangeName, this.RouteName, null, message);
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Generic type.")]
    public class RabbitMessageQueueProvider<TMessage> : RabbitMessageQueueProvider, IMessageQueueProvider<TMessage>
        where TMessage : class, new()
    {
        private readonly ConcurrentDictionary<TMessage, MessageQueueResult> unconsumed = new ConcurrentDictionary<TMessage, MessageQueueResult>();

        public RabbitMessageQueueProvider(string queue, string exchange, string route, IModel model, IStringSerializer serializer)
            : base(queue, exchange, route, model)
        {
            this.Serializer = serializer;
        }

        protected IStringSerializer Serializer { get; }

        public void AcknowledgeMessage(TMessage message)
        {
            if (this.unconsumed.ContainsKey(message))
            {
                var result = this.unconsumed[message];

                try
                {
                    this.Acknowledge(result);
                }
                finally
                {
                    if (this.unconsumed.TryRemove(message, out result) == false)
                    {
                        throw new InvalidOperationException("Failed to remove acknowledgement from cache.");
                    }
                }
            }
        }

        public TMessage ConsumeMessage()
        {
            var bytes = this.Consume();

            if (bytes != null)
            {
                var contents = Encoding.UTF8.GetString(bytes);

                return this.Serializer.Deserialize<TMessage>(contents);
            }

            return default(TMessage);
        }

        public TMessage NextMessage()
        {
            var result = this.Next();

            if (result.Body != null)
            {
                var contents = Encoding.UTF8.GetString(result.Body);
                var message = this.Serializer.Deserialize<TMessage>(contents);

                this.unconsumed.AddOrUpdate(message, key => result, (k, v) => v);

                return message;
            }

            return default(TMessage);
        }

        public void PublishMessage(TMessage message)
        {
            var contents = this.Serializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(contents);

            this.Publish(bytes);
        }
    }
}