namespace NativeCode.Core.Annotations
{
    using System;

    /// <summary>
    /// Attribute class that encapsulates common properties for annotating code.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public abstract class CodeAnnotationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeAnnotationAttribute"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        protected CodeAnnotationAttribute(string reason)
            : this(reason, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeAnnotationAttribute"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="ticket">The ticket.</param>
        protected CodeAnnotationAttribute(string reason, string ticket)
        {
            this.Reason = reason;
            this.Ticket = ticket;
        }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public string Reason { get; }

        /// <summary>
        /// Gets the ticket.
        /// </summary>
        /// <value>The ticket.</value>
        public string Ticket { get; }
    }
}
