namespace Cavern.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using NativeCode.Core.Platform.Security.Authentication;

    public interface ILoginService : IService
    {
        [NotNull]
        Task<AuthenticationResult> AuthenticateAsync([NotNull] string username, [NotNull] string password,
            CancellationToken cancellationToken);
    }
}