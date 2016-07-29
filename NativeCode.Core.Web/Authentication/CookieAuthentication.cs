namespace NativeCode.Core.Web.Authentication
{
    using System;
    using System.Net.Http;
    using System.Web;
    using System.Web.Security;

    using JetBrains.Annotations;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Serialization;

    public class CookieAuthentication
    {
        public virtual HttpCookie CreateCookie([NotNull] CookieAuthenticationData cookie, bool persist = true, int timeout = 20, int version = 1)
        {
            var name = FormsAuthentication.FormsCookieName;
            var ticket = new FormsAuthenticationTicket(version, name, DateTime.Now, DateTime.Now.AddMinutes(20), persist, cookie.Serialize());

            return new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
                       {
                           HttpOnly = true,
                           Path = FormsAuthentication.FormsCookiePath
                       };
        }

        public virtual HttpCookie GetCookie([NotNull] HttpRequestMessage request)
        {
            var cookie = request.GetRequestCookie();

            if (string.IsNullOrWhiteSpace(cookie) == false)
            {
                return new HttpCookie(FormsAuthentication.FormsCookieName, cookie);
            }

            return null;
        }

        public virtual CookieAuthenticationData GetCookieData([NotNull] HttpCookie cookie)
        {
            var ticket = FormsAuthentication.Decrypt(cookie.ToString());

            if (ticket != null)
            {
                var serializer = DependencyLocator.Resolver.Resolve<IStringSerializer>();
                return serializer.Deserialize<CookieAuthenticationData>(ticket.UserData);
            }

            return null;
        }

        public virtual void SetCookie([NotNull] HttpResponseMessage response, [NotNull] HttpCookie cookie)
        {
            response.Headers.TryAddWithoutValidation("Set-Cookie", cookie.ToString());
        }
    }
}