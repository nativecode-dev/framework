namespace NativeCode.Core.DotNet.Win32.Extensions
{
    using NativeCode.Core.DotNet.Win32.Structs;

    public static class RectExtensions
    {
        public static int Height(this Rect rect)
        {
            return rect.Bottom - rect.Top;
        }

        public static int Width(this Rect rect)
        {
            return rect.Right - rect.Left;
        }
    }
}