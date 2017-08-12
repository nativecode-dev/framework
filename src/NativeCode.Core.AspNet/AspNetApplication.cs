namespace NativeCode.Core.AspNet
{
    using Dependencies;
    using Microsoft.AspNetCore.Builder;
    using Platform;
    using Settings;

    public class AspNetApplication<TSettings> : Application<TSettings> where TSettings : Settings
    {
        public AspNetApplication(IDependencyContainer container, TSettings settings)
            : base(CreateAspNetPlatform(container), settings)
        {
        }

        public IApplicationBuilder Configure(IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler();
        }

        private static IPlatform CreateAspNetPlatform(IDependencyContainer container)
        {
            return new AspNetPlatform(container);
        }
    }
}