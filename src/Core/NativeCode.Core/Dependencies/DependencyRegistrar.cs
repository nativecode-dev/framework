namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Reflection;

    using NativeCode.Core.Dependencies.Attributes;
    using NativeCode.Core.Dependencies.Enums;

    public abstract class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual IDependencyRegistrar Register(Type type, string key = null, DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.InternalRegister(type, type, key, lifetime);

            return this;
        }

        public virtual IDependencyRegistrar Register<T>(string key = null, DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.InternalRegister(typeof(T), typeof(T), key, lifetime);

            return this;
        }

        public virtual IDependencyRegistrar Register<T, TImplementation>(string key = null, DependencyLifetime lifetime = DependencyLifetime.Default)
            where TImplementation : T
        {
            this.InternalRegister(typeof(T), typeof(TImplementation), key, lifetime);

            return this;
        }

        public IDependencyRegistrar Register<T, TImplementation>(DependencyKey key, DependencyLifetime lifetime = DependencyLifetime.Default)
            where TImplementation : T
        {
            this.InternalRegister(typeof(T), typeof(TImplementation), GetKey(key, typeof(TImplementation)), lifetime);

            return this;
        }

        public abstract IDependencyRegistrar Register(
            Type type, 
            Type implementation, 
            string key = null, 
            DependencyLifetime lifetime = DependencyLifetime.Default);

        public IDependencyRegistrar RegisterAssembly(Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
            {
                var attributes = type.GetTypeInfo().GetCustomAttributes<DependencyAttribute>();

                foreach (var attribute in attributes)
                {
                    this.Register(attribute.Contract, type, attribute.Key, attribute.Lifetime);
                }
            }

            return this;
        }

        public virtual IDependencyRegistrar RegisterFactory<T>(
            Func<IDependencyResolver, T> factory, 
            string key = null, 
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.InternalRegisterFactory(typeof(T), resolver => factory(resolver), key, lifetime);

            return this;
        }

        public abstract IDependencyRegistrar RegisterFactory(
            Type type, 
            Func<IDependencyResolver, object> factory, 
            string key = null, 
            DependencyLifetime lifetime = DependencyLifetime.Default);

        public virtual IDependencyRegistrar RegisterInstance<T>(T instance, DependencyLifetime lifetime = default(DependencyLifetime))
        {
            this.InternalRegisterInstance(typeof(T), instance, lifetime);

            return this;
        }

        public abstract IDependencyRegistrar RegisterInstance(Type type, object instance, DependencyLifetime lifetime = default(DependencyLifetime));

        private static string GetKey(DependencyKey key, Type type)
        {
            switch (key)
            {
                case DependencyKey.Name:
                    return type.FullName;

                case DependencyKey.QualifiedName:
                    return type.AssemblyQualifiedName;

                case DependencyKey.ShortName:
                    return type.Name;

                default:
                    return null;
            }
        }

        private static string GetKeyOverride(Type type, string key)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<OverrideKeyAttribute>();

            if (attribute != null)
            {
                return GetKeyOverrideString(type, attribute.Key);
            }

            return key;
        }

        private static string GetKeyOverrideString(Type type, DependencyKey key = DependencyKey.Default)
        {
            switch (key)
            {
                case DependencyKey.Name:
                    return type.FullName;

                case DependencyKey.QualifiedName:
                    return type.AssemblyQualifiedName;

                case DependencyKey.ShortName:
                    return type.Name;

                default:
                    return default(string);
            }
        }

        private static DependencyLifetime GetLifetimeOverride(Type type, DependencyLifetime lifetime)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<OverrideLifetimeAttribute>();

            if (attribute != null)
            {
                return attribute.Lifetime;
            }

            return lifetime;
        }

        private void InternalRegister(Type type, Type implementation, string key = null, DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            if (IgnoreDependencyAttribute.ValidateType(type) && IgnoreDependencyAttribute.ValidateType(implementation))
            {
                this.Register(type, implementation, GetKeyOverride(implementation, key), GetLifetimeOverride(implementation, lifetime));
            }
        }

        private void InternalRegisterFactory(
            Type type, 
            Func<IDependencyResolver, object> factory, 
            string key = null, 
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            if (IgnoreDependencyAttribute.ValidateType(type))
            {
                this.RegisterFactory(type, factory, GetKeyOverride(type, key), GetLifetimeOverride(type, lifetime));
            }
        }

        private void InternalRegisterInstance(Type type, object instance, DependencyLifetime lifetime = default(DependencyLifetime))
        {
            if (IgnoreDependencyAttribute.ValidateType(type))
            {
                this.RegisterInstance(type, instance, lifetime);
            }
        }
    }
}