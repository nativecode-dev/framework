namespace NativeCode.Core.Platform.Security
{
    using JetBrains.Annotations;

    public interface ISecretsProvider
    {
        [NotNull]
        byte[] GetSecret([NotNull] string name, [NotNull] string token);

        [NotNull]
        string GetSecretString([NotNull] string name, [NotNull] string token);
    }
}