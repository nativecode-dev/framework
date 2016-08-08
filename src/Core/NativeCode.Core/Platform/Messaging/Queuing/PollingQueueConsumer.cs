namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Processing;
    using NativeCode.Core.Platform.Serialization;

    public class PollingQueueConsumer<TMessage> : MessageQueueConsumer<TMessage>
        where TMessage : class, new()
    {
        public PollingQueueConsumer(IEnumerable<IMessageHandler> handlers, ILogger logger, IMessageQueue<TMessage> queue, IStringSerializer serializer)
            : base(handlers, logger, queue, serializer)
        {
        }

        protected override async Task ConsumeQueueAsync(Uri url, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            this.Logger.Debug($"Consuming messages for {typeof(TMessage).Name}.");

            while (cancellationToken.IsCancellationRequested == false)
            {
                try
                {
                    if (tasks.Count == this.Concurrency)
                    {
                        await this.RemoveCompletedAsync(tasks, cancellationToken);
                        continue;
                    }

                    await this.ConsumeNextMessageAsync(tasks, cancellationToken);
                }
                catch (Exception ex)
                {
                    this.Logger.Exception(ex);
                }
            }
        }
    }
}
