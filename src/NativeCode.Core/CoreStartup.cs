namespace NativeCode.Core
{
    using System;
    using Dependencies;
    using Microsoft.Extensions.DependencyInjection;
    using Platform;

    public abstract class CoreStartup<TSettings> where TSettings : Settings.Settings
    {
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var container = new CoreDependencyContainer(services);
            var platform = new CorePlatform<TSettings>(container);
            services.AddSingleton<IPlatform>(platform);

            return container.Finalize(services.BuildServiceProvider());
        }
    }
}