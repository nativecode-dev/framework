namespace NativeCode.Core.Platform.Security
{
    using System;

    public interface ICryptographer : IDisposable
    {
        byte[] Decrypt(byte[] cipherText, byte[] secret);

        string Decrypt(string cipherText, string secret);

        byte[] Encrypt(byte[] plainText, byte[] secret);

        string Encrypt(string plainText, string secret);
    }
}