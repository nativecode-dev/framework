namespace NativeCode.Core.Platform
{
    using System.Security.Principal;

    public interface IPrincipalProvider
    {
        IPrincipal GetCurrentPrincipal();

        void SetCurrentPrincipal(IPrincipal principal);
    }
}