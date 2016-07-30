namespace NativeCode.Tests.Core.Extensions
{
    using System;

    using NativeCode.Core.Extensions;

    using Xunit;

    public class WhenUsingTypeExtensions
    {
        private interface IDerived
        {
        }

        [Fact]
        public void ShouldReturnFalseWhenGenericTypeNotDerived()
        {
            // Arrange
            var derived = typeof(DerivedClass);

            // Act, Assert
            Assert.False(derived.HasBaseClass<Array>());
        }

        [Fact]
        public void ShouldReturnFalseWhenTypeNotDerived()
        {
            // Arrange
            var @base = typeof(bool);
            var derived = typeof(DerivedClass);

            // Act, Assert
            Assert.False(derived.HasBaseClass(@base));
        }

        [Fact]
        public void ShouldReturnTrueWhenDerivesFromGenericType()
        {
            // Arrange
            var derived = typeof(DerivedClass);

            // Act, Assert
            Assert.True(derived.HasBaseClass<BaseClass>());
        }

        [Fact]
        public void ShouldReturnTrueWhenDerivesFromType()
        {
            // Arrange
            var @base = typeof(BaseClass);
            var derived = typeof(DerivedClass);

            // Act, Assert
            Assert.True(derived.HasBaseClass(@base));
        }

        [Fact]
        public void ShouldReturnTrueWhenGenericTypeImplementsInterface()
        {
            // Arrange
            var derived = typeof(DerivedClass);

            // Acct, Assert
            Assert.True(derived.HasInterface<IDerived>());
        }

        [Fact]
        public void ShouldReturnTrueWhenTypeImplementsInterface()
        {
            // Arrange
            var @interface = typeof(IDerived);
            var derived = typeof(DerivedClass);

            // Acct, Assert
            Assert.True(derived.HasInterface(@interface));
        }

        [Fact]
        public void ShouldReturnWhenGenericTypeNotImplementsInterface()
        {
            // Arrange
            var derived = typeof(DerivedClass);

            // Acct, Assert
            Assert.False(derived.HasInterface<IDisposable>());
        }

        [Fact]
        public void ShouldReturnWhenTypeNotImplementsInterface()
        {
            // Arrange
            var @interface = typeof(IDisposable);
            var derived = typeof(DerivedClass);

            // Acct, Assert
            Assert.False(derived.HasInterface(@interface));
        }

        private class BaseClass
        {
        }

        private class DerivedClass : BaseClass, IDerived
        {
        }
    }
}