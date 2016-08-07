namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;

    using NativeCode.Core.Types;

    public abstract class MessageQueueFactory<TConfiguration> : Disposable, IMessageQueueFactory<TConfiguration>
        where TConfiguration : class, new()
    {
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

        /// <summary>
        /// Creates a new message queue.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>Returns a new queue.</returns>
        public abstract IMessageQueue Create(Type type, Uri connection);
    }
}