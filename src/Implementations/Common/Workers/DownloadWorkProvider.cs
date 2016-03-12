namespace Common.Workers
{
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

        public override Task<bool> BeginWorkAsync(Download entity, CancellationToken cancellationToken)
        {
            return this.downloads.ChangeStateAsync(entity.Key, DownloadState.Downloading, cancellationToken);
        }

        public override Task<bool> CompleteWorkAsync(Download entity, CancellationToken cancellationToken)
        {
            return this.downloads.ChangeStateAsync(entity.Key, DownloadState.Completed, cancellationToken);
        }

        public override Task<bool> FailWorkAsync(Download entity, CancellationToken cancellationToken)
        {
            return this.downloads.ChangeStateAsync(entity.Key, DownloadState.Failed, cancellationToken);
        }

        public override Task<IEnumerable<Download>> GetResumableWorkAsync(CancellationToken cancellationToken)
        {
            return this.downloads.GetResumableWorkForMachineAsync(cancellationToken);
        }

        public override Task<IEnumerable<Download>> GetRetryableWorkAsync(CancellationToken cancellationToken)
        {
            return this.downloads.GetRetryableWorkForMachineAsync(cancellationToken);
        }

        public override Task<IEnumerable<Download>> GetWorkAsync(int count, CancellationToken cancellationToken)
        {
            return this.downloads.ClaimAsync(count, cancellationToken);
        }
    }
}