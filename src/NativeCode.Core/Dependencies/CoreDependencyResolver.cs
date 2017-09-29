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

        public override IDependencyResolver CreateChildResolver()
        {
            var scope = this.provider.CreateScope();
            return new CoreDependencyResolver(scope.ServiceProvider);
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