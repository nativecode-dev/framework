namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageProcessor<in TMessage>
        where TMessage : class, new()
    {
        Task<MessageProcessorResult> ProcessMessageAsync(TMessage message, CancellationToken cancellationToken);
    }
}