namespace Common.Web.Controllers
{
    using System.Web.Http;

    using Common.Web.Models.Info;

    using NativeCode.Core.Dependencies.Attributes;
    using NativeCode.Core.Platform;

    [Dependency]
    [RoutePrefix("api")]
    public class DefaultController : ApiController
    {
        private readonly IPlatform platform;

        public DefaultController(IPlatform platform)
        {
            this.platform = platform;
        }

        [AllowAnonymous]
        [Route("")]
        public AppInfo Get()
        {
            var user = this.User ?? this.platform.GetCurrentPrincipal();

            return new AppInfo { CurrentUser = user.Identity.Name, MachineName = this.platform.MachineName };
        }
    }
}