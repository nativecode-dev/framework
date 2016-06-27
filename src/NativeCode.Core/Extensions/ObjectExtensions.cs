namespace NativeCode.Core.Extensions
{
    using System;

    using JetBrains.Annotations;

    public static class ObjectExtensions
    {
        public static void DisposeIfNeeded(this object instance)
        {
            (instance as IDisposable)?.Dispose();
        }

        public static void Ensure([NotNull] this object instance, Type type)
        {
            if (instance.Not(type))
            {
                throw new InvalidCastException();
            }
        }

        public static bool Is(this object instance, Type type)
        {
            if (instance == null)
            {
                return false;
            }

            return instance.GetType() == type;
        }

        public static bool Not(this object instance, Type type)
        {
            return !instance.Is(type);
        }

        public static string ToKey(this object instance)
        {
            return instance.GetType().AssemblyQualifiedName;
        }
    }
}