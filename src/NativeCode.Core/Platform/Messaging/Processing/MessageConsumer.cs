namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;
    using Logging;
    using Queuing;
    using Serialization;
    using Types;

    public class MessageConsumer<TMessage> : DisposableManager, IMessageConsumer<TMessage>
        where TMessage : class, new()
    {
        public MessageConsumer(IMessageProcessor<TMessage> processor, IMessageQueueAdapter adapter,
            IStringSerializer serializer, ILogger logger)
        {
            this.Adapter = adapter;
            this.Logger = logger;
            this.Processor = processor;
            this.Serializer = serializer;
        }

        protected IMessageQueueAdapter Adapter { get; }

        protected int Concurrency { get; } = 5;

        protected ILogger Logger { get; }

        protected IMessageProcessor<TMessage> Processor { get; }

        protected IStringSerializer Serializer { get; }

        protected TimeSpan ThrottleEmptyQueue { get; set; } = TimeSpan.FromMilliseconds(500);

        protected TimeSpan ThrottleMaxConcurrency { get; set; } = TimeSpan.FromMilliseconds(250);

        public async Task StartAsync(Uri connection, MessageQueueType type, CancellationToken cancellationToken)
        {
            using (var provider = this.Adapter.Connect<TMessage>(connection, type))
            {
                var tracker = new TaskTracker(provider, cancellationToken);

                while (cancellationToken.IsCancellationRequested == false)
                    try
                    {
                        tracker.RemoveCompleted(this.HandleTaskRemoved);

                        // Important that we continue the loop if we are at max concurrency. We'll
                        // pause for a bit to allow processing to potentially complete.
                        if (tracker.Tasks.Count >= this.Concurrency)
                        {
                            await Task.Delay(this.ThrottleMaxConcurrency, cancellationToken);
                            continue;
                        }

                        var result = provider.Next();

                        // If we don't have a response from the queue, then we'll just pause for a bit
                        // to allow new messages to arrive.
                        if (result.Body == null)
                        {
                            await Task.Delay(this.ThrottleEmptyQueue, cancellationToken);
                            continue;
                        }

                        // Process the message we got from the queue.
                        tracker.Tasks.Add(result,
                            Task.Run(() => this.ProcessMessageAsync(result, cancellationToken), cancellationToken));
                    }
                    catch (Exception ex)
                    {
                        this.Logger.Exception(ex);
                    }
            }
        }

        protected virtual void HandleTaskRemoved(MessageQueueResult result, Task<MessageProcessorResult> task,
            TaskTracker tracker)
        {
            if (task.IsCompleted)
                tracker.Provider.Acknowledge(result);

            if (task.IsFaulted)
                this.Logger.Exception(task.Exception);
        }

        private async Task<MessageProcessorResult> ProcessMessageAsync(MessageQueueResult result,
            CancellationToken cancellationToken)
        {
            try
            {
                var contents = Encoding.UTF8.GetString(result.Body, 0, result.Body.Length);
                var message = this.Serializer.Deserialize<TMessage>(contents);

                return await this.Processor.ProcessMessageAsync(message, cancellationToken);
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
                return MessageProcessorResult.Exception;
            }
        }

        protected class TaskTracker
        {
            protected internal TaskTracker(IMessageQueueProvider provider, CancellationToken cancellationToken)
            {
                this.CancellationToken = cancellationToken;
                this.Provider = provider;
            }

            public CancellationToken CancellationToken { get; }

            public IMessageQueueProvider Provider { get; }

            public Dictionary<MessageQueueResult, Task<MessageProcessorResult>> Tasks { get; } =
                new Dictionary<MessageQueueResult, Task<MessageProcessorResult>>();

            public void RemoveCompleted(Action<MessageQueueResult, Task<MessageProcessorResult>, TaskTracker> callback)
            {
                foreach (var kvp in this.Tasks.Where(x => x.Value.IsDone()).ToList())
                {
                    this.Tasks.Remove(kvp.Key);
                    callback(kvp.Key, kvp.Value, this);
                }
            }
        }
    }
}