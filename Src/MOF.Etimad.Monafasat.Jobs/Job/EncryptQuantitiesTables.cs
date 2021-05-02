using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class EncryptQuantitiesTables : IJob
    {
         IOfferAppService offerAppService= null;
      
        public   void Execute()
        {
            using (var serviceProvider = new PrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                offerAppService = serviceProvider.GetService<IOfferAppService>();
                AsyncHelper.RunSync(() => offerAppService.EncryptQuantitiesTables());

            }
        }
    }
}
