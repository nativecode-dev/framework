namespace NativeCode.Tests.Core.Messaging.Queueing
{
    using System;

    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.DotNet.Logging;
    using NativeCode.Core.Packages.Rabbit;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Messaging.Queuing;
    using NativeCode.Core.Platform.Serialization;

    using Xunit;

    public class WhenUsingRabbitMessageQueue : WhenTestingPlatform
    {
        public static readonly Uri RabbitConnectionUrl = new Uri("amqp://netservices:5OBK1r8NiRyrDSx15dVX@services.nativecode.local/framework-testing");

        public WhenUsingRabbitMessageQueue()
        {
            this.Container.Registrar.Register<ILogWriter, TraceLogWriter>(DependencyKey.QualifiedName);
            this.Container.Registrar.Register<IStringSerializer, JsonStringSerializer>();
        }

        [Fact]
        public void ShouldSendAndReceiveMessage()
        {
            // Arrange
            var adapter = new RabbitMessageQueueAdapter(this.Resolve<ILogger>(), this.Resolve<IStringSerializer>());

            using (var provider = adapter.Connect<SimpleQueueMessage>(RabbitConnectionUrl, MessageQueueType.Transient))
            {
                // Act
                var message = new SimpleQueueMessage { Id = Guid.NewGuid() };
                provider.PublishMessage(message);
                var response = provider.NextMessage();
                provider.AcknowledgeMessage(response);

                // Assert
                Assert.Equal(message.Id, response.Id);
            }
        }
    }
}
