namespace NativeCode.Core.Annotations
{
    using System;

    /// <summary>
    /// Marks a class or method as requiring a refactor.
    /// This class cannot be inherited.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Annotations.CodeAnnotationAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class CodeRefactorAttribute : CodeAnnotationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeRefactorAttribute"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public CodeRefactorAttribute(string reason)
            : base(reason)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeRefactorAttribute"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="ticket">The ticket.</param>
        public CodeRefactorAttribute(string reason, string ticket)
            : base(reason, ticket)
        {
        }
    }
}