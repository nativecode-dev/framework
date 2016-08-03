namespace NativeCode.Web.AspNet.WebApi.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (this.ValidateModelState(actionContext))
            {
                base.OnActionExecuting(actionContext);
            }
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (this.ValidateModelState(actionContext))
            {
                return Task.FromResult(0);
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        private bool ValidateModelState(HttpActionContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.ModelState);
                return true;
            }

            return false;
        }
    }
}