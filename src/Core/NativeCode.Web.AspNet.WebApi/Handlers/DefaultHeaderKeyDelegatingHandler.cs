namespace NativeCode.Web.AspNet.WebApi.Handlers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform.Security.KeyManagement;

    public class DefaultHeaderKeyDelegatingHandler : DelegatingHandler
    {
        private const string HttpHeaderKey = "X-API-KEY";

        private static readonly Lazy<string> DefaultHeaderKey = new Lazy<string>(CreateDefaultHeaderKeyValue);

        public bool IsValid(string key)
        {
            return DefaultHeaderKey.Value == key;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains(HttpHeaderKey))
            {
                var key = request.Headers.GetValues(HttpHeaderKey).FirstOrDefault();

                if (this.IsValid(key) == false)
                {
                }
            }

            return base.SendAsync(request, cancellationToken);
        }

        private static string CreateDefaultHeaderKeyValue()
        {
            var keys = DependencyLocator.Resolver.Resolve<IKeyManager>();
            return keys.GetDefaultKey();
        }
    }
}
