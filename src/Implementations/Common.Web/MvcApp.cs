namespace Common.Web
{
    using System.Web.Mvc;
    using System.Web.Optimization;
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

            ConfigureBundles(BundleTable.Bundles);
            ConfigureFilters(container.Resolver, GlobalFilters.Filters);
            ConfigureRoutes(RouteTable.Routes);
        }

        private static void ConfigureFilters(IDependencyResolver resolver, GlobalFilterCollection filters)
        {
            filters.Add(resolver.Resolve<AccountAuthorizeAttribute>());
            filters.Add(resolver.Resolve<HandleErrorAttribute>());
            filters.Add(resolver.Resolve<SiteMaintenanceAttribute>());
        }

        private static void ConfigureRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
        }

        private static void ConfigureBundles(BundleCollection bundles)
        {
#if RELEASE
            BundleTable.EnableOptimizations = true;
#endif
            CreateBootstrapBundles(bundles);
            CreateJqueryBundles(bundles);
            CreateSiteBundles(bundles);
        }

        private static void CreateBootstrapBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrap/styles").Include("~/Content/bootstrap.css").Include("~/Content/bootstrap-theme.css"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/scripts").Include("~/Scripts/bootstrap.js"));
        }

        private static void CreateJqueryBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
        }

        private static void CreateSiteBundles(BundleCollection bundles)
        {
            bundles.Add(new LessBundle("~/bundles/site/styles").Include("~/Content/*.less"));
        }
    }
}