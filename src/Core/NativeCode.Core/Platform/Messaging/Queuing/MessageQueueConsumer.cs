namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Processing;

    public abstract class MessageQueueConsumer<TMessage> : IMessageQueueConsumer
        where TMessage : class, new()
    {
        protected MessageQueueConsumer(ILogger logger, IEnumerable<IMessageHandler> handlers, IMessageQueue<TMessage> queue)
        {
            this.Handlers = handlers;
            this.Logger = logger;
            this.Queue = queue;
        }

        protected IEnumerable<IMessageHandler> Handlers { get; }

        protected ILogger Logger { get; }

        protected IMessageQueue<TMessage> Queue { get; }

        public virtual TimeSpan ThrottleCleanup { get; } = TimeSpan.FromMilliseconds(250);

        public virtual TimeSpan ThrottleConsume { get; } = TimeSpan.FromMilliseconds(50);

        public TimeSpan ThrottleEmptyQueue { get; } = TimeSpan.FromMilliseconds(1000);

        public virtual Task StartAsync(Uri url, CancellationToken cancellationToken)
        {
            this.Logger.Debug($"Starting message consumer for {url}.");

            return Task.Run(() => this.ConsumeQueueAsync(url, cancellationToken), cancellationToken);
        }

        protected abstract Task ConsumeQueueAsync(Uri url, CancellationToken cancellationToken);
    }
}