namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct KeyEventRecord
    {
        public bool KeyDown;

        public short RepeatCount;

        public short VirtualKeyCode;

        public short VirtualScanCode;

        public char UnicodeChar;

        public ControlKeyState ControlKeyState;
    }
}