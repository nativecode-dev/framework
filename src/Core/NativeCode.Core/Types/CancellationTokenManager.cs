namespace NativeCode.Core.Types
{
    using System.Threading;

    public class CancellationTokenManager : DisposableManager
    {
        private readonly CancellationTokenSource root = new CancellationTokenSource();

        public CancellationTokenManager()
        {
            this.EnsureDisposed(this.root);
        }

        public void Cancel()
        {
            this.root.Cancel();
            this.Dispose();
        }

        public CancellationToken CreateToken()
        {
            var source = new CancellationTokenSource();
            this.EnsureDisposed(source);

            return source.Token;
        }
    }
}
