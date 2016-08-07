namespace NativeCode.Core.Platform.Messaging.Processing
{
    using System;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract to process queue messages.
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Determines whether this instance can process the message type.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns><c>true</c> if this instance can process the message type; otherwise, <c>false</c>.</returns>
        bool CanProcessMessage([NotNull] object message);

        /// <summary>
        /// Processes the provided message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Returns a <see cref="Task" />.</returns>
        Task ProcessMessageAsync([NotNull] object message);

        /// <summary>
        /// Processes the provided message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="retries">The retries.</param>
        /// <returns>Returns a <see cref="Task" />.</returns>
        Task ProcessMessageAsync([NotNull] object message, int retries);

        /// <summary>
        /// Processes the provided message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="retries">The retries.</param>
        /// <param name="requeue">if set to <c>true</c> re-queue message.</param>
        /// <returns>Returns a <see cref="Task" />.</returns>
        Task ProcessMessageAsync([NotNull] object message, int retries, bool requeue);
    }
}