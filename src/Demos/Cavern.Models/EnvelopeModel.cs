namespace Cavern.Models
{
    public abstract class EnvelopeModel<T> : Envelope where T : class, new()
    {
        protected EnvelopeModel(string response, T model = default(T)) : base(response)
        {
            if (model == default(T))
                this.Model = new T();

            this.Model = model;
        }

        public T Model { get; }
    }
}