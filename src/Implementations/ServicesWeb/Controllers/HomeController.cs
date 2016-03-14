namespace ServicesWeb.Controllers
{
    using System.Web.Mvc;

    using Common.Web.Filters;

    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [Route("")]
        public ActionResult Index()
        {
            return this.View();
        }

        [AllowAnonymous]
        [Route("maintenance")]
        [HttpGet]
        [IgnoreSiteMaintenance]
        public ActionResult Maintenance()
        {
            return this.View();
        }
    }
}