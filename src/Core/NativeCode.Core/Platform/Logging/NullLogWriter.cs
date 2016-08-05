namespace NativeCode.Core.Platform.Logging
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