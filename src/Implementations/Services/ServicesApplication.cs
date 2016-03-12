﻿namespace Services
{
    using System.Web.Http;

    using Common;
    using Common.Data.Entities;
    using Common.Web;
    using Common.Workers;

    using Humanizer;

    using NativeCode.Core;
    using NativeCode.Core.DotNet;
    using NativeCode.Core.Platform;
    using NativeCode.Packages.Dependencies;
    using NativeCode.Packages.Platform;
    using NativeCode.Web;
    using NativeCode.Web.AspNet.WebApi.Dependencies;

    using Owin;

    public class ServicesApplication : OwinApplication
    {
        public ServicesApplication()
            : base(new UnityDependencyContainer(), true)
        {
            Current = this;
            this.Initialize(
                this.GetType().Name.Humanize(),
                CoreDependencies.Instance,
                DotNetDependencies.Instance,
                WebDependencies.Instance,
                CommonDependencies.Instance,
                CommonWebDependencies.Instance);
        }

        public static IApplication Current { get; private set; }

        protected IWorkManager<Download> Downloads { get; private set; }

        protected override void PostInitialization()
        {
            this.Downloads = this.Container.Resolver.Resolve<IWorkManager<Download>>();
            this.Downloads.StartAsync();

            base.PostInitialization();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                if (this.Downloads != null)
                {
                    this.Downloads.Dispose();
                    this.Downloads = null;
                }
            }

            base.Dispose(disposing);
        }
    }

    public class ServicesStartup : OwinStartup
    {
        public override void Configuration(IAppBuilder builder)
        {
            var configuration = new HttpConfiguration { DependencyResolver = new WebApiDependencyResolver(ServicesApplication.Current.Container) };
            configuration.EnableSystemDiagnosticsTracing();
            configuration.Filters.Add(new AuthorizeAttribute());
            configuration.MapHttpAttributeRoutes();

            builder.UseWebApi(configuration);
        }
    }
}