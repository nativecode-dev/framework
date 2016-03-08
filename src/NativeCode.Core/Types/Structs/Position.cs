namespace NativeCode.Core.Types.Structs
{
    public struct Position
    {
        public static readonly Position Origin = new Position(0, 0);

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }
    }
}