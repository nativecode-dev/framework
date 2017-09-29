﻿namespace NativeCode.Core.Dependencies
{
    using System;

    public abstract class DependencyContainer : IDependencyContainer
    {
        public abstract IDependencyRegistrar Registrar { get; }

        protected bool Disposed { get; private set; }

        public abstract IDependencyContainer CreateChildContainer();

        public abstract IDependencyResolver CreateResolver();

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