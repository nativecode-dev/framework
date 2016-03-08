namespace NativeCode.Core.DotNet.Console.Win32
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleCursorInfo
    {
        public uint DwSize { get; set; }

        public bool Visible { get; set; }
    }
}