namespace NativeCode.Core.DotNet.Console.Win32
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public static readonly Coord Origin = new Coord(0, 0);

        public Coord(int x, int y)
        {
            this.X = Convert.ToInt16(x);
            this.Y = Convert.ToInt16(y);
        }

        public Coord(short x, short y)
        {
            this.X = x;
            this.Y = y;
        }

        public short X { get; set; }

        public short Y { get; set; }
    }
}