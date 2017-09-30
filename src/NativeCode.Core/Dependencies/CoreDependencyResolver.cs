namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Collections.Generic;
    using Attributes;
    using Microsoft.Extensions.DependencyInjection;

    [IgnoreDependency]
    public class CoreDependencyResolver : DependencyResolver
    {
        private readonly IServiceProvider provider;

        internal CoreDependencyResolver(IServiceProvider provider)
        {
            this.provider = provider;
        }

        private CoreDependencyResolver(IServiceScope scope) : this(scope.ServiceProvider)
        {
            this.DeferDispose(scope);
        }

        public override IDependencyResolver CreateChildResolver()
        {
            return new CoreDependencyResolver(this.provider.CreateScope());
        }

        public override object Resolve(Type type, string key = null)
        {
            return this.provider.GetService(type);
        }

        public override IEnumerable<object> ResolveAll(Type type)
        {
            return this.provider.GetServices(type);
        }
    }
}