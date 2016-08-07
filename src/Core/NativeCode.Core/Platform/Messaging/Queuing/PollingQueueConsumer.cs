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

    public class PollingQueueConsumer<TMessage> : MessageQueueConsumer<TMessage>
        where TMessage : class, new()
    {
        private int counter;

        public PollingQueueConsumer(ILogger logger, IMessageQueueFactory<TMessage> factory, IStringSerializer serializer, IEnumerable<IMessageHandler> handlers)
            : base(factory)
        {
            this.Handlers = handlers;
            this.Logger = logger;
            this.Serializer = serializer;
        }

        public int MaxMessasages
        {
            get
            {
                return this.counter;
            }

            set
            {
                if (Interlocked.CompareExchange(ref this.counter, value, this.counter) != this.counter)
                {
                    this.Logger.Warning($"Could not set MaxMessages, currently set at {this.counter}.");
                }
            }
        }

        protected IEnumerable<IMessageHandler> Handlers { get; }

        protected ILogger Logger { get; }

        protected IStringSerializer Serializer { get; }

        protected override async Task ConsumeQueueAsync(Uri url, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            using (var queue = this.Factory.Create<TMessage>(url))
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    try
                    {
                        if (tasks.Count >= this.counter)
                        {
                            await RemoveCompletedAsync(tasks, cancellationToken);
                            continue;
                        }

                        await this.ConsumeNextMessageAsync(cancellationToken, queue, tasks);
                    }
                    catch (Exception ex)
                    {
                        this.Logger.Exception(ex);
                    }
                }
            }
        }

        private static Task RemoveCompletedAsync(ICollection<Task> tasks, CancellationToken cancellationToken)
        {
            foreach (var task in tasks.Where(t => t.IsDone()).ToList())
            {
                tasks.Remove(task);
            }

            return Task.Delay(TimeSpan.FromMilliseconds(250), cancellationToken);
        }

        private async Task ConsumeNextMessageAsync(CancellationToken cancellationToken, IMessageQueue<TMessage> queue, ICollection<Task> tasks)
        {
            var message = queue.Dequeue();

            if (message != null)
            {
                foreach (var handler in this.Handlers.Where(h => h.CanProcessMessage(message)))
                {
                    try
                    {
                        tasks.Add(handler.ProcessMessageAsync(message));
                    }
                    catch (Exception ex)
                    {
                        this.Logger.Exception(ex);
                    }
                }
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
        }
    }
}
