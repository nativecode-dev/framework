namespace NativeCode.Core.DotNet.Win32.Extensions
{
    using Structs;

    public static class SmallRectExtensions
    {
        public static int Height(this SmallRect smallRect)
        {
            return smallRect.Bottom - smallRect.Top;
        }

        public static int Width(this SmallRect smallRect)
        {
            return smallRect.Right - smallRect.Left;
        }
    }
}