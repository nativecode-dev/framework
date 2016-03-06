namespace NativeCode.Core.DotNet.Console
{
    using System;

    using NativeCode.Core.DotNet.Console.Win32;

    public abstract class StdConsoleHandle : ConsoleHandle
    {
        protected StdConsoleHandle(int handle)
        {
            this.Handle = NativeMethods.GetStdHandle(handle);
            ConsoleScreenBufferInfo info;

            if (NativeMethods.GetConsoleScreenBufferInfo(this.Handle, out info))
            {
                this.Info = info;
                this.GetActiveConsoleScreenBuffer();
            }
            else
            {
                throw new InvalidOperationException("Failed to fetch buffer information for the current console.");
            }
        }

        protected ConsoleScreenBufferInfo Info { get; private set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                NativeMethods.FreeConsole();
            }

            base.Dispose(disposing);
        }

        private void GetActiveConsoleScreenBuffer()
        {
            var handle = NativeMethods.CreateFile(
                "CONOUT$",
                EFileAccess.GenericRead | EFileAccess.GenericWrite,
                EFileShare.Read | EFileShare.Write,
                IntPtr.Zero,
                EFileMode.OpenAlways,
                EFileAttributes.Normal,
                IntPtr.Zero);

            if (handle.IsInvalid)
            {
                this.CreateBuffer("Default");
            }
            else
            {
                this.CreateBuffer(handle, "Default");
            }
        }
    }
}