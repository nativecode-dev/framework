﻿namespace NativeCode.Core.DotNet.Win32
{
    using System;
    using System.Diagnostics;
    using Enums;
    using Extensions;
    using Structs;

    public static class WindowHelper
    {
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
            SmallRect window;

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
            SmallRect window;

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
            SmallRect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
            {
                var desktop = GetDisplayBounds();

                var top = window.Top;
                var left = (desktop.Right - window.Width()) / 2;

                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, left, top, window.Width(), window.Height(), 0);
            }

            return false;
        }

        public static bool CenterTop(IntPtr hwnd)
        {
            SmallRect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
            {
                var desktop = GetDisplayBounds();

                var left = (desktop.Right - window.Width()) / 2;

                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, left, 0, window.Width(), window.Height(), 0);
            }

            return false;
        }

        public static bool CenterVertically(IntPtr hwnd)
        {
            SmallRect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
            {
                var desktop = GetDisplayBounds();

                var top = (desktop.Bottom - window.Height()) / 2;
                var left = window.Left;

                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, left, top, window.Width(), window.Height(), 0);
            }

            return false;
        }

        public static SmallRect GetDisplayBounds()
        {
            var rect = new SmallRect();

            if (NativeMethods.SystemParametersInfo(SystemProperty.GetWorkArea, 0, ref rect, SystemPropertyFlags.None))
                return rect;

            return default(SmallRect);
        }

        public static IntPtr GetWindowAtCursor()
        {
            Point cursor;

            if (NativeMethods.GetCursorPos(out cursor))
            {
                var hwnd = NativeMethods.WindowFromPoint(cursor);

                uint id;

                NativeMethods.GetWindowThreadProcessId(hwnd, out id);

                var process = Process.GetProcessById((int) id);

                return process.MainWindowHandle;
            }

            return IntPtr.Zero;
        }

        public static bool Move(IntPtr hwnd, int left, int top)
        {
            SmallRect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, left, top, window.Width(), window.Height(), 0);

            return false;
        }

        public static bool Resize(IntPtr hwnd, int height, int width)
        {
            SmallRect window;

            if (NativeMethods.GetWindowRect(hwnd, out window))
                return NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, window.Left, window.Top, height, width, 0);

            return false;
        }
    }
}