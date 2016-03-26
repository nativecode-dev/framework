namespace NativeCode.Core.Types.Structs
{
    public struct Position
    {
        public static readonly Position Origin = new Position(0, 0);

        public Position(int x, int y, int z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public int X { get; }

        public int Y { get; }

        public int Z { get; }
    }
}