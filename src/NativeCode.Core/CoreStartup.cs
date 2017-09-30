namespace NativeCode.Core
{
    using System;
    using System.Diagnostics;
    using Dependencies;
    using Microsoft.Extensions.DependencyInjection;
    using Platform;

    public abstract class CoreStartup<TSettings> where TSettings : Settings.Settings
    {
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var container = this.CreateContainer(services);

            try
            {
                var platform = this.CreatePlatform(container);
                var application = this.CreateApplication(platform, container);

                this.ConfigureApplication(application);

                // NOTE: We need to initialzie after we have an IServiceProvider
                // instance because the DI by default separates registration from
                // resolving explicitly.
                var provider = this.BuildContainer(container, services);
                this.InitializeApplication(application);

                return provider;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                container.Dispose();
                throw;
            }
        }

        protected virtual IApplication<TSettings> CreateApplication(IPlatform platform, IDependencyContainer container)
        {
            return new CoreApplication<TSettings>(platform, container);
        }

        protected virtual IDependencyContainer CreateContainer(IServiceCollection services)
        {
            return new CoreDependencyContainer(services);
        }

        protected virtual IPlatform CreatePlatform(IDependencyContainer container)
        {
            return new CorePlatform(container);
        }

        protected virtual IServiceProvider BuildContainer(IDependencyContainer container, IServiceCollection services)
        {
            return ((CoreDependencyContainer) container).Build(services.BuildServiceProvider());
        }

        protected abstract void InitializeApplication(IApplication<TSettings> application);

        protected abstract void ConfigureApplication(IApplication<TSettings> application);
    }
}