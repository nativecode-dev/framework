namespace Common.Web
{
    using System.Web.Http;

    using NativeCode.Packages.Platform;

    using Owin;

    public class ServicesStartup : OwinStartup
    {
        public override void Configuration(IAppBuilder builder)
        {
            var container = ServicesApplication.Current.Container;

            var configuration = new HttpConfiguration();
            WebApiApp.Configure(container, configuration);
            builder.UseWebApi(configuration);
        }
    }
}