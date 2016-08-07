namespace NativeCode.Core.DotNet.Platform
{
    using NativeCode.Core.Platform;
    using NativeCode.Core.Settings;
    using NativeCode.Core.Types;

    public abstract class DotNetApplication : Application
    {
        protected DotNetApplication(IPlatform platform, CancellationTokenManager cancellationTokenManager, Settings settings)
            : base(platform, cancellationTokenManager, settings)
        {
        }
    }
}