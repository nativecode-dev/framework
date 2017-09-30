namespace NativeCode
{
    using System;
    using Core;
    using Core.Platform;
    using Core.Web;
    using Core.Web.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class ProgramStartup : CoreWebStartup<ProgramSettings>
    {
        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddNativeCodeWebSockets();

            return base.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseNativeCodeWebSockets();
            app.UseStaticFiles();
        }

        protected override void ConfigureApplication(IApplication<ProgramSettings> application)
        {
            application.Configure(CoreDependencies.Instance, CoreWebDependencies.Instance);
        }

        protected override void InitializeApplication(IApplication<ProgramSettings> application)
        {
            application.Initialize();
        }
    }
}