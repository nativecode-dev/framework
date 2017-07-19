namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CharInfo
    {
        public CharUnion Char;

        [MarshalAs(UnmanagedType.I2)] public Color Color;

        public CharInfo(byte ascii, Color color)
        {
            this.Char = new CharUnion { AsciiChar = ascii };
            this.Color = color;
        }

        public CharInfo(char unicode, Color color)
        {
            this.Char = new CharUnion { UnicodeChar = unicode };
            this.Color = color;
        }
    }
}