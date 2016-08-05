namespace NativeCode.Core.Packages.Rabbit
{
    public class RabbitMessageQueueOptions
    {
        public RabbitMessageQueueOptions(RabbitUri uri)
        {
            this.Uri = uri;
        }

        public bool AutoDelete { get; set; }

        public bool Durable { get; set; } = true;

        public bool Exclusive { get; set; }

        public string QueueName { get; set; }

        public RabbitUri Uri { get; }
    }
}