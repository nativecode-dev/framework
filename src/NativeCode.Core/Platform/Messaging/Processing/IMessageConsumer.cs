namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Queuing;

    public interface IMessageConsumer<in TMessage> : IDisposable
        where TMessage : class, new()
    {
        Task StartAsync([NotNull] Uri connection, MessageQueueType type, CancellationToken cancellationToken = default(CancellationToken));
    }
}