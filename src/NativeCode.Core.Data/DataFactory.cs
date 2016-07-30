namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;

    public abstract class DataFactory : IDataFactory
    {
        public virtual TEntity Create<TEntity, TKey>(TKey key, Action<TEntity> setter) where TEntity : class, IEntity<TKey>, new() where TKey : struct
        {
            var entity = new TEntity();
            var keyset = entity as IEntityIdSetter<TKey>;
            keyset?.SetId(key);
            setter?.Invoke(entity);

            this.Seed<TEntity>(new[] { entity });

            return entity;
        }

        public void Seed<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            this.Seed<TEntity>(new[] { entity });
        }

        public abstract void Seed<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
    }
}