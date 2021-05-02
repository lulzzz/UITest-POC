using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Models;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class NotificationResendJobService : INotificationResendJobService
    {
        public NotificationResendJobService(INotificationQueries iNotificationQuerie, IINotificationCommands notificationCommands, INotificationProxy notificationProxy)
        {
            _NotificationQuerie = iNotificationQuerie;
            _NotificationCommands = notificationCommands;
            _NotificationProxy = notificationProxy;
        }

        public INotificationQueries _NotificationQuerie { get; }
        public IINotificationCommands _NotificationCommands { get; }
        public INotificationProxy _NotificationProxy { get; }

        public async Task ResendFailNotification()
        {
            var notifications = (await _NotificationQuerie.GetNewNotification());
            int count = 0;
            foreach (var item in notifications)
            {
                count++;
                if (item is NotificationSMS)
                {
                    var sms = new Common.NotificationServices.Models.SmsModel();
                    sms.To.Add(((NotificationSMS)item).CurrentMobile);
                    sms.Body = ((NotificationSMS)item).Content;
                    if (await SendOneSms(sms))
                    {
                        ((NotificationSMS)item).Update(Enums.NotifacationStatus.Send);
                    }
                    else
                    {
                        ((NotificationSMS)item).Update(Enums.NotifacationStatus.FailedToSend);
                    }
                    _NotificationCommands.UpdateNotifayWithOutSave(item);
                }
                if (item is NotificationEmail)
                {
                    var email = new Common.NotificationServices.Models.EmailModel();
                    email.To.Add(((NotificationEmail)item).CurrentEmail);
                    email.Body = ((NotificationEmail)item).Content;
                    email.Subject = ((NotificationEmail)item).Title;
                    if (await SendOneEmail(email))
                    {
                        ((NotificationEmail)item).Update(Enums.NotifacationStatus.Send);
                    }
                    else
                    {
                        ((NotificationEmail)item).Update(Enums.NotifacationStatus.FailedToSend);
                    }
                    _NotificationCommands.UpdateNotifayWithOutSave(item);
                }
                if (count == 100)
                {
                    await _NotificationCommands.SaveChangesAsync();
                    count = 0;
                }
            }
            await _NotificationCommands.SaveChangesAsync();
        }

        public async Task<bool> SendOneSms(SmsModel model)
        {
            return await _NotificationProxy.SendSMS(model.To.FirstOrDefault(), model.Body);
        }
        public async Task<bool> SendOneEmail(EmailModel model)
        {
            return await _NotificationProxy.SendEmail(model.To.FirstOrDefault(), model.Subject, model.Body);
        }
    }
}
