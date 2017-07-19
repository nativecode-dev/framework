namespace NativeCode.Core.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class DisposableManager : Disposable
    {
        private readonly List<IDisposable> disposables = new List<IDisposable>();

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false && this.disposables.Any())
            {
                foreach (var disposable in this.disposables)
                    disposable.Dispose();

                this.disposables.Clear();
            }

            base.Dispose(disposing);
        }

        protected void EnsureDisposed<TDisposable>(TDisposable disposable) where TDisposable : IDisposable
        {
            if (this.disposables.Contains(disposable) == false)
                this.disposables.Add(disposable);
        }
    }
}