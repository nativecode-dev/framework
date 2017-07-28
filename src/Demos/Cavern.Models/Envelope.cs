namespace Cavern.Models
{
    using System;
    using System.Collections.Generic;
    using NativeCode.Core.Dependencies.Attributes;

    [IgnoreDependency("Not designed to be injectible.")]
    public abstract class Envelope
    {
        protected Envelope(string response)
        {
            this.Response = response;
        }

        public DateTimeOffset Created { get; } = DateTimeOffset.UtcNow;

        public List<EnvelopeError> Errors { get; set; } = new List<EnvelopeError>();

        public string Response { get; }
    }
}
