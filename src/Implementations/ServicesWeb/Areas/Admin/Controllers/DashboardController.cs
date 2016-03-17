namespace ServicesWeb.Areas.Admin.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Common.DataServices;

    [RoutePrefix("")]
    public class DashboardController : ControllerBase
    {
        public DashboardController(IDownloadService downloads)
        {
            this.Downloads = downloads;
        }

        protected IDownloadService Downloads { get; private set; }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var stats = await this.Downloads.GetDownloadStatsAsync(CancellationToken.None).ConfigureAwait(false);

            return this.View(stats);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Downloads != null)
                {
                    this.Downloads.Dispose();
                    this.Downloads = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}