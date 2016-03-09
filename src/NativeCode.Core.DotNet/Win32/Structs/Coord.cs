namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X;

        public short Y;

        public Coord(short x, short y)
        {
            this.X = x;
            this.Y = y;
        }

        public Coord(int x, int y)
            : this(Convert.ToInt16(x), Convert.ToInt16(y))
        {
        }
    }
}