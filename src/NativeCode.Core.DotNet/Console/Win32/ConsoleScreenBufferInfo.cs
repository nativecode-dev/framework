namespace NativeCode.Core.DotNet.Console.Win32
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleScreenBufferInfo
    {
        public Coord DwSize { get; set; }

        public Coord DwCursorPosition { get; set; }

        public short Attributes { get; set; }

        public SmallRect Window { get; set; }

        public Coord DwMaximumWindowSize { get; set; }
    }
}