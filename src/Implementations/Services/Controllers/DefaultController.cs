namespace Services.Controllers
{
    using System.Web.Http;

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