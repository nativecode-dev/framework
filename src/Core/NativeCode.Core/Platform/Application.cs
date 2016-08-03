namespace NativeCode.Core.Platform
{
    using NativeCode.Core.Settings;

    public abstract class Application : ApplicationProxy
    {
        protected Application(IPlatform platform, Settings settings)
            : base(platform, settings)
        {
        }
    }
}