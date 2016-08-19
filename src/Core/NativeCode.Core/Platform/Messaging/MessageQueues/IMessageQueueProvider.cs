namespace NativeCode.Core.Platform.Messaging.MessageQueues
{
    using JetBrains.Annotations;

    public interface IMessageQueueProvider
    {
        string QueueName { get; }

        void Acknowledge(QueueMessage message);

        [CanBeNull]
        byte[] Consume();

        QueueMessage Next();

        void Publish([NotNull] byte[] message);
    }
}