namespace NativeCode.Core.Extensions
{
    using System;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    public static class ObjectExtensions
    {
        public static void DisposeIfNeeded([NotNull] this object instance)
        {
            (instance as IDisposable)?.Dispose();
        }

        public static void Ensure([NotNull] this object instance, [NotNull] Type type)
        {
            if (instance.Not(type))
            {
                throw new InvalidCastException();
            }
        }

        public static bool Is([NotNull] this object instance, [NotNull] Type type)
        {
            return instance.GetType() == type;
        }

        public static bool Is<T>([NotNull] this T instance)
        {
            return instance.Is(typeof(T));
        }

        public static bool Not([NotNull] this object instance, [NotNull] Type type)
        {
            return !instance.Is(type);
        }

        public static bool Not<T>([NotNull] this T instance)
        {
            return instance.Not(typeof(T));
        }

        [NotNull]
        public static string TypeKey([NotNull] this object instance)
        {
            return instance.GetType().AssemblyQualifiedName;
        }
    }
}