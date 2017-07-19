namespace NativeCode.Tests.Core.Extensions
{
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
    }
}