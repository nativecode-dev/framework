namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Processing;
    using NativeCode.Core.Platform.Serialization;

    public abstract class MessageQueueConsumer<TMessage> : IMessageQueueConsumer
        where TMessage : class, new()
    {
        private int concurrency = 6;

        protected MessageQueueConsumer(IEnumerable<IMessageHandler> handlers, ILogger logger, IMessageQueue<TMessage> queue, IStringSerializer serializer)
        {
            this.Handlers = handlers;
            this.Logger = logger;
            this.Queue = queue;
            this.Serializer = serializer;
        }

        public int Concurrency
        {
            get
            {
                return this.concurrency;
            }

            set
            {
                if (Interlocked.CompareExchange(ref this.concurrency, value, this.concurrency) != this.concurrency)
                {
                    this.Logger.Warning($"Could not set Concurrency, currently set at {this.concurrency}.");
                }
            }
        }

        public virtual TimeSpan ThrottleCleanup { get; } = TimeSpan.FromMilliseconds(250);

        public virtual TimeSpan ThrottleConsume { get; } = TimeSpan.FromMilliseconds(50);

        public TimeSpan ThrottleEmptyQueue { get; } = TimeSpan.FromMilliseconds(1000);

        protected IEnumerable<IMessageHandler> Handlers { get; }

        protected ILogger Logger { get; }

        protected IMessageQueue<TMessage> Queue { get; }

        protected IStringSerializer Serializer { get; }

        public virtual Task StartAsync(Uri url, CancellationToken cancellationToken)
        {
            this.Logger.Debug($"Starting message consumer for {url}.");

            return Task.Run(() => this.ConsumeQueueAsync(url, cancellationToken), cancellationToken);
        }

        protected abstract Task ConsumeQueueAsync(Uri url, CancellationToken cancellationToken);

        protected virtual Task ConsumeNextMessageAsync(ICollection<Task> tasks, CancellationToken cancellationToken)
        {
            var message = this.Queue.Dequeue();

            if (message == default(TMessage))
            {
                return Task.Delay(this.ThrottleEmptyQueue, cancellationToken);
            }

            this.Logger.Debug(this.Serializer.Serialize(message));

            foreach (var handler in this.Handlers.Where(h => h.CanProcessMessage(message)))
            {
                this.Logger.Debug($"Calling handler {handler.GetType().Name}.");

                try
                {
                    tasks.Add(handler.ProcessMessageAsync(message, cancellationToken));
                }
                catch (Exception ex)
                {
                    this.Logger.Exception(ex);
                }
            }

            return Task.Delay(this.ThrottleConsume, cancellationToken);
        }

        protected virtual Task RemoveCompletedAsync(ICollection<Task> tasks, CancellationToken cancellationToken)
        {
            foreach (var task in tasks.Where(t => t.IsDone()).ToList())
            {
                this.Logger.Debug($"Removing task {task.Id}.");
                tasks.Remove(task);
            }

            return Task.Delay(this.ThrottleCleanup, cancellationToken);
        }
    }
}