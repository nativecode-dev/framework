﻿namespace NativeCode.Core.Platform.Security.Authentication
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
        Denied = AuthenticationResultType.Default,

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
        /// Specifies the account was not found.
        /// </summary>
        NotFound = 6
    }
}