namespace NativeCode.Core.Platform
{
    using System.Security.Principal;
    using Dependencies;
    using Types;

    internal class ApplicationScope : DisposableManager, IApplicationScope
    {
        private readonly IDependencyContainer container;

        public ApplicationScope(IDependencyContainer container)
        {
            this.container = container.CreateChildContainer();
        }

        public virtual IApplicationScope CreateChildScope()
        {
            return new ApplicationScope(this.container.CreateChildContainer());
        }

        public IUserScope CreateUserScope(IPrincipal principal)
        {
            var scope = new UserScope(this, principal);

            try
            {
                this.EnsureDisposed(scope);
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
                this.container.Dispose();

            base.Dispose(disposing);
        }
    }
}