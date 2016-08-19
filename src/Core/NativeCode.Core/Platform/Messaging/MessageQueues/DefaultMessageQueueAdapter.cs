namespace NativeCode.Core.Platform.Messaging.MessageQueues
{
    using System;
    using System.Collections.Concurrent;

    public class DefaultMessageQueueAdapter : IMessageQueueAdapter
    {
        private readonly ConcurrentDictionary<string, IMessageQueueProvider> queues = new ConcurrentDictionary<string, IMessageQueueProvider>();

        public IMessageQueueProvider Connect(string name, Uri uri, MessageQueueType type)
        {
            if (this.queues.ContainsKey(name) == false)
            {
                this.queues.AddOrUpdate(name, key => new DefaultMessageQueueProvider(name), (k, v) => v);
            }

            return this.queues[name];
        }

        public IMessageQueueProvider Connect<TMessage>(Uri uri, MessageQueueType type) where TMessage : class, new()
        {
            return this.Connect(typeof(TMessage).Name, uri, type);
        }
    }
}