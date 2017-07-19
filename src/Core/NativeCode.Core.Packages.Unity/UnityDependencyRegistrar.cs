namespace NativeCode.Core.Packages.Unity
{
    using System;
    using Dependencies;
    using Dependencies.Enums;
    using Microsoft.Practices.Unity;

    public class UnityDependencyRegistrar : DependencyRegistrar
    {
        private readonly IUnityContainer container;

        public UnityDependencyRegistrar(IUnityContainer container)
        {
            this.container = container;
        }

        public override IDependencyRegistrar Register(
            Type type,
            Type implementation,
            string key = null,
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.container.RegisterType(type, implementation, key, this.CreateLifetimeManager(lifetime));

            return this;
        }

        public override IDependencyRegistrar RegisterFactory(
            Type type,
            Func<IDependencyResolver, object> factory,
            string key = null,
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            var member = new InjectionFactory(x => factory(new UnityDependencyResolver(x)));
            this.container.RegisterType(type, key, this.CreateLifetimeManager(lifetime), member);

            return this;
        }

        public override IDependencyRegistrar RegisterInstance(Type type, object instance, DependencyLifetime lifetime)
        {
            this.container.RegisterInstance(type, instance, this.CreateLifetimeManager(lifetime));

            return this;
        }

        protected virtual LifetimeManager CreateLifetimeManager(DependencyLifetime lifetime)
        {
            switch (lifetime)
            {
                case DependencyLifetime.PerApplication:
                    return new ContainerControlledLifetimeManager();

                case DependencyLifetime.PerContainer:
                    return new HierarchicalLifetimeManager();

                case DependencyLifetime.PerResolve:
                    return new PerResolveLifetimeManager();

                case DependencyLifetime.PerThread:
                    return new PerThreadLifetimeManager();

                default:
                    return new TransientLifetimeManager();
            }
        }
    }
}