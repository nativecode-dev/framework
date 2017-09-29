namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using System.Collections.Concurrent;
    using Extensions;
    using Types;

    public class MessageQueueAdapter : Disposable, IMessageQueueAdapter
    {
        private readonly ConcurrentDictionary<string, MessageQueueProvider> queues =
            new ConcurrentDictionary<string, MessageQueueProvider>();

        public IMessageQueueProvider Connect(Type messageType, Uri uri, MessageQueueType queueType)
        {
            return this.GetMessageQueueProvider(messageType);
        }

        public IMessageQueueProvider<TMessage> Connect<TMessage>(Uri uri, MessageQueueType queueType)
            where TMessage : class, new()
        {
            return this.GetMessageQueueProvider(typeof(TMessage)) as IMessageQueueProvider<TMessage>;
        }

        private MessageQueueProvider GetMessageQueueProvider(Type messageType)
        {
            var name = messageType.ToKey();

            if (this.queues.ContainsKey(name) == false)
            {
                this.queues.AddOrUpdate(name, key => new MessageQueueProvider(name), (k, v) => v);
            }

            return this.queues[name];
        }
    }
}