namespace NativeCode.Core.Logging
{
    using System;
    using System.Collections.Generic;

    using NativeCode.Core.Extensions;

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

        public void Debug(string message)
        {
            this.WriteLogMessage(LogMessageType.Debug, message);
        }

        public void Error(string message)
        {
            this.WriteLogMessage(LogMessageType.Error, message);
        }

        public void Exception<TException>(TException exception) where TException : Exception
        {
            this.WriteLogMessage(LogMessageType.Debug, exception.Stringify());
        }

        public void Informational(string message)
        {
            this.WriteLogMessage(LogMessageType.Informational, message);
        }

        public void Warning(string message)
        {
            this.WriteLogMessage(LogMessageType.Warning, message);
        }

        private void WriteLogMessage(LogMessageType type, string message)
        {
            foreach (var writer in this.writers)
            {
                writer.Write(type, message);
            }
        }
    }
}