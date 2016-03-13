namespace NativeCode.Core.Extensions
{
    using System.Security.Principal;

    public static class PrincipalExtensions
    {
        public static bool IsAuthenticated(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }
    }
}