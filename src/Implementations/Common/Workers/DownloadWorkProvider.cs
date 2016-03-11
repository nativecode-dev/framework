namespace Common.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;
    using Common.Data.Entities.Enums;
    using Common.DataServices;

    public class DownloadWorkProvider : WorkProvider<Download>
    {
        private readonly IDownloadService downloads;

        public DownloadWorkProvider(IDownloadService downloads)
        {
            this.downloads = downloads;
        }

        public override Task<bool> BeginWorkAsync(Guid key, CancellationToken cancellationToken)
        {
            return this.downloads.ChangeStateAsync(key, DownloadState.Downloading, cancellationToken);
        }

        public override Task<bool> CompleteWorkAsync(Guid key, CancellationToken cancellationToken)
        {
            return this.downloads.ChangeStateAsync(key, DownloadState.Completed, cancellationToken);
        }

        public override Task<bool> FailWorkAsync(Guid key, CancellationToken cancellationToken)
        {
            return this.downloads.ChangeStateAsync(key, DownloadState.Failed, cancellationToken);
        }

        public override Task<IEnumerable<Download>> GetWorkAsync(int count, CancellationToken cancellationToken)
        {
            return this.downloads.ClaimAsync(count, cancellationToken);
        }
    }
}