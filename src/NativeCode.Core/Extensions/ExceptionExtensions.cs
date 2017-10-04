namespace NativeCode.Core.Extensions
{
    using System;
    using System.Text;
    using JetBrains.Annotations;

    public static class ExceptionExtensions
    {
        [NotNull]
        public static StringBuilder CreateExceptionBuilder<TException>([NotNull] this TException exception,
            bool includeStackTrace = true)
            where TException : Exception
        {
            var builder = new StringBuilder();

            var aggregate = exception as AggregateException;

            if (aggregate != null)
            {
                foreach (var ex in aggregate.InnerExceptions)
                {
                    ExceptionExtensions.WriteException(builder, ex, includeStackTrace);
                }
            }
            else
            {
                ExceptionExtensions.WriteException(builder, exception, includeStackTrace);
            }

            return builder;
        }

        [NotNull]
        public static string ToExceptionString<TException>([NotNull] this TException exception, bool includeStackTrace = true)
            where TException : Exception
        {
            return exception.CreateExceptionBuilder(includeStackTrace).ToString();
        }

        private static void WriteException(StringBuilder builder, Exception exception, bool includeStackTrace)
        {
            var current = exception;

            while (true)
            {
                builder.AppendLine(current.Message);

                if (includeStackTrace)
                {
                    builder.AppendLine(current.StackTrace);
                }

                if (current.InnerException != null)
                {
                    current = current.InnerException;
                    continue;
                }

                break;
            }
        }
    }
}