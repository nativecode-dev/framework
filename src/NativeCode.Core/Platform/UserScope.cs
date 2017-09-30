namespace NativeCode.Core.Platform
{
    using System.Security.Principal;
    using Reliability;

    internal class UserScope : Disposable, IUserScope
    {
        public UserScope(IApplicationScope scope, IPrincipal principal)
        {
            this.Principal = principal;
            this.Scope = scope;
        }

        public IPrincipal Principal { get; }

        public IApplicationScope Scope { get; }

        protected override void ReleaseManaged()
        {
            this.Scope.Dispose();
        }
    }
}