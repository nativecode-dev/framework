namespace ServicesWeb.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    [RoutePrefix("")]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}