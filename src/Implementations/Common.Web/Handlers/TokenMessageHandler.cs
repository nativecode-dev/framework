namespace Common.Web.Handlers
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

        private readonly IPlatform platform;

        private readonly ITokenService tokens;

        public TokenMessageHandler(IAccountService accounts, IPlatform platform, ITokenService tokens)
        {
            this.accounts = accounts;
            this.platform = platform;
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

                    this.platform.SetCurrentPrincipal(principal);
                }
            }

            return Principal.Anonymous;
        }
    }
}