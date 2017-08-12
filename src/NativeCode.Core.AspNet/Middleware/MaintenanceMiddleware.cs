namespace NativeCode.Core.AspNet.Middleware
{
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Platform.Maintenance;

    public class MaintenanceMiddleware : BaseMiddleware
    {
        public MaintenanceMiddleware(RequestDelegate next) : base(next)
        {
        }

        protected override void PreInvoke(HttpContext context)
        {
            var maintenance = context.RequestServices.GetService<IMaintenanceProvider>();
            if (maintenance.Active)
                context.Response.StatusCode = (int) HttpStatusCode.ServiceUnavailable;
        }
    }
}