namespace NativeCode.Core.Packages.Rabbit
{
    using System;

    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Types;

    using RabbitMQ.Client;

    public class RabbitConnection : Disposable
    {
        private readonly LazyFactory<IConnection> instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitConnection" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="logger">The logger.</param>
        protected internal RabbitConnection(RabbitUri connection, ILogger logger)
        {
            this.instance = new LazyFactory<IConnection>(() => this.CreateDefaultConnection(connection));
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the connection factory.
        /// </summary>
        protected IConnection Connection => this.instance.Value;

        protected ILogger Logger { get; }

        public IModel CreateModel()
        {
            this.Logger.Debug("Creating model.");
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
                var url = connection.ToUri().AbsoluteUri;
                var factory = new ConnectionFactory { Uri = url };

                this.Logger.Debug($"Creating connection for {url}.");

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