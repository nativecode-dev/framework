namespace ServicesWeb.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform;

    using ServicesWeb.Models;

    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        private readonly IPlatform platform;

        public AccountController(IPlatform platform)
        {
            this.platform = platform;
        }

        [AllowAnonymous]
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [AllowAnonymous]
        [Route("partials/login")]
        [HttpGet]
        public ActionResult LoginPanel()
        {
            return this.PartialView(new LoginPanelModel());
        }

        [AllowAnonymous]
        [Route("")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginPanelModel model)
        {
            if (Membership.ValidateUser(model.Login, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);

                if (string.IsNullOrWhiteSpace(model.RedirectUrl).Not())
                {
                    return this.Redirect(model.RedirectUrl);
                }

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("Index", "Account");
        }
    }
}