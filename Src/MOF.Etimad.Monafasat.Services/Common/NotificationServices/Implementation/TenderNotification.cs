using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Abstractions;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices
{
    public class TenderNotification : ITenderNotification
    {
        private readonly INotificationService _notification;
      //  private readonly INotifayAppService _notifayAppService;
        private readonly IConfiguration _configuration;
        private readonly ISupplierService _supplierService;
        private readonly IIDMAppService _iDMAppService;
        //private readonly ITenderAppService _tenderAppService;
        public TenderNotification(INotificationService notificationservive, 
            //INotifayAppService notifayAppService, 
            IConfiguration configuration, ISupplierService supplierService, IIDMAppService iDMAppService)//, ITenderAppService tenderAppService)
        {
            _notification = notificationservive;
           // _notifayAppService = notifayAppService;
            _configuration = configuration;
            _supplierService = supplierService;
            //_tenderAppService = tenderAppService;
            _iDMAppService = iDMAppService;
        }

        public async Task<bool> SendNotificationForEnquiryReply(EnquiryReply enquiryReply)
        {
            var suppliers = await _iDMAppService.GetAllSupplierOnTender(enquiryReply.Enquiry.TenderId);
            if (enquiryReply.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Approved)
            {
                var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(suppliers);
                //foreach (var cr in IDMSuppliers)
                //{
                //var users = await _notifayAppService.GetUserByCR(cr);
                foreach (var user in IDMSuppliers)
                {
                    await _notifayAppService.SendInvitationBySms(new List<string> { user.mobile }, string.Format(string.Format(_configuration.GetSection("EnquiryReply:SendMessage:body").Value, enquiryReply.Enquiry.Tender.TenderNumber)));
                    await _notifayAppService.SendInvitationByEmail(new List<string> { user.email }, string.Format(string.Format(_configuration.GetSection("EnquiryReply:SendMessage:subject").Value, enquiryReply.Enquiry.Tender.TenderNumber)),
                        string.Format(string.Format(_configuration.GetSection("EnquiryReply:SendMessage:body").Value, enquiryReply.Enquiry.Tender.TenderNumber)));
                    //await _notifayAppService.AddNotifayToAllWithSend(user.UserProfileId, Enums.NotifayType.DefaultNotify,
                    //    string.Format(string.Format(_configuration.GetSection("EnquiryReply:SendMessage:subject").Value, enquiryReply.Enquiry.Tender.TenderNumber)),
                    //    string.Format(string.Format(_configuration.GetSection("EnquiryReply:SendMessage:body").Value, enquiryReply.Enquiry.Tender.TenderNumber)),
                    //    $"Enquiry/SupplierEnquiriesOnTender?id={Util.Encrypt(enquiryReply.Enquiry.TenderId)}", false, UserType.NewMonafasat_TechnicalCommitteeUser);
                }
                //_notifayAppService.AddNotifayToAllWithSend(user.UserProfileId, Enums.NotifayType.DefaultNotify,
                //       string.Format(string.Format(_configuration.GetSection("EnquiryReply:SendMessage:subject").Value, enquiryReply.Enquiry.Tender.TenderNumber)),
                //       string.Format(string.Format(_configuration.GetSection("EnquiryReply:SendMessage:body").Value, enquiryReply.Enquiry.Tender.TenderNumber)),
                //       $"Enquiry/SupplierEnquiriesOnTender?id={Util.Encrypt(enquiryReply.Enquiry.TenderId)}", false, UserType.NewMonafasat_TechnicalCommitteeUser,"1",NotificationEntityType.Tender,x=>x.);
                //}
            }
            return true;
        }

        public async Task<bool> SendNotificationForEnquiryAddition(Enquiry enquiry, List<int> technicalCommitteeUserslist, Tender tender)
        {
            //var technicalCommitteeUserslist = await _iDMAppService.GetTechnicalCommitteeMembersOnTender(enquiryModel.TechnicalCommitteeId);

            foreach (var user in technicalCommitteeUserslist)
            {
                await _notifayAppService.AddNotifayToAllWithSend(user, Enums.NotifayType.DefaultNotify,
                    string.Format(string.Format(_configuration.GetSection("SendingEnquiry:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("SendingEnquiry:SendMessage:body").Value, tender.TenderNumber)),
                    $"Enquiry/SupplierEnquiriesOnTender?id={Util.Encrypt(enquiry.TenderId)}", false, UserType.NewMonafasat_TechnicalCommitteeUser);
            }

            return true;
        }
        public async Task<bool> SendNotificationForJoinCommittee(JoinTechnicalCommitteeModel joinTechnicalCommitteeModel)
        {
            var jonedTechnicalCommitteeUserslist = await _iDMAppService.GetTechnicalCommitteeMembersOnTender(joinTechnicalCommitteeModel.CommitteeId);

            foreach (var user in jonedTechnicalCommitteeUserslist)
            {
                await _notifayAppService.AddNotifayToAllWithSend(user, Enums.NotifayType.DefaultNotify,
                    string.Format(string.Format(_configuration.GetSection("InvitationEnquiryJoinCommittee:SendMessage:subject").Value, joinTechnicalCommitteeModel.EnquiryId)),
                    string.Format(string.Format(_configuration.GetSection("InvitationEnquiryJoinCommittee:SendMessage:body").Value, joinTechnicalCommitteeModel.EnquiryId)),
                    $"Enquiry/EnquiryDetails?enquiryIdString={Util.Encrypt(joinTechnicalCommitteeModel.EnquiryId)}", false, UserType.NewMonafasat_TechnicalCommitteeUser);
            }
            return true;
        }
        public async Task<bool> SendVerificationCode(string activationCode, string userMobileNo, string userEmail)
        {
            Models.SmsModel smsModel = new Models.SmsModel();
            Models.EmailModel emailModel = new Models.EmailModel();
            Dictionary<string, string> smsDic = new Dictionary<string, string>();
            Dictionary<string, string> emailDic = new Dictionary<string, string>();
            smsDic.Add(userMobileNo, string.Format(string.Format(_configuration.GetSection("AcctivationCode:SendMessage:body").Value, activationCode)));
            emailDic.Add(userEmail, string.Format(string.Format(_configuration.GetSection("AcctivationCode:SendMessage:body").Value, activationCode)));
            emailModel.Subject = string.Format(string.Format(_configuration.GetSection("AcctivationCode:SendMessage:subject").Value));
            smsModel.ListOfNumbers = smsDic;
            emailModel.ListOfEmails = emailDic;
            await _notification.SendSms(smsModel);
            await _notification.SendEmail(emailModel);
            return true;
        }
        public async Task<bool> SendPurchaseSadadNumber(string tenderNumber, string sadadNumber, string mobileNumber, string userEmail)
        {
            Models.SmsModel smsModel = new Models.SmsModel();
            Models.EmailModel emailModel = new Models.EmailModel();
            Dictionary<string, string> smsDic = new Dictionary<string, string>();
            Dictionary<string, string> emailDic = new Dictionary<string, string>();
            smsDic.Add(mobileNumber, string.Format(string.Format(_configuration.GetSection("purchaseReceipt:SendMessage:body").Value, tenderNumber, sadadNumber)));
            emailDic.Add(userEmail, string.Format(string.Format(_configuration.GetSection("purchaseReceipt:SendMessage:body").Value, tenderNumber, sadadNumber)));
            emailModel.Subject = string.Format(string.Format(_configuration.GetSection("purchaseReceipt:SendMessage:subject").Value));
            smsModel.ListOfNumbers = smsDic;
            emailModel.ListOfEmails = emailDic;
            await _notification.SendSms(smsModel);
            await _notification.SendEmail(emailModel);
            return true;
        }
        public async Task<bool> SendSuccessPayment(string tenderNumber, string mobileNumber, string userEmail)
        {
            Models.SmsModel smsModel = new Models.SmsModel();
            Models.EmailModel emailModel = new Models.EmailModel();
            Dictionary<string, string> smsDic = new Dictionary<string, string>();
            Dictionary<string, string> emailDic = new Dictionary<string, string>();
            smsDic.Add(mobileNumber, string.Format(string.Format(_configuration.GetSection("successPayment:SendMessage:body").Value, tenderNumber)));
            emailDic.Add(userEmail, string.Format(string.Format(_configuration.GetSection("successPayment:SendMessage:body").Value, tenderNumber)));
            emailModel.Subject = string.Format(string.Format(_configuration.GetSection("successPayment:SendMessage:subject").Value));
            smsModel.ListOfNumbers = smsDic;
            emailModel.ListOfEmails = emailDic;
            await _notification.SendSms(smsModel);
            await _notification.SendEmail(emailModel);
            return true;
        }
        public async Task<bool> SendNotificationForRequestingApproveTenderToAuditor(Tender tender)
        {
            var taskList = new List<Task>();
            // var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.Auditer, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.Auditer, tender.BranchId));
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("PendingTender:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingTender:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_Auditer, tender.TenderId.ToString(), NotificationEntityType.Tender);
            }

            return true;
        }
        public async Task<bool> SendNotificationForRequestingApproveQualificationToAuditor(Tender tender)
        {
            var taskList = new List<Task>();
            //var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.Auditer, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.Auditer, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("PendingQualification:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingQualification:SendMessage:body").Value, tender.TenderNumber)),
                    $"Qualification/PreQualificationApproval?qualificationIdString={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_Auditer);
            }

            return true;
        }
        public async Task<bool> SendNotificationForAcceptingApprovingQualificationToDataEntry(Tender tender)
        {
            var taskList = new List<Task>();
            //var list = (await _notifayAppService.GetUsersByType(Enums.UserType.NewMonafasat_DataEntry));
            //var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.Auditer, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("ApprovingQualification:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("ApprovingQualification:SendMessage:body").Value, tender.TenderNumber)),
                    $"Qualification/PreQualificationApproval?qualificationIdString={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }
        public async Task<bool> SendNotificationFoRejectingApprovingQualificationToDataEntry(Tender tender)
        {
            var taskList = new List<Task>();
            //var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("RejectApprovingQualification:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("RejectApprovingQualification:SendMessage:body").Value, tender.TenderNumber)),
                    $"Qualification/PreQualificationApproval?qualificationIdString={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }
        public async Task<bool> SendNotificationForRequestOpeningApproval(Tender tender, List<CommitteeUser> OpeningCommitteeUserslist)
        {
            var taskList = new List<Task>();
            var list = OpeningCommitteeUserslist.Where(a => a.RoleName == RoleNames.OffersOppeningManager).Select(u => u.UserProfileId).ToList();
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("PendingTenderOpening:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingTenderOpening:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/OpenTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}&actionName=review", false, UserType.NewMonafasat_OffersOpeningManager);
            }

            return true;
        }
        public async Task<bool> SendNotificationForRequestOpeningApproved(Tender tender, List<CommitteeUser> OpeningCommitteeUserslist)
        {
            var taskList = new List<Task>();
            var list = OpeningCommitteeUserslist.Where(a => a.RoleName == RoleNames.OffersOppeningSecretary).Select(u => u.UserProfileId).ToList();
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("PendingOpeningOffersApproved:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingOpeningOffersApproved:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_OffersOpeningSecretary);
            }

            return true;
        }
        public async Task<bool> SendNotificationForRequestOpeningRejected(Tender tender, List<CommitteeUser> OpeningCommitteeUserslist)
        {
            var taskList = new List<Task>();
            var list = OpeningCommitteeUserslist.Where(a => a.RoleName == RoleNames.OffersOppeningSecretary).Select(u => u.UserProfileId).ToList();
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("PendingOpeningOffersRejected:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingOpeningOffersRejected:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_OffersOpeningSecretary);
            }

            return true;
        }
        public async Task<bool> SendNotificationForRequestCheckingApproval(Tender tender, List<CommitteeUser> checkingCommitteeUserslist)
        {
            var list = checkingCommitteeUserslist.Where(a => a.RoleName == RoleNames.OffersCheckManager).Select(u => u.UserProfileId).ToList();
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("PendingTenderChecking:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingTenderChecking:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_OffersCheckManager);
            }

            return true;
        }
        public async Task<bool> SendNotificationForRequestCheckingApproved(Tender tender, List<CommitteeUser> checkingCommitteeUserslist)
        {
            var list = checkingCommitteeUserslist.Where(a => a.RoleName == RoleNames.OffersCheckSecretary).Select(u => u.UserProfileId).ToList();
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("PendingTenderCheckingApproved:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingTenderCheckingApproved:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_OffersCheckSecretary);
            }

            return true;
        }
        public async Task<bool> SendNotificationForRequestCheckingRejected(Tender tender, List<CommitteeUser> checkingCommitteeUserslist)
        {
            var list = checkingCommitteeUserslist.Where(a => a.RoleName == RoleNames.OffersCheckSecretary).Select(u => u.UserProfileId).ToList();
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("PendingTenderCheckingRejected:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingTenderCheckingRejected:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_OffersCheckSecretary);
            }

            return true;
        }
        public async Task<bool> SendNotificationForRequestAwardingApproval(Tender tender, List<CommitteeUser> checkingCommitteeUserslist)
        {
            var list = checkingCommitteeUserslist.Where(a => a.RoleName == RoleNames.OffersCheckManager).Select(u => u.UserProfileId).ToList();
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("PendingTenderAwardApprove:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingTenderAwardApprove:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_OffersCheckManager);
            }

            return true;
        }
        public async Task<bool> SendNotificationForRequestAwardingApproved(Tender tender, List<string> offerSuppliersCrs, List<CommitteeUser> checkingCommitteeUserslist)
        {
            var list = checkingCommitteeUserslist.Where(a => a.RoleName == RoleNames.OffersCheckSecretary).Select(u => u.UserProfileId).ToList();
            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(offerSuppliersCrs);
            foreach (var u in IDMSuppliers)
            {
                await _notifayAppService.SendInvitationBySms(new List<string> { u.mobile }, string.Format(string.Format(_configuration.GetSection("TenderAwardedForSupplier:SendMessage:body").Value, tender.TenderNumber)));
                await _notifayAppService.SendInvitationByEmail(new List<string> { u.email }, string.Format(string.Format(_configuration.GetSection("TenderAwardedForSupplier:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("TenderAwardedForSupplier:SendMessage:body").Value, tender.TenderNumber)));
                //int userId = (await _notifayAppService.GetUserByCR(u)).FirstOrDefault().UserProfileId;
                //await _notifayAppService.AddNotifayToAllWithSend(userId, Enums.NotifayType.OperationsOnTheTender,
                //    string.Format(string.Format(_configuration.GetSection("TenderAwardedForSupplier:SendMessage:subject").Value, tender.TenderNumber)),
                //    string.Format(string.Format(_configuration.GetSection("TenderAwardedForSupplier:SendMessage:body").Value, tender.TenderNumber)),
                //    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_Supplier);
            }
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("TenderAwardedForSecretary:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("TenderAwardedForSecretary:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_OffersCheckSecretary);
            }

            return true;
        }
        public async Task<bool> SendNotificationForAcceptingApprovingTenderToDataEntry(Tender tender)
        {
            var taskList = new List<Task>();
            //var list = (await _notifayAppService.GetUsersByType(Enums.UserType.NewMonafasat_DataEntry));
            var usersList = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));
            //var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            foreach (var u in usersList)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("ApprovingTender:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("ApprovingTender:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }
        public async Task<bool> SendNotificationFoRejectingApprovingTenderToDataEntry(Tender tender)
        {
            var taskList = new List<Task>();
            // var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var usersList = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in usersList)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("RejectApprovingTender:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("RejectApprovingTender:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }
        public async Task<bool> SendNotificationFoRejectingCancelTenderToDataEntry(Tender tender)
        {
            var taskList = new List<Task>();
            //var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("TenderCancelRequestRejected:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("TenderCancelRequestRejected:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }
        public async Task<bool> SendNotificationToSuppliersForChangesInQuantityTable(string tenderNumber, int tenderId, List<Supplier> suppliers)
        {
            var taskList = new List<Task>();
            foreach (var item in suppliers)
            {
                foreach (var supplierUserProfile in item.SupplierUserProfiles)
                {
                    await SendNotificationToSupplier(NotificationType.QuantityTableChange, supplierUserProfile, tenderNumber, tenderId, false);
                }
            }
            return true;
        }
        public async Task<bool> SendNotificationToSuppliersForOffersOpenned(Tender tender, List<Supplier> suppliers)
        {
            var taskList = new List<Task>();
            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(suppliers.Select(s => s.SelectedCr).ToList());
            foreach (var item in IDMSuppliers)
            {
                //foreach (var supplierUserProfile in item.SupplierUserProfiles)
                //{

                await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile }, string.Format(string.Format(_configuration.GetSection("PendingOpeningOffersApprovedForSupplier:SendMessage:body").Value, tender.TenderNumber)));
                await _notifayAppService.SendInvitationByEmail(new List<string> { item.email }, string.Format(string.Format(_configuration.GetSection("PendingOpeningOffersApprovedForSupplier:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingOpeningOffersApprovedForSupplier:SendMessage:body").Value, tender.TenderNumber)));

                //await _notifayAppService.AddNotifayToAllWithSend(supplierUserProfile.UserProfileId, Enums.NotifayType.OperationsOnTheTender,
                //        string.Format(string.Format(_configuration.GetSection("PendingOpeningOffersApprovedForSupplier:SendMessage:subject").Value, tender.TenderNumber)),
                //        string.Format(string.Format(_configuration.GetSection("PendingOpeningOffersApprovedForSupplier:SendMessage:body").Value, tender.TenderNumber)),
                //        $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_Supplier);
                //}
            }
            return true;
        }
        public async Task<bool> SendNotificationToSuppliersForOffersChecked(Tender tender, List<Supplier> suppliers)
        {
            var taskList = new List<Task>();
            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(suppliers.Select(s => s.SelectedCr).ToList());
            foreach (var item in IDMSuppliers)
            {
                await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },
                    string.Format(string.Format(_configuration.GetSection("PendingTenderCheckingApprovedForSupplier:SendMessage:body").Value, tender.TenderNumber)));

                await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                    string.Format(string.Format(_configuration.GetSection("PendingTenderCheckingApprovedForSupplier:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("PendingTenderCheckingApprovedForSupplier:SendMessage:body").Value, tender.TenderNumber)));
            }
            //foreach (var item in suppliers)
            //{
            //    foreach (var supplierUserProfile in item.SupplierUserProfiles)
            //    {
            //        await _notifayAppService.AddNotifayToAllWithSend(supplierUserProfile.UserProfileId, Enums.NotifayType.OperationsOnTheTender,
            //            string.Format(string.Format(_configuration.GetSection("PendingTenderCheckingApprovedForSupplier:SendMessage:subject").Value, tender.TenderNumber)),
            //            string.Format(string.Format(_configuration.GetSection("PendingTenderCheckingApprovedForSupplier:SendMessage:body").Value, tender.TenderNumber)),
            //            $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_Supplier);
            //    }
            //}
            return true;
        }
        public async Task<bool> SendNotificationToSuppliersForAcceptingPrequalificationRequest(Tender tender, List<SupplierPreQualificationDocument> acceptedSuppliers)
        {
            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(acceptedSuppliers.Select(s => s.SupplierId).ToList());
            foreach (var item in IDMSuppliers)
            {
                await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },

                    string.Format(string.Format(_configuration.GetSection("AcceptingSupplierPrequalificationRequest:SendMessage:body").Value, tender.TenderName)));

                await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                    string.Format(string.Format(_configuration.GetSection("AcceptingSupplierPrequalificationRequest:SendMessage:subject").Value)),
                    string.Format(string.Format(_configuration.GetSection("AcceptingSupplierPrequalificationRequest:SendMessage:body").Value, tender.TenderName)));
            }

            //foreach (var cr in acceptedSuppliers)
            //{
            //    var users = await _notifayAppService.GetUserByCR(cr.SupplierId);
            //    foreach (var user in users)
            //    {
            //        await _notifayAppService.AddNotifayToAllWithSend(user.UserProfileId, Enums.NotifayType.OperationsOnTheTender,
            //            string.Format(string.Format(_configuration.GetSection("AcceptingSupplierPrequalificationRequest:SendMessage:subject").Value)),
            //            string.Format(string.Format(_configuration.GetSection("AcceptingSupplierPrequalificationRequest:SendMessage:body").Value, tender.TenderName, cr.PreQualificationResult == 1 ? Resources.QualificationResources.DisplayInputs.Matched : Resources.QualificationResources.DisplayInputs.NotMatched)), UserType.NewMonafasat_Supplier);
            //    }
            //}
            return true;
        }
        public async Task<bool> SendNotificationToSuppliersForRejectingPreQualificationRequest(Tender tender, List<SupplierPreQualificationDocument> rejectedSuppliers)
        {
            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(rejectedSuppliers.Select(s => s.SupplierId).ToList());
            foreach (var item in IDMSuppliers)
            {
                await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },

                    string.Format(string.Format(_configuration.GetSection("RejectingSupplierPrequalificationRequest:SendMessage:body").Value, tender.TenderName)));

                await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                    string.Format(string.Format(_configuration.GetSection("RejectingSupplierPrequalificationRequest:SendMessage:subject").Value)),
                    string.Format(string.Format(_configuration.GetSection("RejectingSupplierPrequalificationRequest:SendMessage:body").Value, tender.TenderName)));
            }

            //foreach (var cr in rejectedSuppliers)
            //{
            //    var users = await _notifayAppService.GetUserByCR(cr.SupplierId);
            //    foreach (var user in users)
            //    {
            //        await _notifayAppService.AddNotifayToAllWithSend(user.UserProfileId, Enums.NotifayType.OperationsOnTheTender,
            //            string.Format(string.Format(_configuration.GetSection("RejectingSupplierPrequalificationRequest:SendMessage:subject").Value)),
            //            string.Format(string.Format(_configuration.GetSection("RejectingSupplierPrequalificationRequest:SendMessage:body").Value, tender.TenderName, cr.PreQualificationResult == 1 ? Resources.QualificationResources.DisplayInputs.Matched : Resources.QualificationResources.DisplayInputs.NotMatched, cr.RejectionReason)), UserType.NewMonafasat_Supplier);
            //    }
            //}
            return true;
        }
        public async Task SendNotificationToSupplier(NotificationType type, SupplierUserProfile u, string tenderNumber, int tenderId, bool isFavourite)
        {
            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(new List<string> { u.SelectedCr });
            //quantity Tables Change
            if (type == NotificationType.QuantityTableChange)
            {

                foreach (var item in IDMSuppliers)
                {
                    await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },

                         string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInQuantityTable:SendMessage:body").Value, tenderNumber)));

                    await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                        string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInQuantityTable:SendMessage:subject").Value)),
                         string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInQuantityTable:SendMessage:body").Value, tenderNumber)));
                }


                //await _notifayAppService.AddNotifayToAllWithSend(u.UserProfileId, Enums.NotifayType.OperationsOnTheTender,
                //    string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInQuantityTable:SendMessage:subject").Value)),
                //    string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInQuantityTable:SendMessage:body").Value, tenderNumber)),
                //    $"Tender/Details/" + Util.Encrypt(tenderId), false, UserType.NewMonafasat_Supplier);
            }
            else if (type == NotificationType.AttachmentsChange)
            {
                string message;
                if (isFavourite)
                {
                    message = _configuration.GetSection("SendToFavouriteSuppliersChangesInAttachments:SendMessage:body").Value;

                    foreach (var item in IDMSuppliers)
                    {
                        await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },

                             string.Format(string.Format(message, tenderNumber)));

                        await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                           string.Format(string.Format(_configuration.GetSection("SendToFavouriteSuppliersChangesInAttachments:SendMessage:subject").Value)),
                             string.Format(string.Format(message, tenderNumber)));
                    }


                    //await _notifayAppService.AddNotifayToAllWithSend(u.UserProfileId, Enums.NotifayType.OperationsOnTheTender,
                    //    string.Format(string.Format(_configuration.GetSection("SendToFavouriteSuppliersChangesInAttachments:SendMessage:subject").Value)),
                    //    string.Format(string.Format(message, tenderNumber)),
                    //    $"Tender/SupplierTenders", false, UserType.NewMonafasat_Supplier);
                }
                else
                {
                    message = _configuration.GetSection("SendToSuppliersChangesInAttachments:SendMessage:body").Value;

                    foreach (var item in IDMSuppliers)
                    {
                        await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },

                             string.Format(string.Format(message, tenderNumber)));

                        await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                        string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInAttachments:SendMessage:subject").Value)),
                             string.Format(string.Format(message, tenderNumber)));
                    }

                    //await _notifayAppService.AddNotifayToAllWithSend(u.UserProfileId,
                    //    Enums.NotifayType.OperationsOnTheTender,
                    //    string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInAttachments:SendMessage:subject").Value)), 
                    //    string.Format(string.Format(message, tenderNumber)), $"Tender/SupplierTenders", false, UserType.NewMonafasat_Supplier);
                }
            }
            else if (type == NotificationType.FavouriteTwoDaysRest)

                foreach (var item in IDMSuppliers)
                {
                    await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },
                        string.Format(string.Format(_configuration.GetSection("SendToSuppliersTwoDaysToApply:SendMessage:body").Value, tenderNumber)));
                    await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                         string.Format(string.Format(_configuration.GetSection("SendToSuppliersTwoDaysToApply:SendMessage:subject").Value)),
                         string.Format(string.Format(_configuration.GetSection("SendToSuppliersTwoDaysToApply:SendMessage:body").Value, tenderNumber)));
                }

            //await _notifayAppService.AddNotifayToAllWithSend(u.UserProfileId,
            //    Enums.NotifayType.OperationsOnTheTender,
            //    string.Format(string.Format(_configuration.GetSection("SendToSuppliersTwoDaysToApply:SendMessage:subject").Value)),
            //    string.Format(string.Format(_configuration.GetSection("SendToSuppliersTwoDaysToApply:SendMessage:body").Value, tenderNumber)),
            //    $"Tender/SupplierTenders", false, UserType.NewMonafasat_Supplier);

        }
        public async Task<bool> SendNotificationToSuppliersForChangesInAttachments(Tender tender, List<string> suppliers, bool isFavourite)
        {
            var taskList = new List<Task>();

            foreach (var item in suppliers)
            {
                var list = (await _notifayAppService.GetUserByCR(item));
                foreach (var u in list)
                {
                    await SendNotificationToSupplier(NotificationType.AttachmentsChange, u, tender.TenderNumber, tender.TenderId, isFavourite);
                    //await _notifayAppService.AddNotifayToAllWithSend(u.UserProfile.id, Enums.NotifayType.OperationsOnTheTender, string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInQuantityTable:SendMessage:subject").Value)), string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInQuantityTable:SendMessage:body").Value, tender.TenderName)), $"offer/ApplyOffer?{Util.Decrypt(tender.TenderId)}");
                }
            }
            return true;
        }
          public async Task<bool> SendSupplierNotificationToApplyFavouriteTender(string tenderNumber, int tenderId, List<string> suppliers, bool isFavourite)
        {
            var taskList = new List<Task>();
            foreach (var item in suppliers)
            {
                var list = (await _notifayAppService.GetUserByCR(item));
                foreach (var u in list)
                {
                    await SendNotificationToSupplier(NotificationType.FavouriteTwoDaysRest, u, tenderNumber, tenderId, isFavourite);
                }
            }
            return true;
        }
        public async Task<bool> SendNotificationForInvitationToSuppliers(Tender tender, List<string> suppliersCr)
        {
            var taskList = new List<Task>();
            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(suppliersCr);
            foreach (var item in IDMSuppliers)
            {
                await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },

                        string.Format(string.Format(_configuration.GetSection("SupplierInvitation:SendInvitation:body").Value, item.supplierNumber, tender.TenderNumber)));

                await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                        string.Format(string.Format(_configuration.GetSection("SupplierInvitation:SendInvitation:subject").Value, tender.TenderNumber)),
                        string.Format(string.Format(_configuration.GetSection("SupplierInvitation:SendInvitation:body").Value, item.supplierNumber, tender.TenderNumber)));
            }


            //foreach (var item in suppliersCr)
            //{
            //    var list = (await _notifayAppService.GetUserByCR(item));
            //    foreach (var u in list)
            //    {
            //        await _notifayAppService.AddNotifayToAllWithSend(u.UserProfile.Id, Enums.NotifayType.OperationsOnTheTender,
            //            string.Format(string.Format(_configuration.GetSection("SupplierInvitation:SendInvitation:subject").Value, tender.TenderNumber)),
            //            string.Format(string.Format(_configuration.GetSection("SupplierInvitation:SendInvitation:body").Value, u.Supplier.SelectedCrName, tender.TenderNumber)),
            //            $"Tender/SupplierInvitationTenders", false, UserType.NewMonafasat_Supplier);
            //    }
            //}
            return true;

        }
        public async Task<bool> SendNotificationForChangesInTenderToAuditer(Tender tender, Enums.UserType receiverType)
        {
            var taskList = new List<Task>();
            // var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.Auditer, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.Auditer, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("SendChangesInQuantityTableForApprovement:SendMessage:subject").Value)),
                    string.Format(string.Format(_configuration.GetSection("SendToSuppliersChangesInQuantityTable:SendMessage:body").Value,
                    tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_Auditer);
            }

            return true;
        }
        public async Task<bool> SendNotificationForApprinvingChangesInTender(Tender tender, Enums.UserType receiverType)
        {
            var taskList = new List<Task>();
            // var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("ApproveChangesInQuantityTable:SendMessage:subject").Value)),
                    string.Format(string.Format(_configuration.GetSection("ApproveChangesInQuantityTable:SendMessage:body").Value,
                    tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }
        public async Task<bool> SendNotificationForRejectingChangesInTender(Tender tender, Enums.UserType receiverType)
        {
            var taskList = new List<Task>();
            //var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(u.Id, Enums.NotifayType.OperationsOnTheTender, string.Format(string.Format(_configuration.GetSection("RejectChangesInQuantityTable:SendMessage:subject").Value)),
                    string.Format(string.Format(_configuration.GetSection("RejectChangesInQuantityTable:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }

        #region Cancelling tender request
        public async Task<bool> ToHigherAuthorityNewCancelRequestCreated(Tender tender, String whoRoleName)
        {
            string sendToRoleName = RoleHelper.GetHigherAuthorityByRoleName(whoRoleName);
            var taskList = new List<Task>();
            var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(sendToRoleName, tender.AgencyCode));
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("newTenderCancelRequest:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("newTenderCancelRequest:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}&STenderStatusId={Util.Encrypt(tender.TenderStatusId)}&displayOnlyMode=true", false, (UserType)Enum.Parse(typeof(UserType), sendToRoleName, true));
            }
            return true;
        }

        /// <summary>
        /// sends notification to the higher authority for change requests from the lower authority
        /// </summary>
        /// <param name="tender"></param>
        /// <param name="whoRoleName"></param>
        /// <param name="changeType">
        /// for redirecting directly on the change tab in tender details page
        /// 2 = dates changes
        /// 4 = quantity tables changes
        /// 5 = attachments changes
        /// </param>
        /// <returns></returns>
        public async Task<bool> ToHigherAuthorityNewUpdateRequestCreated(Tender tender, String whoRoleName, int changeType)
        {
            string sendToRoleName = RoleHelper.GetHigherAuthorityByRoleName(whoRoleName);
            var taskList = new List<Task>();
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(sendToRoleName, tender.BranchId));
            foreach (var u in list)
            {
                String updateType = (tender.Status != null) ? tender.Status.NameAr : "";
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(_configuration.GetSection("NewTenderUpdateRevisionCreated:SendMessage:subject").Value),
                    string.Format(_configuration.GetSection("NewTenderUpdateRevisionCreated:SendMessage:body").Value, tender.TenderNumber, updateType),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", true, (UserType)Enum.Parse(typeof(UserType), sendToRoleName, true));
            }
            return true;
        }

        public async Task<bool> ToLowerAuthorityExtendDateRejected(Tender tender, String whoRoleName)
        {
            string sendToRoleName = RoleHelper.GetLowerAuthorityByRoleName(whoRoleName);
            var taskList = new List<Task>();
            var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(sendToRoleName, tender.AgencyCode));
            foreach (var u in list)
            {
                String updateType = (tender.Status != null) ? tender.Status.NameAr : "";
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(_configuration.GetSection("TenderUpdateRevisionRejected:SendMessage:subject").Value),
                    string.Format(_configuration.GetSection("TenderUpdateRevisionRejected:SendMessage:body").Value, tender.TenderNumber, updateType),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, (UserType)Enum.Parse(typeof(UserType), sendToRoleName, true));
            }
            return true;
        }

        public async Task<bool> ToSuppliersTenderCancelled(Tender tender, List<string> suppliers, bool isFavourite)
        {
            // set message body
            string message = "";
            if (isFavourite)
            {
                message = string.Format(_configuration.GetSection("TenderCancelledFav:SendMessage:body").Value, tender.TenderNumber);
            }
            else
            {
                message = string.Format(_configuration.GetSection("TenderCancelled:SendMessage:body").Value, tender.TenderNumber);
            }

            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(suppliers);
            foreach (var item in IDMSuppliers)
            {
                await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },
                    string.Format(message));

                await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                        string.Format(_configuration.GetSection("TenderCancelled:SendMessage:subject").Value),
                        string.Format(message));
            }

            //var taskList = new List<Task>();
            //foreach (var item in suppliers)
            //{
            //    var list = (await _notifayAppService.GetUserByCR(item));
            //    foreach (var u in list)
            //    {
            //        await _notifayAppService.AddNotifayToAllWithSend(
            //            u.UserProfile.Id,
            //            Enums.NotifayType.OperationsOnTheTender,
            //            string.Format(_configuration.GetSection("TenderCancelled:SendMessage:subject").Value),
            //            string.Format(message),
            //            "", UserType.NewMonafasat_Supplier
            //        );
            //    }
            //}
            return true;
        }

        public async Task<bool> ToLowerAuthorityCancelRequestApproved(Tender tender, String whoRoleName)
        {
            string sendToRoleName = RoleHelper.GetLowerAuthorityByRoleName(whoRoleName);
            var taskList = new List<Task>();
            var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(sendToRoleName, tender.AgencyCode));
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("TenderCancelRequestApproved:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("TenderCancelRequestApproved:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, (UserType)Enum.Parse(typeof(UserType), sendToRoleName, true));
            }
            return true;
        }

        public async Task<bool> ToDataEntryExtendDateApproved(Tender tender)
        {
            var taskList = new List<Task>();
            //var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("ToDataEntryChangesInDates:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("ToDataEntryChangesInDates:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }

        public async Task<bool> ToDataEntryAttachmentUpdateApproved(Tender tender)
        {
            var taskList = new List<Task>();
            // var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("ApproveChangesInAttachment:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("ApproveChangesInAttachment:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }
        public async Task<bool> ToDataEntryAttachmentUpdateRejected(Tender tender)
        {
            var taskList = new List<Task>();
            // var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("RejectChangesInAttachment:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("RejectChangesInAttachment:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }

        public async Task<bool> ToDataEntryExtendDateRejected(Tender tender)
        {
            var taskList = new List<Task>();
            //var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("RejectChangesInExtandingDates:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("RejectChangesInExtandingDates:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }

        public async Task<bool> ToAuditorExtendDateSendToApprove(Tender tender)
        {
            var taskList = new List<Task>();
            // var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(RoleNames.DataEntry, tender.AgencyCode));
            var list = (await _notifayAppService.GetUsersByRoleNameAndBranchId(RoleNames.DataEntry, tender.BranchId));

            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("RejectChangesInExtandingDates:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("RejectChangesInExtandingDates:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, UserType.NewMonafasat_DataEntry);
            }
            return true;
        }

        public async Task<bool> ToSuppliersExtendDateApproved(Tender tender, List<string> suppliers, bool isFavourite)
        {
            // set message body
            string message = "";
            if (isFavourite)
            {
                message = string.Format(_configuration.GetSection("ToSuppliersChangesInDatesFav:SendMessage:body").Value, tender.TenderNumber);
            }
            else
            {
                message = string.Format(_configuration.GetSection("ToSuppliersChangesInDates:SendMessage:body").Value, tender.TenderNumber);
            }

            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(suppliers);
            foreach (var item in IDMSuppliers)
            {
                await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },
                    string.Format(message));

                await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                        string.Format(_configuration.GetSection("ToSuppliersChangesInDates:SendMessage:subject").Value),
                        string.Format(message));
            }


            //var taskList = new List<Task>();
            //foreach (var item in suppliers)
            //{
            //    var list = (await _notifayAppService.GetUserByCR(item));
            //    foreach (var u in list)
            //    {
            //        await _notifayAppService.AddNotifayToAllWithSend(
            //            u.UserProfile.Id,
            //            Enums.NotifayType.OperationsOnTheTender,
            //            string.Format(_configuration.GetSection("ToSuppliersChangesInDates:SendMessage:subject").Value),
            //            string.Format(message),
            //            "", UserType.NewMonafasat_Supplier
            //        );
            //    }
            //}
            return true;
        }

        public async Task<bool> ToSuppliersChangeAreasApproved(Tender tender, List<string> suppliers, bool isFavourite)
        {
            // set message body
            string message = "";
            if (isFavourite)
            {
                message = string.Format(_configuration.GetSection("ToSuppliersChangesInAreasFav:SendMessage:body").Value, tender.TenderNumber);
            }
            else
            {
                message = string.Format(_configuration.GetSection("ToSuppliersChangesInAreas:SendMessage:body").Value, tender.TenderNumber);
            }

            var IDMSuppliers = await _iDMAppService.GetContactOfficersByCR(suppliers);
            foreach (var item in IDMSuppliers)
            {
                await _notifayAppService.SendInvitationBySms(new List<string> { item.mobile },
                    string.Format(message));

                await _notifayAppService.SendInvitationByEmail(new List<string> { item.email },
                        string.Format(_configuration.GetSection("ToSuppliersChangesInDates:SendMessage:subject").Value),
                        string.Format(message));
            }


            //var taskList = new List<Task>();
            //foreach (var item in suppliers)
            //{
            //    var list = (await _notifayAppService.GetUserByCR(item));
            //    foreach (var u in list)
            //    {
            //        await _notifayAppService.AddNotifayToAllWithSend(
            //            u.UserProfile.Id,
            //            Enums.NotifayType.OperationsOnTheTender,
            //            string.Format(_configuration.GetSection("ToSuppliersChangesInDates:SendMessage:subject").Value),
            //            string.Format(message),
            //            "", UserType.NewMonafasat_Supplier
            //        );
            //    }
            //}
            return true;
        }

        public async Task<bool> ToLowerAuthorityCancelRequestRejected(Tender tender, String whoRoleName)
        {
            //string sendToRoleName = RoleHelper.GetLowerAuthorityByRoleName(whoRoleName);
            var taskList = new List<Task>();
            var list = (await _notifayAppService.GetUsersByRoleNameAndAgencyCode(whoRoleName, tender.AgencyCode));
            foreach (var u in list)
            {
                await _notifayAppService.AddNotifayToAllWithSend(
                    u.Id,
                    Enums.NotifayType.OperationsOnTheTender,
                    string.Format(string.Format(_configuration.GetSection("TenderCancelRequestRejected:SendMessage:subject").Value, tender.TenderNumber)),
                    string.Format(string.Format(_configuration.GetSection("TenderCancelRequestRejected:SendMessage:body").Value, tender.TenderNumber)),
                    $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", false, (UserType)Enum.Parse(typeof(UserType), whoRoleName, true));
            }
            return true;
        }

        #endregion
    }
}
