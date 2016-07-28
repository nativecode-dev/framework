namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct Point
    {
        public int X;

        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
