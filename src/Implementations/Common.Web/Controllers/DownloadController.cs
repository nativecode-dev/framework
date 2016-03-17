namespace Common.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Common.Data.Entities;
    using Common.DataServices;
    using Common.Web.Models.Downloads;

    using NativeCode.Core.Dependencies.Attributes;
    using NativeCode.Web.Models;

    [Dependency]
    [RoutePrefix("api/downloads")]
    public class DownloadController : ApiController
    {
        private readonly IDownloadService downloads;

        private readonly IStorageService storage;

        public DownloadController(IDownloadService downloads, IStorageService storage)
        {
            this.downloads = downloads;
            this.storage = storage;
        }

        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            return this.Ok();
        }

        [Route("")]
        public async Task<ResponseCollection<Download>> Get()
        {
            try
            {
                var items = await this.downloads.GetDownloadsAsync(CancellationToken.None).ConfigureAwait(false);

                return Response.Succeed<ResponseCollection<Download>>(x => x.Items = items);
            }
            catch (Exception ex)
            {
                return Response.Fail<ResponseCollection<Download>>(ex);
            }
        }

        [Route("{login}")]
        public async Task<ResponseCollection<DownloadInfo>> Get(string login)
        {
            try
            {
                if (this.User.Identity.Name.EndsWith(login))
                {
                    var items = await this.downloads.GetUserDownloadsAsync(this.User, CancellationToken.None).ConfigureAwait(false);

                    if (items != null)
                    {
                        return Response.Succeed<ResponseCollection<DownloadInfo>>(x => x.Items = items.Select(DownloadInfo.From));
                    }
                }

                return Response.Fail<ResponseCollection<DownloadInfo>>($"Failed to find account {login}.");
            }
            catch (Exception ex)
            {
                return Response.Fail<ResponseCollection<DownloadInfo>>(ex);
            }
        }

        [Route("")]
        public async Task<QueueDownloadResponse> Post(QueueDownloadRequest request)
        {
            try
            {
                var token = CancellationToken.None;
                var share = await this.storage.GetByNameAsync(request.Storage, token).ConfigureAwait(false);

                var download =
                    await
                    this.downloads.EnqueueAsync(
                        new Download
                            {
                                Filename = request.Filename,
                                Source = request.Source,
                                Url = request.Url,
                                Storage = share,
                                Title = request.Title ?? request.Filename
                            },
                        token).ConfigureAwait(false);

                return Response.Succeed<QueueDownloadResponse>(x => x.Id = download.Key);
            }
            catch (Exception ex)
            {
                return Response.Fail<QueueDownloadResponse>(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.downloads.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}