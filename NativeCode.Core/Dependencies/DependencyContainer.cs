namespace NativeCode.Core.Dependencies
{
    using System;

    public abstract class DependencyContainer : IDependencyContainer
    {
        protected bool Disposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract IDependencyRegistrar Registrar { get; }

        public abstract IDependencyResolver Resolver { get; }

        public abstract IDependencyContainer CreateChildContainer();

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.Disposed = true;
                this.DisposeInstance();
            }
        }

        protected abstract void DisposeInstance();
    }
}