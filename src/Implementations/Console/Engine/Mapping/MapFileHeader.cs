namespace Console.Engine.Mapping
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    public struct MapFileHeader
    {
        public uint MapHeight;

        public uint MapWidth;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Name;

        public ushort Version;
    }
}