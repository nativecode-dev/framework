namespace NativeCode.Core.DotNet.Win32
{
    using System;
    using System.Runtime.InteropServices;

    using Microsoft.Win32.SafeHandles;

    using NativeCode.Core.DotNet.Win32.Enums;
    using NativeCode.Core.DotNet.Win32.Structs;

    public static partial class NativeMethods
    {
        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateConsoleScreenBuffer(EFileAccess access, EFileShare share, IntPtr security, uint flags, IntPtr data);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetConsoleCursorInfo(SafeFileHandle hwnd, out ConsoleCursorInfo info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetConsoleScreenBufferInfo(SafeFileHandle hwnd, out ConsoleScreenBufferInfo info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetCurrentConsoleFontEx(SafeFileHandle hwnd, bool maximumWindow, ref ConsoleFontInfoEx info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetStdHandle(uint stdhandle);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleActiveScreenBuffer(SafeFileHandle hwnd);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleCursorInfo(SafeFileHandle hwnd, [In] ref ConsoleCursorInfo info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleCursorPosition(SafeFileHandle hwnd, Coord position);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetCurrentConsoleFontEx(SafeFileHandle hwnd, bool maximumWindow, ref ConsoleFontInfoEx info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleScreenBufferSize(SafeFileHandle hwnd, Coord size);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleWindowInfo(SafeFileHandle hwnd, bool absolute, [In] ref SmallRect rect);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WriteConsoleOutput(SafeFileHandle hwnd, CharInfo[] buffer, Coord size, Coord coord, ref SmallRect smallRect);

        #region Helpers

        public static SafeFileHandle GetStandardOutputHandle()
        {
            var handle = GetStdHandle(StandardHandles.OutputHandle);

            return new SafeFileHandle(handle, false);
        }

        #endregion
    }
}