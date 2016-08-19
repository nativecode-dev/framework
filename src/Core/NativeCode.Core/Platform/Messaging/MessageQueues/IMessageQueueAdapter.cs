namespace NativeCode.Core.Platform.Messaging.MessageQueues
{
    using System;

    public interface IMessageQueueAdapter
    {
        IMessageQueueProvider Connect(string name, Uri uri, MessageQueueType type);

        IMessageQueueProvider Connect<TMessage>(Uri uri, MessageQueueType type) where TMessage : class, new();
    }
}