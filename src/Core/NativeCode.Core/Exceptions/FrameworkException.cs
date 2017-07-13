namespace NativeCode.Core.Exceptions
{
    using System;

    /// <summary>
    /// Abstract class that represents exceptions thrown by the framework.
    /// You should always derive from this class for exceptions.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public abstract class FrameworkException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        protected FrameworkException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        protected FrameworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}