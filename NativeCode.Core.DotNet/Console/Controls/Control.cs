namespace NativeCode.Core.DotNet.Console.Controls
{
    public abstract class Control
    {
        public string Id { get; set; }

        public Control Parent { get; protected set; }

        public int Height { get; protected set; }

        public int Left { get; protected set; }

        public int Top { get; protected set; }

        public int Width { get; protected set; }
    }
}