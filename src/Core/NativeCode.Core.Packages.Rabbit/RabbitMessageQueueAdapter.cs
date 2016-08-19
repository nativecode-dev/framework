namespace NativeCode.Core.Packages.Rabbit
{
    using System;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.MessageQueues;

    public class RabbitMessageQueueAdapter : IMessageQueueAdapter
    {
        public RabbitMessageQueueAdapter(ILogger logger)
        {
            this.Logger = logger;
        }

        protected ILogger Logger { get; }

        public IMessageQueueProvider Connect(string name, Uri uri, MessageQueueType type)
        {
            var url = new RabbitUri(uri);
            var connection = new RabbitConnection(uri, this.Logger);

            return new RabbitMessageQueueProvider(connection, type);
        }

        public IMessageQueueProvider Connect<TMessage>(Uri uri, MessageQueueType type) where TMessage : class, new()
        {
            return this.Connect(typeof(TMessage).Name.ToLowerScore('.'), uri, type);
        }
    }
}
