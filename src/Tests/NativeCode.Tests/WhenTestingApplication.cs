namespace NativeCode.Tests
{
    using NativeCode.Core;
    using NativeCode.Core.DotNet;
    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.Packages.Rabbit;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Settings;

    public abstract class WhenTestingApplication : WhenTestingPlatform
    {
        protected WhenTestingApplication()
        {
            this.Application = new DotNetApplication(this.Platform, new JsonSettings());
            this.Application.Initialize("test-app", CoreDependencies.Instance, DotNetDependencies.Instance,
                RabbitDependencies.Instance);
            this.EnsureDisposed(this.Application);
        }

        protected IApplication Application { get; }
    }
}