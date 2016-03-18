using Microsoft.Owin;

using ServicesWeb;

[assembly: OwinStartup(typeof(Startup))]

namespace ServicesWeb
{
    using System.Web.Http;
    using System.Web.Optimization;

    using Common.Web;

    using NativeCode.Core.Settings;
    using NativeCode.Core.Web.Extensions;
    using NativeCode.Packages.Platform;

    using Owin;

    using ServicesWeb.Config;

    public class Startup : OwinStartup
    {
        // Avoiding constants so R# doesn't complain in Razor.
#if DEBUG
        public static readonly bool IsDebug = true;
#else
        public static readonly bool IsDebug = false;
#endif

        protected ServicesApplication Application { get; private set; }

        public override void Configuration(IAppBuilder builder)
        {
            // We need to make sure that a ServicesApplication instance is created because in an ASP.NET
            // application, the construction logic is reversed since there's no need to call WebApp.Start.
            this.Application = new WebServicesApplication();

            // Set settings and their default values.
            ConfigureSettings(ServicesApplication.Current.Settings);

            // Configure services
            this.ConfigureWebApi(builder);
            this.ConfigureMvc();

            // Register custom handlers.
            builder.UseOwinCookieRequestScope();
            builder.UseOwinDependencyRequestScope(this.Application.Container);
        }

        private void ConfigureMvc()
        {
            MvcConfig.Configure(this.Application.Container);
            ConfigureBundles(BundleTable.Bundles);
        }

        private void ConfigureWebApi(IAppBuilder builder)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Configure(this.Application.Container, configuration);
            builder.UseWebApi(configuration);
        }

        private static void ConfigureBundles(BundleCollection bundles)
        {
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
            ConfigureScripts(bundles);
            ConfigureStyles(bundles);
        }

        private static void ConfigureScripts(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/scripts").Include("~/Scripts/jquery-{version}.js")
                    .Include("~/Scripts/jquery.validate.js")
                    .Include("~/Scripts/jquery.validate.unobtrusive.js")
                    .Include("~/Scripts/bootstrap.js"));
        }

        private static void ConfigureStyles(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/bundles/styles").Include("~/Content/bootstrap.css").Include("~/Content/bootstrap-theme.css").Include("~/Content/Core.less"));
        }

        private static void ConfigureSettings(Settings settings)
        {
            settings.SetValue("Global.DefaultDomain", "NATIVECODE", false);
            settings.SetValue("Global.DefaultDomainDns", "nativecode.local", false);
        }

        private class WebServicesApplication : ServicesApplication
        {
            protected override void PostInitialization()
            {
                this.RegisterModule(ServicesWebDependencies.Instance);
                base.PostInitialization();
            }
        }
    }
}