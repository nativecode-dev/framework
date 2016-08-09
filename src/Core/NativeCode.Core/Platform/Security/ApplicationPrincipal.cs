namespace NativeCode.Core.Platform.Security
{
    using System.Security.Principal;

    public sealed class ApplicationPrincipal : IPrincipal
    {
        /// <summary>
        /// Default anonymouse user.
        /// </summary>
        public static readonly IPrincipal Anonymous = new ApplicationPrincipal(new AnonymousIdentity());

        /// <summary>
        /// Default system user.
        /// </summary>
        public static readonly IPrincipal System = new ApplicationPrincipal(new SystemIdentity());

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationPrincipal" /> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        public ApplicationPrincipal(IIdentity identity)
        {
            this.Identity = identity;
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        public IIdentity Identity { get; }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>true if the current principal is a member of the specified role; otherwise, false.</returns>
        public bool IsInRole(string role)
        {
            return false;
        }

        private class AnonymousIdentity : IIdentity
        {
            public string AuthenticationType { get; } = null;

            public bool IsAuthenticated { get; } = true;

            public string Name { get; } = "anonymous@localhost";
        }

        private class SystemIdentity : IIdentity
        {
            public string AuthenticationType { get; } = null;

            public bool IsAuthenticated { get; } = true;

            public string Name { get; } = "system@localhost";
        }
    }
}