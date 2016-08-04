namespace NativeCode.Web.AspNet.WebApi.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using NativeCode.Core.Platform.Maintenance;

    public class MaintenanceFilterAttribute : ActionFilterAttribute
    {
        public MaintenanceFilterAttribute(IEnumerable<IMaintenanceProvider> providers)
        {
            this.Providers = providers;
        }

        protected IEnumerable<IMaintenanceProvider> Providers { get; }

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
            var immune = context.ActionDescriptor.GetCustomAttributes<MaintenanceIgnoreAttribute>();
            var providers = this.Providers.Where(p => p.Active).ToList();

            if (immune == null && providers.Any())
            {
                var message = providers.Select(provider => $"Maintenance required for {provider.Name}.").ToList();
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.TemporaryRedirect, string.Join(Environment.NewLine, message));
            }
        }
    }
}
