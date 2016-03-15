using Microsoft.Owin;

using ServicesWeb;

[assembly: OwinStartup(typeof(Startup))]

namespace ServicesWeb
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Common.Web;

    using NativeCode.Core.Dependencies;

    using Owin;

    using OwinHandler = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

    public class Startup : ServicesStartup
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

            base.Configuration(builder);

            MvcApp.Configure(this.Application.Container);

            builder.Use<DependencyRequestScope>(this.Application.Container);
        }

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        private class DependencyRequestScope
        {
            private readonly IDependencyContainer container;

            private readonly OwinHandler next;

            [SuppressMessage("ReSharper", "UnusedMember.Local")]
            public DependencyRequestScope(OwinHandler next, IDependencyContainer container)
                : this(next)
            {
                this.container = container;
            }

            private DependencyRequestScope(OwinHandler next)
            {
                this.next = next;
            }

            [SuppressMessage("ReSharper", "UnusedMember.Local")]
            public async Task Invoke(IDictionary<string, object> environment)
            {
                using (this.container.CreateChildContainer())
                {
                    await this.next(environment);
                }
            }
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