using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class EMarketPlace : IJob
    {
        public void Execute()
        {
            ITenderAppJobService tenderAppService = null;
            using (var serviceProvider = new TenderPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                tenderAppService = serviceProvider.GetService<ITenderAppJobService>();
                Console.WriteLine("E-MarketPlace is  Start ===============(" + DateTime.Now.ToString() + ")==================== ");
                try
                {
                    AsyncHelper.RunSync(() => tenderAppService.SendFinalAwardedTendersToEmarketPlace());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Emarket place exception " + ex.Message);
                    LogException.Log(ex, "Emarket place");
                }
                Console.WriteLine("E-MarketPlace is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
