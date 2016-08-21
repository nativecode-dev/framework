namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;

    using JetBrains.Annotations;

    public interface IMessageQueueProvider : IDisposable
    {
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
        void AcknowledgeMessage(TMessage message);

        TMessage ConsumeMessage();

        TMessage NextMessage();

        void PublishMessage(TMessage message);

        void RejectMessage(TMessage message, bool requeue);
    }
}