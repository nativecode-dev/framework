namespace NativeCode.Web.Platform
{
    using System;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;

    using NativeCode.Core.Platform;

    public class WebPlatform : IPlatform
    {
        public string ApplicationPath => HttpRuntime.AppDomainAppPath;

        public string DataPath => HttpRuntime.BinDirectory;

        public string MachineName => Environment.MachineName;

        public IPrincipal GetCurrentPrincipal()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.User;
            }

            return Principal.Anonymous;
        }

        public void SetCurrentPrincipal(IPrincipal principal)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            Thread.CurrentPrincipal = principal;
        }
    }
}