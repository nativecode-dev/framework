namespace NativeCode.Core.DotNet.Console
{
    using System;

    using Microsoft.Win32.SafeHandles;

    public class ConsoleScreenBuffer : IDisposable
    {
        public ConsoleScreenBuffer(IntPtr handle, string name)
        {
            this.Handle = handle;
            this.Name = name;
        }

        public ConsoleScreenBuffer(SafeFileHandle handle, string name)
        {
            this.FileHandle = handle;
            this.Name = name;
        }

        public SafeFileHandle FileHandle { get; set; }

        public IntPtr Handle { get; private set; }

        public string Name { get; }

        protected bool Disposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.Disposed = true;
                this.Handle = IntPtr.Zero;
            }
        }
    }
}