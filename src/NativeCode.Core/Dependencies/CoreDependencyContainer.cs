namespace NativeCode.Core.Dependencies
{
    using System;
    using Attributes;
    using Microsoft.Extensions.DependencyInjection;

    [IgnoreDependency]
    public class CoreDependencyContainer : DependencyContainer
    {
        private IDependencyResolver resolver;

        internal CoreDependencyContainer(IServiceCollection services)
        {
            this.Registrar = new CoreDependencyRegistrar(services);
        }

        private CoreDependencyContainer()
        {
        }

        public override IDependencyRegistrar Registrar { get; }

        public override IDependencyContainer CreateChildContainer()
        {
            return new CoreDependencyContainer();
        }

        public override IDependencyResolver CreateResolver()
        {
            return this.resolver.CreateChildResolver();
        }

        public IServiceProvider Finalize(IServiceProvider provider)
        {
            return this.resolver = new CoreDependencyResolver(provider);
        }

        protected override void DisposeInstance()
        {
            this.resolver.Dispose();
        }
    }
}