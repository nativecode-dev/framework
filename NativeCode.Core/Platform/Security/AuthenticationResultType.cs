namespace NativeCode.Core.Platform.Security
{
    public enum AuthenticationResultType
    {
        /// <summary>
        /// Specifies the default authentication result.
        /// </summary>
        Default = 0, 

        /// <summary>
        /// Specifies the authentication succeeded.
        /// </summary>
        Authenticated = 1, 

        /// <summary>
        /// Specifies the authentication was denied.
        /// </summary>
        Denied = Default, 

        /// <summary>
        /// Specifies the authentication has expired.
        /// </summary>
        Expired = 2, 

        /// <summary>
        /// Specifies the authentication failed.
        /// </summary>
        Failed = 3, 

        /// <summary>
        /// Specifies the account is locked.
        /// </summary>
        Locked = 4, 

        /// <summary>
        /// Specifies the account is marked.
        /// </summary>
        Marked = 5, 

        /// <summary>
        /// Specifies the account was not found.
        /// </summary>
        NotFound = 6
    }
}