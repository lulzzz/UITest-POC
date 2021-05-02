using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AnnouncementAppService : IAnnouncementAppService
    {
        private readonly IAnnouncementCommands _announcementCommands;
        private readonly IAnnouncementQueries _announcementQueries;
        private readonly IAnnouncementDomainService _announcementDomainService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnnouncementAppService(IAnnouncementCommands announcementCommands, IAnnouncementQueries announcementQueries, IHttpContextAccessor httpContextAccessor, IAnnouncementDomainService announcementDomainService)
        {
            _announcementCommands = announcementCommands;
            _announcementQueries = announcementQueries;
            _announcementDomainService = announcementDomainService;

            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<SupplierAnnouncementDetailsModel> GetAnnouncementDetailsForSupplierByAnnouncementId(int announcementId)
        {
            return await _announcementQueries.FindAnnouncementDetailsForSupplierByAnnouncementId(announcementId, _httpContextAccessor.HttpContext.User.SupplierNumber());

        }
        public async Task<AnnouncementDetailsModel> FindAnnouncementDetailsByAnnouncementId(int announcementId)
        {

            return await _announcementQueries.FindAnnouncementDetailsByAnnouncementId(announcementId);
        }
        public async Task<LookupModel> GetAnnouncementNameByAnnouncementId(int announcementId)
        {

            return await _announcementQueries.GetAnnouncementNameByAnnouncementId(announcementId);
        }
        public async Task<CreateAnnouncementModel> GetAnnouncementByIdForEdit(int announcementId)
        {
            var result = await _announcementQueries.GetAnnouncementByIdForEdit(announcementId);
            return result;
        }
        public async Task<bool> JoinAnnouncement(int announcementId)
        {
            var CR = _httpContextAccessor.HttpContext.User.SupplierNumber();
            var announcement = await _announcementQueries.FindAnnouncementByAnnouncementId(announcementId);

            if (announcement.StatusId != (int)Enums.AnnouncementStatus.Approved)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.AnnoucementNotApproved);
            }
            if (announcement.LastApplyingRequestsDate < DateTime.Now)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.AnnouncmentDateExpired);
            }
            var prevRequest = announcement.AnnouncementJoinRequests.Any(q => q.Cr == CR && q.StatusId != (int)Enums.AnnouncementJoinRequestStatus.WithDraw);
            if (prevRequest)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.ThierIsRequestUnderProcessing);

            }

            announcement.AnnouncementJoinRequests.Add(new Core.Entities.AnnouncementJoinRequest(announcementId, CR, (int)Enums.AnnouncementJoinRequestStatus.Sent));
            await _announcementCommands.UpdateAnnouncementAsync(announcement);
            return true;
        }
        public async Task<bool> WithdrawJoinRequest(int announcementId)
        {
            var CR = _httpContextAccessor.HttpContext.User.SupplierNumber();
            var announcement = await _announcementQueries.FindAnnouncementByAnnouncementId(announcementId);
            if (announcement.LastApplyingRequestsDate < DateTime.Now)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.AnnouncmentDateExpired);
            }
            announcement.WithdroawJoinRequest(CR);
            await _announcementCommands.UpdateAnnouncementAsync(announcement);
            return true;
        }

        public async Task<QueryResult<SupplierGridAnnouncementModel>> GetSupplierAnnouncements(SupplierAnnouncementSearchCriteria cretria)
        {
            cretria.CommericalRegisterNo = _httpContextAccessor.HttpContext.User.SupplierNumber();
            return await _announcementQueries.FindSupplierAnnouncements(cretria);
        }

        public async Task<QueryResult<AllAnouuncementsForSupplierVisitorModel>> GetAllSupplierAnnouncements(AllSupplierAnnouncementSearchCriteria criteria)
        {
            if (criteria.AnnouncementPublishDateCriteriaId.HasValue)
            {
                switch (criteria.AnnouncementPublishDateCriteriaId.Value)
                {
                    case (int)Enums.AnnouncementPublishDateCriteria.AnyTime:
                        criteria.FromPublishDate = null;
                        break;
                    case (int)Enums.AnnouncementPublishDateCriteria.TwoDaysAgo:
                        criteria.FromPublishDate = DateTime.Now.AddDays(-2);
                        break;
                    case (int)Enums.AnnouncementPublishDateCriteria.WeekAgo:
                        criteria.FromPublishDate = DateTime.Now.AddDays(-7);
                        break;
                    case (int)Enums.AnnouncementPublishDateCriteria.MonthAgo:
                        criteria.FromPublishDate = DateTime.Now.AddMonths(-1);
                        break;
                }
            }
            return await _announcementQueries.FindAllSupplierAnnouncements(criteria);
        }

        public async Task<QueryResult<AllAnouuncementsForAgencyModel>> GetAllAgencyAnnouncements(AllAgencyAnnouncementSearchCriteriaModel criteria)
        {
            return await _announcementQueries.FindAllAgencyAnnouncements(criteria);
        }

        public async Task<QueryResult<UnderOperationAnouuncementsForAgencyModel>> GetUnderOperationAgencyAnnouncements(UnderOperationAgencyAnnouncementSearchCriteria criteria)
        {
            return await _announcementQueries.FindUnderOperationAgencyAnnouncements(criteria);
        }

        #region Create-Announcement

        public async Task CreateNewAnnouncement(CreateAnnouncementModel announcementModel)
        {
            Check.ArgumentNotNull(nameof(announcementModel), announcementModel);
            if (!string.IsNullOrEmpty(announcementModel.AnnouncementIdString))
            {
                // Update Existing Announcement
                Announcement updatedAnnouncement = await _announcementQueries.GetAnnouncementByIdForCreationStep(Util.Decrypt(announcementModel.AnnouncementIdString));
                updatedAnnouncement.UpdateAnnouncement(announcementModel.AnnouncementName,
                                                announcementModel.AnnouncementPeriod, announcementModel.TenderTypeId, announcementModel.ReasonIdForSelectingTenderType, announcementModel.IntroAboutTender, announcementModel.InsideKsa, announcementModel.Details,
                                                announcementModel.ActivityDescription, announcementModel.BranchId, announcementModel.AgencyCode,
                                                announcementModel.ActivityIds, announcementModel.ConstructionWorkIds, announcementModel.MaintenanceRunnigWorkIds, announcementModel.AreasIds, announcementModel.CountriesIds);
                await _announcementCommands.UpdateAnnouncement(updatedAnnouncement);
            }
            else
            {
                // Add New Announcement
                Announcement announcement = new Announcement(announcementModel.AnnouncementName,
                                                announcementModel.AnnouncementPeriod, announcementModel.TenderTypeId, announcementModel.ReasonIdForSelectingTenderType, announcementModel.IntroAboutTender, announcementModel.InsideKsa, announcementModel.Details,
                                                announcementModel.ActivityDescription, announcementModel.BranchId, announcementModel.AgencyCode,
                                                announcementModel.ActivityIds, announcementModel.ConstructionWorkIds, announcementModel.MaintenanceRunnigWorkIds, announcementModel.AreasIds, announcementModel.CountriesIds);
                await _announcementCommands.CreateAnnouncement(announcement);
            }
            await _announcementCommands.SaveChangesAsync();
        }

        #endregion Create-Announcement

        #region Approve-Announcement-Process 


        public async Task CreateVerificationCode(CreateVerificationCodeModel createVerification)
        {
            Check.ArgumentNotNull(nameof(createVerification), createVerification);
            await _announcementDomainService.CreateVerificationCode(createVerification.Id);
        }
        public async Task SendAnnouncementForApproval(int announcementId)
        {
            Check.ArgumentNotNull(nameof(announcementId), announcementId.ToString());
            var announcement = await _announcementQueries.GetAnnouncementWithNoIncludsById(announcementId);
            announcement.SendAnnouncementForApproval();
            if (string.IsNullOrEmpty(announcement.ReferenceNumber))
            {
                var referenceNumber = await _announcementCommands.UpdateReferenceNumber();
                announcement.SetReferenceNumber(referenceNumber);

            }

            // Add History
            var userId = _announcementDomainService.GetUserId();
            announcement.AddAnnouncementHistoy(userId);
            await _announcementCommands.UpdateAnnouncement(announcement);
            await _announcementCommands.SaveChangesAsync();
            // Send Notification
            await _announcementDomainService.SendAnnouncementToApptovementNotification(announcement);
        }

        public async Task<ApproveAnnouncemntModel> ApproveAnnouncement(VerificationModel verificationModel)
        {
            Check.ArgumentNotNull(nameof(verificationModel.IdString), verificationModel.IdString);
            await _announcementDomainService.CheckVerificationCode(verificationModel.Id, verificationModel.VerificationCode, (int)Enums.VerificationType.Announcement);
            var announcement = await _announcementQueries.GetAnnouncementWithNoIncludsById(verificationModel.Id);
            announcement.ApproveAnnouncement();
            announcement.SetPublishedDate();
            var userName = _announcementDomainService.GetUserFullName();
            announcement.SetApprovedBy(userName);
            // Add History
            var userId = _announcementDomainService.GetUserId();
            announcement.AddAnnouncementHistoy(userId);
            await _announcementCommands.UpdateAnnouncement(announcement);
            await _announcementCommands.SaveChangesAsync();
            // Send Notification
            await _announcementDomainService.ApproveAnnouncementNotification(announcement);
            return new ApproveAnnouncemntModel()
            {
                AnnouncementId = announcement.AnnouncementId,
                AnnouncementIdString = Util.Encrypt(announcement.AnnouncementId),
                LastAnnouncementJoinDate = announcement.LastApplyingRequestsDate.Value.ToHijriDateWithFormat("yyyy-MM-dd")
            };
        }

        public async Task RejectApproveAnnouncement(RejectionReasonModel rejectionReasonModel)
        {
            Check.ArgumentNotNull(nameof(rejectionReasonModel.IdString), rejectionReasonModel.IdString);
            var announcement = await _announcementQueries.GetAnnouncementWithNoIncludsById(rejectionReasonModel.Id);
            announcement.RejectApproveAnnouncement(rejectionReasonModel.RejectionReason);
            // Add History
            var userId = _announcementDomainService.GetUserId();
            announcement.AddAnnouncementHistoy(userId, rejectionReasonModel.RejectionReason);
            await _announcementCommands.UpdateAnnouncement(announcement);
            await _announcementCommands.SaveChangesAsync();
            // Send Notification
            await _announcementDomainService.RejectApproveAnnouncementNotification(announcement);
        }

        public async Task ReOpenAnnouncement(int announcementId)
        {
            Check.ArgumentNotNull(nameof(announcementId), announcementId.ToString());
            var userId = _announcementDomainService.GetUserId();
            var announcement = await _announcementQueries.GetAnnouncementWithNoIncludsById(announcementId);
            announcement.ReOpenAnnouncement();
            announcement.AddAnnouncementHistoy(userId);
            await _announcementCommands.UpdateAnnouncement(announcement);
            await _announcementCommands.SaveChangesAsync();
        }

        #endregion Approve-Announcement-Process

        #region Delete-Announcement

        public async Task DeleteAnnouncement(int announcementId)
        {
            var announcement = await _announcementQueries.GetAnnouncementWithNoIncludsById(announcementId);
            announcement.DeleteAnnouncement();
            await _announcementCommands.UpdateAnnouncement(announcement);
            await _announcementCommands.SaveChangesAsync();
        }
        #endregion Delete-Announcement

        #region Lookups
        public async Task<List<TenderTypeModel>> GetTenderTypes()
        {
            return await _announcementQueries.GetTenderTypes();
        }
        #endregion Lookups
    }
}
