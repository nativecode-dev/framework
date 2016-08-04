namespace NativeCode.Core.Data
{
    using System;

    public interface IRepositoryContext<out TContext> : IDisposable
    {
        /// <summary>
        /// Gets the data context.
        /// </summary>
        TContext DataContext { get; }
    }
}