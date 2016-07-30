﻿namespace NativeCode.Core.Extensions
{
    using System;
    using System.Linq;
    using System.Text;

    using JetBrains.Annotations;

    public static class StringExtensions
    {
        public static bool AllEmpty(params string[] values)
        {
            return values.Any(string.IsNullOrWhiteSpace);
        }

        public static string Dequote([NotNull] this string value)
        {
            if (value.IsQuoted())
            {
                return value.Substring(1, value.Length - 2);
            }

            return value;
        }

        public static string FromBase64String(this string value, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var bytes = Convert.FromBase64String(value);

            return encoding.GetString(bytes, 0, bytes.Length);
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

        public static bool NotEmpty(params string[] values)
        {
            return values.All(x => !string.IsNullOrWhiteSpace(x));
        }

        public static string Quote([NotNull] this string value)
        {
            if (value.IsDoubleQuoted())
            {
                return value;
            }

            return "\"" + value + "\"";
        }

        public static string Replace(this string value, string replacement, params string[] characters)
        {
            return characters.Aggregate(value, (current, character) => current.Replace(character, replacement));
        }

        public static string SingleQuote([NotNull] this string value)
        {
            if (value.IsSingleQuoted())
            {
                return value;
            }

            return "'" + value + "'";
        }

        public static string[] Split(this string value, string separator)
        {
            return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ToBase64String(this string value, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var bytes = encoding.GetBytes(value);

            return Convert.ToBase64String(bytes);
        }

        public static string TrimNewLines(this string value, string replacement = "")
        {
            return value.Replace(Environment.NewLine, replacement);
        }
    }
}