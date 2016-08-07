namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class MessageQueueConsumer<TMessage> : IMessageQueueConsumer
        where TMessage : class, new()
    {
        protected MessageQueueConsumer(IMessageQueue<TMessage> queue)
        {
            this.Queue = queue;
        }

        protected IMessageQueue<TMessage> Queue { get; }

        public virtual Task StartAsync(Uri url, CancellationToken cancellationToken)
        {
            return Task.Run(() => this.ConsumeQueueAsync(url, cancellationToken), cancellationToken);
        }

        protected abstract Task ConsumeQueueAsync(Uri url, CancellationToken cancellationToken);
    }
}