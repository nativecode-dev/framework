namespace NativeCode.Core.DotNet.Console.Win32
{
    using System;
    using System.Runtime.InteropServices;

    public static class NativeMethods
    {
        private const string Kernel32 = "kernel32.dll";

        private const string User32 = "user32.dll";

        [DllImport(Kernel32, SetLastError = true)]
        public static extern IntPtr CreateConsoleScreenBuffer(EFileAccess access, EFileShare share, IntPtr security, uint flags, IntPtr data);

        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool GetConsoleCursorInfo(IntPtr handle, out ConsoleCursorInfo info);

        [DllImport(Kernel32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetConsoleScreenBufferInfo(IntPtr handle, out ConsoleScreenBufferInfo info);

        [DllImport(Kernel32, SetLastError = true)]
        public static extern IntPtr GetStdHandle(int stdhandle);

        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool SetConsoleActiveScreenBuffer(IntPtr handle);

        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool SetConsoleCursorInfo(IntPtr handle, [In] ref ConsoleCursorInfo info);

        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool SetConsoleCursorPosition(IntPtr handle, Coord position);

        public static bool SetConsoleCursorPosition(IntPtr handle, int left, int top)
        {
            return SetConsoleCursorPosition(handle, new Coord(left, top));
        }

        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool WriteConsoleOutput(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPArray)] CharInfo[,] buffer,
            Coord size,
            Coord coord,
            ref SmallRect rect);

        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool WriteConsoleOutput(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPArray)] CharInfo[] buffer,
            Coord size,
            Coord coord,
            ref SmallRect rect);

        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool WriteConsoleOutputA(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPArray)] CharInfo[,] buffer,
            Coord size,
            Coord coord,
            ref SmallRect rect);

        [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool WriteConsoleOutputW(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPArray)] CharInfo[,] buffer,
            Coord size,
            Coord coord,
            ref SmallRect rect);

        public static bool WriteConsoleOutput(
            IntPtr handle,
            char text,
            Coord size,
            Coord position,
            ConsoleColor background,
            ConsoleColor foreground,
            ref SmallRect rect)
        {
            var buffer = new CharInfo[1, 1];
            buffer[0, 0] = new CharInfo(text, background, foreground);

            return WriteConsoleOutput(handle, buffer, size, position, ref rect);
        }

        public static bool WriteConsoleOutput(
            IntPtr handle,
            string text,
            Coord size,
            Coord position,
            ConsoleColor background,
            ConsoleColor foreground,
            ref SmallRect rect)
        {
            var buffer = new CharInfo[text.Length, 1];

            for (var index = 0; index < text.Length; index++)
            {
                buffer[index, 0] = new CharInfo(text[index], background, foreground);
            }

            return WriteConsoleOutput(handle, buffer, size, position, ref rect);
        }
    }
}