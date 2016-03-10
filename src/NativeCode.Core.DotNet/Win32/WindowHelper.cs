namespace NativeCode.Core.DotNet.Win32
{
    using System;
    using System.Diagnostics;

    using NativeCode.Core.DotNet.Win32.Enums;
    using NativeCode.Core.DotNet.Win32.Extensions;
    using NativeCode.Core.DotNet.Win32.Structs;

    public static class WindowHelper
    {
        public static Rect GetDisplayBounds()
        {
            var rect = new Rect();

            if (NativeMethods.SystemParametersInfo(SystemProperty.GetWorkArea, 0, ref rect, SystemPropertyFlags.None))
            {
                return rect;
            }

            return default(Rect);
        }

        public static IntPtr GetWindowAtCursor()
        {
            Point cursor;

            if (NativeMethods.GetCursorPos(out cursor))
            {
                var hwnd = NativeMethods.WindowFromPoint(cursor);

                uint id;

                NativeMethods.GetWindowThreadProcessId(hwnd, out id);

                var process = Process.GetProcessById((int)id);

                return process.MainWindowHandle;
            }

            return IntPtr.Zero;
        }

        public static bool Center(IntPtr hwnd, CenteringPosition position)
        {
            switch (position)
            {
                case CenteringPosition.CenterBottom:
                    return CenterBottom(hwnd);

                case CenteringPosition.CenterHorizontally:
                    return CenterHorizontal(hwnd);

                case CenteringPosition.CenterVertically:
                    return CenterVertically(hwnd);

                default:
                    return Center(hwnd);
            }
        }

        public static bool Center(IntPtr hwnd)
        {
            Rect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
            {
                var desktop = GetDisplayBounds();

                var top = (desktop.Bottom - window.Height()) / 2;
                var left = (desktop.Right - window.Width()) / 2;

                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, left, top, window.Width(), window.Height(), 0);
            }

            return false;
        }

        public static bool CenterBottom(IntPtr hwnd)
        {
            Rect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
            {
                var desktop = GetDisplayBounds();

                var top = desktop.Bottom - window.Height();
                var left = (desktop.Right - window.Width()) / 2;

                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, left, top, window.Width(), window.Height(), 0);
            }

            return false;
        }

        public static bool CenterHorizontal(IntPtr hwnd)
        {
            Rect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
            {
                var desktop = GetDisplayBounds();

                var top = window.Top;
                var left = (desktop.Right - window.Width()) / 2;

                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, left, top, window.Width(), window.Height(), 0);
            }

            return false;
        }

        public static bool CenterVertically(IntPtr hwnd)
        {
            Rect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
            {
                var desktop = GetDisplayBounds();

                var top = (desktop.Bottom - window.Height()) / 2;
                var left = window.Left;

                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, left, top, window.Width(), window.Height(), 0);
            }

            return false;
        }

        public static bool Move(IntPtr hwnd, int left, int top)
        {
            Rect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
            {
                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, left, top, window.Width(), window.Height(), 0);
            }

            return false;
        }

        public static bool Resize(IntPtr hwnd, int height, int width)
        {
            Rect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
            {
                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, window.Left, window.Top, height, width, 0);
            }

            return false;
        }
    }
}