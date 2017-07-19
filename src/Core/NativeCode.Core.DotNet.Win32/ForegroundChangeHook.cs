namespace NativeCode.Core.DotNet.Win32
{
    using System;
    using Enums;

    public class ForegroundChangeHook : HookEvent
    {
        private readonly Action<IntPtr> handler;

        public ForegroundChangeHook(IntPtr hwnd, Action<IntPtr> handler)
            : base(hwnd, Event.SystemForeground, Event.SystemForeground)
        {
            this.handler = handler;
        }

        protected override void Handler(IntPtr hwineventhook, uint eventtype, IntPtr hwnd, int idobject, int idchild,
            uint dweventthread, uint dwmseventtime)
        {
            this.handler(hwnd);
        }
    }
}