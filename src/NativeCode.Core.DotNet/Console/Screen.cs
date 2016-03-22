namespace NativeCode.Core.DotNet.Console
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using Microsoft.Win32.SafeHandles;

    using NativeCode.Core.DotNet.Win32;
    using NativeCode.Core.DotNet.Win32.Enums;
    using NativeCode.Core.DotNet.Win32.Structs;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Types;

    public class Screen : Disposable
    {
        private const EFileAccess DefaultFileAccess = EFileAccess.GenericRead | EFileAccess.GenericWrite;

        private const EFileShare DefaultFileShare = EFileShare.Read | EFileShare.Write;

        public Screen(ScreenSettings settings)
        {
            this.Settings = settings;

            this.ConfigureBufferHandles();
            this.ConfigureKeyMapper();
            this.SetMode(this.Mode, true);
        }

        public BufferHandle ActiveBuffer { get; private set; }

        public BufferHandle BackBuffer { get; private set; }

        public ScreenMode Mode { get; private set; }

        public ScreenSettings Settings { get; }

        protected bool PendingExit { get; private set; }

        public void Flip()
        {
            this.ApplySettings(this.BackBuffer);

            var active = this.ActiveBuffer;
            var back = this.BackBuffer;

            if (NativeMethods.SetConsoleActiveScreenBuffer(back.Handle))
            {
                this.ActiveBuffer = back;
                this.BackBuffer = active;
            }
        }

        public void SetMode(ScreenMode mode, bool force = false)
        {
            if (this.Mode != mode || force)
            {
                this.Mode = mode;

                switch (mode)
                {
                    case ScreenMode.Editor:
                        this.EnterEditorMode();
                        break;

                    case ScreenMode.Rendering:
                        break;

                    default:
                        this.EnterDisplayMode();
                        break;
                }
            }
        }

        public void Start()
        {
            while (this.PendingExit.Not())
            {
                var key = Console.ReadKey(true);
                var handler = this.Settings.KeyMapper.GetMapping(key, this.Mode);

                if (handler != null)
                {
                    handler();
                }
                else
                {
                    this.Write(key);
                }
            }
        }

        public void Stop()
        {
            this.PendingExit = true;
        }

        protected void ApplySettings(BufferHandle buffer)
        {
            var info = new ConsoleCursorInfo { DwSize = Convert.ToUInt32(this.Settings.CursorSize), Visible = this.Settings.CursorVisible };

            if (NativeMethods.SetConsoleCursorInfo(buffer.Handle, ref info).Not())
            {
                Debug.WriteLine($"Failed to update cursor settings, error was {Marshal.GetLastWin32Error()}.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                if (this.ActiveBuffer != null)
                {
                    this.ActiveBuffer.Dispose();
                    this.ActiveBuffer = null;
                }

                if (this.BackBuffer != null)
                {
                    this.BackBuffer.Dispose();
                    this.BackBuffer = null;
                }
            }

            base.Dispose(disposing);
        }

        private void ConfigureBufferHandles()
        {
            var handle = NativeMethods.GetStandardOutputHandle();

            this.ActiveBuffer = this.CreateBuffer(handle, false);
            this.BackBuffer = this.CreateBuffer();
        }

        private void ConfigureKeyMapper()
        {
            this.Settings.KeyMapper.Register(
                "Display.Edit",
                ScreenMode.Display,
                new ConsoleKeyInfo((char)0, ConsoleKey.F10, false, false, false),
                this.EditScreen);

            this.Settings.KeyMapper.Register(
                "Editor.MoveCursorLeft",
                ScreenMode.Editor,
                new ConsoleKeyInfo((char)0, ConsoleKey.LeftArrow, false, false, false),
                this.ActiveBuffer.MoveCursorLeft);

            this.Settings.KeyMapper.Register(
                "Editor.MoveCursorRight",
                ScreenMode.Editor,
                new ConsoleKeyInfo((char)0, ConsoleKey.RightArrow, false, false, false),
                this.ActiveBuffer.MoveCursorRight);

            this.Settings.KeyMapper.Register(
                "Editor.MoveCursorTop",
                ScreenMode.Editor,
                new ConsoleKeyInfo((char)0, ConsoleKey.UpArrow, false, false, false),
                this.ActiveBuffer.MoveCursorUp);

            this.Settings.KeyMapper.Register(
                "Editor.MoveCursorDown",
                ScreenMode.Editor,
                new ConsoleKeyInfo((char)0, ConsoleKey.DownArrow, false, false, false),
                this.ActiveBuffer.MoveCursorDown);
        }

        private BufferHandle CreateBuffer()
        {
            var handle = NativeMethods.CreateConsoleScreenBuffer(DefaultFileAccess, DefaultFileShare, IntPtr.Zero, 1, IntPtr.Zero);
            var safehandle = new SafeFileHandle(handle, true);

            try
            {
                return this.CreateBuffer(safehandle);
            }
            catch
            {
                safehandle.Dispose();
                throw;
            }
        }

        private BufferHandle CreateBuffer(SafeFileHandle handle, bool ownHandle = true)
        {
            return new BufferHandle(this.Settings, handle, ownHandle);
        }

        private void EditScreen()
        {
            this.SetMode(ScreenMode.Editor);
        }

        private void EnterDisplayMode()
        {
            this.Settings.CursorVisible = false;
            this.ApplySettings(this.ActiveBuffer);
        }

        private void EnterEditorMode()
        {
            this.Settings.CursorVisible = true;
            this.ApplySettings(this.ActiveBuffer);
        }

        private void Write(ConsoleKeyInfo key)
        {
            this.ActiveBuffer.Write(key.KeyChar, true);
        }
    }
}