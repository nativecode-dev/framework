namespace NativeCode.Core.Platform.Security
{
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAuthenticationProvider
    {
        bool CanHandle(string login);

        IPrincipal CreatePrincipal(string login);

        Task<AuthenticationResult> AuthenticateAsync(string login, string password, CancellationToken cancellationToken);
    }
}