namespace NativeCode.Core.Types
{
    using System;

    public abstract class Disposable : IDisposable
    {
        protected bool Disposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                this.Disposed = true;
            }
        }
    }
}