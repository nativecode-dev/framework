namespace Common.Web.Services
{
    using System;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;
    using Common.DataServices;
    using Common.Models.Models;
    using Common.Models.Models.Downloads;

    using NativeCode.Core.Types;
    using NativeCode.Core.Types.Mappings;

    public class DownloadManager : Disposable, IDownloadManager
    {
        private readonly IDownloadService downloads;

        private readonly IMappingProvider mappings;

        private readonly IStorageService storage;

        public DownloadManager(IDownloadService downloads, IMappingProvider mappings, IStorageService storage)
        {
            this.downloads = downloads;
            this.mappings = mappings;
            this.storage = storage;
        }

        public async Task<QueueDownloadResponse> EnqueueDownloadAsync(QueueDownloadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var token = CancellationToken.None;
                var share = await this.storage.GetByNameAsync(request.Storage, token).ConfigureAwait(false);
                var download = new Download
                                   {
                                       Filename = request.Filename,
                                       Source = request.Source,
                                       Url = request.Url,
                                       Storage = share,
                                       Title = request.Title ?? request.Filename
                                   };

                await this.downloads.EnqueueAsync(download, token).ConfigureAwait(false);

                return Response.Succeed<QueueDownloadResponse>(x => x.Id = download.Key);
            }
            catch (Exception ex)
            {
                return Response.Fail<QueueDownloadResponse>(ex);
            }
        }

        public async Task<ResponseCollection<DownloadInfo>> GetDownloadsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var items = await this.downloads.GetDownloadsAsync(CancellationToken.None).ConfigureAwait(false);

                return
                    Response.Succeed<ResponseCollection<DownloadInfo>>(
                        response => response.Items = items.Select(i => this.mappings.Map<Download, DownloadInfo>(i)));
            }
            catch (Exception ex)
            {
                return Response.Fail<ResponseCollection<DownloadInfo>>(ex);
            }
        }

        public async Task<ResponseCollection<DownloadInfo>> GetUserDownloadsAsync(IPrincipal principal, CancellationToken cancellationToken)
        {
            try
            {
                var items = await this.downloads.GetUserDownloadsAsync(principal, CancellationToken.None).ConfigureAwait(false);
                return
                    Response.Succeed<ResponseCollection<DownloadInfo>>(
                        response => response.Items = items.Select(i => this.mappings.Map<Download, DownloadInfo>(i)));
            }
            catch (Exception ex)
            {
                return Response.Fail<ResponseCollection<DownloadInfo>>(ex);
            }
        }
    }
}