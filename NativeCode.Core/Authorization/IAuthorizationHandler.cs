namespace NativeCode.Core.Authorization
{
    /// <summary>
    /// Provides a contract for validating authorization.
    /// </summary>
    public interface IAuthorizationHandler
    {
        /// <summary>
        /// Determines if the state meets the requirements.
        /// </summary>
        /// <param name="requirements">The requirements.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if access is authorized, <c>false</c> otherwise.</returns>
        bool IsAuthorized(string requirements, object[] parameters);
    }
}