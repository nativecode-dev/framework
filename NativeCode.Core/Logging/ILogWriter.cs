namespace NativeCode.Core.Logging
{
    using JetBrains.Annotations;

    public interface ILogWriter
    {
        void Flush();

        void Write(LogMessageType type, [NotNull] string message);
    }
}