namespace NativeCode.Web.AspNet.WebApi.Handlers
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform.Security.KeyManagement;

    public class BasicHeaderKeyDelegatingHandler : DelegatingHandler
    {
        private const string HttpHeaderId = "X-API-ID";

        private const string HttpHeaderKey = "X-API-KEY";

        public BasicHeaderKeyDelegatingHandler(IKeyManager keys)
        {
            this.Keys = keys;
        }

        protected IKeyManager Keys { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains(HttpHeaderKey))
            {
                var id = request.Headers.GetValues(HttpHeaderId).FirstOrDefault() ?? "default";
                var key = request.Headers.GetValues(HttpHeaderKey).FirstOrDefault();

                if (this.Keys.GetKey(id) != key)
                {
                    return Task.FromResult(request.CreateResponse(HttpStatusCode.Unauthorized));
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
