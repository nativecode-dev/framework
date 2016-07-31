namespace NativeCode.Tests
{
    using NativeCode.Core.Types;

    using Xunit;

    public abstract class WhenTesting : Disposable
    {
    }

    public abstract class WhenTesting<T> : WhenTesting, ICollectionFixture<T>
        where T : class
    {
    }
}
