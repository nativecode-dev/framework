namespace NativeCode.Core.Platform.Security.KeyManagement
{
    /// <summary>
    /// Provides a contract to provide key management.
    /// </summary>
    public interface IKeyManager
    {
        /// <summary>
        /// Gets the default key.
        /// </summary>
        /// <returns>Returns a key.</returns>
        string GetDefaultKey();

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns a key.</returns>
        string GetKey(string name);
    }
}