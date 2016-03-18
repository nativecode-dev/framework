namespace Common.Web.Services
{
    using System;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Models.Models;
    using Common.Models.Models.Downloads;

    public interface IDownloadManager : IDisposable
    {
        Task<QueueDownloadResponse> EnqueueDownloadAsync(QueueDownloadRequest request, CancellationToken cancellationToken);

        Task<ResponseCollection<DownloadInfo>> GetDownloadsAsync(CancellationToken cancellationToken);

        Task<ResponseCollection<DownloadInfo>> GetUserDownloadsAsync(IPrincipal principal, CancellationToken cancellationToken);
    }
}