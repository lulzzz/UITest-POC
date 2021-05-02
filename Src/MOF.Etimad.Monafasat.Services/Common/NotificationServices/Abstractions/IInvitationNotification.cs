using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices
{
    public interface IInvitationNotification
    {
        Task<bool> SendInvitationByEmail(List<string> suppliersEmails, Tender tender);
        Task<bool> SendInvitationByEmailToSuppliers(List<SupplierInvitationModel> suppliers, Tender tender);
        Task<bool> SendInvitationBySms(List<string> suppliersMobileNo, Tender tender);
        Task<bool> SendInvitationsBySmsToSuppliers(List<SupplierInvitationModel> suppliers, Tender tender);
        Task<bool> SendApproveJoiningRequestBySms(List<string> suppliersMobileNo, string TenderNumber);
        Task<bool> SendApproveJoiningRequestByEmail(List<string> suppliersEmails, string TenderNumber);
        Task<bool> SendRejectJoiningRequestBySms(List<string> suppliersMobileNo, string TenderNumber);
        Task<bool> SendRejectJoiningRequestByEmail(List<string> suppliersEmails, string TenderNumber);
    }
}
