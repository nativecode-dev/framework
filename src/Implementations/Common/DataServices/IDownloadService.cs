namespace Common.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;
    using Common.Models.Models.Enums;

    public interface IDownloadService : IDataService<Download>
    {
        Task<bool> ChangeStateAsync(Guid key, DownloadState state, CancellationToken cancellationToken);

        Task<IEnumerable<Download>> ClaimAsync(int count, CancellationToken cancellationToken);

        Task<Download> EnqueueAsync(Download download, CancellationToken cancellationToken);

        Task<IEnumerable<Download>> GetDownloadsAsync(CancellationToken cancellationToken);

        Task<DownloadStats> GetDownloadStatsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Download>> GetResumableWorkForMachineAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Download>> GetRetryableWorkForMachineAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Download>> GetUserDownloadsAsync(IPrincipal principal, CancellationToken cancellationToken);

        Task<DownloadStats> GetUserDownloadStatsAsync(IPrincipal principal, CancellationToken cancellationToken);
    }
}