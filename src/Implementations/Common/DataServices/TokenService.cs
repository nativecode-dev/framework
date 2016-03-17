namespace Common.DataServices
{
    using System;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data;
    using Common.Data.Entities;

    using NativeCode.Core.Data;

    internal class TokenService : DataService<Token>, ITokenService
    {
        public TokenService(IRepository<Token, CoreDataContext> repository)
            : base(repository)
        {
        }

        public virtual Task<Token> GetTokenAsync(Guid key, CancellationToken cancellationToken)
        {
            return this.Context.Tokens.Include(x => x.Account).FirstOrDefaultAsync(x => x.Key == key, cancellationToken);
        }

        public virtual async Task<Token> NewTokenAsync(Account account, CancellationToken cancellationToken)
        {
            var token = new Token { Account = account };

            if (await this.Context.SaveAsync(token, cancellationToken).ConfigureAwait(false))
            {
                return token;
            }

            throw new InvalidOperationException("Failed to create token for account.");
        }
    }
}