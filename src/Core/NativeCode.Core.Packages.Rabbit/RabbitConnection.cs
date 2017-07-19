﻿namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using Platform.Logging;
    using RabbitMQ.Client;
    using Types;

    public class RabbitConnection : Disposable
    {
        private readonly Lazy<IConnection> instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitConnection" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="logger">The logger.</param>
        protected internal RabbitConnection(RabbitUri connection, ILogger logger)
        {
            this.instance = new Lazy<IConnection>(() => this.GetConnection(connection));
            this.Logger = logger;
        }

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
            return this.CreateQueue(queue, exchange, route, ExchangeType.Direct, true);
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
        /// Creates a transient queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <returns>IModel.</returns>
        public IModel CreateTransientQueue(string queue)
        {
            return this.CreateQueue(queue, string.Empty, string.Empty, ExchangeType.Direct, false);
        }

        /// <summary>
        /// Creates a transient queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <returns>IModel.</returns>
        public IModel CreateTransientQueue(string queue, string exchange)
        {
            return this.CreateQueue(queue, exchange, string.Empty, ExchangeType.Direct, false);
        }

        /// <summary>
        /// Creates a transient queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="exchange">The exchange.</param>
        /// <param name="route">The route.</param>
        /// <returns>IModel.</returns>
        public IModel CreateTransientQueue(string queue, string exchange, string route)
        {
            return this.CreateQueue(queue, exchange, route, ExchangeType.Direct, false);
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
            return this.CreateQueue(queue, exchange, route, ExchangeType.Fanout, true);
        }

        /// <summary>
        /// Creates a default connection factory.
        /// </summary>
        /// <returns>Returns a new <see cref="Connection" />.</returns>
        protected virtual IConnection GetConnection(RabbitUri uri)
        {
            try
            {
                var factory = new ConnectionFactory { Uri = uri };
                var connection = factory.CreateConnection();

                this.Logger.Debug($"Creating connection for {uri}.");
                return connection;
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
        /// <param name="durable">if set to <c>true</c> [durable].</param>
        /// <returns>Returns a new <see cref="IModel" />.</returns>
        protected virtual IModel CreateQueue(string queue, string exchange, string route, string type, bool durable)
        {
            var model = this.Connection.CreateModel();

            try
            {
                if (string.IsNullOrWhiteSpace(exchange) == false)
                    model.ExchangeDeclare(exchange, type, durable);

                if (string.IsNullOrWhiteSpace(queue))
                {
                    var queuename = model.QueueDeclare(string.Empty, false, false, true).QueueName;
                    model.QueueBind(queuename, exchange, string.Empty);
                }
                else
                {
                    model.QueueDeclare(queue, durable, false, false);
                    model.QueueBind(queue, exchange, route);
                }

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