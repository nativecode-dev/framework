namespace ServicesWeb.Config
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using NativeCode.Core.Dependencies;
    using NativeCode.Web.AspNet.Mvc.Dependencies;
    using NativeCode.Web.AspNet.Mvc.Filters;
    using NativeCode.Web.AspNet.Mvc.ModelBinders;

    using ServicesWeb.Filters;

    using DependencyResolver = System.Web.Mvc.DependencyResolver;
    using IDependencyResolver = NativeCode.Core.Dependencies.IDependencyResolver;

    internal static class MvcConfig
    {
        public static void Configure(IDependencyContainer container)
        {
            DependencyResolver.SetResolver(new MvcDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            ConfigureFilters(container.Resolver, GlobalFilters.Filters);
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