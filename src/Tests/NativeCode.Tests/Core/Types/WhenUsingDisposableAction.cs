namespace NativeCode.Tests.Core.Types
{
    using NativeCode.Core.Types;

    using Xunit;

    public class WhenUsingDisposableAction
    {
        [Fact]
        public void ShouldCallInitializerAndFinalizer()
        {
            // Arrange
            var initialized = false;

            // Act
            using (new DisposableAction(() => initialized = true, () => initialized = false))
            {
                Assert.True(initialized);
            }

            // Assert
            Assert.False(initialized);
        }

        [Fact]
        public void ShouldCallFinalizer()
        {
            // Arrange
            var disposed = false;

            // Act
            using (new DisposableAction(() => disposed = true))
            {
                Assert.False(disposed);
            }

            // Assert
            Assert.True(disposed);
        }
    }
}