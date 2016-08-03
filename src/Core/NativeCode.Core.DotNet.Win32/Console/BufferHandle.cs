namespace NativeCode.Core.DotNet.Win32.Console
{
    using System;
    using System.Runtime.InteropServices;

    using Microsoft.Win32.SafeHandles;

    using NativeCode.Core.DotNet.Win32.Exceptions;
    using NativeCode.Core.DotNet.Win32.Structs;
    using NativeCode.Core.Types;
    using NativeCode.Core.Types.Structs;

    public class BufferHandle : Disposable
    {
        public BufferHandle(RendererOptions settings, SafeFileHandle handle)
        {
            this.Handle = handle;
            this.OwnHandle = true;
            this.Settings = settings;

            this.ConfigureBufferHandle();
        }

        public BufferHandle(RendererOptions settings, SafeFileHandle handle, bool ownHandle)
        {
            this.Handle = handle;
            this.OwnHandle = ownHandle;
            this.Settings = settings;

            this.ConfigureBufferHandle();
        }

        public SafeFileHandle Handle { get; private set; }

        public Position Position => new Position(Console.CursorLeft, Console.CursorTop);

        protected bool OwnHandle { get; }

        protected RendererOptions Settings { get; }

        public void MoveCursorDown()
        {
            var info = this.GetScreenBufferInfo();

            if (info.CursorPosition.Y < info.Window.Bottom)
            {
                Console.CursorTop++;
            }
        }

        public void MoveCursorLeft()
        {
            var info = this.GetScreenBufferInfo();

            if (info.CursorPosition.X > info.Window.Left)
            {
                Console.CursorLeft--;
            }
        }

        public void MoveCursorRight()
        {
            var info = this.GetScreenBufferInfo();

            if (info.CursorPosition.X < info.Window.Right)
            {
                Console.CursorLeft++;
            }
        }

        public void MoveCursorUp()
        {
            var info = this.GetScreenBufferInfo();

            if (info.CursorPosition.Y > info.Window.Top)
            {
                Console.CursorTop--;
            }
        }

        public void Write(char character)
        {
            this.Write(character.ToString(), false, Color.ForegroundGreen);
        }

        public void Write(char character, bool moveCursor)
        {
            this.Write(character.ToString(), moveCursor, Color.ForegroundGreen);
        }

        public void Write(string text, bool moveCursor, Color color)
        {
            var info = this.GetScreenBufferInfo();
            var rect = new SmallRect(info.CursorPosition.X, info.CursorPosition.Y, info.CursorPosition.X + text.Length, info.CursorPosition.Y + 1);
            var size = new Coord(text.Length, 1);
            var origin = new Coord(0, 0);

            var buffer = new CharInfo[text.Length];

            for (var index = 0; index < buffer.Length; index++)
            {
                buffer[index].Color = color;
                buffer[index].Char.UnicodeChar = text[index];
            }

            if (NativeMethods.WriteConsoleOutput(this.Handle, buffer, size, origin, ref rect) == false)
            {
                throw new NativeMethodException(Marshal.GetLastWin32Error());
            }

            if (moveCursor && info.CursorPosition.X < info.Window.Right)
            {
                Console.SetCursorPosition(info.CursorPosition.X + 1, info.CursorPosition.Y);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed && this.OwnHandle && this.Handle != null)
            {
                this.Handle.Dispose();
                this.Handle = null;
            }

            base.Dispose(disposing);
        }

        protected ConsoleScreenBufferInfo GetScreenBufferInfo()
        {
            ConsoleScreenBufferInfo info;

            if (NativeMethods.GetConsoleScreenBufferInfo(this.Handle, out info) == false)
            {
                throw new NativeMethodException(Marshal.GetLastWin32Error());
            }

            return info;
        }

        private void ConfigureBuffer()
        {
            Console.SetBufferSize(this.Settings.ScreenWidth, this.Settings.ScreenHeight);

            var boundedHeight = this.Settings.ScreenHeight <= Console.LargestWindowHeight;
            var boundedWidth = this.Settings.ScreenWidth <= Console.LargestWindowWidth;

            if (boundedHeight && boundedWidth)
            {
                Console.SetWindowSize(this.Settings.ScreenWidth, this.Settings.ScreenHeight);
            }
            else if (boundedHeight)
            {
                Console.SetWindowSize(Console.LargestWindowWidth, this.Settings.ScreenHeight);
            }
            else if (boundedWidth)
            {
                Console.SetWindowSize(this.Settings.ScreenWidth, Console.LargestWindowHeight);
            }
        }

        private void ConfigureBufferHandle()
        {
            this.ConfigureFont();
            this.ConfigureBuffer();
        }

        private unsafe void ConfigureFont()
        {
            var current = new ConsoleFontInfoEx { Size = (uint)Marshal.SizeOf<ConsoleFontInfoEx>() };
            var updated = new ConsoleFontInfoEx
                              {
                                  FontFamily = 0,
                                  FontSize = new Coord(12, 12),
                                  FontWeight = 0,
                                  Size = (uint)Marshal.SizeOf<ConsoleFontInfoEx>()
                              };

            var pointer = new IntPtr(updated.FaceName);
            Marshal.Copy(this.Settings.FontName.ToCharArray(), 0, pointer, this.Settings.FontName.Length);

            updated.FontSize = new Coord(current.FontSize.X, current.FontSize.Y);
            updated.FontWeight = current.FontWeight;

            if (NativeMethods.SetCurrentConsoleFontEx(this.Handle, true, ref updated) == false)
            {
                throw new NativeMethodException(Marshal.GetLastWin32Error());
            }
        }
    }
}