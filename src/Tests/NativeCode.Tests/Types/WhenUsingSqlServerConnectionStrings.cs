namespace NativeCode.Tests.Types
{
    using NativeCode.Core.Platform.Connections;
    using Xunit;

    public class WhenUsingSqlServerConnectionStrings
    {
        private const string DefaultConnectionString =
            "Server=(local); Database=NativeCodeServices; Integrated Security=True; MultipleActiveResultSets=True;";

        private const string ExpectedConnectionString =
            "Server=(local); Database=NativeCodeServices; Integrated Security=True; MultipleActiveResultSets=True";

        [Fact]
        public void ParseConnectionString()
        {
            // Arrange, Act
            var sut = new SqlServerConnectionString(DefaultConnectionString);

            // Assert
            Assert.Equal("(local)", sut["Server"]);
            Assert.Equal("NativeCodeServices", sut["Database"]);
            Assert.Equal("True", sut["Integrated Security"]);
            Assert.Equal("True", sut["MultipleActiveResultSets"]);
        }

        [Fact]
        public void ShouldCreateConnectionString()
        {
            // Arrange, Act
            var sut = new SqlServerConnectionString(DefaultConnectionString);

            // Assert
            Assert.Equal(ExpectedConnectionString, sut.ToString());
        }
    }
}