namespace NativeCode.Core.Platform.Security
{
    public interface ISecretsProvider
    {
        byte[] GetSecret(string name, string token);

        string GetSecretString(string name, string token);
    }
}