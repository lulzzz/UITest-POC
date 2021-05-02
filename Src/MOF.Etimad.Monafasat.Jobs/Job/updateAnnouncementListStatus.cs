using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class updateAnnouncementListStatus : IJob
    {
        IAnnouncementListJobAppService announcementListJobAppService;

        public void Execute()
        {
            using (var serviceProvider = new AnnouncementListPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                announcementListJobAppService = serviceProvider.GetService<IAnnouncementListJobAppService>();
                Console.WriteLine("UpdateAnnouncementTemplateListStatus ===============(" + DateTime.Now.ToString() + ")==================== ");
                AsyncHelper.RunSync(() => announcementListJobAppService.updateAnnouncementListStatus());
                Console.WriteLine("UpdateAnnouncementTemplateListStatus is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
