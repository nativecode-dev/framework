namespace ServicesWeb.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using NativeCode.Core.Platform;

    using ServicesWeb.Models;

    [RoutePrefix("accounts")]
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
        public async Task<ActionResult> Login(LoginPanelModel model)
        {
            var principal = await this.platform.AuthenticateAsync(model.Login, model.Password, CancellationToken.None);

            if (principal != null)
            {
                this.platform.SetCurrentPrincipal(principal);
            }

            if (string.IsNullOrWhiteSpace(model.RedirectUrl))
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.Redirect(model.RedirectUrl);
        }
    }
}