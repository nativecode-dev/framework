namespace Cavern.Models
{
    using System.Collections.Generic;

    public abstract class EnvelopeModelCollection<T> : Envelope where T : class
    {
        protected EnvelopeModelCollection(string response, ICollection<T> collection) : base(response)
        {
            this.Collection = collection;
        }

        public ICollection<T> Collection { get; }
    }
}