namespace NativeCode.Core.Messaging
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a contract to send and receive messages.
    /// </summary>
    /// <typeparam name="TConfig">The type of the configuration.</typeparam>
    /// <typeparam name="TQueueConfig">The type of the t queue configuration.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IMessageQueueProvider<out TConfig, in TQueueConfig> : IDisposable
        where TConfig : QueueProviderConfiguration where TQueueConfig : QueueConfiguration
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        TConfig Configuration { get; }

        /// <summary>
        /// Creates a new queue from the provided configuration.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns><c>true</c> if queue created, <c>false</c> otherwise.</returns>
        bool CreateQueue(TQueueConfig options);

        /// <summary>
        /// Creates a new queue from the provided configuration.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if queue created, <c>false</c> otherwise.</returns>
        Task<bool> CreateQueueAsync(TQueueConfig options, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified queue.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns><c>true</c> if queue deleted, <c>false</c> otherwise.</returns>
        bool DeleteQueue(string queueName);

        /// <summary>
        /// Deletes the specified queue.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns><c>true</c> if queue deleted, <c>false</c> otherwise.</returns>
        Task<bool> DeleteQueueAsync(string queueName);
    }
}