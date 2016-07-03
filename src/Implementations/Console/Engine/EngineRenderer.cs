namespace Console.Engine
{
    using Console.Engine.Mapping;
    using Console.Engine.Objects;
    using NativeCode.Core.DotNet.Console;
    using NativeCode.Core.DotNet.Win32;
    using NativeCode.Core.DotNet.Win32.Exceptions;
    using NativeCode.Core.DotNet.Win32.Structs;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Types.Structs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class EngineRenderer : Renderer<EngineRenderContext>
    {
        public EngineRenderer(EngineContext context, int width, int height)
            : base(CreateDefaultSettings(width, height))
        {
            this.Context = context;
        }

        protected EngineContext Context { get; }

        protected MapFile Map { get; private set; }

        protected int MapIndex { get; private set; }

        protected int ViewPositionX { get; private set; }

        protected int ViewPositionY { get; private set; }

        public void ChangeMap(MapFile map, int index = 0)
        {
            this.Map = map;
            this.MapIndex = index;
            this.ViewPositionX = this.Map[this.MapIndex].Start.X;
            this.ViewPositionY = this.Map[this.MapIndex].Start.Y;
        }

        protected override void CursorDown()
        {
            var movable = this.IsMapBounded().Bottom == false;

            if (this.Mode != RenderMode.Editor && movable)
            {
                this.ViewPositionY++;
            }
            else
            {
                base.CursorDown();
            }
        }

        protected override void CursorLeft()
        {
            var movable = this.IsMapBounded().Left == false;

            if (this.Mode != RenderMode.Editor && movable)
            {
                this.ViewPositionX--;
            }
            else
            {
                base.CursorLeft();
            }
        }

        protected override void CursorRight()
        {
            var movable = this.IsMapBounded().Right == false;

            if (this.Mode != RenderMode.Editor && movable)
            {
                this.ViewPositionX++;
            }
            else
            {
                base.CursorRight();
            }
        }

        protected override void CursorUp()
        {
            var movable = this.IsMapBounded().Top == false;

            if (this.Mode != RenderMode.Editor && movable)
            {
                this.ViewPositionY--;
            }
            else
            {
                base.CursorUp();
            }
        }

        protected override void HandleConsoleKey(ConsoleKeyInfo key, RenderMode mode)
        {
            base.HandleConsoleKey(key, mode);

            if (mode == RenderMode.Editor)
            {
                var position = this.ActiveBuffer.Position;
                this.Map.WriteCells(this.MapIndex, position, 1);
            }
        }

        protected override void RenderComplete(EngineRenderContext context)
        {
        }

        protected override void RenderSetup(EngineRenderContext context)
        {
            const int KeyEventType = 1;
            context.IsDirty = context.LastViewPositionX != this.ViewPositionX || context.LastViewPositionY != this.ViewPositionY;
            context.LastViewPositionX = this.ViewPositionX;
            context.LastViewPositionY = this.ViewPositionY;

            var handle = NativeHelper.GetStandardInputHandle();

            int count;
            InputRecord record;

            if (NativeHelper.ReadConsoleInput(handle, out record, 1, out count) == false)
            {
                throw new NativeMethodException(Marshal.GetLastWin32Error());
            }

            if (count == 1 && record.EventType == KeyEventType)
            {
                var state = record.KeyEvent.ControlKeyState;
                var shift = (state & ControlKeyState.ShiftPressed) != 0;
                var alt = (state & (ControlKeyState.LeftAltPressed | ControlKeyState.RightAltPressed)) != 0;
                var control = (state & (ControlKeyState.LeftCtrlPressed | ControlKeyState.RightCtrlPressed)) != 0;

                var consoleKeyInfo = new ConsoleKeyInfo(record.KeyEvent.UnicodeChar, (ConsoleKey)record.KeyEvent.VirtualKeyCode, shift, alt, control);

                this.Execute(consoleKeyInfo);
            }
        }

        protected override void RenderView(EngineRenderContext context, BufferHandle buffer)
        {
            if (context.IsDirty)
            {
                var height = this.Settings.ScreenHeight;
                var width = this.Settings.ScreenWidth;

                var bounds = new Bounds(this.ViewPositionX, this.ViewPositionY, height, width);

                var cells = this.Map.GetRegion(this.MapIndex, bounds);
                var rect = new SmallRect(0, 0, width - 1, height - 1);
                var origin = new Coord(0, 0);
                var size = new Coord(width, height);

                this.RenderGameObjects(cells, bounds);

                if (NativeHelper.WriteConsoleOutput(buffer.Handle, cells, size, origin, ref rect) == false)
                {
                    throw new NativeMethodException(Marshal.GetLastWin32Error());
                }
            }
        }

        private static RendererOptions CreateDefaultSettings(int width, int height)
        {
            return new RendererOptions { CursorVisible = false, ScreenHeight = height, ScreenWidth = width };
        }

        private BoundsClamp IsMapBounded()
        {
            var height = this.Settings.ScreenHeight;
            var width = this.Settings.ScreenWidth;

            var mapHeight = this.Map.MapFileHeader.MapHeight;
            var mapWidth = this.Map.MapFileHeader.MapWidth;

            var right = this.ViewPositionX + width == mapWidth;
            var left = this.ViewPositionX == 0;
            var top = this.ViewPositionY == 0;
            var bottom = this.ViewPositionY + height == mapHeight;

            return new BoundsClamp { Left = left, Top = top, Bottom = bottom, Right = right };
        }

        private void RenderGameObjects(CharInfo[] cells, Bounds bounds)
        {
            var objects = this.UpdateGameObjects(bounds);
            this.UpdateMapCells(objects, cells);
        }

        private IEnumerable<IEngineObjectElement> UpdateGameObjects(Bounds bounds)
        {
            return Enumerable.Empty<IEngineObjectElement>();
        }

        private void UpdateMapCells(IEnumerable<IEngineObjectElement> objects, CharInfo[] cells)
        {
        }
    }
}
