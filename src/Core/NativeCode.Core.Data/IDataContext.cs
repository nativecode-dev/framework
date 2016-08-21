namespace NativeCode.Core.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a contract for providing a data context.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// Saves pending changes.
        /// </summary>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        int SaveChanges();

        /// <summary>
        /// Saves pending changes.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}