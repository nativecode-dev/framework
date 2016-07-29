namespace NativeCode.Packages.Owin.Owin
{
    using System.Collections.Generic;

    using Microsoft.Owin;

    public class OwinCookie
    {
        public OwinCookie(string name, string value, CookieOptions options = null)
        {
            this.Name = name;
            this.Options = options;
            this.Value = value;
        }

        public OwinCookie(KeyValuePair<string, string> kvp, CookieOptions options = null)
            : this(kvp.Key, kvp.Value, options)
        {
        }

        public string Name { get; }

        public CookieOptions Options { get; }

        public string Value { get; set; }
    }
}