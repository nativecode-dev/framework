namespace NativeCode.Core.DotNet.Console.Win32
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X { get; set; }

        public short Y { get; set; }
    }
}