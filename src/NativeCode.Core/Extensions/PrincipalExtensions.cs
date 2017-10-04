namespace NativeCode.Core.Extensions
{
    using System.Security.Principal;
    using JetBrains.Annotations;

    public static class PrincipalExtensions
    {
        public static bool IsAuthenticated([CanBeNull] this IPrincipal principal, bool failOnEmptyIdentity = true)
        {
            if (principal == null || string.IsNullOrWhiteSpace(principal.Identity.Name) && failOnEmptyIdentity)
            {
                return false;
            }

            return principal.Identity.IsAuthenticated;
        }

        [NotNull]
        public static string Name([CanBeNull] this IPrincipal principal)
        {
            if (principal?.Identity != null)
            {
                return principal.Identity.Name;
            }

            return string.Empty;
        }
    }
}