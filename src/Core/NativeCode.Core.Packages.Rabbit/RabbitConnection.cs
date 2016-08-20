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

            this.Uri = connection;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RabbitConnection"/> is durable.
        /// </summary>
        public bool Durable { get; set; } = true;

        /// <summary>
        /// Gets the URI.
        /// </summary>
        public RabbitUri Uri { get; }

        /// <summary>
        /// Gets the connection factory.
        /// </summary>
        protected IConnection Connection => this.instance.Value;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Creates a model.
        /// </summary>
        /// <returns>Returns a new <see cref="IModel" />.</returns>
        public IModel CreateModel()
        {
            this.Logger.Debug("Creating model.");
            return this.Connection.CreateModel();
        }

        /// <summary>
        /// Creates a queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <returns>IModel.</returns>
        public IModel CreateQueue(string queue)
        {
            return this.CreateQueue(queue, string.Empty);
        }

        /// <summary>
        /// Creates a queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <returns>Returns a new <see cref="IModel" />.</returns>
        public IModel CreateQueue(string queue, string exchange)
        {
            return this.CreateQueue(queue, exchange, string.Empty);
        }

        /// <summary>
        /// Creates a queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <param name="route">The route.</param>
        /// <returns>IModel.</returns>
        public IModel CreateQueue(string queue, string exchange, string route)
        {
            return this.CreateQueue(queue, exchange, route, ExchangeType.Direct);
        }

        /// <summary>
        /// Creates a pub-sub queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <returns>IModel.</returns>
        public IModel CreatePubSubQueue(string queue)
        {
            return this.CreatePubSubQueue(queue, string.Empty);
        }

        /// <summary>
        /// Creates a pub-sub queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <returns>Returns a new <see cref="IModel" />.</returns>
        public IModel CreatePubSubQueue(string queue, string exchange)
        {
            return this.CreatePubSubQueue(queue, exchange, string.Empty);
        }

        /// <summary>
        /// Creates a pub-sub queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <param name="route">The route.</param>
        /// <returns>IModel.</returns>
        public IModel CreatePubSubQueue(string queue, string exchange, string route)
        {
            return this.CreateQueue(queue, exchange, route, ExchangeType.Fanout);
        }

        /// <summary>
        /// Creates a default connection factory.
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

        /// <summary>
        /// Creates a queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <param name="route">The route.</param>
        /// <param name="type">The type.</param>
        /// <returns>Returns a new <see cref="IModel" />.</returns>
        protected virtual IModel CreateQueue(string queue, string exchange, string route, string type)
        {
            var model = this.Connection.CreateModel();

            try
            {
                model.ExchangeDeclare(exchange, type, this.Durable);
                model.QueueDeclare(queue, this.Durable, false, false, null);
                model.QueueBind(queue, exchange, route);

                return model;
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
                throw;
            }
        }
    }
}