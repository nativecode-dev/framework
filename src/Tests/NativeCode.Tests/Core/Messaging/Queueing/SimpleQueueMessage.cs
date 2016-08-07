namespace NativeCode.Tests.Core.Messaging.Queueing
{
    using System;

    public class SimpleQueueMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}