namespace Common.DataServices
{
    using System;

    using NativeCode.Core.Data;

    public interface IDataService<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
    }
}