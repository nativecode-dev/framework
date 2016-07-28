namespace NativeCode.Core.Web.Owin.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;

    public class OwinDependencyMiddleware : OwinMiddleware
    {
        private readonly IDependencyContainer container;

        public OwinDependencyMiddleware(Func<IDictionary<string, object>, Task> next, IPlatform platform)
            : this(next)
        {
            this.container = platform.CreateDependencyScope();
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