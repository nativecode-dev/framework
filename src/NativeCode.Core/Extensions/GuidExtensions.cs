namespace NativeCode.Core.Extensions
{
    using System;
    using System.Text;
    using JetBrains.Annotations;

    public static class GuidExtensions
    {
        public static Guid FromBase64String([NotNull] string value, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;

            var bytes = Convert.FromBase64String(value);
            return Guid.Parse(encoding.GetString(bytes, 0, bytes.Length));
        }

        public static string ToBase64String(this Guid value, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;

            var bytes = encoding.GetBytes(value.ToString());
            return Convert.ToBase64String(bytes);
        }
    }
}