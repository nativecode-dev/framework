namespace NativeCode.Core.Web.Handlers.Owin
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Core.Dependencies;

    public class OwinDependencyMiddleware : OwinMiddleware
    {
        private readonly IDependencyContainer container;

        public OwinDependencyMiddleware(Func<IDictionary<string, object>, Task> next, IDependencyContainer container)
            : this(next)
        {
            this.container = container.CreateChildContainer();
        }

        private OwinDependencyMiddleware(Func<IDictionary<string, object>, Task> next)
            : base(next)
        {
        }

        protected override Task PostInvokeAsync(IDictionary<string, object> environment)
        {
            this.container.Dispose();
            return Task.FromResult(0);
        }

        protected override Task PreInvokeAsync(IDictionary<string, object> environment)
        {
            return Task.FromResult(0);
        }
    }
}