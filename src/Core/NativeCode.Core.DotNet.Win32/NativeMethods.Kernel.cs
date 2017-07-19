namespace NativeCode.Core.DotNet.Win32
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using Enums;
    using Microsoft.Win32.SafeHandles;
    using Structs;

    internal static partial class NativeMethods
    {
        public delegate bool EnumCodePagesProcDelegate(string lpCodePageString);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateConsoleScreenBuffer(EFileAccess access, EFileShare share, IntPtr security,
            uint flags, IntPtr data);

        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool EnumSystemCodePages(EnumCodePagesProcDelegate @delegate,
            [MarshalAs(UnmanagedType.U4)] EnumCodePageFlag flags);

        [DllImport(Kernel32, SetLastError = true)]
        public static extern uint GetConsoleCP();

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetConsoleCursorInfo(SafeHandle hwnd, out ConsoleCursorInfo info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetConsoleScreenBufferInfo(SafeHandle hwnd, out ConsoleScreenBufferInfo info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetConsoleScreenBufferInfoEx(SafeHandle hwnd, out ConsoleScreenBufferInfoEx info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetCurrentConsoleFontEx(SafeHandle hwnd, bool maximumWindow,
            ref ConsoleFontInfoEx info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetStdHandle(uint stdhandle);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ReadConsoleInput(SafeHandle hwnd, out InputRecord buffer, uint length, out int count);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleActiveScreenBuffer(SafeHandle hwnd);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleCursorInfo(SafeHandle hwnd, [In] ref ConsoleCursorInfo info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleCursorPosition(SafeHandle hwnd, Coord position);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleScreenBufferInfoEx(SafeHandle hwnd, ref ConsoleScreenBufferInfoEx info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleScreenBufferSize(SafeHandle hwnd, Coord size);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetConsoleWindowInfo(SafeHandle hwnd, bool absolute, [In] ref SmallRect rect);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetCurrentConsoleFontEx(SafeHandle hwnd, bool maximumWindow,
            ref ConsoleFontInfoEx info);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WriteConsoleOutput(SafeHandle hwnd, CharInfo[] buffer, Coord size, Coord coord,
            ref SmallRect smallRect);

        #region Helpers

        public static Encoding GetConsoleEncoding()
        {
            var codepage = GetConsoleCP();

            return GetEncodingForCodePage((int) codepage);
        }

        public static SafeFileHandle GetStandardInputHandle()
        {
            var handle = GetStdHandle(StandardHandles.InputHandle);

            return new SafeFileHandle(handle, false);
        }

        public static SafeFileHandle GetStandardOutputHandle()
        {
            var handle = GetStdHandle(StandardHandles.OutputHandle);

            return new SafeFileHandle(handle, false);
        }

        private static Encoding GetEncodingForCodePage(int codepage)
        {
            try
            {
                return Encoding.GetEncoding(codepage);
            }
            catch (NotSupportedException)
            {
                return Encoding.Default;
            }
        }

        #endregion Helpers
    }
}