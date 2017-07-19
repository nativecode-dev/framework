namespace NativeCode.Web.AspNet.WebApi
{
    using System.Net.Http;
    using System.Web.Http.Filters;
    using Core.Dependencies;
    using Core.Dependencies.Enums;
    using Filters;
    using Handlers;

    public class WebApiDependencies : DependencyModule
    {
        public static readonly IDependencyModule Instance = new WebApiDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            RegisterFilters(registrar);
            RegisterHandlers(registrar);
        }

        private static void RegisterFilters(IDependencyRegistrar registrar)
        {
            registrar.Register<KeyAuthorizationFilterAttribute>();
            registrar.Register<FilterAttribute, KeyAuthorizationFilterAttribute>(DependencyKey.QualifiedName);

            registrar.Register<MaintenanceFilterAttribute>();
            registrar.Register<FilterAttribute, MaintenanceFilterAttribute>(DependencyKey.QualifiedName);

            registrar.Register<ValidateModelFilterAttribute>();
            registrar.Register<FilterAttribute, ValidateModelFilterAttribute>(DependencyKey.QualifiedName);

            registrar.RegisterFactory(resolver => resolver.ResolveAll<ActionFilterAttribute>());
        }

        private static void RegisterHandlers(IDependencyRegistrar registrar)
        {
            registrar.Register<BasicAuthDelegatingHandler>();
            registrar.Register<DelegatingHandler, BasicAuthDelegatingHandler>(DependencyKey.QualifiedName);

            registrar.RegisterFactory(resolver => resolver.ResolveAll<DelegatingHandler>());
        }
    }
}