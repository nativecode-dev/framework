namespace NativeCode.Tests.Core.Types
{
    using NativeCode.Core.Types;

    using Xunit;

    public class WhenUsingConnectionStrings
    {
        private const string DefaultConnectionString = "Server=(local); Database=NativeCodeServices; Integrated Security=True; MultipleActiveResultSets=True;";

        private const string ExpectedConnectionString = "Server=(local); Database=NativeCodeServices; Integrated Security=True; MultipleActiveResultSets=True";

        [Fact]
        public void ParseConnectionString()
        {
            // Arrange, Act
            var sut = new ConnectionString(DefaultConnectionString);

            // Assert
            Assert.Equal("(local)", sut["Server"]);
            Assert.Equal("NativeCodeServices", sut["Database"]);
            Assert.Equal("True", sut["IntegratedSecurity"]);
            Assert.Equal("True", sut["MultipleActiveResultSets"]);
        }

        [Fact]
        public void ShouldCreateConnectionString()
        {
            // Arrange, Act
            var sut = new ConnectionString(DefaultConnectionString);

            // Assert
            Assert.Equal(ExpectedConnectionString, sut.ToString());
        }
    }
}