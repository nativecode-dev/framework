namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;

    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract to queue and retrieve messages.
    /// </summary>
    public interface IMessageQueue : IDisposable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string QueueName { get; }

        /// <summary>
        /// Gets the next set of bytes.
        /// </summary>
        /// <returns>Returns a byte array.</returns>
        [CanBeNull]
        byte[] DequeueBytes();

        /// <summary>
        /// Enqueues the bytes.
        /// </summary>
        /// <param name="data">The data.</param>
        void EnqueueBytes([NotNull] byte[] data);
    }

    /// <summary>
    /// Provides a contract to queue and retrieve messages.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <seealso cref="IMessageQueue" />
    public interface IMessageQueue<TMessage> : IMessageQueue
        where TMessage : class, new()
    {
        /// <summary>
        /// Retrieves the next message.
        /// </summary>
        /// <returns>Returns a message or <c>null</c>.</returns>
        [CanBeNull]
        TMessage DequeueMessage();

        /// <summary>
        /// Enqueues the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void EnqueueMessage([NotNull] TMessage message);
    }
}