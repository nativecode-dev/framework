namespace Console.Engine.Mapping
{
    using System.Runtime.InteropServices;

    using NativeCode.Core.Types.Structs;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    public struct MapLayer
    {
        public byte Elevation;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Name;

        public long Position;

        public Position Start;

        public MapLayerType Type;
    }
}