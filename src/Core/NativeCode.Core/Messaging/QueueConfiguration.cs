namespace NativeCode.Core.Messaging
{
    /// <summary>
    /// Defines a configuration when defining queues.
    /// </summary>
    public abstract class QueueConfiguration
    {
        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="QueueConfiguration" /> is durable.
        /// </summary>
        public bool Durable { get; set; }

        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        public string QueueName { get; set; }
    }
}