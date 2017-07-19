namespace NativeCode.Web.AspNet.Mvc.Extensions
{
    using System.Security.Principal;
    using System.Web.WebPages;

    public static class WebPageRenderingBaseExtensions
    {
        public static bool IsAuthenticated(this WebPageRenderingBase page)
        {
            var principal = GetPrincipal(page);

            if (principal?.Identity != null)
                return true;

            return false;
        }

        public static bool IsAuthenticated(this WebPageRenderingBase page, string rolename)
        {
            var principal = GetPrincipal(page);

            if (principal?.Identity != null)
                return principal.IsInRole(rolename);

            return false;
        }

        private static IPrincipal GetPrincipal(WebPageRenderingBase page)
        {
            return page.User;
        }
    }
}