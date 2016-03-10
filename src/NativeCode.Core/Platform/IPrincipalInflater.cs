namespace NativeCode.Core.Platform
{
    using System.Security.Principal;

    public interface IPrincipalInflater
    {
        bool CanInflate(PrincipalSource source);

        IPrincipal CreatePrincipal(string login);
    }
}