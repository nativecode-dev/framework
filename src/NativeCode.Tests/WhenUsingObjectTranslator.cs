namespace NativeCode.Tests
{
    using Core.Localization.Translation;
    using Core.Platform.Connections;
    using Moq;
    using Xunit;

    public class WhenUsingObjectTranslator
    {
        [Fact]
        public void ShouldDoStuff()
        {
            // Arrange
            var translator = new Mock<ITranslator>();
            translator.Setup(t => t.Translate(It.IsAny<string>())).Verifiable();
            var connectionString = new SqlServerConnectionString
            {
                AsynchronousProcessing = true,
                DataSource = "(local)",
                HostName = "(ocal)",
                IntegratedSecurity = true,
                MultipleActiveResultSets = true,
                TrustedConnection = true,
            };
            var objectTranslator = new ObjectTranslator(translator.Object);

            // Act
            objectTranslator.Translate(connectionString);

            // Assert
            translator.VerifyAll();
        }
    }
}