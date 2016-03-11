namespace NativeCode.Core.Data
{
    public interface IEntityKeySetter<TKey> : IEntity<TKey>
        where TKey : struct
    {
        void SetKey(TKey key);
    }
}