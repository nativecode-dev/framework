namespace NativeCode.Core.Platform
{
    using System.Security.Principal;

    public sealed class Principal : IPrincipal
    {
        public static readonly IPrincipal Anonymous = new Principal(new AnonymousIdentity());

        public static readonly IPrincipal System = new Principal(new SystemIdentity());

        public Principal(IIdentity identity)
        {
            this.Identity = identity;
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        public IIdentity Identity { get; }

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