namespace Common.Web.Controllers
{
    using System.Web.Http;

    using NativeCode.Core.Dependencies.Attributes;

    [Dependency]
    [RoutePrefix("api")]
    public class DefaultController : ApiController
    {
        [AllowAnonymous]
        [Route("")]
        public IHttpActionResult Get()
        {
            return this.Ok();
        }
    }
}