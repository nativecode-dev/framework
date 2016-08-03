namespace NativeCode.Core.Packages.Owin.Extensions
{
    using global::Owin;

    using Microsoft.Owin;

    using NativeCode.Core.Packages.Owin.Owin.Middleware;
    using NativeCode.Core.Platform;

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