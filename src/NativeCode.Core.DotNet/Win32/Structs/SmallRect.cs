namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left;

        public short Top;

        public short Right;

        public short Bottom;

        public SmallRect(short left, short top, short right, short bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public SmallRect(int left, int top, int right, int bottom)
        {
            this.Left = Convert.ToInt16(left);
            this.Top = Convert.ToInt16(top);
            this.Right = Convert.ToInt16(right);
            this.Bottom = Convert.ToInt16(bottom);
        }
    }
}