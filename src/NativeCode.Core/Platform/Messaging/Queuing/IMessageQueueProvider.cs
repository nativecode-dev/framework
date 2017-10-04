namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using JetBrains.Annotations;

    public interface IMessageQueueProvider : IDisposable
    {
        [NotNull]
        string QueueName { get; }

        void Acknowledge(MessageQueueResult result);

        [CanBeNull]
        byte[] Consume();

        MessageQueueResult Next();

        void Publish([NotNull] byte[] message);

        void Reject(MessageQueueResult result, bool requeue);
    }

    public interface IMessageQueueProvider<TMessage> : IMessageQueueProvider
        where TMessage : class, new()
    {
        void AcknowledgeMessage([NotNull] TMessage message);

        [CanBeNull]
        TMessage ConsumeMessage();

        [CanBeNull]
        TMessage NextMessage();

        void PublishMessage([NotNull] TMessage message);

        void RejectMessage([NotNull] TMessage message, bool requeue);
    }
}