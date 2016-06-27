namespace NativeCode.Packages.Data
{
    using NativeCode.Core.Data;

    public abstract class DbDataFactory<TDataContext> : DataFactory
        where TDataContext : IDataContext
    {
        protected TDataContext Context { get; private set; }

        protected DbDataFactory(TDataContext context)
        {
            this.Context = context;
        }
    }
}