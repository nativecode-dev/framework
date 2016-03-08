namespace NativeCode.Core.DotNet.Console
{
    using System;
    using System.Collections.Generic;

    using NativeCode.Core.DotNet.Console.Win32;

    public class Screen
    {
        private readonly List<BufferHandle> buffers = new List<BufferHandle>();

        public Screen()
        {
            var handle = NativeMethods.GetStdHandle(StdHandle.StdOutputHandle);

            this.ActiveBuffer = this.CreateBuffer(handle);
            this.BackBuffer = this.CreateBuffer();
        }

        public BufferHandle ActiveBuffer { get; private set; }

        public BufferHandle BackBuffer { get; private set; }

        public void Flip()
        {
            var active = this.ActiveBuffer;
            var back = this.BackBuffer;

            if (NativeMethods.SetConsoleActiveScreenBuffer(back.Handle))
            {
                this.ActiveBuffer = back;
                this.BackBuffer = active;
            }
        }

        private BufferHandle CreateBuffer()
        {
            var handle = NativeMethods.CreateConsoleScreenBuffer(
                EFileAccess.GenericRead | EFileAccess.GenericWrite,
                EFileShare.Read | EFileShare.Write,
                IntPtr.Zero,
                1,
                IntPtr.Zero);

            return this.CreateBuffer(handle);
        }

        private BufferHandle CreateBuffer(IntPtr handle)
        {
            var buffer = new BufferHandle(handle);
            this.buffers.Add(buffer);

            return buffer;
        }
    }
}