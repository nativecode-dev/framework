namespace NativeCode.Tests.Core.Extensions
{
    using System;
    using NativeCode.Core.Extensions;
    using Xunit;

    public class WhenUsingExceptionExtensions
    {
        [Fact]
        public void ShouldCreateExceptionStringBuilder()
        {
            // Arrange
            var exception = CreateSimpleException();

            // Act
            var builder = exception.CreateExceptionBuilder();
            var message = builder.ToString();

            // Assert
            Assert.NotNull(builder);
            Assert.True(message.Contains("Simple exception"));
        }

        [Fact]
        public void ShouldCreateExceptionStringBuilderWithInner()
        {
            // Arrange
            var exception = CreateSimpleExceptionWithInner();

            // Act
            var builder = exception.CreateExceptionBuilder();
            var message = builder.ToString();

            // Assert
            Assert.NotNull(builder);
            Assert.True(message.Contains("Simple exception with inner"));
        }

        private static Exception CreateSimpleException()
        {
            return new Exception("Simple exception");
        }

        private static Exception CreateSimpleExceptionWithInner()
        {
            return new Exception("Simple exception with inner", CreateSimpleException());
        }
    }
}