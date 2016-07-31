namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Messaging;
    using NativeCode.Core.Types;

    using RabbitMQ.Client;

    /// <summary>
    /// Implements the <see cref="RabbitQueueProviderConfiguration" />
    /// using RabbitMQ.
    /// </summary>
    /// <seealso
    ///     cref="RabbitQueueProviderConfiguration" />
    /// <seealso cref="NativeCode.Core.Types.Disposable" />
    public class RabbitMessageQueueProvider : Disposable, IMessageQueueProvider<RabbitQueueProviderConfiguration, RabbitQueueConfiguration>
    {
        private readonly ConcurrentDictionary<string, RabbitQueueConfiguration> channels = new ConcurrentDictionary<string, RabbitQueueConfiguration>();

        public RabbitMessageQueueProvider(RabbitQueueProviderConfiguration configuration)
        {
            this.Configuration = configuration;
            this.ConnectionFactory = new ConnectionFactory
                                         {
                                             HostName = configuration.BaseAddress.Host,
                                             Port = configuration.BaseAddress.Port,
                                             UserName = configuration.Login,
                                             Password = configuration.Password
                                         };

            this.Connection = this.ConnectionFactory.CreateConnection();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public RabbitQueueProviderConfiguration Configuration { get; }

        /// <summary>
        /// Gets the channels.
        /// </summary>
        protected IReadOnlyDictionary<string, RabbitQueueConfiguration> Channels => new ReadOnlyDictionary<string, RabbitQueueConfiguration>(this.channels);

        /// <summary>
        /// Gets the connection.
        /// </summary>
        protected IConnection Connection { get; private set; }

        /// <summary>
        /// Gets the connection factory.
        /// </summary>
        protected IConnectionFactory ConnectionFactory { get; }

        /// <summary>
        /// Creates a new queue from the provided configuration.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns><c>true</c> if queue created, <c>false</c> otherwise.</returns>
        public bool CreateQueue(RabbitQueueConfiguration options)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified queue.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns><c>true</c> if queue deleted, <c>false</c> otherwise.</returns>
        public bool DeleteQueue(string queueName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified queue.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns><c>true</c> if queue deleted, <c>false</c> otherwise.</returns>
        public Task<bool> DeleteQueueAsync(string queueName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new queue from the provided configuration.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if queue created, <c>false</c> otherwise.</returns>
        public Task<bool> CreateQueueAsync(RabbitQueueConfiguration options, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
