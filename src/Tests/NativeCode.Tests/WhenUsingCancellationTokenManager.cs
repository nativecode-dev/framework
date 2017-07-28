namespace NativeCode.Tests
{
    using System;
    using System.Threading;
    using Core.Platform.Logging;
    using Core.Types;
    using Testing;
    using Xunit;

    public class WhenUsingCancellationTokenManager : WhenTestingDependencies
    {
        [Fact]
        public void ShouldCreateNamedCancellationToken()
        {
            // Arrange
            using (var sut = new CancellationTokenManager(this.Resolve<ILogger>()))
            {
                // Act
                var token = sut.CreateToken("test");

                // Assert
                Assert.False(token.IsCancellationRequested);
            }
        }

        [Fact]
        public void ShouldCancelTokenWhenDisposed()
        {
            // Arrange
            CancellationToken token;

            using (var sut = new CancellationTokenManager(this.Resolve<ILogger>()))
            {
                // Act
                token = sut.CreateToken("test");
            }

            // Assert
            Assert.True(token.IsCancellationRequested);
        }

        [Fact]
        public void ShouldFailToCreateExistingToken()
        {
            // Arrange
            using (var sut = new CancellationTokenManager(this.Resolve<ILogger>()))
            {
                // Act
                var token = sut.CreateToken("test");

                // Assert
                Assert.False(token.IsCancellationRequested);
                Assert.ThrowsAny<InvalidOperationException>(() => sut.CreateToken("test"));
            }
        }
    }
}