﻿namespace NativeCode.Core.Dependencies
{
    using NativeCode.Core.Dependencies.Enums;

    public abstract class DependencyModule : IDependencyModule
    {
        public DependencyModulePriority Priority { get; protected set; }

        public abstract void RegisterDependencies(IDependencyRegistrar registrar);
    }
}