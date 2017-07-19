namespace NativeCode.Tests.Core.Extensions
{
    using System;
    using NativeCode.Core.Extensions;
    using Xunit;

    public class WhenUsingGuidExtensions
    {
        private const string EncodedGuid = "ZWNkOTQ0NzAtMWEwMy00MTY5LWEwNzQtNzM4MDg4NmJhNDJh";

        private static readonly Guid Identifier = Guid.Parse("ecd94470-1a03-4169-a074-7380886ba42a");

        [Fact]
        public void ShouldConvertBase64StringToGuid()
        {
            // Arrange
            // Act
            var sut = GuidExtensions.FromBase64String(EncodedGuid);

            // Assert
            Assert.Equal(Identifier, sut);
        }

        [Fact]
        public void ShouldConvertGuidToBase64String()
        {
            // Arrange
            // Act
            var sut = Identifier.ToBase64String();

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(sut));
            Assert.Equal(EncodedGuid, sut);
        }
    }
}