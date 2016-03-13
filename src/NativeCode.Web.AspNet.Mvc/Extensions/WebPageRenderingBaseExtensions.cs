namespace NativeCode.Web.AspNet.Mvc.Extensions
{
    using System.Web.WebPages;

    public static class WebPageRenderingBaseExtensions
    {
        public static bool IsAuthenticated(this WebPageRenderingBase page)
        {
            if (page.User == null || page.User.Identity == null)
            {
                return false;
            }

            return page.User.Identity.IsAuthenticated;
        }
    }
}