namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform.Logging;

    public abstract class MessageProcessor<TMessage> : IMessageProcessor<TMessage>
        where TMessage : class, new()
    {
        protected MessageProcessor(ILogger logger)
        {
            this.Logger = logger;
        }

        protected ILogger Logger { get; }

        public abstract Task<MessageProcessorResult> ProcessMessageAsync(TMessage message, CancellationToken cancellationToken);
    }
}