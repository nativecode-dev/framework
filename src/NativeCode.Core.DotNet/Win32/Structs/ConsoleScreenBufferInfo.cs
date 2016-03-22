namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleScreenBufferInfo
    {
        public Coord Size;

        public Coord CursorPosition;

        public short Attributes;

        public SmallRect Window;

        public Coord MaximumWindowSize;
    }
}