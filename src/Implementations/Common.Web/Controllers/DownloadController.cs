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

        public DownloadController(IDownloadService downloads)
        {
            this.downloads = downloads;
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
                var items = await this.downloads.GetDownloadsAsync(CancellationToken.None);

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
                var username = this.User.Identity.Name;

                if (string.Equals(username, login, StringComparison.CurrentCultureIgnoreCase))
                {
                    var items = await this.downloads.GetUserDownloadsAsync(this.User, CancellationToken.None);

                    return Response.Succeed<ResponseCollection<DownloadInfo>>(x => x.Items = items.Select(DownloadInfo.From));
                }
            }
            catch (Exception ex)
            {
                return Response.Fail<ResponseCollection<DownloadInfo>>(ex);
            }

            return Response.Fail<ResponseCollection<DownloadInfo>>($"Failed to find account {login}.");
        }

        [Route("")]
        public async Task<QueueDownloadResponse> Post(QueueDownloadRequest request)
        {
            try
            {
                var download =
                    await
                    this.downloads.EnqueueAsync(
                        new Download
                            {
                                Filename = request.Filename,
                                Path = request.Path,
                                Source = request.Source,
                                Url = request.Url,
                                Title = request.Title ?? request.Filename
                            },
                        CancellationToken.None);

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