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

        ISettingsProvider Settings { get; }

        void Initialize(params IDependencyModule[] modules);

        void Initialize(IEnumerable<Assembly> assemblies, params IDependencyModule[] modules);
    }
}