namespace NativeCode.Core.Types
{
    using System;

    using Polly;

    public static class Retry
    {
        public static readonly TimeSpan Long = TimeSpan.FromMilliseconds(100);

        public static readonly TimeSpan Longer = TimeSpan.FromMilliseconds(200);

        public static readonly TimeSpan Short = TimeSpan.FromMilliseconds(10);

        public static void Limit(Action action, int retries = 5)
        {
            Policy.Handle<Exception>().Retry(retries).Execute(action);
        }

        public static void Limit<T>(Func<T> action, int retries = 5)
        {
            Policy.Handle<Exception>().Retry(retries).Execute(action);
        }
    }
}