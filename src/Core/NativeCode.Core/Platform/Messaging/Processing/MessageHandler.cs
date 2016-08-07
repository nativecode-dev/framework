namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform.Logging;
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

        public Task ProcessMessageAsync(object message)
        {
            return this.ProcessMessageAsync(message, RetryCount);
        }

        public Task ProcessMessageAsync(object message, int retries)
        {
            return this.ProcessMessageAsync(message, RetryCount, true);
        }

        public Task ProcessMessageAsync(object message, int retries, bool requeue)
        {
            return Task.Factory.StartNew(() => this.ExecuteAsync(message, retries, requeue));
        }

        protected abstract Task HandleMessageAsync(object message);

        protected abstract Task RequeueMessageAsync(object message);

        private async Task ExecuteAsync(object message, int retries, bool requeue)
        {
            try
            {
                await Retry.Until(() => this.HandleMessageAsync(message), retries);
            }
            catch (Exception ex)
            {
                if (requeue)
                {
                    await this.RequeueMessageAsync(message);
                }

                this.Logger.Exception(ex);
                throw;
            }
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Generic type.")]
    public abstract class MessageHandler<TMessage> : MessageHandler
    {
        protected MessageHandler(ILogger logger)
            : base(logger)
        {
        }

        public override bool CanProcessMessage(object message)
        {
            return message.GetType() == typeof(TMessage);
        }

        protected override Task HandleMessageAsync(object message)
        {
            return this.HandleMessageAsync((TMessage)message);
        }

        protected override Task RequeueMessageAsync(object message)
        {
            return this.RequeueMessageAsync((TMessage)message);
        }

        protected abstract Task HandleMessageAsync(TMessage message);

        protected abstract Task RequeueMessageAsync(TMessage message);
    }
}