namespace NativeCode.Core.DotNet.Console
{
    using System;

    public struct BufferCell
    {
        public BufferCell(int x, int y)
            : this(x, y, default(char), default(ConsoleColor), default(ConsoleColor))
        {
        }

        public BufferCell(int x, int y, char data)
            : this(x, y, data, default(ConsoleColor), default(ConsoleColor))
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

        public ConsoleColor Background { get; set; }

        public char Data { get; set; }

        public ConsoleColor Foreground { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}