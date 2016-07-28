namespace NativeCode.Core.Platform.Security
{
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IHmacSettingsProvider
    {
        Task<string> GetUserSecretAsync(IPrincipal principal, CancellationToken cancellationToken);
    }
}