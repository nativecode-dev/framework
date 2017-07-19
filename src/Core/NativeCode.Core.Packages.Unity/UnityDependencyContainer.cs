namespace NativeCode.Core.Packages.Unity
{
    using System.Threading;
    using Dependencies;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public class UnityDependencyContainer : DependencyContainer
    {
        private static int counter;

        private IUnityContainer container;

        public UnityDependencyContainer()
            : this(new UnityContainer())
        {
            if (Interlocked.CompareExchange(ref counter, 1, 0) == 0)
            {
                ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(this.container));
                DependencyLocator.SetRootContainer(this);
            }
        }

        private UnityDependencyContainer(IUnityContainer container)
        {
            this.container = container;
            this.Registrar = new UnityDependencyRegistrar(this.container);
            this.Resolver = new UnityDependencyResolver(this.container);
        }

        public override IDependencyRegistrar Registrar { get; }

        public override IDependencyResolver Resolver { get; }

        public override IDependencyContainer CreateChildContainer()
        {
            return new UnityDependencyContainer(this.container.CreateChildContainer());
        }

        protected override void DisposeInstance()
        {
            if (this.container != null)
            {
                this.container.Dispose();
                this.container = null;
            }
        }
    }
}