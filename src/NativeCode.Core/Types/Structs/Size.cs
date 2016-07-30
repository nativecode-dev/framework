namespace NativeCode.Core.Types.Structs
{
    public struct Size
    {
        public Size(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }

        public int Height { get; }

        public int Width { get; }
    }
}