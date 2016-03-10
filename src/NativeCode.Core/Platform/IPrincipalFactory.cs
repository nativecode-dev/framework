namespace NativeCode.Core.Platform
{
    using System.Security.Principal;

    public interface IPrincipalFactory
    {
        IPrincipalInflater GetInflater(PrincipalSource source);

        IPrincipal GetPrincipal(PrincipalSource source, string login);
    }
}