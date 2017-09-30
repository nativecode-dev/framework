namespace NativeCode.Core.Web
{
    using Dependencies;
    using Platform;
    using Settings;

    public abstract class CoreWebStartup<TSettings> : CoreStartup<TSettings> where TSettings : Settings
    {
        protected override IApplication<TSettings> CreateApplication(IPlatform platform, IDependencyContainer container)
        {
            return new CoreWebApplication<TSettings>(platform, container);
        }

        protected override IPlatform CreatePlatform(IDependencyContainer container)
        {
            return new CoreWebPlatform(container);
        }
    }
}