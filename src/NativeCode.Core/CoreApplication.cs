namespace NativeCode.Core
{
    using Dependencies;
    using Platform;

    public class CoreApplication<TSettings> : Application<TSettings> where TSettings : Settings.Settings
    {
        public CoreApplication(IPlatform platform, IDependencyContainer container) : base(platform, container)
        {
        }
    }
}