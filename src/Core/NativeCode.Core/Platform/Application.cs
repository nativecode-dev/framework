namespace NativeCode.Core.Platform
{
    using NativeCode.Core.Settings;
    using NativeCode.Core.Types;

    public abstract class Application : ApplicationProxy
    {
        protected Application(IPlatform platform, CancellationTokenManager cancellationTokenManager, Settings settings)
            : base(platform, cancellationTokenManager, settings)
        {
        }
    }
}