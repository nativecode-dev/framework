namespace Common.Web.Services
{
    using Common.Models.Models;
    using Common.Models.Models.Downloads;
    using System;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDownloadManager : IDisposable
    {
        Task<QueueDownloadResponse> EnqueueDownloadAsync(QueueDownloadRequest request, CancellationToken cancellationToken);

        Task<ResponseCollection<DownloadInfo>> GetDownloadsAsync(CancellationToken cancellationToken);

        Task<ResponseCollection<DownloadInfo>> GetUserDownloadsAsync(IPrincipal principal, CancellationToken cancellationToken);
    }
}
