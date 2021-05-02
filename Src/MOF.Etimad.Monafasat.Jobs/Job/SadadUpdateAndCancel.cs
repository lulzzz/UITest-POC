using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class SadadUpdateAndCancel : IJob
    {
        IBillJobAppService billAppService = null;

        public void Execute()
        {

            using (var serviceProvider = new BillingPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                billAppService = serviceProvider.GetService<IBillJobAppService>();
                Console.WriteLine("SadadUpdateAndCancel is  Start ===============(" + DateTime.Now.ToString() + ")==================== ");
                AsyncHelper.RunSync(() => billAppService.UpdateBillThroughTahseel());
                Console.WriteLine("SadadUpdateAndCancel is  End ... " + DateTime.Now.ToString());
            }

        }
    }
}
