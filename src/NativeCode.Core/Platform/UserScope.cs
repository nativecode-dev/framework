namespace NativeCode.Core.Platform
{
    using System.Security.Principal;
    using Types;

    internal class UserScope : Disposable, IUserScope
    {
        public UserScope(IApplicationScope scope, IPrincipal principal)
        {
            this.Principal = principal;
            this.Scope = scope;
        }

        public IPrincipal Principal { get; }

        public IApplicationScope Scope { get; }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                this.Scope.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}