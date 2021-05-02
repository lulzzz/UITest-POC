using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Jobs.Helper;
using MOF.Etimad.Monafasat.Services.JobServices.Qualification;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    class QualificationReCalculateSupplierPoints : IJob
    {
        IQulaificationServiceJob _qulaificationServiceJob = null;

        public void Execute()
        {

            using (var serviceProvider = new QualificatonPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                _qulaificationServiceJob = serviceProvider.GetService<IQulaificationServiceJob>();
                Console.WriteLine("Recalculate Supplier Qualification Points ===============(" + DateTime.Now.ToString() + ")==================== ");
                try
                {
                    AsyncHelper.RunSync(() => _qulaificationServiceJob.RecalculateSupplierPoint());
                }
                catch (Exception ex)
                {

                    LogException.Log(ex, "Recalculate Supplier Qaulification Points");
                }

                Console.WriteLine("Recalculate Supplier Qualification Points is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
