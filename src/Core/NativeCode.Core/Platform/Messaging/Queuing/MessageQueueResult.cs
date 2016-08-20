namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;

    public struct MessageQueueResult
    {
        public MessageQueueResult(byte[] body)
            : this(Guid.NewGuid().ToString(), body)
        {
        }

        public MessageQueueResult(string identifier, byte[] body)
        {
            this.Body = body;
            this.Identifier = identifier;
        }

        public byte[] Body { get; }

        public string Identifier { get; }
    }
}