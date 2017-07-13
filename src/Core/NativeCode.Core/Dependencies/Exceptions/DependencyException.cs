namespace NativeCode.Core.Dependencies.Exceptions
{
    using System;

    using NativeCode.Core.Exceptions;

    /// <summary>
    /// Abstract class for dependency exceptions.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Exceptions.FrameworkException" />
    public abstract class DependencyException : FrameworkException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        protected DependencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}