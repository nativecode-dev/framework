namespace NativeCode.Core.Dependencies
{
    using System;
    using Attributes;
    using Enums;
    using Microsoft.Extensions.DependencyInjection;

    [IgnoreDependency]
    public class CoreDependencyRegistrar : DependencyRegistrar
    {
        private readonly IServiceCollection services;

        public CoreDependencyRegistrar(IServiceCollection services)
        {
            this.services = services;
        }

        public override IDependencyRegistrar Register(DependencyDescription dependency)
        {
            var descriptor = new ServiceDescriptor(dependency.Contract, dependency.Implementation,
                CoreDependencyRegistrar.GetLifetime(dependency.Lifetime));

            this.services.Add(descriptor);
            return this;
        }

        public override IDependencyRegistrar RegisterFactory(DependencyDescription dependency, Func<IDependencyResolver, object> factory)
        {
            object Callback(IServiceProvider provider)
            {
                var resolver = new CoreDependencyResolver(provider);
                return factory(resolver);
            }

            var descriptor = new ServiceDescriptor(dependency.Contract, Callback, CoreDependencyRegistrar.GetLifetime(dependency.Lifetime));

            this.services.Add(descriptor);
            return this;
        }

        public override IDependencyRegistrar RegisterInstance(DependencyDescription dependency, object instance)
        {
            var descriptor = new ServiceDescriptor(dependency.Contract, instance);

            this.services.Add(descriptor);
            return this;
        }

        private static ServiceLifetime GetLifetime(DependencyLifetime lifetime)
        {
            switch (lifetime)
            {
                case DependencyLifetime.PerApplication:
                    return ServiceLifetime.Singleton;

                case DependencyLifetime.PerContainer:
                    return ServiceLifetime.Scoped;

                default:
                    return ServiceLifetime.Transient;
            }
        }
    }
}