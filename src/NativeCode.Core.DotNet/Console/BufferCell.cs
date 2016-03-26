namespace NativeCode.Core.DotNet.Console
{
    using NativeCode.Core.DotNet.Win32.Structs;

    public struct BufferCell
    {
        public BufferCell(int x, int y)
            : this(x, y, default(char), default(Color))
        {
        }

        public BufferCell(int x, int y, char data)
            : this(x, y, data, default(Color))
        {
        }

        public BufferCell(int x, int y, char data, Color color)
        {
            this.Color = color;
            this.Data = data;
            this.X = x;
            this.Y = y;
        }

        public Color Color { get; set; }

        public char Data { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}