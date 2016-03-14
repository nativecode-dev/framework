namespace ServicesWeb.Controllers
{
    using System.Web.Mvc;

    [RoutePrefix("profile")]
    public class ProfileController : Controller
    {
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}