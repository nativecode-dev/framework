namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    public interface IDependencyResolver : IServiceProvider
    {
        object Resolve(Type type, string key = default(string));

        T Resolve<T>(string key = default(string));

        IEnumerable<object> ResolveAll([NotNull] Type type);

        IEnumerable<T> ResolveAll<T>();
    }
}