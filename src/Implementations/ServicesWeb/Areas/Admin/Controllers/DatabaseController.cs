namespace ServicesWeb.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using Common;

    using NativeCode.Core.Dependencies;

    [RoutePrefix("database")]
    public class DatabaseController : ControllerBase
    {
        [Route("upgrade")]
        [HttpGet]
        public ActionResult UpgradeDatabase()
        {
            return this.View();
        }

        [Route("upgrade")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpgradeDatabase(string key)
        {
            try
            {
                Database.Upgrade(DependencyLocator.Resolver);
                return this.RedirectToAction("Index", "Home");
            }
            catch
            {
                return this.RedirectToAction("Index", "Home");
            }
        }
    }
}