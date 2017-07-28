namespace NativeCode.Tests.Core.Extensions
{
    using System;
    using NativeCode.Core.Extensions;
    using Xunit;

    public class WhenUsingObjectExtensions
    {
        [Fact]
        public void ShouldCompareEqualTypesWithIs()
        {
            // Arrange
            var source = new object();
            var target = typeof(object);

            // Act, Assert
            Assert.True(source.Is(target));
        }

        [Fact]
        public void ShouldCompareUnequalTypesWithIs()
        {
            // Arrange
            var source = new object();
            var target = typeof(bool);

            // Act, Assert
            Assert.False(source.Is(target));
        }

        [Fact]
        public void ShouldDisposeObjectIfInterfaceImplemented()
        {
            // Arrange
            object sut = new Disposable();

            // Act
            sut.DisposeIfNeeded();

            // Assert
            Assert.True(((Disposable) sut).Disposed);
        }

        [Fact]
        public void ShouldEnsureTypesMatching()
        {
            // Arrange
            var instance = new object();

            // Act, Assert
            instance.Ensure(typeof(object));
        }

        [Fact]
        public void ShouldEnsureTypesNotMatching()
        {
            // Arrange
            var instance = new object();

            // Act, Assert
            Assert.Throws<InvalidCastException>(() => instance.Ensure(typeof(bool)));
        }

        [Fact]
        public void ShouldReturnFalseWhenComparingNullInstance()
        {
            // Arrange
            object source = null;
            var target = typeof(object);

            // Act, Assert
            Assert.False(source.Is(target));
        }

        private sealed class Disposable : IDisposable
        {
            public bool Disposed { get; private set; }

            public void Dispose()
            {
                this.Disposed = true;
            }
        }
    }
}