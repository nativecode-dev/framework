namespace Services
{
    using System.Web.Http;

    using Common;

    using NativeCode.Core;
    using NativeCode.Core.DotNet;
    using NativeCode.Core.Platform;
    using NativeCode.Packages.Dependencies;
    using NativeCode.Web;
    using NativeCode.Web.AspNet.WebApi.Dependencies;

    using Owin;

    public class ServicesApplication : Application
    {
        public ServicesApplication()
            : base(new UnityDependencyContainer(), true)
        {
            this.Initialize(CoreDependencies.Instance, DotNetDependencies.Instance, CommonDependencies.Instance, WebDependencies.Instance);
        }

        public void Configuration(IAppBuilder builder)
        {
            var configuration = new HttpConfiguration { DependencyResolver = new WebApiDependencyResolver(this.Container) };
            configuration.Filters.Add(new AuthorizeAttribute());
            configuration.MapHttpAttributeRoutes();

            builder.UseWebApi(configuration);
        }
    }
}