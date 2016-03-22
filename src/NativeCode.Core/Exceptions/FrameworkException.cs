namespace NativeCode.Core.Exceptions
{
    using System;

    public abstract class FrameworkException : Exception
    {
        protected FrameworkException(string message)
            : base(message)
        {
        }

        protected FrameworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}