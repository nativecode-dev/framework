namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)]
        public CharUnion Char;

        [FieldOffset(2)]
        public short Attributes;

        public CharInfo(byte ascii, short attributes)
        {
            this.Char = new CharUnion { AsciiChar = ascii };
            this.Attributes = attributes;
        }

        public CharInfo(char unicode, short attributes)
        {
            this.Char = new CharUnion { UnicodeChar = unicode };
            this.Attributes = attributes;
        }
    }
}