namespace Common.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Common.Web.Filters;

    using NativeCode.Core.Dependencies;
    using NativeCode.Web.AspNet.Mvc.Dependencies;

    using DependencyResolver = System.Web.Mvc.DependencyResolver;
    using IDependencyResolver = NativeCode.Core.Dependencies.IDependencyResolver;

    public static class MvcApp
    {
        public static void Configure(IDependencyContainer container)
        {
            DependencyResolver.SetResolver(new MvcDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            ConfigureFilters(container.Resolver, GlobalFilters.Filters);
            ConfigureRoutes(RouteTable.Routes);
        }

        private static void ConfigureFilters(IDependencyResolver resolver, GlobalFilterCollection filters)
        {
            filters.Add(resolver.Resolve<AccountAuthorizeAttribute>());
            filters.Add(resolver.Resolve<HandleErrorAttribute>());
            filters.Add(resolver.Resolve<InstallRedirectAttribute>());
        }

        private static void ConfigureRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
        }
    }
}