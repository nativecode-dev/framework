namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)]
        public char Ascii;

        [FieldOffset(0)]
        public char Unicode;

        [FieldOffset(2)]
        public uint Attributes;

        public CharInfo(char value, uint attributes)
        {
            this.Ascii = value;
            this.Attributes = attributes;
            this.Unicode = value;
        }
    }
}