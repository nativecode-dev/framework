namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;
    using JetBrains.Annotations;

    public interface IMessageQueueAdapter : IDisposable
    {
        [NotNull]
        IMessageQueueProvider Connect([NotNull] Type messageType, [NotNull] Uri uri, MessageQueueType queueType);

        [NotNull]

        IMessageQueueProvider<TMessage> Connect<TMessage>([NotNull] Uri uri, MessageQueueType queueType)
            where TMessage : class, new();
    }
}