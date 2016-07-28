namespace NativeCode.Core.Logging
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class InMemoryLogWriter : ILogWriter
    {
        private readonly ConcurrentQueue<string> messages = new ConcurrentQueue<string>();

        public void Flush()
        {
            while (this.messages.Any())
            {
                string message;
                this.messages.TryDequeue(out message);
            }
        }

        public Task FlushAsync(CancellationToken cancellationToken)
        {
            return new Task(this.Flush);
        }

        public void Write(LogMessageType type, string message)
        {
            this.messages.Enqueue(FormatLogMessage(type, message));
        }

        public Task WriteAsync(LogMessageType type, string message, CancellationToken cancellationToken)
        {
            return new Task(() => this.messages.Enqueue(FormatLogMessage(type, message)), TaskCreationOptions.AttachedToParent);
        }

        private static string FormatLogMessage(LogMessageType type, string message)
        {
            return $"{DateTime.UtcNow} [{type}] {message}";
        }
    }
}