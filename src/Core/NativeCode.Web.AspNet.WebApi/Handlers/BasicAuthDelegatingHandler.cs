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
    using Core.Platform;
    using Core.Platform.Logging;
    using Core.Platform.Security.Authentication;

    /// <summary>
    /// Delegating handler that provides basic authentication via HTTP.
    /// </summary>
    /// <seealso cref="System.Net.Http.DelegatingHandler" />
    public class BasicAuthDelegatingHandler : DelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthDelegatingHandler" /> class.
        /// </summary>
        /// <param name="authentication">The authentication.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="platform">The platform.</param>
        public BasicAuthDelegatingHandler(IAuthenticationProvider authentication, ILogger logger, IPlatform platform)
        {
            this.Authentication = authentication;
            this.Logger = logger;
            this.Platform = platform;
        }

        /// <summary>
        /// Gets the authentication.
        /// </summary>
        protected IAuthenticationProvider Authentication { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the platform.
        /// </summary>
        protected IPlatform Platform { get; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var authorization = request.Headers.Authorization;

            if (authorization != null)
            {
                EnsureMatchingScheme(authorization.Scheme);

                var parts = GetParameterParts(authorization);
                var login = parts[0];
                var password = parts[1];

                var result = await this.Authentication.AuthenticateAsync(login, password, cancellationToken);

                if (result.Result != AuthenticationResultType.Authenticated)
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private static void EnsureMatchingScheme(string scheme)
        {
            if (scheme.ToUpper() != "BASIC")
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        private static string[] GetParameterParts(AuthenticationHeaderValue authorization)
        {
            var bytes = Convert.FromBase64String(authorization.Parameter);
            var value = Encoding.UTF8.GetString(bytes);
            var parts = value.Split(':');

            if (parts.Length != 2)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            return parts;
        }
    }
}