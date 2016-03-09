namespace NativeCode.Core.Data
{
    public interface IEntityKeySetter<in TKey> where TKey : struct
    {
        void SetKey(TKey key);
    }
}