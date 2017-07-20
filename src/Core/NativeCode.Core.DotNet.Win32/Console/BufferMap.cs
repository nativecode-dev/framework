namespace NativeCode.Core.DotNet.Win32.Console
{
    using NativeCode.Core.DotNet.Win32.Structs;
    using NativeCode.Core.Types.Structs;

    public class BufferMap
    {
        private readonly BufferCell[] cells;

        public BufferMap(Size size) : this(size.Height, size.Width)
        {
        }

        public BufferMap(int height, int width)
        {
            this.cells = new BufferCell[height * width];

            this.Height = height;
            this.Width = width;
        }

        public int Height { get; }

        public int Width { get; }

        public BufferCell this[int x, int y] { get { return this.cells[this.Index(x, y)]; } set { this.cells[this.Index(x, y)] = value; } }

        public CharInfo[] GetBuffer(int left, int top, int right, int bottom)
        {
            var height = bottom - top;
            var width = right - left;
            var buffer = new CharInfo[width * height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = this[x + +width, y];
                    buffer[x + width * y].Char.UnicodeChar = cell.Data;
                    buffer[x + width * y].Color = cell.Color;
                }
            }

            return buffer;
        }

        public int Index(int x, int y)
        {
            return x + this.Width * y;
        }

        public void Write(char data, int x, int y)
        {
            this.cells[x + this.Width * y].Data = data;
            this.cells[x + this.Width * y].X = x;
            this.cells[x + this.Width * y].Y = y;
        }

        public void Write(string text, int x, int y)
        {
            for (var index = 0; index < text.Length; index++)
            {
                var character = text[index];
                this.Write(character, x + index, y);
            }
        }
    }
}