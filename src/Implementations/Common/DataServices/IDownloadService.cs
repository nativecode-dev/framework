namespace Common.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;
    using Common.Data.Entities.Enums;

    public interface IDownloadService : IDataService<Download>
    {
        Task<bool> ChangeStateAsync(Guid key, DownloadState state, CancellationToken cancellationToken);

        Task<IEnumerable<Download>> ClaimAsync(int count, CancellationToken cancellationToken);

        Task<Download> QueueAsync(string path, string filename, string url, CancellationToken cancellationToken);
    }
}