namespace NativeCode.Core.Platform.Messaging.Queuing.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class MessageQueueAttribute : Attribute
    {
        public MessageQueueAttribute(string queueName)
        {
            this.QueueName = queueName;
        }

        public string QueueName { get; }
    }
}
