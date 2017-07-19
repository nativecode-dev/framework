namespace NativeCode.Core.DotNet.Win32
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using Exceptions;
    using Microsoft.Win32.SafeHandles;
    using Structs;

    public static class NativeHelper
    {
        public static Encoding GetConsoleEncoding()
        {
            return NativeMethods.GetConsoleEncoding();
        }

        public static SafeFileHandle GetStandardInputHandle()
        {
            return NativeMethods.GetStandardInputHandle();
        }

        public static uint GetWindowThreadProcessId(IntPtr handle)
        {
            uint id = 0;
            NativeMethods.GetWindowThreadProcessId(handle, out id);

            return id;
        }

        public static bool ReadConsoleInput(SafeHandle handle, out InputRecord buffer, uint length, out int count)
        {
            if (NativeMethods.ReadConsoleInput(handle, out buffer, length, out count) == false)
                throw new NativeMethodException(Marshal.GetLastWin32Error());

            return true;
        }

        public static bool WriteConsoleOutput(SafeHandle handle, CharInfo[] buffer, Coord size, Coord coord,
            ref SmallRect rect)
        {
            if (NativeMethods.WriteConsoleOutput(handle, buffer, size, coord, ref rect) == false)
                throw new NativeMethodException(Marshal.GetLastWin32Error());

            return true;
        }
    }
}