namespace NativeCode.Web.AspNet.WebApi.Handlers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;

    public class BasicAuthDelegatingHandler : DelegatingHandler
    {
        private readonly IPlatform platform;

        public BasicAuthDelegatingHandler()
            : this(DependencyLocator.Resolver.Resolve<IPlatform>())
        {
        }

        public BasicAuthDelegatingHandler(IPlatform platform)
        {
            this.platform = platform;
        }

        protected virtual async Task AuthenticateAsync(HttpRequestMessage request, string login, string password, CancellationToken cancellationToken)
        {
            var principal = await this.platform.AuthenticateAsync(login, password, cancellationToken).ConfigureAwait(false);

            if (principal == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            this.platform.SetCurrentPrincipal(principal);

            request.GetRequestContext().Principal = principal;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorization = request.Headers.Authorization;

            if (authorization != null)
            {
                EnsureMatchingScheme(authorization.Scheme);

                var parts = GetParameterParts(authorization);
                var login = parts[0];
                var password = parts[1];

                await this.AuthenticateAsync(request, login, password, cancellationToken).ConfigureAwait(false);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private static string[] GetParameterParts(AuthenticationHeaderValue authorization)
        {
            var bytes = Convert.FromBase64String(authorization.Parameter);
            var value = Encoding.UTF8.GetString(bytes);
            var parts = value.Split(':');

            if (parts.Length != 2)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return parts;
        }

        private static void EnsureMatchingScheme(string scheme)
        {
            if (scheme.ToUpper() != "BASIC")
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}