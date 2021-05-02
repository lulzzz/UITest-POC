using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class CheckFinishedBiddingsRounds : IJob
    {
        ITenderAppJobService _tenderJobService = null;
        public CheckFinishedBiddingsRounds()
        {
            ServiceCollection services = new TenderPrepareDependancies().SetupDependancies();
            services.BuildServiceProvider();
        }

        public void Execute()
        {
            using (var serviceProvider = new TenderPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                _tenderJobService = serviceProvider.GetService<ITenderAppJobService>();
                Console.WriteLine("CheckFinishedBiddingsRounds ===============(" + DateTime.Now.ToString() + ")==================== ");
                AsyncHelper.RunSync(() => _tenderJobService.CheckBiddingTenderRounds());
                Console.WriteLine("CheckFinishedBiddingsRounds is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
