namespace NativeCode.Core.Platform.Security
{
    using System.Security.Principal;

    public class ApplicationIdentity : IIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationIdentity" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public ApplicationIdentity(string name, string type)
        {
            this.AuthenticationType = type;
            this.Name = name;
        }

        /// <summary>
        /// Gets the type of the authentication.
        /// </summary>
        public string AuthenticationType { get; }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        public bool IsAuthenticated { get; } = true;

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        public string Name { get; }
    }
}