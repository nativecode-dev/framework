namespace NativeCode.Core.Web.Authentication
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Web;
    using System.Web.Security;

    using JetBrains.Annotations;

    using Microsoft.Owin;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Serialization;
    using NativeCode.Core.Web.Owin;

    public static class CookieAuthentication
    {
        public static HttpCookie CreateCookie([NotNull] CookieAuthenticationData cookie, bool persist = true, int timeout = 20, int version = 1)
        {
            var name = FormsAuthentication.FormsCookieName;
            var ticket = new FormsAuthenticationTicket(version, name, DateTime.Now, DateTime.Now.AddMinutes(20), persist, cookie.Serialize());

            return new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
                       {
                           HttpOnly = true,
                           Path = FormsAuthentication.FormsCookiePath
                       };
        }

        public static HttpCookie GetCookie([NotNull] this HttpRequestMessage request)
        {
            var cookie = request.GetRequestCookie();

            if (string.IsNullOrWhiteSpace(cookie).Not())
            {
                return new HttpCookie(FormsAuthentication.FormsCookieName, cookie);
            }

            return null;
        }

        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
        public static OwinCookie GetCookie([NotNull] this IOwinRequest request, string name)
        {
            foreach (var kvp in request.Cookies)
            {
                var key = kvp.Key;

                if (key == name)
                {
                    return new OwinCookie(kvp);
                }
            }

            return null;
        }

        public static CookieAuthenticationData GetCookieData([NotNull] this HttpCookie cookie)
        {
            var ticket = FormsAuthentication.Decrypt(cookie.ToString());

            if (ticket != null)
            {
                var serializer = DependencyLocator.Resolver.Resolve<IStringSerializer>();
                return serializer.Deserialize<CookieAuthenticationData>(ticket.UserData);
            }

            return null;
        }

        public static void SetCookie([NotNull] this HttpResponseMessage response, [NotNull] OwinCookie cookie)
        {
            response.Headers.TryAddWithoutValidation("Set-Cookie", cookie.ToString());
        }

        public static void SetCookie([NotNull] this IOwinResponse response, [NotNull] OwinCookie cookie)
        {
            response.Cookies.Append(cookie.Name, cookie.Value, cookie.Options);
        }
    }
}