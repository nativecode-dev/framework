namespace NativeCode.Core.Platform.Security
{
    public interface IHasher
    {
        byte[] Hash(byte[] data);

        string Hash(string data);
    }
}