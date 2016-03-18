namespace ServicesWeb.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;

    using Common.Models.Models;

    using NativeCode.Core.Extensions;

    using ServicesWeb.Filters;

    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [AllowAnonymous]
        [Route("login")]
        [IgnoreSiteMaintenance]
        [HttpGet]
        public ActionResult Login()
        {
            return this.View(new LoginModel());
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