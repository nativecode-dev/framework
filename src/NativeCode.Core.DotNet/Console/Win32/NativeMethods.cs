namespace NativeCode.Core.DotNet.Console.Win32
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    using Microsoft.Win32.SafeHandles;

    internal static class NativeMethods
    {
        private const string Kernel32 = "kernel32.dll";

        private const string User32 = "user32.dll";

        [DllImport(Kernel32, EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern int AllocConsole();

        [DllImport(Kernel32, SetLastError = true)]
        internal static extern bool AttachConsole(uint id);

        [DllImport(Kernel32)]
        internal static extern IntPtr CreateConsoleScreenBuffer(
            EFileAccess access,
            EFileShare share,
            IntPtr security,
            uint flags,
            IntPtr data);

        [DllImport(Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern SafeFileHandle CreateFile(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] EFileAccess access,
            [MarshalAs(UnmanagedType.U4)] EFileShare share,
            IntPtr security,
            [MarshalAs(UnmanagedType.U4)] EFileMode mode,
            [MarshalAs(UnmanagedType.U4)] EFileAttributes attributes,
            IntPtr template);

        [DllImport(Kernel32, CharSet = CharSet.Ansi, SetLastError = true)]
        internal static extern SafeFileHandle CreateFileA(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] EFileAccess access,
            [MarshalAs(UnmanagedType.U4)] EFileShare share,
            IntPtr security,
            [MarshalAs(UnmanagedType.U4)] EFileMode mode,
            [MarshalAs(UnmanagedType.U4)] EFileAttributes attributes,
            IntPtr template);

        [DllImport(Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern SafeFileHandle CreateFileW(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] EFileAccess access,
            [MarshalAs(UnmanagedType.U4)] EFileShare share,
            IntPtr security,
            [MarshalAs(UnmanagedType.U4)] EFileMode mode,
            [MarshalAs(UnmanagedType.U4)] EFileAttributes attributes,
            IntPtr template);

        [DllImport(Kernel32, SetLastError = true, ExactSpelling = true)]
        internal static extern bool FreeConsole();

        [DllImport(Kernel32)]
        internal static extern IntPtr GetConsoleWindow();

        [DllImport(Kernel32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetConsoleScreenBufferInfo(IntPtr handle, out ConsoleScreenBufferInfo info);

        [DllImport(User32)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport(Kernel32, SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int handle);

        [DllImport(User32, SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr handle, out int id);

        [DllImport(Kernel32)]
        internal static extern bool SetConsoleActiveScreenBuffer(IntPtr handle);

        [DllImport(User32)]
        internal static extern bool SetForegroundWindow(IntPtr handle);
    }
}