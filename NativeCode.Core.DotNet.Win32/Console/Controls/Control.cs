namespace NativeCode.Core.DotNet.Win32.Console.Controls
{
    public abstract class Control
    {
        public int Height { get; protected set; }

        public string Id { get; set; }

        public int Left { get; protected set; }

        public Control Parent { get; protected set; }

        public int Top { get; protected set; }

        public int Width { get; protected set; }
    }
}