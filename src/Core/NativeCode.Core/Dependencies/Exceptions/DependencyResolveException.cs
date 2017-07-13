namespace NativeCode.Core.Dependencies.Exceptions
{
    using System;

    /// <summary>
    /// Exception that describes the dependency container was unable to resolve the
    /// requested dependency.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Dependencies.Exceptions.DependencyException" />
    public class DependencyResolveException : DependencyException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyResolveException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DependencyResolveException(Type type)
            : this(type, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyResolveException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="innerException">The inner exception.</param>
        public DependencyResolveException(Type type, Exception innerException)
            : base(CreateExceptionMessage(type), innerException)
        {
        }

        private static string CreateExceptionMessage(Type type)
        {
            return $"Could not resolved type {type.FullName}.";
        }
    }
}