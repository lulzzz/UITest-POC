using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Abstractions;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationProxy _proxy = ProxyResolver.Resolve<INotificationProxy>();

        public async Task<bool> SendEmail(EmailModel model)
        {
            List<bool> tasks = new List<bool>();
            foreach (var item in model.ListOfEmails)
            {
                tasks.Add(await _proxy.SendEmail(item.Key, model.Subject, item.Value));
            }
            return tasks.Any(x => x == false);

        }

        public async Task<bool> SendOneEmail(EmailModel model)
        {
           
            return await _proxy.SendEmail(model.To.FirstOrDefault(), model.Subject, model.Body);
        }

        public async Task<bool> SendOneSms(SmsModel model)
        {
            return await _proxy.SendSMS(model.To.FirstOrDefault(), model.Body);
        }

        public async Task<bool> SendSms(SmsModel model)
        {
            List<bool> tasks = new List<bool>();
            foreach (var item in model.ListOfNumbers)
            {
                await _proxy.SendSMS(item.Key, item.Value);
            }
            return tasks.Any(x => x == false);


        }
    }
}
