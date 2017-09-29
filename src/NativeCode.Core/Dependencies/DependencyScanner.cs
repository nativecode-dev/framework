namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Enums;
    using Extensions;

    [IgnoreDependency]
    public class DependencyScanner
    {
        public IEnumerable<DependencyDescription> ScanAssemblies(IEnumerable<Assembly> assemblies)
        {
            return from assembly in assemblies
                   where this.IncludeAssembly(assembly)
                   from type in assembly.ExportedTypes
                   where this.IncludeType(type)
                   select this.CreateDescription(type);
        }

        public static IEnumerable<DependencyDescription> Scan(IEnumerable<Assembly> assemblies)
        {
            var scanner = new DependencyScanner();
            return scanner.ScanAssemblies(assemblies);
        }

        protected virtual DependencyDescription CreateDescription(Type type)
        {
            if (this.ScanAttributes(type, out var dependency))
            {
                return dependency;
            }

            return new DependencyDescription
            {
                Contract = type,
                Implementation = type
            };
        }

        protected virtual bool IncludeAssembly(Assembly assembly)
        {
            var name = assembly.GetName().FullName;

            if (assembly.IsDynamic)
            {
                return false;
            }

            return name.StartsWith("Microsoft") == false;
        }

        protected virtual bool IncludeType(Type type)
        {
            if (type.HasAttribute<IgnoreDependencyAttribute>())
            {
                return false;
            }

            return type.HasAttribute<DependencyAttribute>() || type.HasInterface<IDependencyModule>();
        }

        protected bool ScanAttributes(Type type, out DependencyDescription description)
        {
            if (type.HasAttribute<DependencyAttribute>(out var attribute) == false)
            {
                description = null;

                return false;
            }

            description = new DependencyDescription
            {
                Contract = attribute.Contract,
                Implementation = type,
                Key = DependencyKey.Name,
                KeyValue = attribute.Key,
                Lifetime = attribute.Lifetime
            };

            return true;
        }
    }
}