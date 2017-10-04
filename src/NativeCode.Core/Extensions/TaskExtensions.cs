namespace NativeCode.Core.Extensions
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    public static class TaskExtensions
    {
        public static bool IsDone([NotNull] this Task task)
        {
            return task.IsCompleted || task.IsErrorState();
        }

        public static bool IsErrorState([NotNull] this Task task)
        {
            return task.IsCanceled || task.IsFaulted;
        }

        public static ConfiguredTaskAwaitable Capture([NotNull] this Task task)
        {
            return task.ConfigureAwait(true);
        }

        public static ConfiguredTaskAwaitable<T> Capture<T>([NotNull] this Task<T> task)
        {
            return task.ConfigureAwait(true);
        }

        public static ConfiguredTaskAwaitable NoCapture([NotNull] this Task task)
        {
            return task.ConfigureAwait(false);
        }

        public static ConfiguredTaskAwaitable<T> NoCapture<T>([NotNull] this Task<T> task)
        {
            return task.ConfigureAwait(false);
        }
    }
}