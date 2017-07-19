namespace NativeCode.Core.DotNet.Logging
{
    using System.Diagnostics;
    using Core.Platform.Logging;

    public class TraceLogWriter : ILogWriter
    {
        public void Flush()
        {
            Trace.Flush();
        }

        public void Write(LogMessageType type, string message)
        {
            Trace.WriteLine(message, type.ToString());
        }
    }
}