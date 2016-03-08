namespace NativeCode.Core.DotNet.Console.Win32
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public SmallRect(int left, int top, int right, int bottom)
        {
            this.Left = Convert.ToInt16(left);
            this.Top = Convert.ToInt16(top);
            this.Right = Convert.ToInt16(right);
            this.Bottom = Convert.ToInt16(bottom);
        }

        public short Left { get; set; }

        public short Top { get; set; }

        public short Right { get; set; }

        public short Bottom { get; set; }
    }
}