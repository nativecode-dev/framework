namespace NativeCode.Core.Extensions
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.Extensions.Logging;

    public static class LoggingExtensions
    {
        public static void Exception([NotNull] this ILogger logger, Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }
}