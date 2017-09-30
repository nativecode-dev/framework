namespace NativeCode.Core.Extensions
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.Extensions.Logging;
    using Platform.Serialization;

    public static class LoggingExtensions
    {
        public static void Critical([NotNull] this ILogger logger, Exception ex)
        {
            logger.LogCritical(ex, ex.Message);
        }

        public static void Exception([NotNull] this ILogger logger, Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        public static void JsonInfo<T>([NotNull] this ILogger logger, T instance, IStringSerializer serializer)
        {
            logger.LogInformation(serializer.Serialize(instance));
        }
    }
}