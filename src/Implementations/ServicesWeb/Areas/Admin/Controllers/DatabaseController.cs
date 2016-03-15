namespace ServicesWeb.Areas.Admin.Controllers
{
    using System;
    using System.Web.Mvc;

    using Common;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform;

    using ServicesWeb.Areas.Admin.Models;

    [RoutePrefix("database")]
    public class DatabaseController : ControllerBase
    {
        private readonly IApplication application;

        public DatabaseController(IApplication application)
        {
            this.application = application;
        }

        [Route("upgrade")]
        [HttpGet]
        public ActionResult UpgradeDatabase()
        {
            this.ViewBag.Token = this.application.Settings.GetValue<string>("Settings.DatabaseVerificationToken");

            return this.View(new DatabaseUpgradeModel());
        }

        [Route("upgrade")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpgradeDatabase(DatabaseUpgradeModel model)
        {
            model.UpgradeErrorMessage = null;

            try
            {
                Database.Upgrade(DependencyLocator.Resolver);
                return this.RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                model.UpgradeErrorMessage = ex.Stringify();
                return this.View(model);
            }
        }
    }
}