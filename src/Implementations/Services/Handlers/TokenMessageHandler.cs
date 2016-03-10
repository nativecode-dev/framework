namespace Services.Handlers
{
    using System;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.DataServices;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform;
    using NativeCode.Web.AspNet.WebApi.Handlers;

    public class TokenMessageHandler : PrincipalMessageHandler
    {
        private readonly IAccountService accounts;

        private readonly IPrincipalProvider principals;

        private readonly ITokenService tokens;

        public TokenMessageHandler(IAccountService accounts, IPrincipalProvider principals, ITokenService tokens)
        {
            this.accounts = accounts;
            this.principals = principals;
            this.tokens = tokens;
        }

        protected override async Task<IPrincipal> GetPrincipalAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorization = request.Headers.Authorization;

            if (authorization != null && authorization.Scheme.ToUpper() == "TOKEN")
            {
                Guid key;

                if (Guid.TryParse(authorization.Parameter.FromBase64String(), out key))
                {
                    var token = await this.tokens.GetTokenAsync(key, cancellationToken);
                    var principal = await this.accounts.CreatePrincipalAsync(token.Account, cancellationToken);

                    this.principals.SetCurrentPrincipal(principal);
                }
            }

            return Principal.Anonymous;
        }
    }
}