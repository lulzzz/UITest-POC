using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class CheckingOffers : IJob
    {
        ITenderAppJobService _tenderJobSerivce = null;
        public void Execute()
        {
            using (var serviceProvider = new TenderPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                _tenderJobSerivce = serviceProvider.GetService<ITenderAppJobService>();
                Console.WriteLine("CheckingOffers ===============(" + DateTime.Now.ToString() + ")==================== ");
                AsyncHelper.RunSync(() => _tenderJobSerivce.GetTenderOffersForChecking());
                Console.WriteLine("CheckingOffers is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
