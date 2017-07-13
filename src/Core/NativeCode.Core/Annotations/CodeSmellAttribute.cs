namespace NativeCode.Core.Annotations
{
    using System;

    /// <summary>
    /// Marks a target as being a suspected code smell.
    /// This class cannot be inherited.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Annotations.CodeAnnotationAttribute" />
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public sealed class CodeSmellAttribute : CodeAnnotationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeSmellAttribute"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public CodeSmellAttribute(string reason)
            : base(reason)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeSmellAttribute"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="ticket">The ticket.</param>
        public CodeSmellAttribute(string reason, string ticket)
            : base(reason, ticket)
        {
        }
    }
}