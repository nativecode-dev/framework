namespace NativeCode.Core.Extensions
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static bool IsDone(this Task task)
        {
            return task.IsCompleted || task.IsErrorState();
        }

        public static bool IsErrorState(this Task task)
        {
            return task.IsCanceled || task.IsFaulted;
        }

        public static ConfiguredTaskAwaitable Capture(this Task task)
        {
            return task.ConfigureAwait(true);
        }

        public static ConfiguredTaskAwaitable<T> Capture<T>(this Task<T> task)
        {
            return task.ConfigureAwait(true);
        }

        public static ConfiguredTaskAwaitable NoCapture(this Task task)
        {
            return task.ConfigureAwait(false);
        }

        public static ConfiguredTaskAwaitable<T> NoCapture<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false);
        }
    }
}