namespace Common.DataServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;
    using Common.Data.Entities.Security;

    public interface ITokenService : IDataService<Token>
    {
        Task<Token> GetTokenAsync(Guid key, CancellationToken cancellationToken);

        Task<Token> NewTokenAsync(Account account, CancellationToken cancellationToken);
    }
}