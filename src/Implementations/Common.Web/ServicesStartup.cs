namespace Common.Web
{
    using System.Web.Http;

    using NativeCode.Core.Settings;
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

            ConfigureSettings(ServicesApplication.Current.Settings);
        }

        private static void ConfigureSettings(Settings settings)
        {
            settings.SetValue("Services.DefaultDomain", "NATIVECODE");
            settings.SetValue("Services.DefaultDomainDns", "nativecode.local");
        }
    }
}