namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Threading;

    /// <summary>
    /// Static helper that implements the service locator pattern.
    /// </summary>
    public static class DependencyLocator
    {
        private static IDependencyContainer root;

        private static int counter;

        public static IDependencyResolver CreateResolver()
        {
            return DependencyLocator.root.CreateResolver();
        }

        public static IDependencyContainer GetContainer()
        {
            return DependencyLocator.root.CreateChildContainer();
        }

        /// <summary>
        /// Sets the root container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <exception cref="System.InvalidOperationException">Cannot set the root container more than once.</exception>
        public static void SetRootContainer(IDependencyContainer container)
        {
            if (Interlocked.CompareExchange(ref DependencyLocator.counter, 1, 0) == 0)
            {
                DependencyLocator.root = container;
            }
            else
            {
                throw new InvalidOperationException("Cannot set the root container more than once.");
            }
        }
    }
}