namespace ServicesWeb.Api.Controllers
{
    using Common.Models.Models;
    using Common.Models.Models.Downloads;
    using Common.Web.Services;
    using NativeCode.Core.Dependencies.Attributes;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    [Dependency]
    [RoutePrefix("api/downloads")]
    public class DownloadController : ApiController
    {
        private readonly IDownloadManager downloads;

        public DownloadController(IDownloadManager downloads)
        {
            this.downloads = downloads;
        }

        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            return this.Ok();
        }

        [Route("")]
        public Task<ResponseCollection<DownloadInfo>> Get()
        {
            return this.downloads.GetDownloadsAsync(CancellationToken.None);
        }

        [Route("{login}")]
        public Task<ResponseCollection<DownloadInfo>> Get(string login)
        {
            return this.downloads.GetUserDownloadsAsync(this.User, CancellationToken.None);
        }

        [Route("")]
        public Task<QueueDownloadResponse> Post(QueueDownloadRequest request)
        {
            return this.downloads.EnqueueDownloadAsync(request, CancellationToken.None);
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
