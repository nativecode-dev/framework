namespace NativeCode.Core
{
    using Platform;

    public class CoreApplication<TSettings> : Application<TSettings> where TSettings : Settings.Settings
    {
        public CoreApplication(IPlatform platform, TSettings settings) : base(platform, settings)
        {
        }
    }
}