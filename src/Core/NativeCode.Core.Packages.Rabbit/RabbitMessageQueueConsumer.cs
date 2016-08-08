namespace NativeCode.Core.Packages.Rabbit
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Processing;
    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Platform.Serialization;

    public class RabbitMessageQueueConsumer<TMessage> : MessageQueueConsumer<TMessage>
        where TMessage : class, new()
    {
        public RabbitMessageQueueConsumer(IEnumerable<IMessageHandler> handlers, ILogger logger, IMessageQueue<TMessage> queue, IStringSerializer serializer)
            : base(handlers, logger, queue, serializer)
        {
        }

        protected RabbitMessageQueue<TMessage> RabbitQueue => (RabbitMessageQueue<TMessage>)this.Queue;

        protected override Task ConsumeQueueAsync(Uri url, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
