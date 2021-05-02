using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class OpenOffers : IJob
    {
        ITenderAppJobService _tenderJobSerivce = null;
        public void Execute()
        {
            using (var serviceProvider = new TenderPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                _tenderJobSerivce = serviceProvider.GetService<ITenderAppJobService>();
                Console.WriteLine("OpenOffers ===============(" + DateTime.Now.ToString() + ")==================== ");
                AsyncHelper.RunSync(() => _tenderJobSerivce.GetTenderOffersForOpening(0));
                Console.WriteLine("OpenOffers is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
