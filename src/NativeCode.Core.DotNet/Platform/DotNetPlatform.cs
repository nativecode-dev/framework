namespace NativeCode.Core.DotNet.Platform
{
    using System;
    using System.Security.Principal;
    using System.Threading;

    using NativeCode.Core.Platform;

    public class DotNetPlatform : IPlatform
    {
        public string ApplicationPath => AppDomain.CurrentDomain.BaseDirectory;

        public string DataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public string MachineName => Environment.MachineName;

        public IPrincipal GetCurrentPrincipal()
        {
            if (string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name))
            {
                return Principal.Anonymous;
            }

            return Thread.CurrentPrincipal;
        }

        public void SetCurrentPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
        }
    }
}