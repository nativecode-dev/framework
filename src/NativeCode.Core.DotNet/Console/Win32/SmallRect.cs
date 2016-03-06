namespace NativeCode.Core.DotNet.Console.Win32
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left { get; set; }

        public short Top { get; set; }

        public short Right { get; set; }

        public short Bottom { get; set; }
    }
}