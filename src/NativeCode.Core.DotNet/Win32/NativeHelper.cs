namespace NativeCode.Core.DotNet.Win32
{
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

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

        public static bool ReadConsoleInput(SafeHandle handle, out Structs.InputRecord buffer, uint length, out int count)
        {
            return NativeMethods.ReadConsoleInput(handle, out buffer, length, out count);
        }

        public static bool WriteConsoleOutput(SafeHandle handle, Structs.CharInfo[] buffer, Structs.Coord size, Structs.Coord coord, ref Structs.SmallRect rect)
        {
            return NativeMethods.WriteConsoleOutput(handle, buffer, size, coord, ref rect);
        }
    }
}
