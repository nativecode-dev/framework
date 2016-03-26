namespace Console.Engine.Mapping
{
    using System.Runtime.InteropServices;

    using NativeCode.Core.DotNet.Win32.Structs;
    using NativeCode.Core.Types.Structs;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    public struct MapCell
    {
        public CharInfo Data;

        public byte Elevation;

        public Position Position;

        public MapCellState State;

        public MapCellType Type;
    }

    public enum MapCellType
    {
        Default = 0,

        Structure = 1
    }
}