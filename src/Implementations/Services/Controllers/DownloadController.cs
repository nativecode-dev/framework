namespace Services.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Common.DataServices;

    using NativeCode.Web.Models;

    using Services.Models.Downloads;

    [RoutePrefix("api/downloads")]
    public class DownloadController : ApiController
    {
        private readonly IDownloadService downloads;

        public DownloadController(IDownloadService downloads)
        {
            this.downloads = downloads;
        }

        [AllowAnonymous]
        public async Task<QueueDownloadResponse> Post(QueueDownloadRequest request)
        {
            var download = await this.downloads.QueueAsync(request.Path, request.Filename, request.Url, CancellationToken.None);

            return Response.Succeed<QueueDownloadResponse>(x => x.Id = download.Key);
        }
    }
}