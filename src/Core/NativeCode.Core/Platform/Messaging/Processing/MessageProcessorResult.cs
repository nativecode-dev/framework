namespace NativeCode.Core.Platform.Messaging.Processing
{
    public enum MessageProcessorResult
    {
        None = 0,

        Completed = 1,

        Exception = 2,

        Failed = 3,

        Requeue = 4
    }
}