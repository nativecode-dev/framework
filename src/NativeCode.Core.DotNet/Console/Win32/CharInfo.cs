namespace NativeCode.Core.DotNet.Console.Win32
{
    using System;
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

        public CharInfo(char character)
        {
            this.ascii = default(char);
            this.attributes = 0;
            this.unicode = character;
        }

        public CharInfo(char character, ConsoleColor background, ConsoleColor foreground)
        {
            this.ascii = default(char);
            this.attributes = (uint)foreground;
            this.unicode = character;
        }

        public char Ascii => this.ascii;

        public uint Attrbutes => this.attributes;

        public char Unicode => this.unicode;
    }
}