namespace Common.DataServices
{
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;
    using Common.Data.Entities.Security;

    public interface IAccountService : IDataService<Account>
    {
        Task<Account> FindAsync(string login, CancellationToken cancellationToken);

        Task<Account> FromPrincipalAsync(IPrincipal principal, CancellationToken cancellationToken);
    }
}