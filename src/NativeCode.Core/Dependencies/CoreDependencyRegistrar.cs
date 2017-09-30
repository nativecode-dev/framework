namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Attributes;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;

    [IgnoreDependency]
    public class CoreDependencyRegistrar : DependencyRegistrar
    {
        private readonly DependencyKeyTracker tracker;

        private readonly IServiceCollection services;

        public CoreDependencyRegistrar(IServiceCollection services)
        {
            this.services = services;
            this.tracker = new DependencyKeyTracker();
        }

        public override IDependencyRegistrar Register(DependencyDescription dependency)
        {
            ServiceDescriptor descriptor;

            if (dependency.KeyValue.IsEmpty() && this.tracker.HasKey(dependency.Contract) == false)
            {
                descriptor = new ServiceDescriptor(dependency.Contract, dependency.Implementation, dependency.Lifetime.Convert());
            }
            else
            {
                descriptor = this.RegisterNamed(dependency);
            }

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

            var descriptor = new ServiceDescriptor(dependency.Contract, Callback, dependency.Lifetime.Convert());

            this.services.Add(descriptor);
            return this;
        }

        public override IDependencyRegistrar RegisterInstance(DependencyDescription dependency, object instance)
        {
            var descriptor = new ServiceDescriptor(dependency.Contract, instance);

            this.services.Add(descriptor);
            return this;
        }

        private ServiceDescriptor RegisterNamed(DependencyDescription dependency)
        {
            if (this.tracker.HasKey(dependency.Contract) == false)
            {
                var enumerable = typeof(IEnumerable<>).MakeGenericType(dependency.Contract);
                var descriptor = new ServiceDescriptor(enumerable, provider => this.Resolve(provider, dependency.Contract),
                    dependency.Lifetime.Convert());

                this.services.Add(descriptor);
            }

            if (this.tracker.TryAdd(dependency) == false)
            {
                throw new InvalidOperationException($"Failed to add dependency: {dependency}.");
            }

            return new ServiceDescriptor(dependency.Contract, dependency.Implementation, dependency.Lifetime.Convert());
        }

        private IEnumerable<object> Resolve(IServiceProvider provider, Type contract)
        {
            return this.tracker.GetDependencies(contract)
                .Select(dependency => provider.GetService(dependency.Implementation));
        }
    }
}