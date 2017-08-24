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
            var setup = translator.Setup(t => t.TranslateString(It.IsAny<string>())).Returns(() => "test");
			setup.Verifiable();
			var sut = new ObjectTranslator(translator.Object);

			// Act
			sut.Translate(translatable);

            // Assert
            translator.Verify(t => t.TranslateString(It.IsAny<string>()));
            Assert.Equal("test", translatable.TranslatableProperty);
        }

        protected class TranslatableObject
        {
            [Translate]
            public string TranslatableProperty { get; set; } = string.Empty;
        }
    }
}