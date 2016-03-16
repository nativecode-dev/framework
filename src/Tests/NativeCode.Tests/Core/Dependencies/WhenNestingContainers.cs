namespace NativeCode.Tests.Core.Dependencies
{
    using NativeCode.Core.Types;
    using NativeCode.Packages.Dependencies;

    using Xunit;

    public class WhenNestingContainers
    {
        [Fact]
        public void ShouldRegisterInPrimaryContainerAndResolveInChild()
        {
            // Arrange
            using (var parent = new UnityDependencyContainer())
            {
                TestClass sut;
                parent.Registrar.Register<TestClass>();

                using (var child = parent.CreateChildContainer())
                {
                    sut = child.Resolver.Resolve<TestClass>();
                }

                var control = parent.Resolver.Resolve<TestClass>();

                // Assert
                Assert.False(sut.WasDisposed());
                Assert.NotStrictEqual(sut, control);
            }
        }

        public class TestClass : Disposable
        {
            public bool WasDisposed()
            {
                return this.Disposed;
            }
        }
    }
}