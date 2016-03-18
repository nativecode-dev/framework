namespace ServicesWeb.Config
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using NativeCode.Core.Platform;
    using NativeCode.Web.AspNet.Mvc.Dependencies;
    using NativeCode.Web.AspNet.Mvc.Filters;
    using NativeCode.Web.AspNet.Mvc.ModelBinders;

    using ServicesWeb.Filters;

    using IDependencyResolver = NativeCode.Core.Dependencies.IDependencyResolver;

    internal static class MvcConfig
    {
        public static void Configure(IPlatform platform)
        {
            DependencyResolver.SetResolver(new MvcDependencyResolver(platform.CreateDependencyScope()));

            AreaRegistration.RegisterAllAreas();

            ConfigureFilters(platform.Resolver, GlobalFilters.Filters);
            ConfigureRoutes(RouteTable.Routes);

            ModelBinders.Binders.DefaultBinder = new ExtendedModelBinder();
        }

        private static void ConfigureFilters(IDependencyResolver resolver, GlobalFilterCollection filters)
        {
            filters.Add(resolver.Resolve<HandleErrorAttribute>());
            filters.Add(resolver.Resolve<SiteMaintenanceAttribute>());
            filters.Add(resolver.Resolve<ValidateModelStateAttribute>());
        }

        private static void ConfigureRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
        }
    }
}