namespace NativeCode.Core.Platform.Logging
{
    using System;
    using System.Collections.Generic;
    using Extensions;

    public class Logger : ILogger
    {
        private readonly IEnumerable<ILogWriter> writers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger" /> class.
        /// </summary>
        /// <param name="writers">The writers.</param>
        public Logger(IEnumerable<ILogWriter> writers)
        {
            this.writers = writers;
        }

        public void Debug(string message, params string[] tags)
        {
            this.WriteLogMessage(LogMessageType.Debug, message, tags);
        }

        public void Error(string message, params string[] tags)
        {
            this.WriteLogMessage(LogMessageType.Error, message, tags);
        }

        public void Exception<TException>(TException exception, params string[] tags) where TException : Exception
        {
            this.WriteLogMessage(LogMessageType.Debug, exception.ToExceptionString(), tags);
        }

        public void Informational(string message, params string[] tags)
        {
            this.WriteLogMessage(LogMessageType.Informational, message, tags);
        }

        public void Warning(string message, params string[] tags)
        {
            this.WriteLogMessage(LogMessageType.Warning, message, tags);
        }

        private void WriteLogMessage(LogMessageType type, string message, params string[] tags)
        {
            var log = new LogMessage
            {
                Message = message,
                Type = type
            };

            log.Tags.AddRange(tags);

            foreach (var writer in this.writers)
                writer.Write(log);
        }
    }
}