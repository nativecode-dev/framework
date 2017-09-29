namespace NativeCode.Core.Types
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using Platform.Logging;

    public class CancellationTokenManager : DisposableManager, ICancellationTokenManager
    {
        private readonly ConcurrentDictionary<string, CancellationTokenSource> tokens =
            new ConcurrentDictionary<string, CancellationTokenSource>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CancellationTokenManager" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CancellationTokenManager(ILogger logger)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Cancels all active <see cref="CancellationToken" />s.
        /// </summary>
        /// <param name="throwOnCancel">if set to <c>true</c> throw on cancel.</param>
        public void Cancel(bool throwOnCancel)
        {
            foreach (var name in this.tokens.Keys)
            {
                this.Cancel(name, throwOnCancel);
            }

            this.tokens.Clear();
        }

        /// <summary>
        /// Cancels the named <see cref="CancellationToken" />.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="throwOnCancel">if set to <c>true</c> throw on cancel.</param>
        public void Cancel(string name, bool throwOnCancel)
        {
            CancellationTokenSource source;

            if (this.tokens.ContainsKey(name) && this.tokens.TryGetValue(name, out source))
            {
                source.Cancel();
                source.Dispose();
            }
        }

        /// <summary>
        /// Creates a new <see cref="CancellationToken" /> with a name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns a new <see cref="CancellationToken" />.</returns>
        /// <exception cref="InvalidOperationException">Failed to create a CancellationToken named {name}.</exception>
        public CancellationToken CreateToken(string name)
        {
            CancellationToken source;

            if (this.TryCreateToken(name, out source))
            {
                return source;
            }

            throw new InvalidOperationException($"Failed to create a CancellationToken named {name}.");
        }

        /// <summary>
        /// Tries to create a new <see cref="CancellationToken" />.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="token">The token.</param>
        /// <returns><c>true</c> if token was created, <c>false</c> otherwise.</returns>
        public bool TryCreateToken(string name, out CancellationToken token)
        {
            if (this.tokens.ContainsKey(name))
            {
                return false;
            }

            CancellationTokenSource source = null;

            try
            {
                source = new CancellationTokenSource();
                token = source.Token;

                if (this.tokens.TryAdd(name, source))
                {
                    return true;
                }

                source.Dispose();
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
                source?.Dispose();
            }

            return false;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                this.Cancel(false);
            }

            base.Dispose(disposing);
        }
    }
}