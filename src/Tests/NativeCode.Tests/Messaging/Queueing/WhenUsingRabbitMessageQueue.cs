namespace NativeCode.Tests.Messaging.Queueing
{
    using System;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.DotNet.Logging;
    using NativeCode.Core.Packages.Rabbit;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Platform.Serialization;
    using Testing;
    using Xunit;

    public class WhenUsingRabbitMessageQueue : WhenTestingPlatform
    {
        public static readonly Uri RabbitConnectionUrl =
            new Uri("amqp://testuser:p4ssw0rd@queue.nativecode.com:5672/testing");

        public WhenUsingRabbitMessageQueue()
        {
            this.Container.Registrar.Register<ILogWriter, TraceLogWriter>(DependencyKey.QualifiedName);
            this.Container.Registrar.Register<IStringSerializer, JsonStringSerializer>();
        }

        [Fact]
        [Trait("Type", "Integration")]
        public void ShouldSendAndReceiveMessage()
        {
            // Arrange
            using (var adapter =
                new RabbitMessageQueueAdapter(this.Resolve<ILogger>(), this.Resolve<IStringSerializer>()))
            using (var provider = adapter.Connect<SimpleQueueMessage>(RabbitConnectionUrl, MessageQueueType.Transient))
            {
                // Act
                var message = new SimpleQueueMessage { Id = Guid.NewGuid() };
                provider.PublishMessage(message);
                var response = provider.ConsumeMessage();

                // Assert
                Assert.Equal(message.Id, response.Id);
            }
        }

        [Fact]
        [Trait("Type", "Integration")]
        public void ShouldAllowMultipleConnections()
        {
            // Arrange, Act
            using (var adapter =
                new RabbitMessageQueueAdapter(this.Resolve<ILogger>(), this.Resolve<IStringSerializer>()))
            using (var providerA = adapter.Connect<SimpleQueueMessage>(RabbitConnectionUrl, MessageQueueType.Transient))
            using (var providerB = adapter.Connect<SimpleQueueMessage>(RabbitConnectionUrl, MessageQueueType.Transient))
            {
                // Assert
                Assert.Equal($"transient-simple.queue.message@{Environment.MachineName.ToLower()}:inbox", providerA.QueueName);
                Assert.Equal($"transient-simple.queue.message@{Environment.MachineName.ToLower()}:inbox", providerB.QueueName);
            }
        }
    }
}