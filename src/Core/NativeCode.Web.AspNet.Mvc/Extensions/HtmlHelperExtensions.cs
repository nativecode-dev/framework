namespace NativeCode.Web.AspNet.Mvc.Extensions
{
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionLinkRoot(this HtmlHelper html, string linkText, string actionName,
            string controllerName)
        {
            return html.ActionLink(linkText, actionName, new { Area = string.Empty, Controller = controllerName });
        }
    }
}