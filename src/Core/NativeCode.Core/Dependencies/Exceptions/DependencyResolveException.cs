namespace NativeCode.Core.Dependencies.Exceptions
{
    using System;

    public class DependencyResolveException : DependencyException
    {
        public DependencyResolveException(Type type)
            : this(type, null)
        {
        }

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