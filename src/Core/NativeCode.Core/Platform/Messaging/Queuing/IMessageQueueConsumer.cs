namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageQueueConsumer : IDisposable
    {
        Task StartAsync(Uri url, CancellationToken cancellationToken);
    }
}