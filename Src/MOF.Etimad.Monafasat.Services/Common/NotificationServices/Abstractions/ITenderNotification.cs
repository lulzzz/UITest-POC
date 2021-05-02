using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices
{
    public interface ITenderNotification
    {
        //Task<bool> SendNotificationForUpdatingTenderStatus(Tender tender);
        Task<bool> SendNotificationForEnquiryReply(EnquiryReply enquiryReply);
        Task<bool> SendNotificationForJoinCommittee(JoinTechnicalCommitteeModel joinTechnicalCommittee);
        Task<bool> SendVerificationCode(string activationCode, string userMobileNo, string userEmail);
        Task<bool> SendPurchaseSadadNumber(string tenderNumber, string sadadNumber, string mobileNumber, string userEmail);
        Task<bool> SendSuccessPayment(string tenderNumber, string mobileNumber, string userEmail);
        Task<bool> SendNotificationToSuppliersForChangesInQuantityTable(string tenderNumber, int tenderId, List<Supplier> suppliers);
        Task<bool> SendNotificationForChangesInTenderToAuditer(Tender tender, Enums.UserType receiverType);
        Task<bool> SendNotificationForApprinvingChangesInTender(Tender tender, Enums.UserType receiverType);
        Task<bool> SendNotificationForRejectingChangesInTender(Tender tender, Enums.UserType receiverType);
        Task<bool> SendNotificationForInvitationToSuppliers(Tender tender, List<string> suppliers);
        Task<bool> SendNotificationForRequestingApproveTenderToAuditor(Tender tender);
        Task<bool> SendNotificationForEnquiryAddition(Enquiry enquiry, List<int> technicalCommitteeUserslist, Tender tender);
        Task<bool> SendNotificationForAcceptingApprovingTenderToDataEntry(Tender tender);
        Task<bool> SendNotificationFoRejectingApprovingTenderToDataEntry(Tender tender);
        Task<bool> ToHigherAuthorityNewCancelRequestCreated(Tender tender, String whoRoleName);
        Task<bool> ToHigherAuthorityNewUpdateRequestCreated(Tender tender, String whoRoleName, int changeType);
        Task<bool> ToLowerAuthorityCancelRequestApproved(Tender tender,String whoRoleName);
        Task<bool> ToLowerAuthorityCancelRequestRejected(Tender tender, String whoRoleName);
        Task<bool> ToLowerAuthorityExtendDateRejected(Tender tender, String whoRoleName);
        //string getHigherAuthorityByRoleName(String whoRoleName);
        //string getLowerAuthorityByRoleName(String whoRoleName);
        Task<bool> ToSuppliersTenderCancelled(Tender tender, List<string> suppliersCr,bool isFavourite);
        Task<bool> SendNotificationToSuppliersForChangesInAttachments(Tender tender, List<string> suppliers, bool isFavourite);

        Task<bool> ToSuppliersExtendDateApproved(Tender tender, List<string> suppliers, bool isFavourite);
        Task<bool> ToDataEntryExtendDateApproved(Tender tender);
        Task<bool> SendNotificationForRequestOpeningApproval(Tender tender, List<CommitteeUser> OpeningCommitteeUserslist);
        Task<bool> SendNotificationForRequestOpeningApproved(Tender tender, List<CommitteeUser> OpeningCommitteeUserslist);
        Task<bool> SendNotificationForRequestOpeningRejected(Tender tender, List<CommitteeUser> OpeningCommitteeUserslist);
        Task<bool> SendNotificationForRequestCheckingApproval(Tender tender, List<CommitteeUser> checkingCommitteeUserslist);
        Task<bool> SendNotificationForRequestCheckingApproved(Tender tender, List<CommitteeUser> checkingCommitteeUserslist);
        Task<bool> SendNotificationForRequestCheckingRejected(Tender tender, List<CommitteeUser> checkingCommitteeUserslist);
        Task<bool> SendNotificationForRequestAwardingApproval(Tender tender, List<CommitteeUser> checkingCommitteeUserslist);
        Task<bool> SendNotificationForRequestAwardingApproved(Tender tender, List<string> offerSuppliersCrs, List<CommitteeUser> checkingCommitteeUserslist);
        Task<bool> ToSuppliersChangeAreasApproved(Tender tender, List<string> suppliers, bool isFavourite);

        Task<bool> SendSupplierNotificationToApplyFavouriteTender(string tenderNumber, int tenderId, List<string> suppliers, bool isFavourite);
        Task<bool> SendNotificationFoRejectingCancelTenderToDataEntry(Tender tender);
        Task<bool> ToDataEntryExtendDateRejected(Tender tender);
        Task<bool> ToDataEntryAttachmentUpdateApproved(Tender tender);
        Task<bool> ToDataEntryAttachmentUpdateRejected(Tender tender);
        Task<bool> SendNotificationToSuppliersForOffersOpenned(Tender tender, List<Supplier> suppliers);
        Task<bool> SendNotificationToSuppliersForOffersChecked(Tender tender, List<Supplier> suppliers);
        Task<bool> SendNotificationToSuppliersForAcceptingPrequalificationRequest(Tender tender, List<SupplierPreQualificationDocument> acceptedSuppliers);
        Task<bool> SendNotificationToSuppliersForRejectingPreQualificationRequest(Tender tender, List<SupplierPreQualificationDocument> rejectedSuppliers);
        Task<bool> SendNotificationForRequestingApproveQualificationToAuditor(Tender tender);
        Task<bool> SendNotificationForAcceptingApprovingQualificationToDataEntry(Tender tender);
        Task<bool> SendNotificationFoRejectingApprovingQualificationToDataEntry(Tender tender);
    }
}
