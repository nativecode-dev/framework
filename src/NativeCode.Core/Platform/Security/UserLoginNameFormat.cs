namespace NativeCode.Core.Platform.Security
{
    public enum UserLoginNameFormat
    {
        /// <summary>
        /// Specifies the default login format.
        /// </summary>
        Default = 0, 

        /// <summary>
        /// Specifies to use the NT LANMAN login format.
        /// </summary>
        Domain = 1, 

        /// <summary>
        /// Specifies to use the login only.
        /// </summary>
        Name = Default, 

        /// <summary>
        /// Specifies to use the ADS compatible UPN.
        /// </summary>
        UserPrincipalName = 2
    }
}