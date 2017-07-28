namespace NativeCode.Testing
{
    using Core;
    using Core.Platform;
    using Core.Settings;

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