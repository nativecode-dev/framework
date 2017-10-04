namespace NativeCode.Core.Extensions
{
    using System;
    using JetBrains.Annotations;

    public static class EnumExtensions
    {
        [NotNull]
        public static T ToEnum<T>([NotNull] this string value, bool ignoreCase = true) where T : IEquatable<T>
        {
            return (T) Enum.Parse(typeof(T), value, ignoreCase);
        }

        [NotNull]
        public static string ToKey([NotNull] this Enum value)
        {
            return value.ToString();
        }
    }
}