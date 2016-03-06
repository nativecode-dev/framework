namespace NativeCode.Core.DotNet.Console.Win32
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)]
        private readonly char unicode;

        [FieldOffset(0)]
        private readonly char ascii;

        [FieldOffset(2)]
        private readonly uint attributes;

        public char Ascii => this.ascii;

        public char Unicode => this.unicode;

        public uint Attrbutes => this.attributes;
    }
}