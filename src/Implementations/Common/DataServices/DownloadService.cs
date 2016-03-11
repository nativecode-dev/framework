namespace Common.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data;
    using Common.Data.Entities;
    using Common.Data.Entities.Enums;

    using NativeCode.Core.Data;
    using NativeCode.Core.Platform;

    public class DownloadService : DataService<Download>, IDownloadService
    {
        private readonly IAccountService accounts;

        private readonly IPlatform platform;

        public DownloadService(IAccountService accounts, IPlatform platform, IRepository<Download, CoreDataContext> repository)
            : base(repository)
        {
            this.accounts = accounts;
            this.platform = platform;
        }

        public Task<bool> ChangeStateAsync(Guid key, DownloadState state, CancellationToken cancellationToken)
        {
            var download = this.Context.Downloads.Find(key);

            if (download != null)
            {
                download.State = state;
                return this.Context.SaveAsync(cancellationToken);
            }

            return Task.FromResult(false);
        }

        public async Task<IEnumerable<Download>> ClaimAsync(int count, CancellationToken cancellationToken)
        {
            var results = new List<Download>();

            using (var transaction = this.CreateTransactionScope())
            {
                var downloads = await this.Context.Downloads.Where(x => x.State == DownloadState.Queued).Take(count).ToListAsync(cancellationToken);

                foreach (var download in downloads)
                {
                    download.MachineName = this.platform.MachineName;
                    download.State = DownloadState.Claimed;
                    results.Add(download);
                }

                if (await this.Context.SaveAsync(cancellationToken))
                {
                    transaction.Complete();
                }
            }

            return results;
        }

        public async Task<Download> QueueAsync(string path, string filename, string url, CancellationToken cancellationToken)
        {
            var principal = this.platform.GetCurrentPrincipal();

            if (string.IsNullOrWhiteSpace(principal.Identity.Name))
            {
                principal = Principal.Anonymous;
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