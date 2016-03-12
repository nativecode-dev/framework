﻿namespace NativeCode.Packages.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.Dependencies.Exceptions;

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
            else
            {
                throw new InvalidOperationException("Cannot construct more than one instance per AppDomain.");
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

        private class UnityDependencyRegistrar : DependencyRegistrar
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
                this.container.RegisterType(type, implementation, key, CreateLifetimeManager(lifetime));

                return this;
            }

            public override IDependencyRegistrar RegisterFactory(
                Type type,
                Func<IDependencyResolver, object> factory,
                string key = null,
                DependencyLifetime lifetime = DependencyLifetime.Default)
            {
                var member = new InjectionFactory(x => factory(new UnityDependencyResolver(x)));
                this.container.RegisterType(type, key, CreateLifetimeManager(lifetime), member);

                return this;
            }

            public override IDependencyRegistrar RegisterInstance(Type type, object instance)
            {
                this.container.RegisterInstance(type, instance);

                return this;
            }

            private static LifetimeManager CreateLifetimeManager(DependencyLifetime lifetime)
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

        private class UnityDependencyResolver : DependencyResolver
        {
            private readonly IUnityContainer container;

            public UnityDependencyResolver(IUnityContainer container)
            {
                this.container = container;
            }

            public override object Resolve(Type type, string key = null)
            {
                try
                {
                    return this.container.Resolve(type, key);
                }
                catch (ResolutionFailedException rfe)
                {
                    throw new DependencyResolveException(type, rfe);
                }
            }

            public override IEnumerable<object> ResolveAll(Type type)
            {
                try
                {
                    return this.container.ResolveAll(type);
                }
                catch (ResolutionFailedException rfe)
                {
                    throw new DependencyResolveException(type, rfe);
                }
            }
        }
    }
}