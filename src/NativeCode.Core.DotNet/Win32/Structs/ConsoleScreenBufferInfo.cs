namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleScreenBufferInfo
    {
        public Coord DwSize;

        public Coord DwCursorPosition;

        public short Attributes;

        public Rect Window;

        public Coord DwMaximumWindowSize;
    }
}