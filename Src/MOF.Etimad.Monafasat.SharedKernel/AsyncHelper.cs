using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None,
            TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;
            var task = _myTaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            });
            task.ContinueWith(cnt => cnt.Dispose());
            var awaiter = task.Unwrap().GetAwaiter();
            return awaiter.GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;
            var task = _myTaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            });
            task.ContinueWith(cnt => cnt.Dispose());
            var awaiter = task.Unwrap().GetAwaiter();
            awaiter.GetResult();
        }
    }
}
