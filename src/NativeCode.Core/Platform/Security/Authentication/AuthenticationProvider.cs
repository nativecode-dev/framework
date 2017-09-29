namespace NativeCode.Core.Platform.Security.Authentication
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class AuthenticationProvider : IAuthenticationProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationProvider" /> class.
        /// </summary>
        /// <param name="handlers">The handlers.</param>
        public AuthenticationProvider(IEnumerable<IAuthenticationHandler> handlers)
        {
            this.Handlers = handlers;
        }

        /// <summary>
        /// Gets the handlers.
        /// </summary>
        protected IEnumerable<IAuthenticationHandler> Handlers { get; }

        public Task<AuthenticationResult> AuthenticateAsync(string login, string password,
            CancellationToken cancellationToken)
        {
            foreach (var handler in this.Handlers.Where(h => h.CanHandle(login)))
            {
                return handler.AuthenticateAsync(login, password, cancellationToken);
            }

            return Task.FromResult(new AuthenticationResult(AuthenticationResultType.Failed));
        }
    }
}