namespace NativeCode.Core.Extensions
{
    using System;

    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value, bool ignoreCase = true) where T : struct
        {
            return (T) Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static string ToKey(this Enum value)
        {
            return value.ToString();
        }
    }
}