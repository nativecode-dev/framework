namespace ServicesWeb.Controllers
{
    using System.Web.Mvc;

    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [Route("")]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}