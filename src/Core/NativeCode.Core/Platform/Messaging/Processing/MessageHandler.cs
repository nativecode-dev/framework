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
            return Task.Factory.StartNew(() => this.ExecuteProcessor(message, retries, requeue));
        }

        protected abstract void ProcessMessage(object message);

        protected abstract void RequeueMessage(object message);

        private void ExecuteProcessor(object message, int retries, bool requeue)
        {
            try
            {
                Retry.Until(() => this.ProcessMessage(message), retries);
            }
            catch (Exception ex)
            {
                if (requeue)
                {
                    this.RequeueMessage(message);
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

        protected override void ProcessMessage(object message)
        {
            this.ProcessMessage((TMessage)message);
        }

        protected override void RequeueMessage(object message)
        {
            this.RequeueMessage((TMessage)message);
        }

        protected abstract void ProcessMessage(TMessage message);

        protected abstract void RequeueMessage(TMessage message);
    }
}