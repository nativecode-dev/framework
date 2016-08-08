namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Collections.Concurrent;

    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Platform.Serialization;
    using NativeCode.Core.Types;

    public class RabbitMessageQueueFactory : MessageQueueFactory<RabbitMessageQueueOptions>
    {
        private const int MaxRetryCount = 10;

        private static readonly ConcurrentDictionary<string, RabbitConnection> Connections = new ConcurrentDictionary<string, RabbitConnection>();

        public RabbitMessageQueueFactory(ILogger logger, IStringSerializer serializer)
            : base(logger, serializer)
        {
        }

        public override IMessageQueue<TMessage> Create<TMessage>(Uri connection)
        {
            return this.Create<TMessage>(new RabbitMessageQueueOptions(connection));
        }

        public override IMessageQueue<TMessage> Create<TMessage>(RabbitMessageQueueOptions options)
        {
            var connection = this.GetRabbitConnection(options.Uri);

            return Retry.Until(
                () => new RabbitMessageQueue<TMessage>(this.Logger, this.Serializer, connection, options),
                MaxRetryCount,
                (ex, count) => this.Logger.Exception(ex));
        }

        protected virtual RabbitConnection GetRabbitConnection(RabbitUri uri)
        {
            return Connections.AddOrUpdate(uri, key => this.NewConnection(uri), this.UpdateConnection);
        }

        protected virtual RabbitConnection NewConnection(RabbitUri uri)
        {
            return new RabbitConnection(uri, this.Logger);
        }

        protected virtual RabbitConnection UpdateConnection(string key, RabbitConnection value)
        {
            return value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                foreach (var connection in Connections.Values)
                {
                    connection.Dispose();
                }

                Connections.Clear();
            }

            base.Dispose(disposing);
        }
    }
}