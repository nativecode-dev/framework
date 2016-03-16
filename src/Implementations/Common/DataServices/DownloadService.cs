﻿namespace Common.DataServices
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
                var downloads =
                    await this.Context.Downloads.Where(x => x.State == DownloadState.Queued && x.MachineName == null).Take(count).ToListAsync(cancellationToken);

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

        public async Task<Download> EnqueueAsync(Download download, CancellationToken cancellationToken)
        {
            var principal = this.platform.GetCurrentPrincipal();
            var account = await this.accounts.FromPrincipalAsync(principal, cancellationToken);

            if (account == null)
            {
                throw new InvalidOperationException($"Account does not exist for the current user, {principal.Identity.Name}");
            }

            download.Account = account;

            if (await this.Context.SaveAsync(download, cancellationToken))
            {
                return download;
            }

            throw new InvalidOperationException("Failed to save download entity.");
        }

        public Task<IEnumerable<Download>> GetDownloadsAsync(CancellationToken cancellationToken)
        {
            return this.Context.Downloads.ToListAsync(cancellationToken).ContinueWith(x => (IEnumerable<Download>)x.Result, cancellationToken);
        }

        public async Task<DownloadStats> GetDownloadStatsAsync(CancellationToken cancellationToken)
        {
            return new DownloadStats
                       {
                           Claimed = await this.Context.Downloads.CountAsync(x => x.State == DownloadState.Claimed, cancellationToken),
                           Completed = await this.Context.Downloads.CountAsync(x => x.State == DownloadState.Completed, cancellationToken),
                           Downloading = await this.Context.Downloads.CountAsync(x => x.State == DownloadState.Downloading, cancellationToken),
                           Failed = await this.Context.Downloads.CountAsync(x => x.State == DownloadState.Failed, cancellationToken),
                           Queued = await this.Context.Downloads.CountAsync(x => x.State == DownloadState.Queued, cancellationToken),
                           Retrying = await this.Context.Downloads.CountAsync(x => x.State == DownloadState.Retry, cancellationToken),
                           Total = await this.Context.Downloads.CountAsync(cancellationToken)
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
            return await this.Context.Downloads.Where(x => x.Account.Login == principal.Identity.Name).ToListAsync(cancellationToken);
        }

        public async Task<DownloadStats> GetUserDownloadStatsAsync(IPrincipal principal, CancellationToken cancellationToken)
        {
            var account = await this.Context.Accounts.Include(x => x.Downloads).SingleOrDefaultAsync(x => x.Login == principal.Identity.Name, cancellationToken);

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
            return this.Context.Downloads.Where(filter).Where(x => x.MachineName == this.platform.MachineName);
        }
    }
}