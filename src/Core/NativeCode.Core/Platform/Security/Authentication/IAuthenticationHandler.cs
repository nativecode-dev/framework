namespace NativeCode.Core.Platform.Security.Authentication
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a contract to authenticate a set of credentials.
    /// </summary>
    public interface IAuthenticationHandler
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

        /// <summary>
        /// Determines whether this instance can handle the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns><c>true</c> if this instance can handle the specified login; otherwise, <c>false</c>.</returns>
        bool CanHandle(string login);
    }
}