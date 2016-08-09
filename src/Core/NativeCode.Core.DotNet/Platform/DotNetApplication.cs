namespace NativeCode.Core.DotNet.Platform
{
    using NativeCode.Core.Platform;
    using NativeCode.Core.Settings;

    public abstract class DotNetApplication : Application
    {
        protected DotNetApplication(IPlatform platform, Settings settings)
            : base(platform, settings)
        {
        }
    }
}