namespace ServicesWeb.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;

    using Common.Web.Filters;

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
        [Route("login")]
        [HttpPost]
        [IgnoreSiteMaintenance]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (Membership.ValidateUser(model.Login, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);

                if (string.IsNullOrWhiteSpace(model.RedirectUrl).Not())
                {
                    return this.Redirect(model.RedirectUrl);
                }
            }

            return this.RedirectToAction("Index", "Home");
        }

        [Route("logout")]
        [IgnoreSiteMaintenance]
        [HttpGet]
        public ActionResult Logout()
        {
            if (this.User != null && this.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}