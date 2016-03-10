namespace NativeCode.Core.Dependencies.Exceptions
{
    using System;

    public abstract class DependencyException : Exception
    {
        protected DependencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}