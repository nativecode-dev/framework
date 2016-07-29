namespace NativeCode.Packages.Owin.Extensions
{
    using Microsoft.Owin;

    using NativeCode.Core.Dependencies;
    using NativeCode.Packages.Owin.Authentication;
    using NativeCode.Packages.Owin.Owin;

    public static class OwinRequestExtensions
    {
        public static OwinCookie GetCookie(this IOwinRequest request, string name)
        {
            return DependencyLocator.Resolver.Resolve<OwinCookieAuthentication>().GetCookie(request, name);
        }
    }
}
