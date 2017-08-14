namespace NativeCode.Tests
{
    using Core.Localization.Translation;
    using Core.Localization.Translation.Attributes;
    using Moq;
    using Xunit;

    public class WhenUsingObjectTranslator
    {
        [Fact]
        public void ShouldTranslateStringProperties()
        {
            // Arrange
            var translatable = new TranslatableObject();
            var translator = new Mock<ITranslator>();
            translator.Setup(t => t.Translate(It.IsAny<string>())).Returns(() => "test");
            var objectTranslator = new ObjectTranslator(translator.Object);

            // Act
            objectTranslator.Translate(translatable);

            // Assert
            translator.Verify(t => t.Translate(It.IsAny<string>()));
            Assert.Equal("test", translatable.TranslatableProperty);
        }

        protected class TranslatableObject
        {
            [Translate]
            public string TranslatableProperty { get; set; } = string.Empty;
        }
    }
}