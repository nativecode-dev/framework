namespace NativeCode.Core.Packages.Owin.Authentication
{
    using System;
    using System.Web.Security;

    using Microsoft.Owin;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Packages.Owin.Owin;
    using NativeCode.Core.Serialization;
    using NativeCode.Core.Web.Platform.Providers.Cookies;

    public class OwinCookieProvider<TData> : CookieProvider<IOwinRequest, IOwinResponse, OwinCookie, TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwinCookieProvider{TData}" /> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        public OwinCookieProvider(IStringSerializer serializer)
            : base(serializer)
        {
        }

        public override OwinCookie CreateCookie(string name, TData data, bool persist = true, int timeout = 20, int version = 1)
        {
            throw new NotImplementedException();
        }

        public override OwinCookie GetCookie(IOwinRequest request, string name)
        {
            foreach (var kvp in request.Cookies)
            {
                var key = kvp.Key;

                if (key == name)
                {
                    return new OwinCookie(kvp);
                }
            }

            return new OwinCookie(name, null);
        }

        public override TData GetCookieData(OwinCookie cookie)
        {
            var ticket = FormsAuthentication.Decrypt(cookie.ToString());

            if (ticket != null)
            {
                var serializer = DependencyLocator.Resolver.Resolve<IStringSerializer>();
                return serializer.Deserialize<TData>(ticket.UserData);
            }

            return default(TData);
        }

        public override void SetCookie(IOwinResponse response, OwinCookie cookie)
        {
            response.Cookies.Append(cookie.Name, cookie.Value, cookie.Options);
        }
    }
}