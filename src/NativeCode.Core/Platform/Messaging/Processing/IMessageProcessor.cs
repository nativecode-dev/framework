namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    public interface IMessageProcessor<in TMessage>
        where TMessage : class, new()
    {
        Task<MessageProcessorResult> ProcessMessageAsync([NotNull] TMessage message,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}