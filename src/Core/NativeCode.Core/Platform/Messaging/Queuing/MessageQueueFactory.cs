namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;

    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Serialization;
    using NativeCode.Core.Types;

    public abstract class MessageQueueFactory<TConfiguration> : Disposable, IMessageQueueFactory<TConfiguration>
        where TConfiguration : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageQueueFactory{TConfiguration}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="serializer">The serializer.</param>
        protected MessageQueueFactory(ILogger logger, IStringSerializer serializer)
        {
            this.Logger = logger;
            this.Serializer = serializer;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        protected IStringSerializer Serializer { get; }

        /// <summary>
        /// Creates a new message queue.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="options">The configuration.</param>
        /// <returns>Returns a new queue.</returns>
        public abstract IMessageQueue<TMessage> Create<TMessage>(TConfiguration options) where TMessage : class, new();

        /// <summary>
        /// Creates a new message queue.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="connection">The connection.</param>
        /// <returns>Returns a new queue.</returns>
        public abstract IMessageQueue<TMessage> Create<TMessage>(Uri connection) where TMessage : class, new();
    }
}