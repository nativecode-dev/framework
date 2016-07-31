namespace NativeCode.Core.Packages.Rabbit
{
    using NativeCode.Core.Messaging;

    public class RabbitQueueProviderConfiguration : QueueProviderConfiguration
    {
        public string VirtualHost { get; set; }
    }
}