using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Jobs
{
    public class ResendNotification : IJob
    {
        INotificationResendJobService _notificationJobService = null;
        public void Execute()
        {
            using (var serviceProvider = new ResendNotificationPrepareDependancies().SetupDependancies().BuildServiceProvider())
            {
                _notificationJobService = serviceProvider.GetService<INotificationResendJobService>();
                Console.WriteLine("Resend Notification is  Start ===============(" + DateTime.Now.ToString() + ")==================== ");
                AsyncHelper.RunSync(() => _notificationJobService.ResendFailNotification());
                Console.WriteLine("Resend Notification is  End ... " + DateTime.Now.ToString());
            }
        }
    }
}
