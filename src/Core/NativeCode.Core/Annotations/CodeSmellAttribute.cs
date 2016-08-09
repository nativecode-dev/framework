namespace NativeCode.Core.Annotations
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public sealed class CodeSmellAttribute : CodeAnnotationAttribute
    {
        public CodeSmellAttribute(string reason)
            : base(reason)
        {
        }

        public CodeSmellAttribute(string reason, string ticket)
            : base(reason, ticket)
        {
        }
    }
}