namespace NativeCode.Core.Reliability
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class DisposableManager : Disposable
    {
        private readonly List<IDisposable> disposables = new List<IDisposable>();

        protected void DeferDispose<TDisposable>(TDisposable disposable) where TDisposable : IDisposable
        {
            if (this.disposables.Contains(disposable) == false)
            {
                this.disposables.Add(disposable);
            }
        }

        protected override void ReleaseManaged()
        {
            if (this.disposables.Any())
            {
                foreach (var disposable in this.disposables)
                {
                    disposable.Dispose();
                }

                this.disposables.Clear();
            }
        }
    }
}