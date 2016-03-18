namespace NativeCode.Core.DotNet.Platform
{
    using NativeCode.Core.Platform;

    public abstract class DotNetApplication : Application
    {
        protected DotNetApplication(IPlatform platform)
            : base(platform)
        {
        }
    }
}