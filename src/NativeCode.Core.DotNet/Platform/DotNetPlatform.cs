namespace NativeCode.Core.DotNet.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Security;

    public class DotNetPlatform : IPlatform
    {
        private readonly IEnumerable<IAuthenticationProvider> authenticators;

        public DotNetPlatform(IEnumerable<IAuthenticationProvider> authenticators)
        {
            this.authenticators = authenticators;
        }

        public virtual string ApplicationPath => AppDomain.CurrentDomain.BaseDirectory;

        public virtual string DataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public virtual string MachineName => Environment.MachineName;

        public async Task<IPrincipal> AuthenticateAsync(string login, string password, CancellationToken cancellationToken)
        {
            foreach (var authenticator in this.authenticators)
            {
                if (authenticator.CanHandle(login))
                {
                    var response = await authenticator.AuthenticateAsync(login, password, cancellationToken).ConfigureAwait(false);

                    if (response.Result == AuthenticationResultType.Authenticated)
                    {
                        return response.Principal;
                    }
                }
            }

            return null;
        }

        public virtual IPrincipal CreatePrincipal(string login)
        {
            foreach (var authenticator in this.authenticators)
            {
                if (authenticator.CanHandle(login))
                {
                    return authenticator.CreatePrincipal(login);
                }
            }

            return Principal.Anonymous;
        }

        public virtual IPrincipal GetCurrentPrincipal()
        {
            if (string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name))
            {
                var identity = WindowsIdentity.GetCurrent();

                if (identity != null)
                {
                    return new WindowsPrincipal(identity);
                }

                return Principal.Anonymous;
            }

            return Thread.CurrentPrincipal;
        }

        public virtual void SetCurrentPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
        }
    }
}