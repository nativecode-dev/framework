namespace NativeCode.Tests
{
    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.Platform;

    public abstract class WhenTestingPlatform : WhenTestingDependencies
    {
        protected WhenTestingPlatform()
        {
            this.Platform = new DotNetPlatform(this.Container);
            this.EnsureDisposed(this.Platform);
        }

        protected IPlatform Platform { get; }
    }
}