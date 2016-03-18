namespace Common.Platform
{
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.DataServices;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Security;

    public class AccountAuthenticationProvider : IAuthenticationProvider
    {
        private readonly IAccountService accounts;

        public AccountAuthenticationProvider(IAccountService accounts)
        {
            this.accounts = accounts;
        }

        public bool CanHandle(string login)
        {
            return true;
        }

        public IPrincipal CreatePrincipal(string login)
        {
            return new Principal(new Identity(login, "Database"));
        }

        public async Task<AuthenticationResult> AuthenticateAsync(string login, string password, CancellationToken cancellationToken)
        {
            IPrincipal principal = null;
            var result = AuthenticationResultType.Failed;
            var account = await this.accounts.FindAsync(login, cancellationToken).ConfigureAwait(false);

            if (account != null && (StringExtensions.AllEmpty(account.DomainHost, account.DomainName) && account.Password == password))
            {
                result = AuthenticationResultType.Authenticated;
                principal = this.CreatePrincipal(login);
            }

            return new AuthenticationResult(result, principal);
        }
    }
}