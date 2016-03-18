namespace NativeCode.Core.Web.Extensions
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Web.Handlers.Owin;

    using Owin;

    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseOwinCookieRequestScope(this IAppBuilder app)
        {
            return app.Use<OwinCookieMiddleware>();
        }

        public static IAppBuilder UseOwinDependencyRequestScope(this IAppBuilder app, IDependencyContainer container)
        {
            return app.Use<OwinDependencyMiddleware>(container);
        }
    }
}