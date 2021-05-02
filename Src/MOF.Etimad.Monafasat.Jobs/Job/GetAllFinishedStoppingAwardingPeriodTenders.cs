using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;


namespace MOF.Etimad.Monafasat.Jobs
{
    public class GetAllFinishedStoppingAwardingPeriodTenders : IJob
    {
        public void Execute()
        {
            ITenderAppJobService tenderAppService = null;

            using (var serviceProvider = new TenderPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                tenderAppService = serviceProvider.GetService<ITenderAppJobService>();
                Console.WriteLine("GetAllFinishedStoppingAwardingPeriodTenders is  Start ===============(" + DateTime.Now.ToString() + ")==================== ");
                AsyncHelper.RunSync(() => tenderAppService.SendNotificatoinAfterFinishTendersStoppingPeriod());
                Console.WriteLine("GetAllFinishedStoppingAwardingPeriodTenders is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
