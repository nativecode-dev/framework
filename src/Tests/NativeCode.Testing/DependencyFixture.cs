namespace NativeCode.Testing
{
    using Core.Dependencies;
    using Core.Packages.Unity;
    using Core.Types;

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
                this.Container.Dispose();

            base.Dispose(disposing);
        }
    }
}