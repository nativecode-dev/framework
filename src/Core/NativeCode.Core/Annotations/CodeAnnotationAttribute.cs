namespace NativeCode.Core.Annotations
{
    using System;

    public abstract class CodeAnnotationAttribute : Attribute
    {
        protected CodeAnnotationAttribute(string reason)
            : this(reason, null)
        {
        }

        protected CodeAnnotationAttribute(string reason, string ticket)
        {
            this.Reason = reason;
            this.Ticket = ticket;
        }

        public string Reason { get; }

        public string Ticket { get; }
    }
}
