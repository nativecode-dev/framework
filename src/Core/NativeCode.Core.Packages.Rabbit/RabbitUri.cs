namespace NativeCode.Core.Packages.Rabbit
{
    using System;

    using NativeCode.Core.Types;

    public class RabbitUri
    {
        public const string Scheme = "amqp";

        public RabbitUri()
        {
        }

        public RabbitUri(Uri source)
        {
            this.Host = source.Host;
            this.Port = source.Port;
            this.VirtualHost = source.PathAndQuery;

            if (string.IsNullOrWhiteSpace(source.UserInfo))
            {
                return;
            }

            if (source.UserInfo.Contains(":"))
            {
                var parts = source.UserInfo.Split(':');
                this.Credentials.Login = parts[0];
                this.Credentials.Password = parts[1];
            }
            else
            {
                this.Credentials.Login = source.UserInfo;
            }
        }

        public Credentials Credentials { get; set; } = new Credentials();

        public string Host { get; set; } = "localhost";

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
    }
}