namespace NativeCode.Core.Dependencies.Exceptions
{
    using System;

    using NativeCode.Core.Exceptions;

    public abstract class DependencyException : FrameworkException
    {
        protected DependencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}