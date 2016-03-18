namespace ServicesWeb.Config
{
    using System.Web.Http;

    using NativeCode.Core.Platform;
    using NativeCode.Web.AspNet.WebApi.Dependencies;
    using NativeCode.Web.AspNet.WebApi.Filters;
    using NativeCode.Web.AspNet.WebApi.Formatters;

    using ServicesWeb.Api.Handlers;

    internal static class WebApiConfig
    {
        public static void Configure(IPlatform platform)
        {
            GlobalConfiguration.Configure(configuration => Configure(platform, configuration));
        }

        public static void Configure(IPlatform platform, HttpConfiguration configuration)
        {
            configuration.DependencyResolver = new WebApiDependencyResolver(platform.CreateDependencyScope());

            // Filters
            configuration.Filters.Add(platform.Resolver.Resolve<AuthorizeAttribute>());
            configuration.Filters.Add(platform.Resolver.Resolve<ValidateModelAttribute>());

            // Formatters
            configuration.Formatters.Add(platform.Resolver.Resolve<JsonBrowserMediaTypeFormatter>());

            // Handlers
            configuration.MessageHandlers.Add(platform.Resolver.Resolve<AccountAuthDelegatingHandler>());

            // Extensions
            configuration.EnableSystemDiagnosticsTracing();
            configuration.MapHttpAttributeRoutes();
        }
    }
}