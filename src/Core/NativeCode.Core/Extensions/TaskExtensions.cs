namespace NativeCode.Core.Extensions
{
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
    }
}