namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleScreenBufferInfoEx
    {
        public uint Size;

        public Coord CursorSize;

        public Coord CursorPosition;

        public short Attributes;

        public SmallRect Window;

        public Coord MaximumWindowSize;

        public ushort PopupAttributes;

        public bool FullscreenSupported;

        public int Color0;

        public int Color1;

        public int Color2;

        public int Color3;

        public int Color4;

        public int Color5;

        public int Color6;

        public int Color7;

        public int Color8;

        public int Color9;

        public int ColorA;

        public int ColorB;

        public int ColorC;

        public int ColorD;

        public int ColorE;

        public int ColorF;
    }
}
