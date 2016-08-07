namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Text;

    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Platform.Serialization;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    internal class RabbitMessageQueue<TMessage> : MessageQueue<TMessage>
        where TMessage : class, new()
    {
        public RabbitMessageQueue(ILogger logger, IStringSerializer serializer, RabbitConnection connection, RabbitMessageQueueOptions options)
            : base(logger, serializer)
        {
            try
            {
                this.Channel = connection.CreateModel();
                this.Options = options;

                if (string.IsNullOrWhiteSpace(this.Options.QueueName) == false)
                {
                    this.QueueName = this.Options.QueueName;
                }

                this.Channel.ExchangeDeclare(this.ExchangeName, ExchangeType.Topic, options.Durable, options.AutoDelete, null);
                this.Channel.QueueDeclare(this.QueueName, options.Durable, options.Exclusive, options.AutoDelete, null);
                this.Channel.QueueBind(this.QueueName, this.ExchangeName, this.RoutingKey);

                this.Channel.CallbackException += this.ModelOnCallbackException;

                this.Logger.Debug($"Connected to message queue {this.QueueName} at {this.Options.Uri}.");
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
                throw;
            }
        }

        private void ModelOnCallbackException(object sender, CallbackExceptionEventArgs callbackExceptionEventArgs)
        {
            this.Logger.Exception(callbackExceptionEventArgs.Exception);
        }

        public string RoutingKey => this.QueueName;

        public string ExchangeName => $"{this.QueueName}";

        protected IModel Channel { get; private set; }

        protected RabbitMessageQueueOptions Options { get; }

        public override byte[] GetBytes()
        {
            this.EnsureModelAvailable();

            var result = this.Channel.BasicGet(this.QueueName, false);

            if (result == null)
            {
                return null;
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

        public override void PublishBytes(byte[] data)
        {
            this.EnsureModelAvailable();

            this.Channel.BasicPublish(this.ExchangeName, this.RoutingKey, null, data);
        }

        public override TMessage Dequeue()
        {
            var bytes = this.GetBytes();

            if (bytes == null)
            {
                return default(TMessage);
            }

            return this.Serializer.Deserialize<TMessage>(Encoding.UTF8.GetString(bytes));
        }

        public override void Enqueue(TMessage message)
        {
            var serialized = this.Serializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(serialized);
            this.PublishBytes(bytes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false && this.Channel != null)
            {
                this.Channel.CallbackException -= this.ModelOnCallbackException;

                this.Channel.Close();
                this.Channel.Dispose();
                this.Channel = null;
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
