namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System.Reflection;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Queuing.Attributes;
    using NativeCode.Core.Platform.Serialization;
    using NativeCode.Core.Types;

    public abstract class MessageQueue<TMessage> : Disposable, IMessageQueue<TMessage>
        where TMessage : class, new()
    {
        protected MessageQueue(ILogger logger, IStringSerializer serializer)
        {
            this.Logger = logger;
            this.Serializer = serializer;
        }

        public string QueueName { get; protected set; } = GetQueueName();

        protected ILogger Logger { get; }

        protected IStringSerializer Serializer { get; }

        /// <summary>
        /// Gets the next set of bytes.
        /// </summary>
        /// <returns>Returns a byte array.</returns>
        public abstract byte[] DequeueBytes();

        /// <summary>
        /// Enqueues the bytes.
        /// </summary>
        /// <param name="data">The data.</param>
        public abstract void EnqueueBytes(byte[] data);

        /// <summary>
        /// Retrieves the next message.
        /// </summary>
        /// <returns>Returns a message or <c>null</c>.</returns>
        public abstract TMessage DequeueMessage();

        /// <summary>
        /// Enqueues the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public abstract void EnqueueMessage(TMessage message);

        private static string GetQueueName()
        {
            var type = typeof(TMessage);
            var attribute = type.GetTypeInfo().GetCustomAttribute<MessageQueueAttribute>();

            if (attribute != null && string.IsNullOrWhiteSpace(attribute.QueueName) == false)
            {
                return attribute.QueueName;
            }

            return type.Name.ToLowerScore('.');
        }
    }
}
