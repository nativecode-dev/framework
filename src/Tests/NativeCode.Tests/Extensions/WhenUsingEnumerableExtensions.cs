namespace NativeCode.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using NativeCode.Core.Extensions;
    using Xunit;

    public class WhenUsingEnumerableExtensions
    {
        [Fact]
        public void ShouldEnumerateWhileNotCancelled()
        {
            // Arrange
            var collection = Enumerable.Range(1, 100);
            using (var cts = new CancellationTokenSource())
            {
                var count = 0;

                // Act
                foreach (var index in collection.TakeUntil(cts.Token))
                {
                    count++;

                    if (index >= 10)
                        cts.Cancel();
                }

                // Asset
                Assert.Equal(10, count);
            }
        }

        [Theory]
        [InlineData(new [] { 0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 768 })]
        public void ShouldGetElementAtRandomIndex(int[] collection)
        {
            // Arrange
            var casted = collection.Cast<object>();
            var random = new Random();
            var index = random.Next(0, collection.Length);

            // Act
            var value = casted.Get<int>(index);

            // Assert
            Assert.Equal(collection[index], value);
        }

        [Fact]
        public void ShouldThrowArgumentOutOfRangeExceptionCallingGetElementAtIndexWithInvalidValue()
        {
            // Arrange
            var collection = new[] { 0, 2, 4, 6, 8, 10 };
            var casted = collection.Cast<object>();

            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => casted.Get<int>(-1));
        }

        [Theory]
        [InlineData(new[] { 0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 768 })]
        public void ShouldGetElementAtRandomIndexSafely(int[] collection)
        {
            // Arrange
            var random = new Random();
            var index = random.Next(0, collection.Length);

            // Act
            var notsafe = collection.SafeGet(-index);
            var safe = collection.SafeGet(index);

            // Assert
            Assert.Equal(default(int), notsafe);
            Assert.True(collection.Contains(safe));
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenGivenInvalidIndexOnNullCollection()
        {
            // Arrange
            var reference = (IEnumerable<int>) null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            var value = reference.SafeGet(-1);

            // Assert
            Assert.Equal(0, value);
        }
    }
}