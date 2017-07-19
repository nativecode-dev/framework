namespace NativeCode.Web.AspNet.Mvc.Extensions
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public static class ActionExecutingContextExtensions
    {
        public static bool ContainsAttribute<T>(this ActionExecutingContext context, bool inherit = true)
            where T : Attribute
        {
            return
                context.ActionDescriptor.GetCustomAttributes(inherit)
                    .Concat(context.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(inherit))
                    .OfType<T>()
                    .Any();
        }
    }
}