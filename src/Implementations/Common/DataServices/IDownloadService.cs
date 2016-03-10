namespace Common.DataServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data;
    using Common.Data.Entities;

    using NativeCode.Core.Data;
    using NativeCode.Core.Platform;

    public interface IDownloadService
    {
        Task<Download> QueueAsync(string path, string filename, string url, CancellationToken cancellationToken);
    }

    public class DownloadService : DataService<Download>, IDownloadService
    {
        private readonly IAccountService accounts;

        private readonly IPrincipalProvider principals;

        public DownloadService(IRepository<Download, CoreDataContext> repository, IAccountService accounts, IPrincipalProvider principals)
            : base(repository)
        {
            this.accounts = accounts;
            this.principals = principals;
        }

        public async Task<Download> QueueAsync(string path, string filename, string url, CancellationToken cancellationToken)
        {
            var principal = this.principals.GetCurrentPrincipal();

            if (string.IsNullOrWhiteSpace(principal.Identity.Name))
            {
                throw new InvalidOperationException("Unknown user.");
            }

            var account = await this.accounts.FromPrincipalAsync(principal, cancellationToken);
            var download = new Download { Account = account, Filename = filename, Path = path, Title = filename, Url = url };

            if (await this.Context.SaveAsync(download, cancellationToken))
            {
                return download;
            }

            throw new InvalidOperationException("Failed to save download entity.");
        }
    }
}