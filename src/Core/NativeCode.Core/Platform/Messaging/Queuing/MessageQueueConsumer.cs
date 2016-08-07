namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Types;

    public abstract class MessageQueueConsumer<TMessage> : DisposableManager, IMessageQueueConsumer
        where TMessage : class, new()
    {
        protected MessageQueueConsumer(IMessageQueueFactory<TMessage> factory)
        {
            this.Factory = factory;
            this.EnsureDisposed(this.Factory);
        }

        protected IMessageQueueFactory<TMessage> Factory { get; }

        public virtual Task StartAsync(Uri url, CancellationToken cancellationToken)
        {
            return Task.Run(() => this.ConsumeQueueAsync(url, cancellationToken), cancellationToken);
        }

        protected abstract Task ConsumeQueueAsync(Uri url, CancellationToken cancellationToken);
    }
}