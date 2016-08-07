﻿namespace NativeCode.Core.Platform.Messaging.Queuing
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

        public PollingQueueConsumer(ILogger logger, IMessageQueue<TMessage> queue, IStringSerializer serializer, IEnumerable<IMessageHandler> handlers)
            : base(queue)
        {
            this.Handlers = handlers;
            this.Logger = logger;
            this.Serializer = serializer;
        }

        public int Concurrency
        {
            get
            {
                return this.counter;
            }

            set
            {
                if (Interlocked.CompareExchange(ref this.counter, value, this.counter) != this.counter)
                {
                    this.Logger.Warning($"Could not set Concurrency, currently set at {this.counter}.");
                }
            }
        }

        protected IEnumerable<IMessageHandler> Handlers { get; }

        protected ILogger Logger { get; }

        protected IStringSerializer Serializer { get; }

        protected override async Task ConsumeQueueAsync(Uri url, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            while (cancellationToken.IsCancellationRequested == false)
            {
                try
                {
                    if (tasks.Count >= this.counter)
                    {
                        await this.RemoveCompletedAsync(tasks, cancellationToken);
                        continue;
                    }

                    await this.ConsumeNextMessageAsync(this.Queue, tasks, cancellationToken);
                }
                catch (Exception ex)
                {
                    this.Logger.Exception(ex);
                }
            }
        }

        private Task RemoveCompletedAsync(ICollection<Task> tasks, CancellationToken cancellationToken)
        {
            foreach (var task in tasks.Where(t => t.IsDone()).ToList())
            {
                this.Logger.Debug($"Removing task {task.Id}.");
                tasks.Remove(task);
            }

            return Task.Delay(TimeSpan.FromMilliseconds(250), cancellationToken);
        }

        private async Task ConsumeNextMessageAsync(IMessageQueue<TMessage> queue, ICollection<Task> tasks, CancellationToken cancellationToken)
        {
            var message = queue.Dequeue();

            if (message != null)
            {
                this.Logger.Debug(this.Serializer.Serialize(message));

                foreach (var handler in this.Handlers.Where(h => h.CanProcessMessage(message)))
                {
                    this.Logger.Debug($"Using handler {handler.GetType().Name}.");

                    try
                    {
                        tasks.Add(handler.ProcessMessageAsync(message, cancellationToken));
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
