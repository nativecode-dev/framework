using Microsoft.Owin;

using ServicesWeb;

[assembly: OwinStartup(typeof(Startup))]

namespace ServicesWeb
{
    using Common.Web;

    using Owin;

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
            this.Application = new ServicesApplication();

            base.Configuration(builder);

            MvcApp.Configure(this.Application.Container);
        }
    }
}