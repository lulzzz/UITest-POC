using MOF.Etimad.Monafasat.Integration.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Integration
{
    public interface INotificationProxy
    {
        Task<bool> SendEmail(string emailAddress, string emailSubject, string emailContent, string emailCC = null, string emailBCC = null);
        Task<bool> SendSMS(string mobileNumber, string messageContent);

        Task<bool> SendBulkEmail(List<EmailMessageNoitification> lstEmails);
        Task<bool> SendBulkSMS(List<SMSMessageNoitification> lstSMSs);
    }
}
