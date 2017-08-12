namespace NativeCode.Core.Dependencies
{
    using System;

    public abstract class DependencyContainer : IDependencyContainer
    {
        public abstract IDependencyRegistrar Registrar { get; }

        public abstract IDependencyResolver Resolver { get; }

        protected bool Disposed { get; private set; }

        public abstract IDependencyContainer CreateChildContainer();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

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