namespace NativeCode.Tests.Extensions
{
    using NativeCode.Core.Extensions;
    using Xunit;

    public class WhenUsingStringExtensions
    {
        private const string StringNoQuotes = "'Tis the season to be jolly!";

        private const string StringWithBothQuotes = "'\"" + StringNoQuotes + "\"'";

        private const string StringWithDoubleQuotes = "\"" + StringNoQuotes + "\"";

        private const string StringWithSingleQuotes = "'" + StringNoQuotes + "'";

        [Fact]
        public void ShouldAddDoubleQuotes()
        {
            // Arrange, Act
            var quoted = StringNoQuotes.Quote();

            // Assert
            Assert.Equal(StringWithDoubleQuotes, quoted);
        }

        [Fact]
        public void ShouldAddSingleQuotes()
        {
            // Arrange, Act
            var quoted = StringNoQuotes.SingleQuote();

            // Assert
            Assert.Equal(StringWithSingleQuotes, quoted);
        }

        [Fact]
        public void ShouldDoNothingWithUnquotedString()
        {
            // Arrange, Act
            var dequoted = StringNoQuotes.Dequote();

            // Assert
            Assert.Equal(StringNoQuotes, dequoted);
        }

        [Fact]
        public void ShouldNotAddDoubleQuotesToStringWithDoubleQuotes()
        {
            // Arrange, Act
            var quoted = StringWithDoubleQuotes.Quote();

            // Assert
            Assert.Equal(StringWithDoubleQuotes, quoted);
        }

        [Fact]
        public void ShouldNotAddSingleQuotesToStringWithDoubleQuotes()
        {
            // Arrange, Act
            var quoted = StringWithDoubleQuotes.SingleQuote();

            // Assert
            Assert.Equal(StringWithBothQuotes, quoted);
        }

        [Fact]
        public void ShouldRemoveDoubleQuotes()
        {
            // Arrange, Act
            var dequoted = StringWithDoubleQuotes.Dequote();

            // Assert
            Assert.Equal(StringNoQuotes, dequoted);
        }

        [Fact]
        public void ShouldRemoveSingleQuotes()
        {
            // Arrange, Act
            var dequoted = StringWithSingleQuotes.Dequote();

            // Assert
            Assert.Equal(StringNoQuotes, dequoted);
        }

        [Fact]
        public void ShouldValidateStringIsDoubleQuoted()
        {
            // Arrange, Act, Assert
            Assert.True(StringWithDoubleQuotes.IsDoubleQuoted());
        }

        [Fact]
        public void ShouldValidateStringIsSingleQuoted()
        {
            // Arrange, Act, Assert
            Assert.True(StringWithSingleQuotes.IsSingleQuoted());
        }
    }
}