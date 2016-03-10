namespace Common.DataServices
{
    using System.Data.Entity;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data;
    using Common.Data.Entities;

    using NativeCode.Core.Data;
    using NativeCode.Core.Platform;

    public class AccountService : DataService<Account>, IAccountService
    {
        private readonly IPrincipalFactory principals;

        public AccountService(IRepository<Account, CoreDataContext> repository, IPrincipalFactory principals)
            : base(repository)
        {
            this.principals = principals;
        }

        public Task<IPrincipal> CreatePrincipalAsync(Account account, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.principals.GetPrincipal(account.AccountSource, account.Login));
        }

        public Task<Account> FromPrincipalAsync(IPrincipal principal, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.SingleOrDefaultAsync(x => x.Login == principal.Identity.Name, cancellationToken);
        }
    }
}