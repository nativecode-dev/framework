namespace NativeCode.Core.Web
{
    using Dependencies;
    using Platform;
    using Settings;

    public class CoreWebApplication<TSettings> : CoreApplication<TSettings> where TSettings : Settings
    {
        public CoreWebApplication(IPlatform platform, IDependencyContainer container) : base(platform, container)
        {
        }
    }
}