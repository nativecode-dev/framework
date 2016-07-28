namespace NativeCode.Core.Web.Hmac
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.Owin;

    public class CanonicalMessage
    {
        private readonly List<string> lines = new List<string>();

        public CanonicalMessage(IOwinRequest request)
        {
            this.ContentHash = request.Headers[HttpHeaders.ContentHash];
            this.ContentLength = Convert.ToInt64(request.Headers[HttpHeaders.ContentLength]);
            this.MediaType = request.MediaType;
            this.Method = request.Method;
            this.Timestamp = Convert.ToInt64(request.Headers[HttpHeaders.KeyRequestSignatureTimestamp]);
        }

        protected string ContentHash { get; }

        protected long? ContentLength { get; }

        protected string MediaType { get; }

        protected string Method { get; }

        protected long Timestamp { get; }

        public string Sign(HmacSignatureAlgorithm algorithm, string secret)
        {
            return null;
        }

        public override string ToString()
        {
            var builder = new StringBuilder(10);
            builder.AppendLine(this.Method);
            builder.AppendLine(this.ContentHash);
            builder.AppendLine(this.MediaType);
            builder.AppendLine(this.Timestamp.ToString());
            builder.AppendLine(this.ContentLength.GetValueOrDefault().ToString());

            return builder.ToString();
        }
    }
}