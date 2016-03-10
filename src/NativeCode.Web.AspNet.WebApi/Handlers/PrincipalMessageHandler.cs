namespace NativeCode.Web.AspNet.WebApi.Handlers
{
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class PrincipalMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var principal = await this.GetPrincipalAsync(request, cancellationToken);

            if (principal != null)
            {
                var context = request.GetRequestContext();
                context.Principal = principal;
            }

            return await base.SendAsync(request, cancellationToken);
        }

        protected abstract Task<IPrincipal> GetPrincipalAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    }
}