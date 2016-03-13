namespace Common.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Common.Web.Filters;

    using NativeCode.Core.Dependencies;
    using NativeCode.Web.AspNet.Mvc.Dependencies;

    using DependencyResolver = System.Web.Mvc.DependencyResolver;

    public static class MvcApp
    {
        public static void Configure(IDependencyContainer container)
        {
            DependencyResolver.SetResolver(new MvcDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            ConfigureFilters(GlobalFilters.Filters);
            ConfigureRoutes(RouteTable.Routes);
        }

        private static void ConfigureFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InstallRedirectAttribute());
        }

        private static void ConfigureRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
        }
    }
}