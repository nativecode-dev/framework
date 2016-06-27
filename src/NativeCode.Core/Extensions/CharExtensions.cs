namespace NativeCode.Core.Extensions
{
    public static class CharExtensions
    {
        public static bool IsQuoted(this char value)
        {
            return value.IsDoubleQuote() || value.IsSingleQuote();
        }

        public static bool IsDoubleQuote(this char value)
        {
            return value == '"';
        }

        public static bool IsSingleQuote(this char value)
        {
            return value == '\'';
        }
    }
}