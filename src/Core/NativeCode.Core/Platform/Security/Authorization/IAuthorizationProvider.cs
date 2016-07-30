namespace NativeCode.Core.Platform.Security.Authorization
{
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform.Security.Authorization.Types;

    /// <summary>
    /// Provides a contract to validate authorizations.
    /// </summary>
    public interface IAuthorizationProvider
    {
        /// <summary>
        /// Gets the collection of permissions for a given feature.
        /// </summary>
        /// <param name="feature">The feature.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of permissions.</returns>
        Task<IEnumerable<Permission>> GetFeaturePermissionsAsync(Feature feature, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of permissions for a given principal and feature.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="feature">The feature.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of permissions.</returns>
        Task<IEnumerable<Permission>> GetFeaturePermissionsAsync(IPrincipal principal, Feature feature, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of features.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of features.</returns>
        Task<IEnumerable<Feature>> GetFeaturesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of features for a given principal.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of features.</returns>
        Task<IEnumerable<Feature>> GetFeaturesAsync(IPrincipal principal, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of permissions.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of permissions.</returns>
        Task<IEnumerable<Permission>> GetPermissionsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of permissions for a given principal.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of permissions.</returns>
        Task<IEnumerable<Permission>> GetPermissionsAsync(IPrincipal principal, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of permissions for a given role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of permissions.</returns>
        Task<IEnumerable<Permission>> GetRolePermissionsAsync(Role role, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of permissions for a given principal and role.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="role">The role.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of permissions.</returns>
        Task<IEnumerable<Permission>> GetRolePermissionsAsync(IPrincipal principal, Role role, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of roles.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of roles.</returns>
        Task<IEnumerable<Role>> GetRolesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of roles for a given principal.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a collection of roles.</returns>
        Task<IEnumerable<Role>> GetRolesAsync(IPrincipal principal, CancellationToken cancellationToken);
    }
}