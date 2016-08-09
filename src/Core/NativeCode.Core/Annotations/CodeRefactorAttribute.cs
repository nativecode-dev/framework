namespace NativeCode.Core.Annotations
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class CodeRefactorAttribute : CodeAnnotationAttribute
    {
        public CodeRefactorAttribute(string reason)
            : base(reason)
        {
        }

        public CodeRefactorAttribute(string reason, string ticket)
            : base(reason, ticket)
        {
        }
    }
}