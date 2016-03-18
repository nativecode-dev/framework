namespace NativeCode.Core.Web.Handlers.Owin
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Owin;

    using NativeCode.Core.Web.Authentication;

    public class OwinCookieMiddleware : OwinMiddleware
    {
        public OwinCookieMiddleware(Func<IDictionary<string, object>, Task> next)
            : base(next)
        {
        }

        protected override Task PostInvokeAsync(IDictionary<string, object> environment)
        {
            return Task.FromResult(0);
        }

        protected override Task PreInvokeAsync(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);
            var cookie = context.Request.GetCookie();

            if (cookie != null)
            {
                return Task.FromResult(0);
            }

            return Task.FromResult(0);
        }
    }
}