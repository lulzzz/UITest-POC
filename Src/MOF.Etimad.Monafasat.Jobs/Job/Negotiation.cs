using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class Negotiation : IJob
    {
        ICommunicationRequestAppJobService _tenderJobService = null;
        public void Execute()
        {

            using (var serviceProvider = new CommunicationRequestPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                _tenderJobService = serviceProvider.GetService<ICommunicationRequestAppJobService>();
                Console.WriteLine("Negotiation ===============(" + DateTime.Now.ToString() + ")==================== ");
                try
                {
                    AsyncHelper.RunSync(() => _tenderJobService.UpdateAllNegotiationWaitingSupplierResponse());

                }
                catch (Exception ex)
                {

                    LogException.Log(ex, "Negotiation First Stage Service");
                }
                try
                {
                    AsyncHelper.RunSync(() => _tenderJobService.UpdateAllSecondNegotiationWaitingSupplierResponse());

                }
                catch (Exception ex)
                {

                    LogException.Log(ex, "Negotiation Second Stage Service");
                }
                Console.WriteLine("Negotiation is  End ... " + DateTime.Now.ToString());
            }


        }


    }
}
