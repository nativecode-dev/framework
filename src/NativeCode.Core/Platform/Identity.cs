namespace NativeCode.Core.Platform
{
    using System.Security.Principal;

    public class Identity : IIdentity
    {
        public Identity(string name)
            : this(name, PrincipalSource.Generic)
        {
        }

        public Identity(string name, PrincipalSource source)
        {
            this.AuthenticationType = source.ToString();
            this.Name = name;
        }

        public string AuthenticationType { get; }

        public bool IsAuthenticated { get; } = true;

        public string Name { get; }
    }
}