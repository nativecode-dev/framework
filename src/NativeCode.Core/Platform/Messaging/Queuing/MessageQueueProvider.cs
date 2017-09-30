namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System.Collections.Concurrent;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Reliability;
    using Serialization;

    public class MessageQueueProvider : DisposableManager, IMessageQueueProvider
    {
        private readonly ConcurrentQueue<byte[]> queue = new ConcurrentQueue<byte[]>();

        public MessageQueueProvider(string name)
        {
            this.QueueName = name;
        }

        public string QueueName { get; }

        public void Acknowledge(MessageQueueResult result)
        {
        }

        public byte[] Consume()
        {
            byte[] message;

            return this.queue.TryDequeue(out message) ? message : null;
        }

        public MessageQueueResult Next()
        {
            return new MessageQueueResult(this.Consume());
        }

        public void Publish(byte[] message)
        {
            this.queue.Enqueue(message);
        }

        public void Reject(MessageQueueResult result, bool requeue)
        {
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Generic type.")]
    public class MessageQueueProvider<TMessage> : MessageQueueProvider, IMessageQueueProvider<TMessage>
        where TMessage : class, new()
    {
        private readonly ConcurrentDictionary<TMessage, MessageQueueResult> mappings =
            new ConcurrentDictionary<TMessage, MessageQueueResult>();

        public MessageQueueProvider(string name, IStringSerializer serializer)
            : base(name)
        {
            this.Serializer = serializer;
        }

        protected IStringSerializer Serializer { get; }

        public void AcknowledgeMessage(TMessage message)
        {
            if (this.mappings.ContainsKey(message))
            {
                MessageQueueResult result;
                this.mappings.TryRemove(message, out result);
            }
        }

        public TMessage ConsumeMessage()
        {
            var bytes = this.Consume();

            if (bytes != null)
            {
                var contents = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                return this.Serializer.Deserialize<TMessage>(contents);
            }

            return default(TMessage);
        }

        public TMessage NextMessage()
        {
            var message = this.Next();

            if (message.Body != null)
            {
                var contents = Encoding.UTF8.GetString(message.Body, 0, message.Body.Length);
                return this.Serializer.Deserialize<TMessage>(contents);
            }

            return default(TMessage);
        }

        public void PublishMessage(TMessage message)
        {
            var contents = this.Serializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(contents);

            this.Publish(bytes);
        }

        public void RejectMessage(TMessage message, bool requeue)
        {
        }
    }
}