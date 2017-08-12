namespace NativeCode.Core.Platform.Logging
{
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
        /// Writes the <see cref="LogMessage" /> instance.
        /// </summary>
        /// <param name="logMessage"><see cref="LogMessage" /> instance.</param>
        void Write(LogMessage logMessage);
    }
}