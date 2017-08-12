namespace NativeCode.Core.Platform.Logging
{
    public class NullLogWriter : ILogWriter
    {
        public void Flush()
        {
        }

        public void Write(LogMessage logMessage)
        {
        }
    }
}