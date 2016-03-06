namespace NativeCode.Core.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    using JetBrains.Annotations;

    public static class TypeExtensions
    {
        public static bool HasBaseClass([NotNull] this Type source, [NotNull] Type type)
        {
            var current = source.GetTypeInfo();

            while (current.BaseType != null)
            {
                if (current.BaseType == type)
                {
                    return true;
                }

                current = current.BaseType.GetTypeInfo();
            }

            return false;
        }

        public static bool HasBaseClass<T>([NotNull] this Type source) where T : class
        {
            return source.HasBaseClass(typeof(T));
        }

        public static bool HasInterface([NotNull] this Type source, [NotNull] Type type)
        {
            return source.GetTypeInfo().ImplementedInterfaces.Any(x => x == type);
        }

        public static bool HasInterface<T>([NotNull] this Type source) where T : class
        {
            return source.HasInterface(typeof(T));
        }

        public static string ToKey([NotNull] this Type source)
        {
            return source.ToKey(x => x.AssemblyQualifiedName);
        }

        public static string ToKey([NotNull] this Type source, [NotNull] Func<Type, string> selector)
        {
            return selector(source);
        }
    }
}