namespace Common.DataServices
{
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;

    public interface IAccountService
    {
        Task<IPrincipal> CreatePrincipalAsync(Account account, CancellationToken cancellationToken);

        Task<Account> FromPrincipalAsync(IPrincipal principal, CancellationToken cancellationToken);
    }
}