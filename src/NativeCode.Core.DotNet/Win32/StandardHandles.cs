namespace NativeCode.Core.DotNet.Win32
{
    public static class StandardHandles
    {
        public const uint InputHandle = unchecked((uint)(-10));

        public const uint OutputHandle = unchecked((uint)(-11));

        public const uint ErrorHandle = unchecked((uint)(-12));
    }
}