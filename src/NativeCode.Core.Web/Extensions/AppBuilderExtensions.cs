namespace NativeCode.Core.Web.Extensions
{
    using global::Owin;

    using Microsoft.Owin;

    using NativeCode.Core.Platform;
    using NativeCode.Core.Web.Owin.Middleware;

    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseOwinCookieRequestScope(this IAppBuilder app, string name, CookieOptions options)
        {
            return app.Use<OwinCookieMiddleware>(name, options);
        }

        public static IAppBuilder UseOwinDependencyRequestScope(this IAppBuilder app, IPlatform platform)
        {
            return app.Use<OwinDependencyMiddleware>(platform);
        }
    }
}