using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices
{
    public class InvitationNotification : IInvitationNotification
    {
        private readonly INotificationAppService _notification;
        private readonly IConfiguration _configuration;

        public InvitationNotification(INotificationAppService notificationservive, IConfiguration configuration )
        {
            _notification = notificationservive;
            _configuration = configuration;
          
        }
        #region Methods
        public async Task<bool> SendInvitationByEmail(List<string> suppliersEmails, Tender tender)
        {
            // get supplier email form identity server by (offer.SuplierId)
            Models.EmailModel emailModel = new Models.EmailModel();
            var monafasatUrl = "(https://monafasat.etimad.sa)";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersEmails)
            {
                dic.Add(item, string.Format(_configuration.GetSection("InvitationEmail:SendInvitationByEmail:body").Value,
                     tender.TenderName, monafasatUrl));
                emailModel.Subject = string.Format(string.Format(_configuration.GetSection("InvitationEmail:SendInvitationByEmail:subject").Value, tender.TenderName));
            };
            emailModel.ListOfEmails = dic;
            return await _notification.SendEmail(emailModel);
        }
        public async Task<bool> SendInvitationByEmailToSuppliers(List<SupplierInvitationModel> suppliers, Tender tender)
        {
            Models.EmailModel emailModel = new Models.EmailModel();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliers)
            {
                foreach (var email in item.SupplierContactOfficersEmails)
                {
                    dic.Add(email, string.Format(
                          _configuration.GetSection("SupplierInvitation:SendInvitation:body").Value,
                          item.SupplierName, tender.TenderNumber));
                    emailModel.Subject = string.Format(_configuration.GetSection("SupplierInvitation:SendInvitation:subject").Value, tender.TenderName);
                }
            }
            emailModel.ListOfEmails = dic;
            return await _notification.SendEmail(emailModel);
        }
        public async Task<bool> SendInvitationBySms(List<string> suppliersMobileNo, Tender tender)
        {
            Models.SmsModel smsModel = new Models.SmsModel();
            var monafasatUrl = "(https://monafasat.etimad.sa)";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersMobileNo)
            {
                dic.Add(item, string.Format(string.Format(_configuration.GetSection("InvitationEmail:SendInvitationBySms").Value, tender.TenderName, monafasatUrl)));
            }
            smsModel.ListOfNumbers = dic;
            return await _notification.SendOneSms(smsModel);
        }
        public async Task<bool> SendInvitationsBySmsToSuppliers(List<SupplierInvitationModel> suppliers, Tender tender)
        {
            Models.SmsModel smsModel = new Models.SmsModel();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliers)
            {
                foreach (var email in item.SupplierContactOfficersEmails)
                {
                    dic.Add(email, string.Format(_configuration.GetSection("SupplierInvitation:SendInvitationBySms").Value, item.SupplierName, tender.TenderName));
                }

            }
            smsModel.ListOfNumbers = dic;
            return await _notification.SendSms(smsModel);
        }



        public async Task<bool> SendApproveJoiningRequestByEmail(List<string> suppliersEmails, string TenderNumber)
        {
            // get supplier email form identity server by (offer.SuplierId)
            Models.EmailModel emailModel = new Models.EmailModel();
            var monafasatUrl = "(https://monafasat.etimad.sa)";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersEmails)
            {
                dic.Add(item, string.Format(_configuration.GetSection("ApproveJoiningSupplierOnTender:SendInvitationByEmail:body").Value,
                     TenderNumber, monafasatUrl));
                emailModel.Subject = string.Format(string.Format(_configuration.GetSection("ApproveJoiningSupplierOnTender:SendInvitationByEmail:subject").Value, TenderNumber));
            };
            emailModel.ListOfEmails = dic;
            return await _notification.SendEmail(emailModel);
        }

        public async Task<bool> SendApproveJoiningRequestBySms(List<string> suppliersMobileNo, string TenderNumber)
        {
            Models.SmsModel smsModel = new Models.SmsModel();
            var monafasatUrl = "(https://monafasat.etimad.sa)";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersMobileNo)
            {
                dic.Add(item, string.Format(string.Format(_configuration.GetSection("ApproveJoiningSupplierOnTender:SendInvitationBySms").Value, TenderNumber, monafasatUrl)));
            }
            smsModel.ListOfNumbers = dic;
            return await _notification.SendSms(smsModel);
        }

        public async Task<bool> SendRejectJoiningRequestByEmail(List<string> suppliersEmails, string TenderNumber)
        {
            // get supplier email form identity server by (offer.SuplierId)
            Models.EmailModel emailModel = new Models.EmailModel();
            var monafasatUrl = "(https://monafasat.etimad.sa)";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersEmails)
            {
                dic.Add(item, string.Format(_configuration.GetSection("RejectJoiningSupplierOnTender:SendInvitationByEmail:body").Value,
                     TenderNumber, monafasatUrl));
                emailModel.Subject = string.Format(string.Format(_configuration.GetSection("RejectJoiningSupplierOnTender:SendInvitationByEmail:subject").Value, TenderNumber));
            };
            emailModel.ListOfEmails = dic;
            return await _notification.SendEmail(emailModel);
        }

        public async Task<bool> SendRejectJoiningRequestBySms(List<string> suppliersMobileNo, string TenderNumber)
        {
            Models.SmsModel smsModel = new Models.SmsModel();
            var monafasatUrl = "(https://monafasat.etimad.sa)";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersMobileNo)
            {
                dic.Add(item, string.Format(string.Format(_configuration.GetSection("RejectJoiningSupplierOnTender:SendInvitationBySms").Value, TenderNumber, monafasatUrl)));
            }
            smsModel.ListOfNumbers = dic;
            return await _notification.SendSms(smsModel);
        }
        #endregion
    }
}
