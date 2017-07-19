namespace NativeCode.Core.Packages.Owin.Extensions
{
    using System.Linq;
    using Microsoft.Owin;
    using Owin;

    public static class OwinRequestExtensions
    {
        public static OwinCookie GetCookie(this IOwinRequest request, string name)
        {
            return new OwinCookie(request.Cookies.SingleOrDefault(c => c.Key == name));
        }
    }
}