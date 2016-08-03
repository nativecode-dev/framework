namespace NativeCode.Web.AspNet.WebApi.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using NativeCode.Core.Dependencies.Attributes;
    using NativeCode.Core.Platform.Maintenance;

    [Dependency]
    public class MaintenanceFilterAttribute : ActionFilterAttribute
    {
        public MaintenanceFilterAttribute(IMaintainUpgradeState maintenance)
        {
            this.Maintenance = maintenance;
        }

        protected IMaintainUpgradeState Maintenance { get; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            this.ValidateMaintenance(actionContext.ActionDescriptor);
            base.OnActionExecuting(actionContext);
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            this.ValidateMaintenance(actionContext.ActionDescriptor);
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        private void ValidateMaintenance(HttpActionDescriptor action)
        {
            var immune = action.GetCustomAttributes<MaintenanceImmuneAttribute>();

            if (immune == null && this.Maintenance.Active)
            {
                var response = new HttpResponseMessage(HttpStatusCode.TemporaryRedirect);

                // TODO: Set the reponse header location.
                throw new HttpResponseException(response);
            }
        }
    }
}
