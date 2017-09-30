namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public abstract class MessageProcessor<TMessage> : IMessageProcessor<TMessage>
        where TMessage : class, new()
    {
        protected MessageProcessor(ILoggerFactory factory)
        {
            this.Logger = factory.CreateLogger<MessageProcessor<TMessage>>();
        }

        protected ILogger Logger { get; }

        public abstract Task<MessageProcessorResult> ProcessMessageAsync(TMessage message,
            CancellationToken cancellationToken);
    }
}