namespace NativeCode.Core.DotNet.Win32.Console
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using Microsoft.Win32.SafeHandles;

    using NativeCode.Core.DotNet.Win32;
    using NativeCode.Core.DotNet.Win32.Enums;
    using NativeCode.Core.DotNet.Win32.Exceptions;
    using NativeCode.Core.DotNet.Win32.Structs;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Types;

    public abstract class Renderer<TContext> : Disposable
        where TContext : RenderContext, new()
    {
        private const EFileAccess DefaultFileAccess = EFileAccess.GenericRead | EFileAccess.GenericWrite;

        private const EFileShare DefaultFileShare = EFileShare.Read | EFileShare.Write;

        private readonly TContext context = new TContext();

        protected Renderer(RendererOptions settings)
        {
            this.Settings = settings;

            this.ConfigureBufferHandles();
            this.ConfigureKeyMapper();
            this.SetMode(RenderMode.Display, true);
        }

        public BufferHandle ActiveBuffer { get; private set; }

        public BufferHandle BackBuffer { get; private set; }

        public int CursorX { get; private set; }

        public int CursorY { get; private set; }

        public RendererKeyMapper KeyMapper { get; } = new RendererKeyMapper();

        public RenderMode Mode { get; private set; }

        public RendererOptions Settings { get; }

        public void Execute(ConsoleKeyInfo key)
        {
            var handler = this.KeyMapper.GetMapping(key, this.Mode);

            if (handler == null)
            {
                this.HandleConsoleKey(key, this.Mode);
            }
            else
            {
                handler();
            }
        }

        public void Render()
        {
            var buffer = this.ActiveBuffer;

            try
            {
                if (this.Mode == RenderMode.Rendering)
                {
                    buffer = this.BackBuffer;
                }

                this.context.LastRenderStart = DateTimeOffset.UtcNow;
                this.RenderSetup(this.context);
                this.RenderView(this.context, buffer);

                if (this.Mode == RenderMode.Rendering && this.context.IsDirty)
                {
                    this.Flip();
                }
            }
            catch (Exception ex)
            {
                this.context.Exceptions.Add(ex);
                Debug.WriteLine(ex.Stringify());
            }
            finally
            {
                this.RenderComplete(this.context);
                this.context.LastRenderStop = DateTimeOffset.UtcNow;
            }
        }

        public void SetMode(RenderMode mode, bool force = false)
        {
            if (this.Mode != mode || force)
            {
                this.Mode = mode;

                switch (mode)
                {
                    case RenderMode.Editor:
                        this.EnterEditorMode();
                        break;

                    case RenderMode.Rendering:
                        break;

                    default:
                        this.EnterDisplayMode();
                        break;
                }
            }
        }

        protected void ApplySettings(BufferHandle buffer)
        {
            var info = new ConsoleCursorInfo { Size = (uint)this.Settings.CursorSize, Visible = this.Settings.CursorVisible };

            if (NativeMethods.SetConsoleCursorInfo(buffer.Handle, ref info) == false)
            {
                Debug.WriteLine($"Failed to update cursor settings, error was {Marshal.GetLastWin32Error()}.");
            }
        }

        protected virtual void CursorDown()
        {
            if (this.CursorY < this.Settings.ScreenHeight - 1)
            {
                this.CursorY++;
                this.UpdateCursorPosition();
            }
        }

        protected virtual void CursorLeft()
        {
            if (this.CursorX > 0)
            {
                this.CursorX--;
                this.UpdateCursorPosition();
            }
        }

        protected virtual void CursorRight()
        {
            if (this.CursorX < this.Settings.ScreenWidth - 1)
            {
                this.CursorX++;
                this.UpdateCursorPosition();
            }
        }

        protected virtual void CursorUp()
        {
            if (this.CursorY > 0)
            {
                this.CursorY--;
                this.UpdateCursorPosition();
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

        protected void Flip()
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

        protected virtual void HandleConsoleKey(ConsoleKeyInfo key, RenderMode mode)
        {
            if (mode == RenderMode.Editor)
            {
                this.Write(key);
            }
        }

        protected abstract void RenderComplete(TContext context);

        protected abstract void RenderSetup(TContext context);

        protected abstract void RenderView(TContext context, BufferHandle buffer);

        protected void UpdateCursorPosition()
        {
            var position = new Coord(this.CursorX, this.CursorY);

            if (NativeMethods.SetConsoleCursorPosition(this.ActiveBuffer.Handle, position) == false)
            {
                throw new NativeMethodException(Marshal.GetLastWin32Error());
            }
        }

        protected virtual void Write(ConsoleKeyInfo key)
        {
            this.ActiveBuffer.Write(key.KeyChar, true);
        }

        private void ConfigureBufferHandles()
        {
            var handle = NativeMethods.GetStandardOutputHandle();

            this.ActiveBuffer = this.CreateBuffer(handle, false);
            this.BackBuffer = this.CreateBuffer();
        }

        private void ConfigureKeyMapper()
        {
            // Display actions
            this.KeyMapper.Register("Display.Help", ConsoleKey.F1, RenderMode.Display, this.EditScreen);

            // General actions
            this.KeyMapper.Register("Any.CursorLeft", ConsoleKey.LeftArrow, RenderMode.Any, this.CursorLeft);
            this.KeyMapper.Register("Any.CursorRight", ConsoleKey.RightArrow, RenderMode.Any, this.CursorRight);
            this.KeyMapper.Register("Any.CursorUp", ConsoleKey.UpArrow, RenderMode.Any, this.CursorUp);
            this.KeyMapper.Register("Any.CursorDown", ConsoleKey.DownArrow, RenderMode.Any, this.CursorDown);
            this.KeyMapper.Register("Any.Edit", ConsoleKey.F10, RenderMode.Any, this.EditScreen);

            // Editor actions
            this.KeyMapper.Register("Editor.Cancel", ConsoleKey.Escape, RenderMode.Editor, () => this.SetMode(RenderMode.Display));
            this.KeyMapper.Register("Editor.Save", ConsoleKey.W, RenderMode.Editor, this.EnterDisplayMode, control: true);
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
            this.SetMode(RenderMode.Editor);
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
    }
}