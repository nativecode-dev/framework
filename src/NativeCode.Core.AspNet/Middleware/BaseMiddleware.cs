namespace NativeCode.Core.AspNet.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public abstract class BaseMiddleware
    {
        protected BaseMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        protected RequestDelegate Next { get; }

        protected virtual async Task Invoke(HttpContext context)
        {
            this.PreInvoke(context);
            await this.Next(context);
            this.PostInvoke(context);
        }

        protected virtual void PostInvoke(HttpContext context)
        {
        }

        protected virtual void PreInvoke(HttpContext context)
        {
        }
    }
}