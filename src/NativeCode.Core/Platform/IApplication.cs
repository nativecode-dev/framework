namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using NativeCode.Core.Dependencies;

    public interface IApplication : IDisposable
    {
        IDependencyContainer Container { get; }

        void Initialize(params IDependencyModule[] modules);

        void Initialize(IEnumerable<Assembly> assemblies, params IDependencyModule[] modules);
    }
}