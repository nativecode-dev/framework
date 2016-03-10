namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Threading;

    public static class DependencyLocator
    {
        private static int counter;

        public static IDependencyResolver Resolver { get; private set; }

        public static void SetRootContainer(IDependencyContainer container)
        {
            if (Interlocked.CompareExchange(ref counter, 1, 0) == 0)
            {
                Resolver = container.Resolver;
            }
            else
            {
                throw new InvalidOperationException("Cannot set the root container more than once.");
            }
        }
    }
}