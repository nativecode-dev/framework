namespace NativeCode.Core.Extensions
{
    using JetBrains.Annotations;

    public static class StringExtensions
    {
        public static string Dequote([NotNull] this string value)
        {
            if (value.IsQuoted())
            {
                return value.Substring(1, value.Length - 2);
            }

            return value;
        }

        public static bool IsDoubleQuoted([NotNull] this string value)
        {
            return value.StartsWith("\"") && value.EndsWith("\"");
        }

        public static bool IsQuoted([NotNull] this string value)
        {
            return value.IsDoubleQuoted() || value.IsSingleQuoted();
        }

        public static bool IsSingleQuoted([NotNull] this string value)
        {
            return value.StartsWith("'") && value.EndsWith("'");
        }

        public static string Quote([NotNull] this string value)
        {
            if (value.IsDoubleQuoted())
            {
                return value;
            }

            return "\"" + value + "\"";
        }

        public static string SingleQuote([NotNull] this string value)
        {
            if (value.IsSingleQuoted())
            {
                return value;
            }

            return "'" + value + "'";
        }
    }
}