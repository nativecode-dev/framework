namespace Console.Engine.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NativeCode.Core.DotNet.Win32.Structs;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Types;
    using NativeCode.Core.Types.Structs;

    public class MapFile : Disposable
    {
        internal static readonly int MapFileHeaderSize = StructExtensions.GetSize<MapFileHeader>();

        internal static readonly int MapHeaderSize = StructExtensions.GetSize<MapLayer>();

        internal static readonly int MapCellSize = StructExtensions.GetSize<MapCell>();

        private readonly Dictionary<string, MapLayer> maps = new Dictionary<string, MapLayer>();

        private readonly Action<long, long> progress;

        public MapFile(Stream stream, bool owner = true, Action<long, long> progress = null)
        {
            this.progress = progress;

            this.Stream = stream;
            this.StreamOwner = owner;

            this.InitializeMap();
        }

        public MapFile(Stream stream, string name, int width, int height, bool owner = true, Action<long, long> progress = null)
        {
            this.progress = progress;

            this.Stream = stream;
            this.StreamOwner = owner;

            this.InitializeMap(name, width, height);
        }

        public MapLayer this[int index]
        {
            get
            {
                var key = this.maps.Keys.ElementAt(index);
                return this.maps[key];
            }
        }

        public MapFileHeader MapFileHeader { get; private set; }

        protected Stream Stream { get; private set; }

        protected bool StreamOwner { get; }

        public void CreateMap(string name, int width, int height)
        {
            this.WriteMapFileHeader(name, width, height);
        }

        public CharInfo[] GetRegion(string name, Bounds bounds)
        {
            var cells = new CharInfo[bounds.Height * bounds.Width];
            var height = bounds.Height;
            var width = bounds.Width;

            if (this.PositionAtMapCells(name))
            {
                var header = this.maps[name];

                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        var position = this.GetCellPosition(x + bounds.Left, y + bounds.Top);
                        this.Stream.Position = header.Position + MapHeaderSize + position;

                        var cell = this.ReadMapCell();
                        cells[x + bounds.Width * y].Color = cell.Data.Color;
                        cells[x + bounds.Width * y].Char.AsciiChar = cell.Data.Char.AsciiChar;
                        cells[x + bounds.Width * y].Char.UnicodeChar = cell.Data.Char.UnicodeChar;
                    }
                }
            }

            return cells;
        }

        public CharInfo[] GetRegion(int index, Bounds bounds)
        {
            var name = this.maps.Keys.ElementAt(index);
            return this.GetRegion(name, bounds);
        }

        public void CreateMapHeader(string name)
        {
            this.WriteMapHeader(name, this.Stream.Position);
        }

        public bool PositionAtMapCells(string name)
        {
            if (this.maps.ContainsKey(name))
            {
                this.Stream.Position = this.maps[name].Position + MapHeaderSize;
                return true;
            }

            return false;
        }

        public bool PositionAtMapCells(int index)
        {
            var name = this.maps.Keys.ElementAt(index);
            return this.PositionAtMapCells(name);
        }

        public void Reset()
        {
            this.Stream.Position = MapFileHeaderSize;
        }

        public void WriteCells(string name, Position position, int count)
        {
            this.PositionAtMapCells(name);
            var index = position.X + this.MapFileHeader.MapWidth * position.Y;

            // TODO: Incomplete
        }

        public void WriteCells(int index, Position position, int count)
        {
            var name = this.maps.Keys.ElementAt(index);
            this.WriteCells(name, position, count);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                if (this.StreamOwner && this.Stream != null)
                {
                    this.Stream.Flush();
                    this.Stream.Dispose();
                }

                this.Stream = null;
            }

            base.Dispose(disposing);
        }

        protected long GetCellPosition(int x, int y)
        {
            var index = x + this.MapFileHeader.MapWidth * y;
            var position = index * MapCellSize;

            return position;
        }

        private void CreateMapCells()
        {
            var colors = new[]
                             {
                                 Color.BackgroundBlue | Color.ForegroundBlue,
                                 Color.BackgroundGreen | Color.ForegroundGreen,
                                 Color.BackgroundRed | Color.ForegroundRed,
                                 Color.BackgroundYellow | Color.ForegroundYellow
                             };

            var size = this.MapFileHeader.MapHeight * this.MapFileHeader.MapWidth;
            var random = new Random();

            for (var y = 0; y < this.MapFileHeader.MapHeight; y++)
            {
                for (var x = 0; x < this.MapFileHeader.MapWidth; x++)
                {
                    var color = colors[random.Next(0, colors.Length - 1)];

                    var cell = new MapCell
                                   {
                                       Data = { Color = color, Char = { UnicodeChar = '\u0020' } },
                                       Position = new Position(x, y),
                                       State = MapCellState.None,
                                       Type = MapCellType.Default
                                   };

                    this.WriteMapCell(cell);
                }

                this.progress?.Invoke(this.MapFileHeader.MapWidth * y, size);
            }
        }

        private void InitializeMap(string name, int width, int height)
        {
            this.Stream.Position = 0;

            this.CreateMap(name, width, height);
            this.CreateMapHeader(name);
            this.CreateMapCells();
        }

        private void InitializeMap()
        {
            this.ReadMapHeader();
            this.ReadMapLayers();
        }

        private MapCell ReadMapCell()
        {
            var buffer = new byte[MapCellSize];
            this.Read(buffer);

            return StructExtensions.FromBytes<MapCell>(buffer);
        }

        private void ReadMapHeader()
        {
            var buffer = new byte[MapFileHeaderSize];

            this.Stream.Position = 0;
            this.Read(buffer);

            this.MapFileHeader = StructExtensions.FromBytes<MapFileHeader>(buffer);
        }

        private void ReadMapHeader(long position)
        {
            var buffer = new byte[MapHeaderSize];

            this.Stream.Position = position;
            this.Read(buffer);

            var map = StructExtensions.FromBytes<MapLayer>(buffer);

            if (this.maps.ContainsKey(map.Name) == false)
            {
                this.maps.Add(map.Name, map);
            }

            // Forward the stream to the next map layer.
            this.Stream.Position += (this.MapFileHeader.MapHeight * this.MapFileHeader.MapWidth) * MapCellSize;
        }

        private void ReadMapLayers()
        {
            this.Stream.Position = MapFileHeaderSize;

            while (this.Stream.Position < this.Stream.Length)
            {
                this.ReadMapHeader(this.Stream.Position);
            }
        }

        private void WriteMapFileHeader(string name, int width, int height)
        {
            this.MapFileHeader = new MapFileHeader { MapHeight = (uint)height, MapWidth = (uint)width, Name = name, Version = 1 };
            var buffer = this.MapFileHeader.GetBytes();

            this.Stream.Position = 0;
            this.Write(buffer);
        }

        private void WriteMapHeader(string name, long position)
        {
            var x = (int)this.MapFileHeader.MapWidth / 2;
            var y = (int)this.MapFileHeader.MapHeight / 2;
            var start = new Position(x, y);
            var header = new MapLayer { Name = name, Position = position, Start = start };

            if (this.maps.ContainsKey(name))
            {
                header = this.maps[name];
            }
            else
            {
                this.maps.Add(name, header);
            }

            var buffer = header.GetBytes();

            this.Stream.Position = header.Position;
            this.Write(buffer);
        }

        private void WriteMapCell(MapCell cell)
        {
            var buffer = cell.GetBytes();
            this.Write(buffer);
        }

        private void Read(byte[] buffer)
        {
            this.Stream.Read(buffer, 0, buffer.Length);
        }

        private void Write(byte[] buffer)
        {
            this.Stream.Write(buffer, 0, buffer.Length);
        }
    }
}