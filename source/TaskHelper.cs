using System;
using System.Threading;
using System.Threading.Tasks;

namespace SMI
{
    internal static class TaskHelper
    {
        public static void RunSync<T>(Func<Task<T>> func)
        {
            var thd = new Thread(() => func().ConfigureAwait(false).GetAwaiter().GetResult());
            thd.IsBackground = true;
            thd.Start();
            thd.Join();
        }

        public static TResult RunSyncWithReturn<TResult>(Func<Task<TResult>> func)
        {
            TResult result = default;
            var thd = new Thread(() => result = func().ConfigureAwait(false).GetAwaiter().GetResult());
            thd.IsBackground = true;
            thd.Start();
            thd.Join();

            return result;
        }

        public static TResult RunSyncWithReturn<TResult, T>(Func<T, Task<TResult>> func, T x)
        {
            TResult result = default;
            var thd = new Thread(() => result = func(x).ConfigureAwait(false).GetAwaiter().GetResult());
            thd.IsBackground = true;
            thd.Start();
            thd.Join();

            return result;
        }

        public static TResult RunSyncWithReturn<TResult, T, T1>(Func<T, T1, Task<TResult>> func, T x, T1 x1)
        {
            TResult result = default;
            var thd = new Thread(() => result = func(x, x1).ConfigureAwait(false).GetAwaiter().GetResult());
            thd.IsBackground = true;
            thd.Start();
            thd.Join();

            return result;
        }
    }
}
