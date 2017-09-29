namespace NativeCode.Core.Platform.Messaging.Queuing
{
    public enum MessageQueueType
    {
        Default = 0,

        Publisher = 1,

        Subscriber = 2,

        Transient = 3,

        WorkQueue = MessageQueueType.Default
    }
}