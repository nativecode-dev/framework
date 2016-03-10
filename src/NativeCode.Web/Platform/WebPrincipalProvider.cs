namespace NativeCode.Web.Platform
{
    using System.Security.Principal;
    using System.Web;

    using NativeCode.Core.DotNet.Platform;

    public class WebPrincipalProvider : DotNetPrincipalProvider
    {
        public override IPrincipal GetCurrentPrincipal()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.User;
            }

            return base.GetCurrentPrincipal();
        }

        public override void SetCurrentPrincipal(IPrincipal principal)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            base.SetCurrentPrincipal(principal);
        }
    }
}