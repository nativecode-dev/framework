namespace NativeCode.Packages.Owin.Authentication
{
    using System.Web;
    using System.Web.Security;

    using JetBrains.Annotations;

    using Microsoft.Owin;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Serialization;
    using NativeCode.Core.Web.Authentication;
    using NativeCode.Packages.Owin.Owin;

    public class OwinCookieAuthentication : CookieAuthentication
    {
        public OwinCookie GetCookie([NotNull] IOwinRequest request, string name)
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

        public CookieAuthenticationData GetCookieData([NotNull] HttpCookie cookie)
        {
            var ticket = FormsAuthentication.Decrypt(cookie.ToString());

            if (ticket != null)
            {
                var serializer = DependencyLocator.Resolver.Resolve<IStringSerializer>();
                return serializer.Deserialize<CookieAuthenticationData>(ticket.UserData);
            }

            return null;
        }

        public void SetCookie([NotNull] IOwinResponse response, [NotNull] OwinCookie cookie)
        {
            response.Cookies.Append(cookie.Name, cookie.Value, cookie.Options);
        }
    }
}