namespace NativeCode.Core.Packages.EntityFramework
{
    using NativeCode.Core.Data;

    public abstract class DbDataFactory<TDataContext> : DataFactory
        where TDataContext : IDataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbDataFactory{TDataContext}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected DbDataFactory(TDataContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected TDataContext Context { get; private set; }
    }
}