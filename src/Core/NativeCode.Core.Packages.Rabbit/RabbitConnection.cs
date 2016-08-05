namespace NativeCode.Core.Packages.Rabbit
{
    using System;

    using NativeCode.Core.Types;

    using RabbitMQ.Client;

    public class RabbitConnection : Disposable
    {
        private readonly Lazy<IConnection> instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitConnection" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public RabbitConnection(RabbitUri connection)
        {
            this.instance = new Lazy<IConnection>(() => this.CreateDefaultConnection(connection));
        }

        /// <summary>
        /// Gets the connection factory.
        /// </summary>
        protected IConnection Connection => this.instance.Value;

        public IModel CreateModel()
        {
            return this.Connection.CreateModel();
        }

        /// <summary>
        /// Creates the default connection factory.
        /// </summary>
        /// <returns>Returns a new <see cref="Connection" />.</returns>
        protected virtual IConnection CreateDefaultConnection(RabbitUri connection)
        {
            var factory = new ConnectionFactory { Uri = connection.ToUri().AbsoluteUri };
            return factory.CreateConnection();
        }
    }
}