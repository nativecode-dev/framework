namespace NativeCode.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using JetBrains.Annotations;

    public static class EnumerableExtensions
    {
        [CanBeNull]
        public static T Get<T>([NotNull] this IEnumerable<object> collection, int index, T defaultValue = default(T))
        {
            var value = collection.ElementAt(index);

            if (object.Equals(value, defaultValue) == false)
            {
                return (T) value;
            }

            return defaultValue;
        }

        [NotNull]
        public static T SafeGet<T>([NotNull] this IEnumerable<T> collection, int index, T defaultValue = default(T))
        {
            try
            {
                var value = collection.ElementAtOrDefault(index);

                if (object.Equals(value, defaultValue) == false)
                {
                    return value;
                }
            }
            catch
            {
                return defaultValue;
            }

            return defaultValue;
        }

        public static IEnumerable<T> TakeUntil<T>([NotNull] this IEnumerable<T> collection,
            CancellationToken cancellationToken)
        {
            return collection.TakeWhile(x => !cancellationToken.IsCancellationRequested);
        }
    }
}