namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Queuing;

    public interface IMessageConsumer<in TMessage> : IDisposable
        where TMessage : class, new()
    {
        Task StartAsync(Uri connection, MessageQueueType type, CancellationToken cancellationToken);
    }
}