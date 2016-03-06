namespace NativeCode.Core.DotNet.Console
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Win32.SafeHandles;

    using NativeCode.Core.DotNet.Console.Win32;

    public abstract class ConsoleHandle : IDisposable
    {
        private readonly List<ConsoleScreenBuffer> buffers = new List<ConsoleScreenBuffer>();

        public IntPtr Handle { get; protected set; }

        public int ActiveIndex { get; private set; }

        protected bool Disposed { get; private set; }

        public ConsoleScreenBuffer CreateBuffer(string name)
        {
            var handle = NativeMethods.CreateConsoleScreenBuffer(
                EFileAccess.GenericRead | EFileAccess.GenericWrite,
                EFileShare.Read | EFileShare.Write,
                IntPtr.Zero,
                1,
                IntPtr.Zero);

            return this.CreateBuffer(handle, name);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SetActive(int index)
        {
            var buffer = this.buffers[index];

            if (this.SetActive(buffer))
            {
                this.ActiveIndex = index;
            }
        }

        protected bool SetActive(ConsoleScreenBuffer buffer)
        {
            var handle = buffer.Handle;
            return NativeMethods.SetConsoleActiveScreenBuffer(handle);
        }

        protected ConsoleScreenBuffer CreateBuffer(IntPtr handle, string name)
        {
            var buffer = new ConsoleScreenBuffer(handle, name);
            this.buffers.Add(buffer);

            return buffer;
        }

        protected ConsoleScreenBuffer CreateBuffer(SafeFileHandle handle, string name)
        {
            var buffer = new ConsoleScreenBuffer(handle, name);
            this.buffers.Add(buffer);

            return buffer;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.Disposed = true;
            }
        }
    }
}