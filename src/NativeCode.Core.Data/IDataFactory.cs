namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a contract to manage code-first entity records.
    /// </summary>
    public interface IDataFactory
    {
        TEntity Create<TEntity, TKey>(TKey key, Action<TEntity> setter = null) where TEntity : class, IEntity<TKey>, new() where TKey : struct;

        void Seed<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void Seed<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
    }
}