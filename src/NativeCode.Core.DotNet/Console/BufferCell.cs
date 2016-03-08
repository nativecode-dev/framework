namespace NativeCode.Core.DotNet.Console
{
    using System;

    public struct BufferCell
    {
        public BufferCell(int x, int y) : this(x, y, default(char), default(ConsoleColor), default(ConsoleColor))
        {
        }

        public BufferCell(int x, int y, char data) : this(x, y, data, default(ConsoleColor), default(ConsoleColor))
        {
        }

        public BufferCell(int x, int y, char data, ConsoleColor background, ConsoleColor foreground)
        {
            this.Background = background;
            this.Data = data;
            this.Foreground = foreground;
            this.X = x;
            this.Y = y;
        }

        public ConsoleColor Background { get; private set; }

        public char? Data { get; set; }

        public ConsoleColor Foreground { get; private set; }

        public int X { get; }

        public int Y { get; }
    }
}