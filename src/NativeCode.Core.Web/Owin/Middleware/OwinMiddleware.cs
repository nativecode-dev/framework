namespace NativeCode.Core.Web.Owin.Middleware
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Microsoft.Owin;

    using OwinHandler = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

    public abstract class OwinMiddleware
    {
        private readonly OwinHandler next;

        protected OwinMiddleware(OwinHandler next)
        {
            this.next = next;
        }

        protected IOwinRequest Request { get; private set; }

        protected IOwinResponse Response { get; private set; }

        [SuppressMessage("ReSharper", "ConsiderUsingAsyncSuffix")]
        public async Task Invoke(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);
            this.Request = context.Request;
            this.Response = context.Response;

            try
            {
                await this.PreInvokeAsync(environment).ConfigureAwait(false);
                await this.next(environment).ConfigureAwait(false);
                await this.PostInvokeAsync(environment).ConfigureAwait(false);
            }
            finally
            {
                this.Request = null;
                this.Response = null;
            }
        }

        protected abstract Task PostInvokeAsync(IDictionary<string, object> environment);

        protected abstract Task PreInvokeAsync(IDictionary<string, object> environment);
    }
}