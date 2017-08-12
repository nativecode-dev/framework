namespace NativeCode.Core.Types
{
    using System;
    using System.Threading;

    /// <summary>
    /// Factory for managing a Lazy{T} instance.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <remarks>Note that this version is not thread-safe.</remarks>
    public class LazyFactory<T>
    {
        private readonly Func<T> factory;

        private Lazy<T> lazy;

        public LazyFactory(Func<T> factory)
            : this(factory, LazyThreadSafetyMode.ExecutionAndPublication)
        {
        }

        public LazyFactory(Func<T> factory, bool threadSafe)
            : this(factory, threadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
        {
        }

        public LazyFactory(Func<T> factory, LazyThreadSafetyMode threadSafetyMode)
        {
            this.factory = factory;
            this.lazy = new Lazy<T>(this.factory, threadSafetyMode);
        }

        public bool IsValueCreated => this.lazy.IsValueCreated;

        public T Value => this.lazy.Value;

        public void Reset()
        {
            var disposable = this.lazy.Value as IDisposable;

            this.lazy = new Lazy<T>(this.factory);

            disposable?.Dispose();
        }

        public override string ToString()
        {
            return this.lazy.ToString();
        }
    }
}