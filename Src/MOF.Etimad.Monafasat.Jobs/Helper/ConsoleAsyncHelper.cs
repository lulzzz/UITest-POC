using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class ConsoleAsyncHelper
    {
        private readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None,
          TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;
            return _myTaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }

        public void RunSync(Func<Task> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;
            _myTaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }
    }
}
