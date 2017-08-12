namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Threading;

    /// <summary>
    /// Static helper that implements the service locator pattern.
    /// </summary>
    public static class DependencyLocator
    {
        private static int counter;

        /// <summary>
        /// Gets the root container.
        /// </summary>
        /// <value>The root container.</value>
        public static IDependencyContainer RootContainer { get; private set; }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <value>The resolver.</value>
        public static IDependencyResolver Resolver => RootContainer.Resolver;

        /// <summary>
        /// Sets the root container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <exception cref="System.InvalidOperationException">Cannot set the root container more than once.</exception>
        public static void SetRootContainer(IDependencyContainer container)
        {
            if (Interlocked.CompareExchange(ref counter, 1, 0) == 0)
                RootContainer = container;
            else
                throw new InvalidOperationException("Cannot set the root container more than once.");
        }
    }
}