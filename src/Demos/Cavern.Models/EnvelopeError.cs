namespace Cavern.Models
{
    public class EnvelopeError
    {
        public EnvelopeError(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public string Code { get; }

        public string Message { get; }
    }
}