namespace NativeCode.Core.Platform
{
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    public interface IPlatform
    {
        [NotNull]
        string ApplicationPath { get; }

        [NotNull]
        string DataPath { get; }

        [NotNull]
        string MachineName { get; }

        Task<IPrincipal> AuthenticateAsync(string login, string password, CancellationToken cancellationToken);

        IPrincipal CreatePrincipal(string login);

        IPrincipal GetCurrentPrincipal();

        void SetCurrentPrincipal(IPrincipal principal);
    }
}