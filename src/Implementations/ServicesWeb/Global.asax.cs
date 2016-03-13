namespace ServicesWeb
{
    using System;
    using System.Web.Optimization;

    using Common;
    using Common.Web;

    using NativeCode.Core;
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.DotNet;
    using NativeCode.Packages.Dependencies;
    using NativeCode.Web;
    using NativeCode.Web.Platform;

    public class Global : WebApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            this.Initialize(
                "NativeCode Services",
                CoreDependencies.Instance,
                DotNetDependencies.Instance,
                WebDependencies.Instance,
                CommonDependencies.Instance,
                CommonWebDependencies.Instance);

            MvcApp.Configure(this.Container);
            WebApiApp.Configure(this.Container);

            ConfigureBundles(BundleTable.Bundles);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override IDependencyContainer CreateDependencyContainer()
        {
            return new UnityDependencyContainer();
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