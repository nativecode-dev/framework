namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Text;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Platform.Serialization;
    using NativeCode.Core.Types;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    internal class RabbitMessageQueue<TMessage> : Disposable, IMessageQueue<TMessage>
        where TMessage : class, new()
    {
        public RabbitMessageQueue(IStringSerializer serializer, ILogger logger, RabbitConnection connection, RabbitMessageQueueOptions options)
        {
            this.Logger = logger;
            this.Channel = connection.CreateModel();
            this.Options = options;
            this.Serializer = serializer;

            this.Channel.ExchangeDeclare(this.ExchangeName, ExchangeType.Topic, options.Durable, options.AutoDelete, null);
            this.Channel.QueueDeclare(this.QueueName, options.Durable, options.Exclusive, options.AutoDelete, null);
            this.Channel.QueueBind(this.QueueName, this.ExchangeName, this.RoutingKey);

            this.Channel.CallbackException += this.ModelOnCallbackException;
        }

        private void ModelOnCallbackException(object sender, CallbackExceptionEventArgs callbackExceptionEventArgs)
        {
            this.Logger.Exception(callbackExceptionEventArgs.Exception);
        }

        public string QueueName => typeof(TMessage).Name.ToLowerScore('.');

        public string RoutingKey => this.QueueName;

        public string ExchangeName => $"{this.QueueName}";

        protected IModel Channel { get; }

        protected ILogger Logger { get; }

        protected RabbitMessageQueueOptions Options { get; }

        protected IStringSerializer Serializer { get; }

        public byte[] GetBytes()
        {
            this.EnsureModelAvailable();

            var result = this.Channel.BasicGet(this.QueueName, true);

            if (result == null)
            {
                return new byte[0];
            }

            try
            {
                return result.Body;
            }
            finally
            {
                this.Channel.BasicAck(result.DeliveryTag, false);
            }
        }

        public void PublishBytes(byte[] data)
        {
            this.EnsureModelAvailable();

            this.Channel.BasicPublish(this.ExchangeName, this.RoutingKey, null, data);
        }

        public TMessage Dequeue()
        {
            var bytes = this.GetBytes();

            if (bytes == null)
            {
                return default(TMessage);
            }

            return this.Serializer.Deserialize<TMessage>(Encoding.UTF8.GetString(bytes));
        }

        public void Enqueue(TMessage message)
        {
            var serialized = this.Serializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(serialized);
            this.PublishBytes(bytes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                this.Channel.Close();
                this.Channel.Dispose();
            }

            base.Dispose(disposing);
        }

        private void EnsureModelAvailable()
        {
            if (this.Channel == null || this.Channel.IsClosed)
            {
                throw new InvalidOperationException("Message queue unavailable.");
            }
        }
    }
}
