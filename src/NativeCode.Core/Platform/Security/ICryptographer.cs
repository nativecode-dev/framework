namespace NativeCode.Core.Platform.Security
{
    using System;
    using JetBrains.Annotations;

    public interface ICryptographer : IDisposable
    {
        [NotNull]
        byte[] Decrypt([NotNull] byte[] cipherText, [NotNull] byte[] secret);

        [NotNull]
        string Decrypt([NotNull] string cipherText, [NotNull] string secret);

        [NotNull]
        byte[] Encrypt([NotNull] byte[] plainText, [NotNull] byte[] secret);

        [NotNull]
        string Encrypt([NotNull] string plainText, [NotNull] string secret);
    }
}