namespace NativeCode.Core.DotNet.Console.Win32
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SecurityAttributes
    {
        public uint Length { get; set; }

        public IntPtr SecurityDescriptor { get; set; }

        public uint InheritHandle { get; set; }
    }
}