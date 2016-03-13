namespace ServicesWeb.Controllers
{
    using System.Web.Mvc;

    using Common;
    using Common.Web.Filters;

    using NativeCode.Core.Dependencies;

    using ServicesWeb.Models;

    [AllowAnonymous]
    [RoutePrefix("install")]
    [IgnoreInstallRedirect]
    public class InstallController : Controller
    {
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            if (Database.UpgradeRequired)
            {
                return this.View(new InstallationModel());
            }

            return this.RedirectToAction("Index", "Home");
        }

        [Route("")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InstallationModel model)
        {
            try
            {
                Database.Upgrade(DependencyLocator.Resolver);
                return this.RedirectToAction("Index", "Home");
            }
            catch
            {
                return this.View(model);
            }
        }
    }
}