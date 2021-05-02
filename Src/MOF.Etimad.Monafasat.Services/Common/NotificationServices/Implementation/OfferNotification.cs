using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices.Implementation
{
    public class OfferNotification : IOfferNotification
    {
        private readonly INotificationService _notification;
        private readonly IConfiguration configuration;
        public OfferNotification(INotificationService notificationservive, IConfiguration configuration)
        {
            _notification = notificationservive;

            //configuration = new ConfigurationBuilder()
            //  .SetBasePath(Directory.GetCurrentDirectory())
            //  .AddJsonFile("NotificationMessages.json")
            //  .Build();
            this.configuration = configuration;


        }
        public async Task<bool> ConfirmReceived(Offer offer)
        {

            var supplierEmail = "test@test.com";// test data
            var emailModel = new Models.EmailModel
            {
                To = new List<string> { supplierEmail },
                Subject = configuration.GetSection("Email:ConfirmReceived:subject").Value,
                Body = string.Format(
                    configuration.GetSection("Email:ConfirmReceived:body").Value,
                    offer.OfferId)
            };

            // get supplier mobile number form identity server by (offer.SuplierId)
            var supplierMobile = "0511111111";// test data


            var smsModel = new Models.SmsModel
            {
                To = new List<string> { supplierMobile },
                Body = string.Format(
                    configuration.GetSection("SMS:ConfirmReceived").Value,
                    offer.OfferId)
            };
            var task2 = await _notification.SendSms(smsModel);
            var task1 = await _notification.SendEmail(emailModel);



            return (task1 == true && task1 == true);

        }

        public async Task<bool> SendOffer(Offer offer)
        {
            // get supplier email form identity server by (offer.SuplierId)

            var supplierEmail = "test@test.com";// test data
            var emailModel = new Models.EmailModel
            {
                To = new List<string> { supplierEmail },
                Subject = configuration.GetSection("Email:SendOffer:subject").Value,
                Body = string.Format(
                    configuration.GetSection("Email:SendOffer:body").Value,
                    offer.OfferId)
            };

            // get supplier mobile number form identity server by (offer.SuplierId)
            var supplierMobile = "0511111111";// test data


            var smsModel = new Models.SmsModel
            {
                To = new List<string> { supplierMobile },
                Body = string.Format(
                    configuration.GetSection("SMS:SendOffer").Value,
                    offer.OfferId)
            };
            var task1 = await _notification.SendEmail(emailModel);
            var task2 = await _notification.SendSms(smsModel);

            return (task1 == true && task1 == true);
        }
    }
}
