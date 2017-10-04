namespace NativeCode.Core.Extensions
{
    using System.IO;
    using JetBrains.Annotations;
    using Reliability;

    public static class StreamExtensions
    {
        [NotNull]
        public static StreamMonitor Monitor([NotNull] this Stream stream)
        {
            return new StreamMonitor(stream);
        }

        [NotNull]
        public static StreamMonitor Monitor([NotNull] this Stream stream, long length)
        {
            return new StreamMonitor(stream, length);
        }
    }
}