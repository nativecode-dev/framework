namespace NativeCode.Core.DotNet.Platform.Security
{
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform.Security;
    using NativeCode.Core.Platform.Security.Authorization;

    public class HmacSettingsProvider : IHmacSettingsProvider
    {
        public Task<string> GetUserSecretAsync(IPrincipal principal, CancellationToken cancellationToken)
        {
            return Task.FromResult(principal.Identity.Name);
        }
    }
}