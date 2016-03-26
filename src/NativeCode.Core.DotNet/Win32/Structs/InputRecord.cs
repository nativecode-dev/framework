namespace NativeCode.Core.DotNet.Win32.Structs
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct InputRecord
    {
        public short EventType;

        public KeyEventRecord KeyEvent;
    }
}