namespace Common.DataServices
{
    using System.Data.Entity;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data;
    using Common.Data.Entities;

    using NativeCode.Core.Data;
    using NativeCode.Core.DotNet.Types;
    using NativeCode.Core.Platform;

    public class AccountService : DataService<Account>, IAccountService
    {
        private readonly IPlatform platform;

        public AccountService(IRepository<Account, CoreDataContext> repository, IPlatform platform)
            : base(repository)
        {
            this.platform = platform;
        }

        public Task<IPrincipal> CreatePrincipalAsync(Account account, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.platform.CreatePrincipal(account.Login));
        }

        public Task<Account> FindAsync(string login, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.Include(x => x.Properties).SingleOrDefaultAsync(x => x.Login == login, cancellationToken);
        }

        public Task<Account> FromPrincipalAsync(IPrincipal principal, CancellationToken cancellationToken)
        {
            if (ActiveDirectoryName.IsValid(principal.Identity.Name))
            {
                var adname = ActiveDirectoryName.Parse(principal.Identity.Name);

                return this.Context.Accounts.SingleOrDefaultAsync(x => x.Login == adname.Account, cancellationToken);
            }

            return this.Context.Accounts.SingleOrDefaultAsync(x => x.Login == principal.Identity.Name, cancellationToken);
        }
    }
}