﻿namespace NativeCode.Core.Platform
{
    using System.Security.Principal;
    using Dependencies;
    using Reliability;

    internal class ApplicationScope : DisposableManager, IApplicationScope
    {
        private readonly IDependencyContainer container;

        public ApplicationScope(IDependencyContainer container)
        {
            this.container = container;
        }

        public virtual IApplicationScope CreateChildScope()
        {
            return new ApplicationScope(this.container.CreateChildContainer());
        }

        public IDependencyResolver CreateResolver()
        {
            return this.container.CreateResolver();
        }

        public IUserScope CreateUserScope(IPrincipal principal)
        {
            var scope = new UserScope(this, principal);

            try
            {
                this.DeferDispose(scope);
                return scope;
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                this.container.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}