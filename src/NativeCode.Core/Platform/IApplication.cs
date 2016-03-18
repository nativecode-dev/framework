namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Settings;

    public interface IApplication : IDisposable
    {
        IDependencyContainer Container { get; }

        Settings Settings { get; }

        string GetApplicationName();

        string GetApplicationVersion();

        void Initialize(string name, params IDependencyModule[] modules);

        void Initialize(string name, IEnumerable<Assembly> assemblies, params IDependencyModule[] modules);
    }
}