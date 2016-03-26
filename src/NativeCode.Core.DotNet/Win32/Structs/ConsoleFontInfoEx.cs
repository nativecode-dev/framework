namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct ConsoleFontInfoEx
    {
        public uint Size;

        public uint Font;

        public Coord FontSize;

        public int FontFamily;

        public int FontWeight;

        public fixed char FaceName [ConsoleMetrics.FontFaceSize];
    }
}