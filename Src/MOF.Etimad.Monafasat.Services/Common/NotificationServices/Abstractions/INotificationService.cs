using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices.Abstractions
{
    public interface INotificationService
    {
        Task<bool> SendEmail(EmailModel model);
        Task<bool> SendSms(SmsModel model);

        Task<bool> SendOneEmail(EmailModel model);
        Task<bool> SendOneSms(SmsModel model);
    }
}
