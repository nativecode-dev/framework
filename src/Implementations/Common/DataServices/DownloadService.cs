namespace Common.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data;
    using Common.Data.Entities;
    using Common.Models.Models.Enums;

    using NativeCode.Core.Data;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Security;

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
                var downloads =
                    await
                    this.Context.Downloads.Where(x => x.State == DownloadState.Queued && x.ClaimMachineName == null)
                        .Take(count)
                        .ToListAsync(cancellationToken)
                        .ConfigureAwait(false);

                foreach (var download in downloads)
                {
                    download.ClaimMachineName = this.platform.MachineName;
                    download.State = DownloadState.Claimed;
                    results.Add(download);
                }

                if (await this.Context.SaveAsync(cancellationToken).ConfigureAwait(false))
                {
                    transaction.Complete();
                }
            }

            return results;
        }

        public async Task<Download> EnqueueAsync(Download download, CancellationToken cancellationToken)
        {
            var principal = this.platform.GetCurrentPrincipal();
            var account = await this.accounts.FromPrincipalAsync(principal, cancellationToken).ConfigureAwait(false);

            if (account == null)
            {
                throw new InvalidOperationException($"Account does not exist for the current user, {principal.Identity.Name}");
            }

            download.Account = account;

            if (await this.Context.SaveAsync(download, cancellationToken).ConfigureAwait(false))
            {
                return download;
            }

            throw new InvalidOperationException("Failed to save download entity.");
        }

        public Task<IEnumerable<Download>> GetDownloadsAsync(CancellationToken cancellationToken)
        {
            return this.Context.Downloads.Include(x => x.Storage)
                .ToListAsync(cancellationToken)
                .ContinueWith(x => (IEnumerable<Download>)x.Result, cancellationToken);
        }

        public async Task<DownloadStats> GetDownloadStatsAsync(CancellationToken cancellationToken)
        {
            var downloads = this.Context.Downloads;

            return new DownloadStats
                       {
                           Claimed = await downloads.CountAsync(x => x.State == DownloadState.Claimed, cancellationToken).ConfigureAwait(false),
                           Completed = await downloads.CountAsync(x => x.State == DownloadState.Completed, cancellationToken).ConfigureAwait(false),
                           Downloading =
                               await downloads.CountAsync(x => x.State == DownloadState.Downloading, cancellationToken).ConfigureAwait(false),
                           Failed = await downloads.CountAsync(x => x.State == DownloadState.Failed, cancellationToken).ConfigureAwait(false),
                           Queued = await downloads.CountAsync(x => x.State == DownloadState.Queued, cancellationToken).ConfigureAwait(false),
                           Retrying = await downloads.CountAsync(x => x.State == DownloadState.Retry, cancellationToken).ConfigureAwait(false),
                           Total = await downloads.CountAsync(cancellationToken).ConfigureAwait(false)
                       };
        }

        public Task<IEnumerable<Download>> GetResumableWorkForMachineAsync(CancellationToken cancellationToken)
        {
            return
                this.QueryByMachineName(x => x.State == DownloadState.Claimed || x.State == DownloadState.Downloading)
                    .ToListAsync(cancellationToken)
                    .ContinueWith(x => (IEnumerable<Download>)x.Result, cancellationToken);
        }

        public Task<IEnumerable<Download>> GetRetryableWorkForMachineAsync(CancellationToken cancellationToken)
        {
            return this.QueryByMachineName(x => x.State == DownloadState.Retry)
                .ToListAsync(cancellationToken)
                .ContinueWith(x => (IEnumerable<Download>)x.Result, cancellationToken);
        }

        public async Task<IEnumerable<Download>> GetUserDownloadsAsync(IPrincipal principal, CancellationToken cancellationToken)
        {
            var login = principal.Identity.Name;

            if (UserLoginName.IsValid(principal))
            {
                login = UserLoginName.Parse(principal.Identity.Name).Login;
            }

            return
                await this.Context.Downloads.Include(x => x.Storage).Where(x => x.Account.Login == login).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<DownloadStats> GetUserDownloadStatsAsync(IPrincipal principal, CancellationToken cancellationToken)
        {
            var account =
                await
                this.Context.Accounts.Include(x => x.Downloads)
                    .Include(x => x.Downloads.Select(d => d.Storage))
                    .SingleOrDefaultAsync(x => x.Login == principal.Identity.Name, cancellationToken)
                    .ConfigureAwait(false);

            if (account == null)
            {
                return new DownloadStats();
            }

            return new DownloadStats
                       {
                           Claimed = account.Downloads.Count(x => x.State == DownloadState.Claimed),
                           Completed = account.Downloads.Count(x => x.State == DownloadState.Completed),
                           Downloading = account.Downloads.Count(x => x.State == DownloadState.Downloading),
                           Failed = account.Downloads.Count(x => x.State == DownloadState.Failed),
                           Queued = account.Downloads.Count(x => x.State == DownloadState.Queued),
                           Retrying = account.Downloads.Count(x => x.State == DownloadState.Retry),
                           Total = account.Downloads.Count()
                       };
        }

        private IQueryable<Download> QueryByMachineName(Expression<Func<Download, bool>> filter)
        {
            return this.Context.Downloads.Where(filter).Where(x => x.ClaimMachineName == this.platform.MachineName);
        }
    }
}