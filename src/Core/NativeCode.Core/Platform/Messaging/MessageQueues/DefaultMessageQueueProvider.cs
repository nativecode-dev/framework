namespace NativeCode.Core.Platform.Messaging.MessageQueues
{
    using System.Collections.Concurrent;

    public class DefaultMessageQueueProvider : IMessageQueueProvider
    {
        private readonly ConcurrentQueue<byte[]> queue = new ConcurrentQueue<byte[]>();

        public DefaultMessageQueueProvider(string name)
        {
            this.QueueName = name;
        }

        public string QueueName { get; }

        public void Acknowledge(QueueMessage message)
        {
        }

        public byte[] Consume()
        {
            byte[] message;

            return this.queue.TryDequeue(out message) ? message : null;
        }

        public QueueMessage Next()
        {
            return new QueueMessage(this.Consume());
        }

        public void Publish(byte[] message)
        {
            this.queue.Enqueue(message);
        }
    }
}