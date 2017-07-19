namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Collections.Generic;
    using Extensions;
    using Types;

    public class RabbitUri
    {
        public const string Scheme = "amqp";

        private readonly Dictionary<string, string> options = new Dictionary<string, string>();

        public RabbitUri()
        {
        }

        public RabbitUri(Uri source)
            : this()
        {
            this.Host = source.Host;
            this.Port = source.IsDefaultPort ? this.Port : source.Port;
            this.VirtualHost = source.PathAndQuery;

            this.Credentials.Login = source.Login();
            this.Credentials.Password = source.Password();

            this.ParseOptions(source.Query);
        }

        public Credentials Credentials { get; set; } = new Credentials();

        public string Host { get; set; } = "localhost";

        public IReadOnlyDictionary<string, string> Options => this.options;

        public int Port { get; set; } = 5672;

        public string VirtualHost { get; set; }

        public static implicit operator RabbitUri(Uri source)
        {
            return new RabbitUri(source);
        }

        public static implicit operator string(RabbitUri source)
        {
            return source.ToUri().AbsoluteUri;
        }

        public static implicit operator Uri(RabbitUri source)
        {
            return new Uri(source.ToString());
        }

        public override string ToString()
        {
            return this.ToUri().AbsoluteUri;
        }

        public virtual Uri ToUri()
        {
            var builder = new UriBuilder(Scheme, this.Host, this.Port, this.VirtualHost)
            {
                Password = this.Credentials.Password,
                UserName = this.Credentials.Login
            };

            return builder.Uri;
        }

        private void ParseOptions(string query)
        {
            if (string.IsNullOrWhiteSpace(query) || query == "?")
                return;

            query = query.Remove(0, 1);
            var parts = query.Split("&");

            foreach (var part in parts)
                if (part.Contains("="))
                {
                    var kvp = part.Split("=");
                    this.options.Add(kvp[0], kvp[1]);
                }
                else
                {
                    this.options.Add(part, null);
                }
        }
    }
}