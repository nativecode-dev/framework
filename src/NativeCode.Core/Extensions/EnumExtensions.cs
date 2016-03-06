namespace NativeCode.Core.Extensions
{
    using System;

    public static class EnumExtensions
    {
        public static string ToKey(this Enum value)
        {
            return value.ToString();
        }
    }
}