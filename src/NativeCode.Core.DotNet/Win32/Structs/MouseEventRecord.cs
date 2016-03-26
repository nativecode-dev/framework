namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct MouseEventRecord
    {
        [FieldOffset(0)]
        public Coord MousePosition;

        [FieldOffset(4)]
        public uint ButtonState;

        [FieldOffset(8)]
        public ControlKeyState ControlKeyState;

        [FieldOffset(12)]
        public uint EventFlags;
    }
}