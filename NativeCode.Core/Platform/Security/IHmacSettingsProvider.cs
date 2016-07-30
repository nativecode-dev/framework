namespace NativeCode.Core.Platform.Security
{
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a contract for retrieving HMAC settings.
    /// </summary>
    public interface IHmacSettingsProvider
    {
        /// <summary>
        /// Gets the user secret.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns the user secret.</returns>
        Task<string> GetUserSecretAsync(IPrincipal principal, CancellationToken cancellationToken);
    }
}