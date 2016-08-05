namespace NativeCode.Web.AspNet.WebApi.Filters
{
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using NativeCode.Core.Platform.Security.KeyManagement;
    using NativeCode.Core.Web;

    public class KeyAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        public KeyAuthorizationFilterAttribute(IKeyManager keyManager)
        {
            this.KeyManager = keyManager;
        }

        protected IKeyManager KeyManager { get; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            this.EnsureAuthorized(actionContext);
            base.OnAuthorization(actionContext);
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            this.EnsureAuthorized(actionContext);
            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }

        private void EnsureAuthorized(HttpActionContext context)
        {
            if (context.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() == false)
            {
                var key = context.Request.GetApiKey();
                var id = context.Request.GetApiKeyId() ?? "default";

                if (string.IsNullOrWhiteSpace(key) == false && key != this.KeyManager.GetKey(id))
                {
                    context.Response.StatusCode = HttpStatusCode.Unauthorized;
                }
            }
        }
    }
}
