namespace NativeCode.Tests
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Packages.Unity;
    using NativeCode.Core.Types;

    public class DependencyFixture : Disposable
    {
        public DependencyFixture()
        {
            this.Container = new UnityDependencyContainer();
        }

        protected IDependencyContainer Container { get; }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                this.Container.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}