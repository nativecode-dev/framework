namespace Common.Web.Filters
{
    using System.Web.Mvc;

    using NativeCode.Core.Extensions;
    using NativeCode.Web.AspNet.Mvc.Extensions;

    public class SiteMaintenanceAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ContainsAttribute<IgnoreSiteMaintenanceAttribute>().Not() && Database.UpgradeRequired)
            {
                var user = context.RequestContext.HttpContext.User;

                if (user != null && user.IsAuthenticated())
                {
                    context.Result = new RedirectResult("~/admin/database/upgrade");
                }
                else
                {
                    context.Result = new RedirectResult("~/maintenance");
                }
            }

            base.OnActionExecuting(context);
        }
    }
}