namespace NativeCode.Core.Platform.Security.Authorization.Types
{
    public struct Permission
    {
        public static readonly Permission AllowAnonymous = new Permission("AllowAnonymous");

        public static readonly Permission AllowImpersonateUser = new Permission("AllowImpersonateUser");

        public static readonly Permission AllowImpersonateSystem = new Permission("AllowImpersonateSystem");

        public Permission(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}