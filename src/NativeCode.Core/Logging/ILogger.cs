namespace NativeCode.Core.Logging
{
    using System;

    using JetBrains.Annotations;

    public interface ILogger
    {
        void Debug([NotNull] string message);

        void Error([NotNull] string message);

        void Exception<TException>([NotNull] TException exception) where TException : Exception;

        void Informational([NotNull] string message);

        void Warning([NotNull] string message);
    }
}