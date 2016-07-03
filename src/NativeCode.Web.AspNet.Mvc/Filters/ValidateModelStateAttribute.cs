namespace NativeCode.Web.AspNet.Mvc.Filters
{
    using System;
    using System.Web.Mvc;

    using NativeCode.Core.Extensions;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller.ViewData.ModelState.IsValid == false)
            {
                context.Result = new RedirectToRouteResult(context.RouteData.Values);
            }

            base.OnActionExecuting(context);
        }
    }
}