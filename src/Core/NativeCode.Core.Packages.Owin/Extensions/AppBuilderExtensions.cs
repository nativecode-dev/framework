namespace NativeCode.Core.Packages.Owin.Extensions
{
    using Core.Platform;
    using global::Owin;
    using Microsoft.Owin;
    using Owin.Middleware;

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