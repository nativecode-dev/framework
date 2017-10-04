namespace NativeCode.Core.Platform.Security
{
    using JetBrains.Annotations;

    public interface IHasher
    {
        [NotNull]
        byte[] Hash([NotNull] byte[] data);

        [NotNull]
        string Hash([NotNull] string data);
    }
}