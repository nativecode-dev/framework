namespace NativeCode.Core.Web.Owin.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Owin;

    using NativeCode.Core.Web.Authentication;

    public class OwinCookieMiddleware : OwinMiddleware
    {
        private readonly string name;

        private readonly CookieOptions options;

        public OwinCookieMiddleware(Func<IDictionary<string, object>, Task> next, string name, CookieOptions options)
            : base(next)
        {
            this.name = name;
            this.options = options;
        }

        protected override Task PostInvokeAsync(IDictionary<string, object> environment)
        {
            return Task.FromResult(0);
        }

        protected override Task PreInvokeAsync(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);
            var cookie = context.Request.GetCookie(this.name);

            if (cookie != null)
            {
                // TODO: Load principal somehow?
                return Task.FromResult(0);
            }

            return Task.FromResult(0);
        }
    }
}