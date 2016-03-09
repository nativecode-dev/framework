namespace NativeCode.Core.DotNet.Win32
{
    using System;
    using System.Runtime.InteropServices;

    using NativeCode.Core.DotNet.Win32.Enums;
    using NativeCode.Core.DotNet.Win32.Structs;

    public static partial class NativeMethods
    {
        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateConsoleScreenBuffer(EFileAccess access, EFileShare share, IntPtr security, uint flags, IntPtr data);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetConsoleCursorInfo(IntPtr hwnd, out ConsoleCursorInfo info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetConsoleScreenBufferInfo(IntPtr hwnd, out ConsoleScreenBufferInfo info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetStdHandle(int stdhandle);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleActiveScreenBuffer(IntPtr hwnd);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleCursorInfo(IntPtr hwnd, [In] ref ConsoleCursorInfo info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleCursorPosition(IntPtr hwnd, Coord position);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WriteConsoleOutput(IntPtr hwnd, [MarshalAs(UnmanagedType.LPArray)] CharInfo[,] buffer, Coord size, Coord coord, ref Rect rect);
    }
}