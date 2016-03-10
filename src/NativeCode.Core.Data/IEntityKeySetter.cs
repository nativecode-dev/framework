namespace NativeCode.Core.Data
{
    internal interface IEntityKeySetter<in TKey>
        where TKey : struct
    {
        void SetKey(TKey key);
    }
}