namespace NativeCode.Tests.Core.Messaging.Queueing
{
    using System;

    using NativeCode.Core.Logging;
    using NativeCode.Core.Packages.Rabbit;
    using NativeCode.Core.Serialization;

    using Xunit;

    public class WhenUsingRabbitMessageQueue : WhenTestingPlatform
    {
        public static readonly Uri RabbitConnectionUrl = new Uri("amqp://netservices:5OBK1r8NiRyrDSx15dVX@services.nativecode.local/netservices-testing");

        public WhenUsingRabbitMessageQueue()
        {
            this.Container.Registrar.Register<IStringSerializer, JsonStringSerializer>();
        }

        [Fact]
        public void ShouldSendAndReceiveMessage()
        {
            // Arrange
            using (var factory = new RabbitMessageQueueFactory(this.Resolve<ILogger>(), this.Resolve<IStringSerializer>()))
            using (var queue = factory.Create<TestQueueMessage>(RabbitConnectionUrl))
            {
                // Act
                var request = new TestQueueMessage();
                queue.Enqueue(request);
                var message = queue.Dequeue();

                // Assert
                Assert.NotNull(message);
                Assert.Equal(request.Id, message.Id);
            }
        }
    }
}
