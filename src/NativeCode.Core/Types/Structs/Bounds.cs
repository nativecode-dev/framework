namespace NativeCode.Core.Types.Structs
{
    public struct Bounds
    {
        public Bounds(int height, int width)
            : this(0, 0, height, width)
        {
        }

        public Bounds(int left, int top, int height, int width)
        {
            this.Height = height;
            this.Left = left;
            this.Top = top;
            this.Width = width;
        }

        public int Height { get; private set; }

        public int Left { get; private set; }

        public int Top { get; private set; }

        public int Width { get; private set; }
    }
}