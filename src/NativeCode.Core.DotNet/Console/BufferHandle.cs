namespace NativeCode.Core.DotNet.Console
{
    using System;

    using NativeCode.Core.DotNet.Console.Win32;
    using NativeCode.Core.Types.Structs;

    public class BufferHandle
    {
        public BufferHandle(IntPtr handle)
        {
            this.Handle = handle;
            this.InvalidateState();
        }

        public ConsoleColor Background => ConsoleColor.Gray;

        public ConsoleColor Foreground => ConsoleColor.Cyan;

        public IntPtr Handle { get; }

        public Position Position { get; private set; }

        public Size Size { get; private set; }

        public Size SizeVisible { get; private set; }

        public int CursorSize { get; private set; }

        public bool CursorVisible { get; private set; }

        protected BufferMap Map { get; private set; }

        public bool HideCursor()
        {
            var cursor = new ConsoleCursorInfo { DwSize = Convert.ToUInt32(this.CursorSize), Visible = false };

            return NativeMethods.SetConsoleCursorInfo(this.Handle, ref cursor);
        }

        public bool SetCursorPosition(int left, int top)
        {
            return NativeMethods.SetConsoleCursorPosition(this.Handle, left, top);
        }

        public bool ShowCursor()
        {
            var cursor = new ConsoleCursorInfo { DwSize = Convert.ToUInt32(this.CursorSize), Visible = true };

            return NativeMethods.SetConsoleCursorInfo(this.Handle, ref cursor);
        }

        public bool Write(char value, ConsoleColor? background = null, ConsoleColor? foreground = null)
        {
            this.InvalidateState();

            if (this.WriteAt(this.Position, value, background ?? this.Background, foreground ?? this.Foreground))
            {
                this.SetCursorPosition(this.Position.Y + 1, this.Position.X);
                return true;
            }

            return false;
        }

        public bool Write(string text, ConsoleColor? background = null, ConsoleColor? foreground = null)
        {
            this.InvalidateState();

            if (this.WriteAt(this.Position, text, background ?? this.Background, foreground ?? this.Foreground))
            {
                this.SetCursorPosition(this.Position.Y + text.Length, this.Position.X);
                return true;
            }

            return false;
        }

        public bool WriteAt(Position position, char data, ConsoleColor background, ConsoleColor foreground)
        {
            return this.WriteAt(position.X, position.Y, data, background, foreground);
        }

        public bool WriteAt(Position position, string data, ConsoleColor background, ConsoleColor foreground)
        {
            return this.WriteAt(position.X, position.Y, data, background, foreground);
        }

        public bool WriteAt(int left, int top, char data, ConsoleColor background, ConsoleColor foreground)
        {
            var rect = new SmallRect(left, top, 1, 1);
            var size = new Coord(1, 1);

            return NativeMethods.WriteConsoleOutput(this.Handle, data, size, new Coord(left, top), background, foreground, ref rect);
        }

        public bool WriteAt(int left, int top, string data, ConsoleColor background, ConsoleColor foreground)
        {
            for (var index = 0; index < data.Length; index++)
            {
                this.Map[left + index, top] = new BufferCell(left + index, top, data[index], background, foreground);
            }

            var buffer = this.Map.GetBuffer(left, top, left + data.Length, 1);

            var rect = new SmallRect(left, top, data.Length, 1);
            var size = new Coord(data.Length, 1);

            return NativeMethods.WriteConsoleOutput(this.Handle, buffer, size, new Coord(left, top), ref rect);
        }

        protected void InvalidateState()
        {
            ConsoleScreenBufferInfo info;

            if (NativeMethods.GetConsoleScreenBufferInfo(this.Handle, out info))
            {
                var left = info.DwCursorPosition.X;
                var top = info.DwCursorPosition.Y;

                this.Position = new Position(left, top);
                this.Size = new Size(info.DwSize.Y, info.DwSize.X);
                this.SizeVisible = new Size(info.DwMaximumWindowSize.Y, info.DwMaximumWindowSize.X);

                if (this.Map == null)
                {
                    this.Map = new BufferMap(this.Size);
                }

                ConsoleCursorInfo cursor;

                if (NativeMethods.GetConsoleCursorInfo(this.Handle, out cursor))
                {
                    this.CursorSize = Convert.ToInt32(cursor.DwSize);
                    this.CursorVisible = cursor.Visible;
                }
            }
        }
    }
}