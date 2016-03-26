namespace Console.Engine.Mapping
{
    public class MapReader
    {
        public MapReader(MapFile map, MapLayer layer)
        {
            this.Layer = layer;
            this.Map = map;
        }

        protected MapLayer Layer { get; }

        protected MapFile Map { get; }

        public bool Reset()
        {
            return this.Map.PositionAtMapCells(this.Layer.Name);
        }

        private long GetCellPosition(int x, int y)
        {
            var index = x * this.Map.MapFileHeader.MapWidth + y;
            var position = index * MapFile.MapCellSize;

            return position;
        }
    }
}