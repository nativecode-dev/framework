namespace NativeCode.Core.Platform.Logging
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides an abstraction around logging messages.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes a debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tags">Set of tag strings.</param>
        void Debug([NotNull] string message, params string[] tags);

        /// <summary>
        /// Writes an error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tags">Set of tag strings.</param>
        void Error([NotNull] string message, params string[] tags);

        /// <summary>
        /// Writes an exception message.
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="tags">Set of tag strings.</param>
        void Exception<TException>([NotNull] TException exception, params string[] tags) where TException : Exception;

        /// <summary>
        /// Writes an informational message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tags">Set of tag strings.</param>
        void Informational([NotNull] string message, params string[] tags);

        /// <summary>
        /// Writes a warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tags">Set of tag strings.</param>
        void Warning([NotNull] string message, params string[] tags);
    }
}