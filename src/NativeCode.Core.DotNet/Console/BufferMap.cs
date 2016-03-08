namespace NativeCode.Core.DotNet.Console
{
    using NativeCode.Core.DotNet.Console.Win32;
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

        public BufferCell this[int x, int y]
        {
            get { return this.cells[this.Index(x, y)]; }
            set { this.cells[this.Index(x, y)] = value; }
        }

        public int Height { get; }

        public int Width { get; }

        public int Index(int x, int y)
        {
            return x + y * this.Width;
        }

        public void Write(char data, int x, int y)
        {
            this.cells[x + y * this.Height] = new BufferCell(x, y, data);
        }

        public CharInfo[] GetBuffer(int left, int top, int right, int bottom)
        {
            var height = bottom - top;
            var width = right - left;
            var buffer = new CharInfo[height * width];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = this[x, y];
                    buffer[x + y * width] = new CharInfo(cell.Data.GetValueOrDefault(), cell.Background, cell.Foreground);
                }
            }

            return buffer;
        }
    }
}