using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;


namespace MOF.Etimad.Monafasat.Jobs
{
    public class Plaint : IJob
    {
        ICommunicationRequestAppJobService _palintJobService = null;
        public void Execute()
        {
            using (var serviceProvider = new CommunicationRequestPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                _palintJobService = serviceProvider.GetService<ICommunicationRequestAppJobService>();
                Console.WriteLine("Plaint ===============(" + DateTime.Now.ToString() + ")==================== ");
                AsyncHelper.RunSync(() => _palintJobService.FindTendersWithPlaintsAfterStoppingPeriodJob());
                Console.WriteLine("Plaint is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
