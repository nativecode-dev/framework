namespace Common.Web.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

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

        [AllowAnonymous]
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            return this.Ok();
        }

        [AllowAnonymous]
        [Route("")]
        public async Task<QueueDownloadResponse> Post(QueueDownloadRequest request)
        {
            var download = await this.downloads.QueueAsync(request.Path, request.Filename, request.Url, CancellationToken.None);

            return Response.Succeed<QueueDownloadResponse>(x => x.Id = download.Key);
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