namespace NativeCode.Core.DotNet.Win32
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    using NativeCode.Core.DotNet.Win32.Enums;
    using NativeCode.Core.DotNet.Win32.Structs;

    public static partial class NativeMethods
    {
        public delegate void WinEventDelegate(
            IntPtr hWinEventHook,
            uint eventType,
            IntPtr hwnd,
            int idObject,
            int idChild,
            uint dwEventThread,
            uint dwmsEventTime);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetCursorPos(out Point point);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint id);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out SmallRect smallRect);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        [Obsolete("Use SetWinEventHook that uses enum parameters.", false)]
        public static extern IntPtr SetWinEventHook(
            uint eventMin,
            uint eventMax,
            IntPtr hmodWinEventProc,
            WinEventDelegate lpfnWinEventProc,
            uint idProcess,
            uint idThread,
            uint dwFlags);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWinEventHook(
            [MarshalAs(UnmanagedType.U4)] Event min,
            [MarshalAs(UnmanagedType.U4)] Event max,
            IntPtr hwnd,
            WinEventDelegate proc,
            uint process,
            uint thread,
            [MarshalAs(UnmanagedType.U4)] WinEvent flags);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr insertAfter, int x, int y, int cx, int cy, uint flags);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SystemParametersInfo(SystemProperty uiAction, uint uiParam, ref SmallRect pvParam, SystemPropertyFlags fWinIni);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool UnhookWinEvent(IntPtr hook);

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr WindowFromPoint(Point point);
    }
}