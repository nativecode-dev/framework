namespace Common.DataServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;

    public interface ITokenService : IDataService<Token>
    {
        Task<Token> GetTokenAsync(Guid key, CancellationToken cancellationToken);

        Task<Token> NewTokenAsync(Account account, CancellationToken cancellationToken);
    }
}