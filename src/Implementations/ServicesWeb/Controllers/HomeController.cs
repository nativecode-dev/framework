namespace ServicesWeb.Controllers
{
    using System.Web.Http;
    using System.Web.Mvc;

    using Common.Models.Models;

    using ServicesWeb.Filters;

    [System.Web.Mvc.RoutePrefix("")]
    public class HomeController : Controller
    {
        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("")]
        public ActionResult Index()
        {
            return this.View();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("error")]
        [System.Web.Mvc.HttpGet]
        [IgnoreSiteMaintenance]
        public ActionResult Error([FromUri] ErrorModel error)
        {
            return this.View(error);
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("maintenance")]
        [System.Web.Mvc.HttpGet]
        [IgnoreSiteMaintenance]
        public ActionResult Maintenance()
        {
            return this.View();
        }
    }
}