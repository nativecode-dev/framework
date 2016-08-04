namespace NativeCode.Core.Logging
{
    public class NullLogWriter : ILogWriter
    {
        public void Flush()
        {
        }

        public void Write(LogMessageType type, string message)
        {
        }
    }
}