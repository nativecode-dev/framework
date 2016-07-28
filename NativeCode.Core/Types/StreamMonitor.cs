namespace NativeCode.Core.Types
{
    using System;
    using System.IO;

    public class StreamMonitor : Stream
    {
        private readonly Stream source;

        public StreamMonitor(Stream source, bool owner = true)
            : this(source, 0, owner)
        {
        }

        public StreamMonitor(Stream source, long length, bool owner = true)
        {
            this.source = source;
            this.Length = length;
            this.StreamOwner = owner;
        }

        public event EventHandler<StreamMonitorEventArgs> StreamRead;

        public event EventHandler<StreamMonitorEventArgs> StreamWrite;

        public override bool CanRead => this.source.CanRead;

        public override bool CanSeek => this.source.CanSeek;

        public override bool CanWrite => this.source.CanWrite;

        public override long Length { get; }

        protected bool StreamOwner { get; }

        public override long Position { get; set; }

        public override void Flush()
        {
            this.source.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var bytesRead = this.source.Read(buffer, offset, count);

            this.StreamRead?.Invoke(this, new StreamMonitorEventArgs(this.Position += bytesRead, this.Length));

            return bytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.source.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.source.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.source.Write(buffer, offset, count);

            this.StreamWrite?.Invoke(this, new StreamMonitorEventArgs(this.Position += count, this.Length));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.StreamOwner)
            {
                this.source.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}