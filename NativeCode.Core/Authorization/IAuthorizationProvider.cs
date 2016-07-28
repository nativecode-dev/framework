namespace NativeCode.Core.Authorization
{
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Authorization.Types;

    public interface IAuthorizationProvider
    {
        Task<IEnumerable<Feature>> GetFeaturesAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Permission>> GetFeaturePermissionsAsync(Feature feature, CancellationToken cancellationToken);

        Task<IEnumerable<Permission>> GetPermissionsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Role>> GetRolesAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Permission>> GetRolePermissionsAsync(Role role, CancellationToken cancellationToken);

        Task<IEnumerable<Feature>> GetFeaturesAsync(IPrincipal principal, CancellationToken cancellationToken);

        Task<IEnumerable<Permission>> GetFeaturePermissionsAsync(IPrincipal principal, Feature feature, CancellationToken cancellationToken);

        Task<IEnumerable<Permission>> GetPermissionsAsync(IPrincipal principal, CancellationToken cancellationToken);

        Task<IEnumerable<Role>> GetRolesAsync(IPrincipal principal, CancellationToken cancellationToken);

        Task<IEnumerable<Permission>> GetRolePermissionsAsync(IPrincipal principal, Role role, CancellationToken cancellationToken);
    }
}