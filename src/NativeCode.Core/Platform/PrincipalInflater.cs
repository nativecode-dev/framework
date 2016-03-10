namespace NativeCode.Core.Platform
{
    using System.Security.Principal;

    public class PrincipalInflater : IPrincipalInflater
    {
        public bool CanInflate(PrincipalSource source)
        {
            return source == PrincipalSource.Generic;
        }

        public IPrincipal CreatePrincipal(string login)
        {
            return new Principal(new Identity(login));
        }
    }
}