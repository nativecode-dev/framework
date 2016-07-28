namespace NativeCode.Core.Authorization.Exceptions
{
    using NativeCode.Core.Exceptions;

    public class AuthorizationAssertionFailedException : FrameworkException
    {
        public AuthorizationAssertionFailedException(string requirements)
            : base(CreateExceptionMessage(requirements))
        {
        }

        private static string CreateExceptionMessage(string requirements)
        {
            return $"Assertion failed while trying to resolve {requirements}.";
        }
    }
}