namespace NativeCode.Core.Messaging
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a contract to interface with a queue instance.
    /// </summary>
    /// <typeparam name="TConfig">A derived <see cref="QueueConfiguration" />.</typeparam>
    /// <typeparam name="TMessage">The type of the t message.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IMessageQueueClient<out TConfig, TMessage> : IDisposable
        where TConfig : QueueConfiguration where TMessage : class
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        TConfig Configuration { get; }

        /// <summary>
        /// Enqueues the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Enqueue(TMessage message);

        /// <summary>
        /// Enqueues the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a <see cref="Task" />.</returns>
        Task EnqueueAsync(TMessage message, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves the next available message from the queue.
        /// </summary>
        /// <returns>Returns a message or <c>null</c>.</returns>
        TMessage Dequeue();

        /// <summary>
        /// Retrieves the next available message from the queue.
        /// </summary>
        /// <returns>Returns a message or <c>null</c>.</returns>
        Task<TMessage> DequeueAsync();
    }
}