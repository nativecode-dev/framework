namespace Common.Web
{
    using System.Web.Http;

    using Common.Web.Handlers;

    using NativeCode.Core.Dependencies;
    using NativeCode.Web.AspNet.WebApi.Dependencies;
    using NativeCode.Web.AspNet.WebApi.Filters;
    using NativeCode.Web.AspNet.WebApi.Formatters;

    public static class WebApiApp
    {
        public static void Configure(IDependencyContainer container)
        {
            GlobalConfiguration.Configure(configuration => Configure(container, configuration));
        }

        public static void Configure(IDependencyContainer container, HttpConfiguration configuration)
        {
            configuration.DependencyResolver = new WebApiDependencyResolver(container);

            // Filters
            configuration.Filters.Add(container.Resolver.Resolve<AuthorizeAttribute>());
            configuration.Filters.Add(container.Resolver.Resolve<ValidateModelAttribute>());

            // Formatters
            configuration.Formatters.Add(container.Resolver.Resolve<JsonBrowserMediaTypeFormatter>());

            // Handlers
            configuration.MessageHandlers.Add(container.Resolver.Resolve<AccountAuthDelegatingHandler>());

            // Extensions
            configuration.EnableSystemDiagnosticsTracing();
            configuration.MapHttpAttributeRoutes();
        }
    }
}