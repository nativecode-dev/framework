namespace ServicesWeb.Filters
{
    using System.Web.Mvc;

    using Common;

    using NativeCode.Core.Extensions;
    using NativeCode.Web.AspNet.Mvc.Extensions;

    public class SiteMaintenanceAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ContainsAttribute<IgnoreSiteMaintenanceAttribute>().Not() && Database.UpgradeRequired)
            {
                var user = context.RequestContext.HttpContext.User;
                context.Result = user.IsAuthenticated() ? new RedirectResult("~/admin/database/upgrade") : new RedirectResult("~/maintenance");
            }

            base.OnActionExecuting(context);
        }
    }
}