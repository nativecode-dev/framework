namespace ServicesWeb.Api.Handlers
{
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.DataServices;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Security;
    using NativeCode.Web.AspNet.WebApi.Handlers;

    public class AccountAuthDelegatingHandler : BasicAuthDelegatingHandler
    {
        private readonly IAccountService accounts;

        public AccountAuthDelegatingHandler(IAccountService accounts)
        {
            this.accounts = accounts;
        }

        protected override async Task AuthenticateAsync(HttpRequestMessage request, string login, string password, CancellationToken cancellationToken)
        {
            if (UserLoginName.IsValid(login, UserLoginNameFormat.UserPrincipalName).Not())
            {
                var account = await this.accounts.FindAsync(login, cancellationToken).ConfigureAwait(false);
                var property = account?.Properties.SingleOrDefault(x => x.Name == "UserPrincipalName");

                if (property != null)
                {
                    login = property.Value;
                }
            }

            await base.AuthenticateAsync(request, login, password, cancellationToken).ConfigureAwait(false);
        }
    }
}