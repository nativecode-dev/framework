namespace NativeCode.Core.Web.Authorization
{
    using System.Web;

    using NativeCode.Core.Authorization;
    using NativeCode.Core.Authorization.Exceptions;
    using NativeCode.Core.Extensions;

    public class HttpContextRedirectAuthorizationHandler : IAuthorizationHandler
    {
        public void AssertDeny(string requirements, object[] parameters)
        {
            if (HttpContext.Current != null)
            {
                var url = parameters.Get<string>(0);
                var end = parameters.Get<bool>(1);

                HttpContext.Current.Response.Redirect(url, end);
            }

            throw new AuthorizationAssertionFailedException(requirements);
        }
    }
}