namespace NativeCode.Web.AspNet.WebApi.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
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
            this.ValidateMaintenance(actionContext);
            base.OnActionExecuting(actionContext);
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            this.ValidateMaintenance(actionContext);
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        private void ValidateMaintenance(HttpActionContext context)
        {
            var immune = context.ActionDescriptor.GetCustomAttributes<MaintenanceImmuneAttribute>();

            if (immune == null && this.Maintenance.Active)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.TemporaryRedirect, "Site has maintenance mode enabled.");
            }
        }
    }
}
