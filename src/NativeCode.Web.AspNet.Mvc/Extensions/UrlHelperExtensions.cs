namespace NativeCode.Web.AspNet.Mvc.Extensions
{
    using System.Web.Mvc;

    public static class UrlHelperExtensions
    {
        public static string ActionRoot(this UrlHelper url, string actionName, string controllerName)
        {
            return url.Action(actionName, controllerName, new { Area = string.Empty });
        }
    }
}