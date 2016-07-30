namespace NativeCode.Core.Platform.Security.Authorization.Exceptions
{
    using NativeCode.Core.Exceptions;

    public class AuthorizationAssertionException : FrameworkException
    {
        public AuthorizationAssertionException(string requirements)
            : base(CreateExceptionMessage(requirements))
        {
        }

        private static string CreateExceptionMessage(string requirements)
        {
            return $"Assertion failed while trying to resolve {requirements}.";
        }
    }
}