namespace ServicesWeb.Controllers
{
    using System.Web.Mvc;

    [RoutePrefix("downloads")]
    public class DownloadController : Controller
    {
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}