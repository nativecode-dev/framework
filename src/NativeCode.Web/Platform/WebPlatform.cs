namespace NativeCode.Web.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Web;

    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.Platform.Security;

    public class WebPlatform : DotNetPlatform
    {
        public WebPlatform(IEnumerable<IAuthenticationProvider> authenticators)
            : base(authenticators)
        {
        }

        public override string ApplicationPath => HttpRuntime.AppDomainAppPath;

        public override string DataPath => HttpRuntime.BinDirectory;

        public override string MachineName => Environment.MachineName;

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