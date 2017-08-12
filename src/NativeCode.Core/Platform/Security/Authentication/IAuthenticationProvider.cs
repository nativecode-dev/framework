namespace NativeCode.Core.Platform.Security.Authentication
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Authenticates the specified credentials.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a <see cref="AuthenticationResult" />;.</returns>
        Task<AuthenticationResult> AuthenticateAsync(string login, string password,
            CancellationToken cancellationToken);
    }
}