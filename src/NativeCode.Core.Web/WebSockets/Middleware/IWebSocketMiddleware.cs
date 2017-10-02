namespace NativeCode.Core.Web.WebSockets.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IWebSocketMiddleware
    {
        Task Invoke(HttpContext context, Func<Task> next);
    }
}