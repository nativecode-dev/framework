namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;

    public interface IMessageQueueAdapter : IDisposable
    {
        IMessageQueueProvider Connect(Type messageType, Uri uri, MessageQueueType queueType);

        IMessageQueueProvider<TMessage> Connect<TMessage>(Uri uri, MessageQueueType queueType)
            where TMessage : class, new();
    }
}