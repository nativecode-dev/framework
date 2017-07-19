namespace NativeCode.Tests.Core.Extensions
{
    using System.Runtime.InteropServices;
    using NativeCode.Core.Extensions;
    using Xunit;

    public class WhenUsingStructExtensions
    {
        private static readonly byte[] ExpectedBytes = { 10, 0, 0, 0 };

        [Fact]
        public void ShouldGetAllocationSizeOfStruct()
        {
            // Arrange, Act
            var size = StructExtensions.GetSize<SimpleStruct>();

            // Assert
            Assert.Equal(4, size);
        }

        [Fact]
        public void ShouldGetBytesForStruct()
        {
            // Arrange
            var value = new SimpleStruct { Counter = 10 };

            // Act
            var bytes = value.GetBytes();

            // Assert
            Assert.Equal(ExpectedBytes, bytes);
        }

        [Fact]
        public void ShouldGetStructFromBytes()
        {
            // Arrange, Act
            var value = StructExtensions.FromBytes<SimpleStruct>(ExpectedBytes);

            // Assert
            Assert.Equal(10, value.Counter);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
        private struct SimpleStruct
        {
            public int Counter { get; set; }
        }
    }
}