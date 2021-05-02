
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class UpdateOfferStatus : IJob
    {
        ICommunicationRequestAppJobService communicationAppJobService = null;
        public void Execute()
        {
            using (var serviceProvider = new CommunicationRequestPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                communicationAppJobService = serviceProvider.GetService<ICommunicationRequestAppJobService>();
                Console.WriteLine("UpdateOfferStatus ===============(" + DateTime.Now.ToString() + ")==================== ");
                AsyncHelper.RunSync(() => communicationAppJobService.ExcludeSupplierOffer());
                Console.WriteLine("UpdateOfferStatus is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
