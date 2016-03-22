namespace NativeCode.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using JetBrains.Annotations;

    public static class EnumerableExtensions
    {
        public static T Get<T>(this object[] collection, int index, T defaultValue = default(T))
        {
            var value = collection.ElementAtOrDefault(index);

            if (Equals(value, defaultValue) == false)
            {
                return (T)value;
            }

            return defaultValue;
        }

        public static T SafeGet<T>(this IEnumerable<T> collection, int index, T defaultValue = default(T))
        {
            var value = collection.ElementAtOrDefault(index);

            if (Equals(value, defaultValue) == false)
            {
                return value;
            }

            return defaultValue;
        }

        public static IEnumerable<T> TakeUntil<T>([NotNull] this IEnumerable<T> collection, CancellationToken cancellationToken)
        {
            return collection.TakeWhile(x => !cancellationToken.IsCancellationRequested);
        }
    }
}