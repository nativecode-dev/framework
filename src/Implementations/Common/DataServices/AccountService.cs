namespace Common.DataServices
{
    using System.Data.Entity;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data;
    using Common.Data.Entities;

    using NativeCode.Core.Data;
    using NativeCode.Core.Platform.Security;

    public class AccountService : DataService<Account>, IAccountService
    {
        public AccountService(IRepository<Account, CoreDataContext> repository)
            : base(repository)
        {
        }

        public Task<Account> FindAsync(string login, CancellationToken cancellationToken)
        {
            return
                this.Context.Accounts.Include(x => x.Properties)
                    .Where(x => x.Login == login || x.Properties.Any(p => p.Value == login))
                    .SingleOrDefaultAsync(x => x.Login == login, cancellationToken);
        }

        public Task<Account> FromPrincipalAsync(IPrincipal principal, CancellationToken cancellationToken)
        {
            if (UserLoginName.IsValid(principal.Identity.Name, UserLoginNameFormat.UserPrincipalName))
            {
                var adname = UserLoginName.Parse(principal.Identity.Name);

                return this.Context.Accounts.SingleOrDefaultAsync(x => x.Login == adname.Login, cancellationToken);
            }

            return this.Context.Accounts.SingleOrDefaultAsync(x => x.Login == principal.Identity.Name, cancellationToken);
        }
    }
}