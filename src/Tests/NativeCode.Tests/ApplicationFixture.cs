namespace NativeCode.Tests
{
    using NativeCode.Core;
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.DotNet;
    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.Platform;

    public class ApplicationFixture : DependencyFixture
    {
        public ApplicationFixture()
        {
            this.Application = new TestApplication(new TestPlatform(this.Container));
            this.Application.Initialize("Test Application", this.GetDependencyModules());
        }

        protected IApplication Application { get; }

        protected IDependencyRegistrar Registrar => this.Application.Platform.Registrar;

        protected IDependencyResolver Resolver => this.Application.Platform.Resolver;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                this.Application.Dispose();
            }

            base.Dispose(disposing);
        }

        protected virtual IDependencyModule[] GetDependencyModules()
        {
            return new[] { CoreDependencies.Instance, DotNetDependencies.Instance };
        }

        private class TestApplication : ApplicationProxy
        {
            public TestApplication(IPlatform platform)
                : base(platform)
            {
            }
        }

        private class TestPlatform : DotNetPlatform
        {
            public TestPlatform(IDependencyContainer container)
                : base(container)
            {
            }
        }
    }
}