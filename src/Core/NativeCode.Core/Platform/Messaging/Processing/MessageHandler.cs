namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Types;

    public abstract class MessageHandler : IMessageHandler
    {
        private const int RetryCount = 5;

        internal MessageHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        protected ILogger Logger { get; }

        public abstract bool CanProcessMessage(object message);

        public Task ProcessMessageAsync(object message, CancellationToken cancellationToken)
        {
            return this.ProcessMessageAsync(message, RetryCount, cancellationToken);
        }

        public Task ProcessMessageAsync(object message, int retries, CancellationToken cancellationToken)
        {
            return this.ProcessMessageAsync(message, RetryCount, true, cancellationToken);
        }

        public Task ProcessMessageAsync(object message, int retries, bool requeue, CancellationToken cancellationToken)
        {
            return Task.Run(() => this.ExecuteAsync(message, retries, requeue, cancellationToken), cancellationToken);
        }

        protected abstract Task HandleMessageAsync(object message, CancellationToken cancellationToken);

        protected abstract Task RequeueMessageAsync(object message, CancellationToken cancellationToken);

        private async Task ExecuteAsync(object message, int retries, bool requeue, CancellationToken cancellationToken)
        {
            try
            {
                this.Logger.Debug($"Running handler {this.GetType().Name} {retries} time(s).");
                await Retry.Until(() => this.HandleMessageAsync(message, cancellationToken), retries);
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);

                if (requeue)
                {
                    this.Logger.Debug($"Re-queuing message for handler {this.GetType().Name}.");
                    await this.RequeueMessageAsync(message, cancellationToken);
                }
            }
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Generic type.")]
    public abstract class MessageHandler<TMessage> : MessageHandler
        where TMessage : class, new()
    {
        protected MessageHandler(IMessageQueue<TMessage> queue, ILogger logger)
            : base(logger)
        {
            this.Queue = queue;
        }

        protected IMessageQueue<TMessage> Queue { get; }

        public override bool CanProcessMessage(object message)
        {
            var type = typeof(TMessage);
            this.Logger.Debug($"Message Type: {message.GetType().Name}, Expected Type: {type.Name}.");
            return message.GetType() == type;
        }

        protected override Task HandleMessageAsync(object message, CancellationToken cancellationToken)
        {
            return this.HandleMessageAsync((TMessage)message, cancellationToken);
        }

        protected override Task RequeueMessageAsync(object message, CancellationToken cancellationToken)
        {
            return this.RequeueMessageAsync((TMessage)message, cancellationToken);
        }

        protected abstract Task HandleMessageAsync(TMessage message, CancellationToken cancellationToken);

        protected abstract Task RequeueMessageAsync(TMessage message, CancellationToken cancellationToken);
    }
}