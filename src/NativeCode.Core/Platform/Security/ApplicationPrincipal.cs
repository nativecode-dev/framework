namespace NativeCode.Core.Platform.Security
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
    using Dependencies.Attributes;

    [IgnoreDependency]
    public sealed class ApplicationPrincipal : ClaimsPrincipal
    {
        /// <summary>
        /// Default anonymouse user.
        /// </summary>
        public static readonly ClaimsPrincipal Anonymous = ApplicationPrincipal.CreateAnonymousUser();

        /// <summary>
        /// Default system user.
        /// </summary>
        public static readonly ClaimsPrincipal System = ApplicationPrincipal.CreateSystemUser();

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationPrincipal" /> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        public ApplicationPrincipal(IIdentity identity) : base(identity)
        {
        }

        private static ClaimsPrincipal CreateAnonymousUser()
        {
            var claims = new[] { new Claim("system:anonymous", "anonymous") };
            return new ApplicationPrincipal(new AnonymousIdentity(claims));
        }

        private static ClaimsPrincipal CreateSystemUser()
        {
            var claims = new[] { new Claim("system:system", "system") };
            return new ApplicationPrincipal(new SystemIdentity(claims));
        }

        private class AnonymousIdentity : ClaimsIdentity
        {
            public AnonymousIdentity(IEnumerable<Claim> claims) : base(claims)
            {
            }

            public override bool IsAuthenticated => true;

            public override string Name => "anonymous@localhost";
        }

        private class SystemIdentity : ClaimsIdentity
        {
            public SystemIdentity(IEnumerable<Claim> claims) : base(claims)
            {
            }

            public override bool IsAuthenticated => true;

            public override string Name => "system@localhost";
        }
    }
}