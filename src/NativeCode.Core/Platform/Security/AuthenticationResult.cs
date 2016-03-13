namespace NativeCode.Core.Platform.Security
{
    using System.Security.Principal;

    public class AuthenticationResult
    {
        public AuthenticationResult(AuthenticationResultType result, IPrincipal principal = null)
        {
            this.Principal = principal;
            this.Result = result;
        }

        public IPrincipal Principal { get; private set; }

        public AuthenticationResultType Result { get; private set; }
    }
}