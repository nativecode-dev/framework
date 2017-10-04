namespace NativeCode.Core.Reliability
{
    using System;
    using Polly;

    public static class Retry
    {
        public static readonly TimeSpan Long = TimeSpan.FromMilliseconds(100);

        public static readonly TimeSpan Longer = TimeSpan.FromMilliseconds(200);

        public static readonly TimeSpan Short = TimeSpan.FromMilliseconds(10);

        public static void Forever(Action action)
        {
            Policy.Handle<Exception>().RetryForever().Execute(action);
        }

        public static void Forever(Action action, Action<Exception> callback)
        {
            Policy.Handle<Exception>().RetryForever(callback).Execute(action);
        }

        public static void Forever<T>(Func<T> action)
        {
            Policy.Handle<Exception>().RetryForever().Execute(action);
        }

        public static T Forever<T>(Func<T> action, Action<Exception> callback)
        {
            return Policy.Handle<Exception>().RetryForever(callback).Execute(action);
        }

        public static void Until(Action action, int retries)
        {
            Policy.Handle<Exception>().Retry(retries).Execute(action);
        }

        public static void Until(Action action, int retries, Action<Exception, int> callback)
        {
            Policy.Handle<Exception>().Retry(retries, callback).Execute(action);
        }

        public static T Until<T>(Func<T> action, int retries)
        {
            return Policy.Handle<Exception>().Retry(retries).Execute(action);
        }

        public static T Until<T>(Func<T> action, int retries, Action<Exception, int> callback)
        {
            return Policy.Handle<Exception>().Retry(retries, callback).Execute(action);
        }
    }
}