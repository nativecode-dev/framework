namespace NativeCode.Core.Packages.Rabbit
{
    using System;

    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Types;

    using RabbitMQ.Client;

    public class RabbitConnection : Disposable
    {
        private readonly Lazy<IConnection> instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitConnection" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="logger">The logger.</param>
        public RabbitConnection(RabbitUri connection, ILogger logger)
        {
            this.instance = new Lazy<IConnection>(() => this.CreateDefaultConnection(connection));
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the connection factory.
        /// </summary>
        protected IConnection Connection => this.instance.Value;

        protected ILogger Logger { get; }

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
            try
            {
                var factory = new ConnectionFactory { Uri = connection.ToUri().AbsoluteUri };
                return factory.CreateConnection();
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
                throw;
            }
        }
    }
}