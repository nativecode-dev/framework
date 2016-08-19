namespace NativeCode.Core.Platform.Messaging.MessageQueues
{
    using System;

    public struct QueueMessage
    {
        public QueueMessage(byte[] body)
            : this(Guid.NewGuid().ToString(), body)
        {
        }

        public QueueMessage(string identifier, byte[] body)
        {
            this.Body = body;
            this.Identifier = identifier;
        }

        public byte[] Body { get; }

        public string Identifier { get; }
    }
}