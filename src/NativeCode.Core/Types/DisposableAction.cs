namespace NativeCode.Core.Types
{
    using System;

    public class DisposableAction : Disposable
    {
        private readonly Action finalizer;

        public DisposableAction(Action finalizer)
        {
            this.finalizer = finalizer;
        }

        public DisposableAction(Action initializer, Action finalizer) : this(finalizer)
        {
            initializer();
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                this.finalizer();
            }

            base.Dispose(disposing);
        }
    }
}