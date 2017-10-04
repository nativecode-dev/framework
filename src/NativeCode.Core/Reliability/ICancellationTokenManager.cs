namespace NativeCode.Core.Reliability
{
    using System;
    using System.Threading;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract to manage <see cref="CancellationToken" />s.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ICancellationTokenManager : IDisposable
    {
        /// <summary>
        /// Cancels all active <see cref="CancellationToken" />s.
        /// </summary>
        /// <param name="throwOnCancel">if set to <c>true</c> throw on cancel.</param>
        void Cancel(bool throwOnCancel);

        /// <summary>
        /// Cancels the named <see cref="CancellationToken" />.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="throwOnCancel">if set to <c>true</c> throw on cancel.</param>
        void Cancel([NotNull] string name, bool throwOnCancel);

        /// <summary>
        /// Creates a new <see cref="CancellationToken" /> with a name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns a new <see cref="CancellationToken" />.</returns>
        CancellationToken CreateToken([NotNull] string name);

        /// <summary>
        /// Tries to create a new <see cref="CancellationToken" />.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="token">The token.</param>
        /// <returns><c>true</c> if token was created, <c>false</c> otherwise.</returns>
        bool TryCreateToken([NotNull] string name, out CancellationToken token);
    }
}