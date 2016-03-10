namespace NativeCode.Core.Platform
{
    using System.Security.Principal;
    using System.Threading;

    using NativeCode.Core.Types;

    public class PrincipalProvider : Disposable, IPrincipalProvider
    {
        private readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        private IPrincipal current;

        public IPrincipal GetCurrentPrincipal()
        {
            this.locker.EnterReadLock();

            try
            {
                return this.current;
            }
            finally
            {
                this.locker.ExitReadLock();
            }
        }

        public void SetCurrentPrincipal(IPrincipal principal)
        {
            this.locker.EnterWriteLock();

            try
            {
                this.current = principal;
            }
            finally
            {
                this.locker.ExitWriteLock();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.locker.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}