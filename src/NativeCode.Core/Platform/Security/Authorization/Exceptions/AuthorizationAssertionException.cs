namespace NativeCode.Core.Platform.Security.Authorization.Exceptions
{
    using Core.Exceptions;

    public class AuthorizationAssertionException : FrameworkException
    {
        public AuthorizationAssertionException(string requirements)
            : base(AuthorizationAssertionException.CreateExceptionMessage(requirements))
        {
        }

        private static string CreateExceptionMessage(string requirements)
        {
            return $"Assertion failed while trying to resolve {requirements}.";
        }
    }
}