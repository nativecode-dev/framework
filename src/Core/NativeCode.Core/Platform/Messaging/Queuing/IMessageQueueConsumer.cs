namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageQueueConsumer
    {
        TimeSpan ThrottleCleanup { get; }

        TimeSpan ThrottleConsume { get; }

        TimeSpan ThrottleEmptyQueue { get; }

        Task StartAsync(Uri url, CancellationToken cancellationToken);
    }
}