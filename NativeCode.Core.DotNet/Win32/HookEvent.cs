namespace NativeCode.Core.DotNet.Win32
{
    using System;

    using NativeCode.Core.DotNet.Win32.Enums;
    using NativeCode.Core.Types;

    public abstract class HookEvent : Disposable
    {
        private readonly NativeMethods.WinEventDelegate handler;

        private readonly IntPtr hook;

        protected HookEvent(IntPtr hwnd, Event minEvent)
            : this(hwnd, minEvent, minEvent)
        {
        }

        protected HookEvent(IntPtr hwnd, Event minEvent, Event maxEvent)
        {
            this.handler = this.Handler;
            this.hook = NativeMethods.SetWinEventHook(minEvent, maxEvent, hwnd, this.handler, 0, 0, WinEvent.OutOfContext);
        }

        protected abstract void Handler(IntPtr hwineventhook, uint eventtype, IntPtr hwnd, int idobject, int idchild, uint dweventthread, uint dwmseventtime);

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                NativeMethods.UnhookWinEvent(this.hook);
            }

            base.Dispose(disposing);
        }
    }
}