namespace NativeCode.Core.Platform.Messaging.Queuing
{
    using System;

    /// <summary>
    /// Provides a factory to create <see cref="IMessageQueue" /> instances.
    /// </summary>
    public interface IMessageQueueFactory
    {
        /// <summary>
        /// Creates a new message queue.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="connection">The connection.</param>
        /// <returns>Returns a new queue.</returns>
        IMessageQueue<TMessage> Create<TMessage>(Uri connection) where TMessage : class, new();
    }

    /// <summary>
    /// Provides a factory to create <see cref="IMessageQueue" /> instances.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of the configuration.</typeparam>
    public interface IMessageQueueFactory<in TConfiguration> : IMessageQueueFactory
        where TConfiguration : class
    {
        /// <summary>
        /// Creates a new message queue.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="options">The configuration.</param>
        /// <returns>Returns a new queue.</returns>
        IMessageQueue<TMessage> Create<TMessage>(TConfiguration options) where TMessage : class, new();
    }
}
