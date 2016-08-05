namespace NativeCode.Core.Platform.Logging
{
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract to write log messages.
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Flushes this instance.
        /// </summary>
        void Flush();

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        void Write(LogMessageType type, [NotNull] string message);
    }
}