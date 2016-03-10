namespace NativeCode.Core.DotNet.Platform
{
    using System.Security.Principal;
    using System.Threading;

    using NativeCode.Core.Platform;

    public class DotNetPrincipalProvider : IPrincipalProvider
    {
        public virtual IPrincipal GetCurrentPrincipal()
        {
            return Thread.CurrentPrincipal;
        }

        public virtual void SetCurrentPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
        }
    }
}