namespace NativeCode.Core.DotNet.Platform
{
    using System.Security.Principal;

    using NativeCode.Core.DotNet.Types;
    using NativeCode.Core.Platform;

    public class WindowsPrincipalInflater : IPrincipalInflater
    {
        public bool CanInflate(PrincipalSource source)
        {
            return source == PrincipalSource.Windows;
        }

        public IPrincipal CreatePrincipal(string login)
        {
            ActiveDirectoryName account;

            if (ActiveDirectoryName.TryParse(login, out account))
            {
                return new WindowsPrincipal(new WindowsIdentity(login));
            }

            return Principal.Anonymous;
        }
    }
}