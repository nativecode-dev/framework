namespace NativeCode.Core.Platform
{
    using System.Security.Principal;

    public class Identity : IIdentity
    {
        public Identity(string name, string type)
        {
            this.AuthenticationType = type;
            this.Name = name;
        }

        public string AuthenticationType { get; }

        public bool IsAuthenticated { get; } = true;

        public string Name { get; }
    }
}