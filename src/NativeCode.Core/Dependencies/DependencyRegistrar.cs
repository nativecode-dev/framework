namespace NativeCode.Core.Dependencies
{
    using Attributes;
    using Enums;
    using Extensions;
    using System;
    using System.Reflection;

    public abstract class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual IDependencyRegistrar Register(Type type, string key = null,
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.InternalRegister(type, type, key, lifetime);

            return this;
        }

        public virtual IDependencyRegistrar Register<T>(string key = null,
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            this.InternalRegister(typeof(T), typeof(T), key, lifetime);

            return this;
        }

        public virtual IDependencyRegistrar Register<T, TImplementation>(string key = null,
            DependencyLifetime lifetime = DependencyLifetime.Default)
            where TImplementation : T
        {
            this.InternalRegister(typeof(T), typeof(TImplementation), key, lifetime);

            return this;
        }

        public IDependencyRegistrar Register<T, TImplementation>(DependencyKey key,
            DependencyLifetime lifetime = DependencyLifetime.Default)
            where TImplementation : T
        {
            this.InternalRegister(typeof(T), typeof(TImplementation), DependencyRegistrar.GetKey(key, typeof(TImplementation)), lifetime);

            return this;
        }

        public virtual IDependencyRegistrar Register(
            Type type,
            Type implementation,
            string key = null,
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            var dependency = new DependencyDescription
            {
                Contract = type,
                Implementation = implementation,
                Key = key.IsEmpty() ? DependencyKey.None : DependencyKey.Name,
                KeyValue = key,
                Lifetime = lifetime
            };

            return this.Register(dependency);
        }

        public abstract IDependencyRegistrar Register(DependencyDescription dependency);

        public IDependencyRegistrar RegisterAssembly(Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
            {
                var attributes = type.GetTypeInfo().GetCustomAttributes<DependencyAttribute>();

                foreach (var attribute in attributes)
                {
                    if (string.IsNullOrWhiteSpace(attribute.Key) == false)
                    {
                        this.Register(attribute.Contract, type, attribute.Key, attribute.Lifetime);
                        continue;
                    }

                    this.Register(attribute.Contract, type, DependencyRegistrar.GetKeyOverrideString(type, attribute.KeyType),
                        attribute.Lifetime);
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

        public virtual IDependencyRegistrar RegisterFactory(
            Type type,
            Func<IDependencyResolver, object> factory,
            string key = null,
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            var dependency = new DependencyDescription
            {
                Contract = type,
                Key = key.IsEmpty() ? DependencyKey.None : DependencyKey.Name,
                KeyValue = key,
                Lifetime = lifetime
            };

            return this.RegisterFactory(dependency, factory);
        }

        public abstract IDependencyRegistrar RegisterFactory(DependencyDescription dependency, Func<IDependencyResolver, object> factory);

        public virtual IDependencyRegistrar RegisterInstance<T>(T instance,
            DependencyLifetime lifetime = default(DependencyLifetime))
        {
            this.InternalRegisterInstance(typeof(T), instance, lifetime);

            return this;
        }

        public virtual IDependencyRegistrar RegisterInstance(Type type, object instance,
            DependencyLifetime lifetime = default(DependencyLifetime))
        {
            var dependency = new DependencyDescription
            {
                Contract = type,
                Key = DependencyKey.None,
                Lifetime = lifetime
            };

            return this.RegisterInstance(dependency);
        }

        public abstract IDependencyRegistrar RegisterInstance(DependencyDescription dependency, object instance);

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
                return DependencyRegistrar.GetKeyOverrideString(type, attribute.Key);
            }

            return key;
        }

        private static string GetKeyOverrideString(Type type, DependencyKey key = DependencyKey.None)
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

        private void InternalRegister(Type type, Type implementation, string key = null,
            DependencyLifetime lifetime = DependencyLifetime.Default)
        {
            if (IgnoreDependencyAttribute.ValidateType(type) && IgnoreDependencyAttribute.ValidateType(implementation))
            {
                this.Register(type, implementation, DependencyRegistrar.GetKeyOverride(implementation, key),
                    DependencyRegistrar.GetLifetimeOverride(implementation, lifetime));
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
                this.RegisterFactory(type, factory, DependencyRegistrar.GetKeyOverride(type, key),
                    DependencyRegistrar.GetLifetimeOverride(type, lifetime));
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