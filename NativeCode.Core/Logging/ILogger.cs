namespace NativeCode.Core.Logging
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
        void Debug([NotNull] string message);

        /// <summary>
        /// Writes an error message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error([NotNull] string message);

        /// <summary>
        /// Writes an exception message.
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="exception">The exception.</param>
        void Exception<TException>([NotNull] TException exception) where TException : Exception;

        /// <summary>
        /// Writes an informational message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Informational([NotNull] string message);

        /// <summary>
        /// Writes a warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning([NotNull] string message);
    }
}