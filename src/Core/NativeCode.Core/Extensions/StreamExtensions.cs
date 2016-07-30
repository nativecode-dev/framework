namespace NativeCode.Core.Extensions
{
    using System.IO;

    using NativeCode.Core.Types;

    public static class StreamExtensions
    {
        public static StreamMonitor Monitor(this Stream stream)
        {
            return new StreamMonitor(stream);
        }

        public static StreamMonitor Monitor(this Stream stream, long length)
        {
            return new StreamMonitor(stream, length);
        }
    }
}