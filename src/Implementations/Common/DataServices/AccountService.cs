namespace Common.DataServices
{
    using Common.Data;
    using Common.Data.Entities.Security;
    using NativeCode.Core.Data;
    using NativeCode.Core.Platform.Security;
    using System.Data.Entity;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    public class AccountService : DataService<Account>, IAccountService
    {
        public AccountService(IRepository<Account, CoreDataContext> repository)
            : base(repository)
        {
        }

        public async Task<Account> FindAsync(string login, CancellationToken cancellationToken)
        {
            return
                await this.Context.Accounts.Include(x => x.Properties)
                    .Where(x => x.Login == login || x.Properties.Any(p => p.Value == login))
                    .SingleOrDefaultAsync(x => x.Login == login, cancellationToken);
        }

        public async Task<Account> FromPrincipalAsync(IPrincipal principal, CancellationToken cancellationToken)
        {
            if (UserLoginName.IsValid(principal.Identity.Name, UserLoginNameFormat.UserPrincipalName))
            {
                var adname = UserLoginName.Parse(principal.Identity.Name);

                return await this.Context.Accounts.SingleOrDefaultAsync(x => x.Login == adname.Login, cancellationToken);
            }

            return await this.Context.Accounts.SingleOrDefaultAsync(x => x.Login == principal.Identity.Name, cancellationToken);
        }
    }
}
