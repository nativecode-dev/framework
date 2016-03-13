namespace Common.Web.Filters
{
    using System.Web.Mvc;

    using NativeCode.Core.Extensions;
    using NativeCode.Web.AspNet.Mvc.Extensions;

    public class InstallRedirectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ContainsAttribute<IgnoreInstallRedirectAttribute>().Not() && Database.UpgradeRequired)
            {
                context.Result = new RedirectResult("~/install");
            }

            base.OnActionExecuting(context);
        }
    }
}