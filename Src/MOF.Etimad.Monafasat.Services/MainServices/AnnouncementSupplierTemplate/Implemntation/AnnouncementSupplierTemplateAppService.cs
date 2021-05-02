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
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class AnnouncementSupplierTemplateAppService : IAnnouncementSupplierTemplateAppService
    {
        private readonly IAnnouncementSupplierTemplateCommands _announcementCommands;
        private readonly IAnnouncementSupplierTemplateQueries _announcementQueries;
        private readonly IAnnouncementSupplierTemplateDomainService _announcementDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly INotificationAppService _notificationAppService;
        private readonly IIDMQueries _iDMQueries;
        private readonly IBlockAppService _blockAppService;
        public AnnouncementSupplierTemplateAppService(IAnnouncementSupplierTemplateCommands announcementCommands, IAnnouncementSupplierTemplateQueries announcementQueries, IHttpContextAccessor httpContextAccessor, IAnnouncementSupplierTemplateDomainService announcementDomainService,
            INotificationAppService notificationAppService, IIDMQueries iDMQueries, IBlockAppService blockAppService)
        {
            _announcementCommands = announcementCommands;
            _announcementQueries = announcementQueries;
            _announcementDomainService = announcementDomainService;
            _httpContextAccessor = httpContextAccessor;
            _notificationAppService = notificationAppService;
            _iDMQueries = iDMQueries;
            _blockAppService = blockAppService;
        }

        public async Task<QueryResult<AllAnnouncementSupplierTemplateAgencyModel>> GetAllAnnouncementSupplierTemplates(AgencyAnnouncementSupplierTemplateSearchCriteria criteria)
        {
            return await _announcementQueries.FindAllAnnouncementSupplierTemplates(criteria);
        }

        public async Task<QueryResult<JoinRequestModel>> GetJoinRequestsSuppliersByAnnouncementIdAsync(JoinRequestSuppliersSearchCriteria criteria)
        {
            return await _announcementQueries.GetJoinRequestsSuppliersByAnnouncementIdAsync(criteria);
        }

        public async Task<QueryResult<LinkedAgenciesAnnouncementTemplateModel>> GetBeneficiaryAgencyByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            return await _announcementQueries.GetBeneficiaryAgencyByAnnouncementIdAsync(criteria);
        }

        public async Task<QueryResult<AnnouncementSupplierTemplateSupplierGridModel>> GetAllAnnouncementSupplierTemplatesForSupplier(AnnouncementSupplierTemplateSupplierSearchCriteriaModel criteria)
        {
            if (criteria.AnnouncementPublishedDateDaysId.HasValue)
            {
                switch (criteria.AnnouncementPublishedDateDaysId.Value)
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
            return await _announcementQueries.GetAllAnnouncementSupplierTemplatesForSupplier(criteria);
        }

        public async Task<QueryResult<JoinRequestModel>> GetJoinRequestsByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            return await _announcementQueries.GetJoinRequestsByAnnouncementIdAsync(criteria);
        }
        public async Task<QueryResult<JoinRequestModel>> GetApprovedSuppliersJoinRequestsByAnnouncementId(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            return await _announcementQueries.GetApprovedSuppliersJoinRequestsByAnnouncementId(criteria);
        }

        public async Task<QueryResult<TenderAnnouncementSuppliersTemplateDetails>> GetTendersByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            return await _announcementQueries.GetTendersByAnnouncementIdAsync(criteria);
        }

        public async Task<AnnouncementSuppliersTemplateCancelModel> FindAnnouncementDetailsByAnnouncementIdForCancelation(int announcementId)
        {
            var result = await _announcementQueries.FindAnnouncementDetailsByAnnouncementIdForCancelation(announcementId);
            return result;
        }

        public async Task<CreateAnnouncementSupplierTemplateModel> CreateNewAnnouncementSupplierTemplate(CreateAnnouncementSupplierTemplateModel announcementModel)
        {
            int createdById = _httpContextAccessor.HttpContext.User.UserId();
            announcementModel.AgencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            IsValidToCreate(announcementModel);
            Check.ArgumentNotNull(nameof(announcementModel), announcementModel);
            List<AnnouncementSuppliersTemplateAttachment> attachments = new List<AnnouncementSuppliersTemplateAttachment>();

            if (announcementModel.Attachments != null)
            {
                foreach (var item in announcementModel.Attachments)
                {
                    attachments.Add(new AnnouncementSuppliersTemplateAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId));
                }
            }
            if (!string.IsNullOrEmpty(announcementModel.AnnouncementIdString))
            {
                await UpdateAnnouncemnetTemplateData(announcementModel, createdById, attachments, (int)Enums.AnnouncementSupplierTemplateInsertionType.FromCreation);
                return announcementModel;
            }
            else
            {
                AnnouncementSupplierTemplate announcementSupplierTemplate = new AnnouncementSupplierTemplate();
                announcementSupplierTemplate.CreateAnnouncementSupplierTemplate(announcementModel, attachments, createdById);
                announcementSupplierTemplate.SetReferenceNumber(await _announcementCommands.UpdateReferenceNumber());
                announcementSupplierTemplate.SetApprovalStatus(Enums.AnnouncementSupplierTemplateStatus.ReadyForApproval);
                await _announcementCommands.CreateAnnouncementSupplierTemplate(announcementSupplierTemplate);
                await _announcementCommands.SaveChangesAsync();
                announcementModel.AnnouncementId = announcementSupplierTemplate.AnnouncementId;
                announcementModel.AnnouncementIdString = Util.Encrypt(announcementSupplierTemplate.AnnouncementId);
                return announcementModel;
            }
        }

        public async Task<UpdateAnnouncementSupplierTemplateModel> UpdateAnnouncementSupplierTemplateList(UpdateAnnouncementSupplierTemplateModel announcementModel)
        {
            IsValidToUpdate(announcementModel);
            int createdById = _httpContextAccessor.HttpContext.User.UserId();
            Check.ArgumentNotNull(nameof(announcementModel), announcementModel);
            List<AnnouncementSuppliersTemplateAttachment> attachments = new List<AnnouncementSuppliersTemplateAttachment>();

            if (announcementModel.Attachments != null)
            {
                foreach (var item in announcementModel.Attachments)
                {
                    attachments.Add(new AnnouncementSuppliersTemplateAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId));
                }
            }
            var announcementSupplierTemplate = await _announcementQueries.GetAnnouncementByIdForCreationStep(Util.Decrypt(announcementModel.AnnouncementIdString));
            announcementSupplierTemplate.UpdateAnnouncementSupplierTemplateListData(announcementModel, attachments, createdById);
            await _announcementCommands.UpdateAnnouncementSupplierTemplate(announcementSupplierTemplate);
            await _announcementCommands.SaveChangesAsync();
            return announcementModel;
        }

        public async Task<ExtendAnnouncementSupplierTemplateModel> ExtendAnnouncementTemplate(ExtendAnnouncementSupplierTemplateModel announcementModel)
        {
            IsValidToExtendAnnouncementSupplier(announcementModel);
            int createdById = _httpContextAccessor.HttpContext.User.UserId();
            Check.ArgumentNotNull(nameof(announcementModel), announcementModel);
            List<AnnouncementSuppliersTemplateAttachment> attachments = new List<AnnouncementSuppliersTemplateAttachment>();
            if (announcementModel.Attachments != null)
            {
                foreach (var item in announcementModel.Attachments)
                {
                    attachments.Add(new AnnouncementSuppliersTemplateAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId));
                }
            }
            var announcementSupplierTemplate = await _announcementQueries.GetAnnouncementByIdForCreationStep(Util.Decrypt(announcementModel.AnnouncementIdString));
            announcementModel.ReferenceNumber = announcementSupplierTemplate.ReferenceNumber;
            announcementSupplierTemplate.ExtendAnnouncementSupplierTemplateData(announcementModel, attachments, createdById);
            await _announcementCommands.UpdateAnnouncementSupplierTemplate(announcementSupplierTemplate);
            await _announcementCommands.SaveChangesAsync();
            await SendNotificationAfterExtendAnnouncementTemplate(announcementModel);
            return announcementModel;
        }


        public async Task DeleteAnnouncement(int announcementId)
        {
            var announcement = await _announcementQueries.GetAnnouncementWithNoIncludsById(announcementId);
            announcement.DeleteAnnouncement();
            await _announcementCommands.UpdateAnnouncementSupplierTemplate(announcement);
            await _announcementCommands.SaveChangesAsync();
        }

        public async Task<CreateAnnouncementSupplierTemplateModel> SaveDraft(CreateAnnouncementSupplierTemplateModel announcementModel)
        {
            int createdById = _httpContextAccessor.HttpContext.User.UserId();
            announcementModel.AgencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            Check.ArgumentNotNull(nameof(announcementModel), announcementModel);

            List<AnnouncementSuppliersTemplateAttachment> attachments = new List<AnnouncementSuppliersTemplateAttachment>();

            if (announcementModel.Attachments != null)
            {
                foreach (var item in announcementModel.Attachments)
                {
                    attachments.Add(new AnnouncementSuppliersTemplateAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId));
                }
            }
            if (!string.IsNullOrEmpty(announcementModel.AnnouncementIdString))
            {

                await UpdateAnnouncemnetTemplateData(announcementModel, createdById, attachments, (int)Enums.AnnouncementSupplierTemplateInsertionType.FromSaveDraft);
                return announcementModel;
            }
            else
            {
                AnnouncementSupplierTemplate announcementSupplierTemplate = new AnnouncementSupplierTemplate();
                announcementSupplierTemplate.CreateAnnouncementSupplierTemplate(announcementModel, attachments, createdById);
                announcementSupplierTemplate.SetReferenceNumber(await _announcementCommands.UpdateReferenceNumber());
                announcementSupplierTemplate.SetApprovalStatus(Enums.AnnouncementSupplierTemplateStatus.UnderCreation);
                await _announcementCommands.CreateAnnouncementSupplierTemplate(announcementSupplierTemplate);
                await _announcementCommands.SaveChangesAsync();
                announcementModel.AnnouncementId = announcementSupplierTemplate.AnnouncementId;
                announcementModel.AnnouncementIdString = Util.Encrypt(announcementSupplierTemplate.AnnouncementId);
                return announcementModel;
            }

        }


        private async Task UpdateAnnouncemnetTemplateData(CreateAnnouncementSupplierTemplateModel announcementModel, int createdById, List<AnnouncementSuppliersTemplateAttachment> attachments, int insertionType)
        {
            if (!announcementModel.IsRequiredAttachmentToJoinList)
            {
                announcementModel.RequiredAttachment = "";
            }
            if (!announcementModel.IsEffectiveList)
            {
                announcementModel.EffectiveListDate = null;
            }

            AnnouncementSupplierTemplate announcementSupplierTemplate = await _announcementQueries.GetAnnouncementByIdForCreationStep(Util.Decrypt(announcementModel.AnnouncementIdString));
            announcementSupplierTemplate.UpdateAnnouncementSupplierTemplateData(announcementModel, attachments, createdById);

            if (insertionType == (int)Enums.AnnouncementSupplierTemplateInsertionType.FromCreation)
            {
                announcementSupplierTemplate.SetApprovalStatus(Enums.AnnouncementSupplierTemplateStatus.ReadyForApproval);
            }
            else
            {
                announcementSupplierTemplate.SetApprovalStatus(Enums.AnnouncementSupplierTemplateStatus.UnderCreation);
            }
            await _announcementCommands.UpdateAnnouncementSupplierTemplate(announcementSupplierTemplate);
            await _announcementCommands.SaveChangesAsync();
        }

        public async Task<ApproveAnnouncemntSupplierTemplateModel> GetAnnouncementSupplierTemplateDetailsByannouncementId(int announcementId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(announcementId), announcementId.ToString());
            var obj = await _announcementQueries.GetAnnouncementSupplierTemplateDetailsForApproval(announcementId);
            return obj;
        }

        public async Task<AnnouncementSuppliersTemplateDetailsModel> FindAnnouncementDetailsByAnnouncementId(int announcementId)
        {
            return await _announcementQueries.FindAnnouncementDetailsByAnnouncementId(announcementId);

        }
        public async Task<AnnouncementBasicDetailModel> GetAnnouncementBasicDetailsByAnnouncementId(int announcementId)
        {
            return await _announcementQueries.FindAnnouncementBasicDetailsByAnnouncementId(announcementId);

        }

        public async Task<AnnouncementTemplateListDetailsModel> GetAnnouncementTemplateListDetailsByAnnouncementId(int announcementId)
        {
            return await _announcementQueries.FindAnnouncementTemplateListDetailsByAnnouncementId(announcementId);

        }

        public async Task<AnnouncementTemplateDetailsForPrintModel> GetAnnouncementDetailsByAnnouncementIdForPrint(int announcementId)
        {
            return await _announcementQueries.GetAnnouncementDetailsByAnnouncementIdForPrint(announcementId);

        }
        public async Task<AnnouncementTemplateDetailsForSupplierForPrintModel> GetAnnouncementDetailsForSupplierForPrint(int announcementId, string cr)
        {
            return await _announcementQueries.GetAnnouncementDetailsForSupplierForPrint(announcementId, cr);

        }
         
        public async Task<AnnouncementDescriptionModel> GetAnnouncementDescriptionDetailsByAnnouncementId(int announcementId)
        {
            return await _announcementQueries.FindAnnouncementDescriptionDetailsByAnnouncementId(announcementId);

        }
        public async Task<AnnouncementTemplateActivityAndAreaDetailsModel> GetAnnouncementTemplateActivityAndAddressDetails(int announcementId)
        {
            return await _announcementQueries.GetAnnouncementTemplateActivityAndAddressDetails(announcementId);

        }

        public async Task<AnnouncementSuppliersTemplateDetailsModel> GettAnnouncementTemplateDetails(int announcementId)
        {
            return await _announcementQueries.FindAnnouncementDetailsByAnnouncementId(announcementId);

        }

        public async Task<AddPublicListToAgencyAnnouncementListsModel> GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(int announcementId, string agencyCode)
        {
            return await _announcementQueries.GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(announcementId, agencyCode);

        }

        public async Task<AnnouncementTemplateMainDetailsModel> GetAnnouncementTemplateDetailsForSuppliers(int announcementId, string cr)
        {
            bool isSupplierBlocked = await _blockAppService.CheckifSupplierBlockedByCrNo(cr);
            if (isSupplierBlocked)
            {
                var announcementTemplate = await _announcementQueries.GetAnnouncementTemplateDetailsForSuppliers(announcementId, cr);
                announcementTemplate.isSupplierBlocked = true;
                return announcementTemplate;
            }
            return await _announcementQueries.GetAnnouncementTemplateDetailsForSuppliers(announcementId, cr);

        }

        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetails(int joinRequestId, string cr)
        {
            return await _announcementQueries.GetAnnouncementTemplateJoinRequestDetails(joinRequestId, cr);

        }
        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(int announcementId, string agencyCode, string userRole)
        {
            return await _announcementQueries.GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(announcementId, agencyCode, userRole);

        }
        public async Task CreateVerificationCode(CreateVerificationCodeModel createVerification)
        {
            Check.ArgumentNotNull(nameof(createVerification), createVerification);
            await _announcementDomainService.CreateVerificationCode(createVerification.Id);
        }


        public async Task<ApproveAnnouncemntSupplierTemplateModel> ApproveAnnouncement(VerificationModel verificationModel)
        {
            Check.ArgumentNotNull(nameof(verificationModel.IdString), verificationModel.IdString);
            await _announcementDomainService.CheckVerificationCode(verificationModel.Id, verificationModel.VerificationCode, (int)Enums.VerificationType.Announcement);
            var announcement = await _announcementQueries.GetAnnouncementWithNoIncludsById(verificationModel.Id);
            announcement.ApproveAnnouncement();
            announcement.SetPublishedDate();
            var userName = _announcementDomainService.GetUserFullName();
            announcement.SetApprovedBy(userName);
            var userId = _announcementDomainService.GetUserId();
            announcement.AddAnnouncementHistoy(userId);
            await _announcementCommands.UpdateAnnouncementSupplierTemplate(announcement);
            await _announcementCommands.SaveChangesAsync();
            return new ApproveAnnouncemntSupplierTemplateModel()
            {
                AnnouncementId = announcement.AnnouncementId,
                AnnouncementIdString = Util.Encrypt(announcement.AnnouncementId),
            };
        }

        public async Task<AnnouncementSuppliersTemplateCancelModel> CancelAnnouncement(AnnouncementSuppliersTemplateCancelModel cancelModel)
        {
            Check.ArgumentNotNull(nameof(cancelModel.AnnouncementIdString), cancelModel.AnnouncementIdString);
            await _announcementDomainService.CheckVerificationCode(cancelModel.AnnouncementId, cancelModel.VerificationCode, (int)Enums.VerificationType.Announcement);
            var announcement = await _announcementQueries.GetAnnouncementWithNoIncludsById(cancelModel.AnnouncementId);
            announcement.UpdateAnnouncementStatus(Enums.AnnouncementSupplierTemplateStatus.Canceled, cancelModel.CancelationReason, _httpContextAccessor.HttpContext.User.UserId(), true);
            cancelModel.ReferenceNumber = announcement.ReferenceNumber;
            await SendNotificationAfterApproveAnnouncementCancel(cancelModel);
            await _announcementCommands.UpdateAnnouncementSupplierTemplate(announcement);
            await _announcementCommands.SaveChangesAsync();
            return new AnnouncementSuppliersTemplateCancelModel()
            {
                AnnouncementId = announcement.AnnouncementId,
                AnnouncementIdString = Util.Encrypt(announcement.AnnouncementId),
            };
        }

        public async Task<CreateAnnouncementSupplierTemplateModel> GetAnnouncementByIdForEdit(int announcementId)
        {
            var result = await _announcementQueries.GetAnnouncementByIdForEdit(announcementId);
            return result;
        }

        public async Task<ExtendAnnouncementSupplierTemplateModel> GetAnnouncementByIdForExtend(int announcementId)
        {
            var result = await _announcementQueries.GetAnnouncementByIdForExtendAnnouncement(announcementId);
            return result;
        }

        private async Task SendNotificationAfterApproveAnnouncementCancel(AnnouncementSuppliersTemplateCancelModel cancelModel)
        {
            var JoinedSuppliers = await _announcementQueries.GetJoinedAnnouncementSuppliers(cancelModel.AnnouncementId);
            var AcceptedSuppliers = await _announcementQueries.GetAcceptedAnnouncementSuppliers(cancelModel.AnnouncementId);
            MainNotificationTemplateModel mainNotificationTemplateModelForSupplier;
            NotificationArguments NotificationArguments = new NotificationArguments();
            NotificationArguments.BodyEmailArgs = new object[] { cancelModel.ReferenceNumber };
            NotificationArguments.SubjectEmailArgs = new object[] { };
            NotificationArguments.PanelArgs = new object[] { cancelModel.ReferenceNumber };
            NotificationArguments.SMSArgs = new object[] { cancelModel.ReferenceNumber };
            mainNotificationTemplateModelForSupplier = new MainNotificationTemplateModel(NotificationArguments, $"AnnouncementSuppliersTemplate/CancelAnnouncementSuppliersTemplate?announcementIdString={Util.Encrypt(cancelModel.AnnouncementId)}", NotificationEntityType.Announcement, cancelModel.AnnouncementId.ToString(), 0);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.CancelAnnouncementTemplate.OperationsToBeApproved.sendApprovedCancelAnnouncementTemplate, AcceptedSuppliers, mainNotificationTemplateModelForSupplier);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.CancelAnnouncementTemplate.OperationsToBeApproved.AcceptJoinedSupplierAnnouncementTemplate, JoinedSuppliers, mainNotificationTemplateModelForSupplier);
        }

        private async Task SendNotificationAfterExtendAnnouncementTemplate(ExtendAnnouncementSupplierTemplateModel extendAnnouncement)
        {
            var AcceptedSuppliers = await _announcementQueries.GetAcceptedAnnouncementSuppliers(extendAnnouncement.AnnouncementId);
            if (AcceptedSuppliers.Any())
            {
                MainNotificationTemplateModel mainNotificationTemplateModelForSupplier;
                NotificationArguments NotificationArguments = new NotificationArguments();
                NotificationArguments.BodyEmailArgs = new object[] { extendAnnouncement.ReferenceNumber, extendAnnouncement.TemplateExtendMechanism };
                NotificationArguments.SubjectEmailArgs = new object[] { };
                NotificationArguments.PanelArgs = new object[] { extendAnnouncement.ReferenceNumber };
                NotificationArguments.SMSArgs = new object[] { extendAnnouncement.ReferenceNumber };
                mainNotificationTemplateModelForSupplier = new MainNotificationTemplateModel(NotificationArguments, $"AnnouncementSuppliersTemplate/AnnouncementSuppliersTemplateDetailsForSuppliers?announcementIdString={Util.Encrypt(extendAnnouncement.AnnouncementId)}", NotificationEntityType.Announcement, extendAnnouncement.AnnouncementId.ToString(), 0);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.ExtendAnnouncementTemplate.OperationsOnExtendAnnouncement.SendNotificationToAcceptedSuppliers, AcceptedSuppliers, mainNotificationTemplateModelForSupplier);
            }
        }

        public async Task<UpdateAnnouncementSupplierTemplateModel> GetAnnouncementByIdForEditApproval(int announcementId)
        {
            var result = await _announcementQueries.GetAnnouncementByIdForEditApproval(announcementId);
            return result;
        }

        private void IsValidToCreate(CreateAnnouncementSupplierTemplateModel announcement)
        {
            if (string.IsNullOrEmpty(announcement.AnnouncementSupplierTemplateName) || string.IsNullOrEmpty(announcement.IntroAboutAnnouncementTemplate) || string.IsNullOrEmpty(announcement.Descriptions))
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.EnterRequiredData);
            }
            if (announcement.TenderTypesId == null || !announcement.TenderTypesId.Any())
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.EnterTenderTypes);
            }
            if (announcement.InsideKsa)
            {
                if (announcement.AreasIds == null || !announcement.AreasIds.Any())
                {
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderAreas);
                }
            }
            else
            {
                if (announcement.CountriesIds == null || !announcement.CountriesIds.Any())
                {
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderCountries);
                }
            }

            if (announcement.ActivityIds == null || !announcement.ActivityIds.Any())
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.ActivityIdRequired);
            }

            if (announcement.IsEffectiveList && announcement.EffectiveListDate == null)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.EffectiveListDateRequired);
            }

            if (announcement.IsEffectiveList && announcement.EffectiveListDate.Value.Date < DateTime.Now.Date)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.DateLessThanToday);
            }

            if (announcement.IsRequiredAttachmentToJoinList && announcement.RequiredAttachment == null)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.AnnouncementRequiredAttachment);
            }

            if (string.IsNullOrEmpty(announcement.AgencyAddress) || string.IsNullOrEmpty(announcement.AgencyFax) || string.IsNullOrEmpty(announcement.AgencyPhone) || string.IsNullOrEmpty(announcement.AgencyEmail))
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.AgencyRequired);
            }
        }

        private void IsValidToExtendAnnouncementSupplier(ExtendAnnouncementSupplierTemplateModel announcement)
        {

            if (string.IsNullOrEmpty(announcement.TemplateExtendMechanism) || string.IsNullOrEmpty(announcement.AnnouncementSupplierTemplateName) || string.IsNullOrEmpty(announcement.IntroAboutAnnouncementTemplate) || string.IsNullOrEmpty(announcement.Descriptions))
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.EnterRequiredData);
            }
            if (announcement.TenderTypesId == null || !announcement.TenderTypesId.Any())
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.EnterTenderTypes);
            }
            if (announcement.InsideKsa)
            {
                if (announcement.AreasIds == null || !announcement.AreasIds.Any())
                {
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderAreas);
                }
            }
            else
            {
                if (announcement.CountriesIds == null || !announcement.CountriesIds.Any())
                {
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderCountries);
                }
            }

            if (announcement.ActivityIds == null || !announcement.ActivityIds.Any())
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.ActivityIdRequired);
            }

            if (announcement.IsEffectiveList && announcement.EffectiveListDate == null)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.EffectiveListDateRequired);
            }

            if (announcement.IsEffectiveList && announcement.EffectiveListDate.Value.Date < DateTime.Now.Date)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.DateLessThanToday);
            }

            if (announcement.IsRequiredAttachmentToJoinList && announcement.RequiredAttachment == null)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.AnnouncementRequiredAttachment);
            }

            if (string.IsNullOrEmpty(announcement.AgencyAddress) || string.IsNullOrEmpty(announcement.AgencyFax) || string.IsNullOrEmpty(announcement.AgencyPhone) || string.IsNullOrEmpty(announcement.AgencyEmail))
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.AgencyRequired);
            }
        }

        private void IsValidToUpdate(UpdateAnnouncementSupplierTemplateModel announcement)
        {

            if (announcement.IsEffectiveList && announcement.EffectiveListDate == null)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.EffectiveListDateRequired);
            }

            if (announcement.IsEffectiveList && announcement.EffectiveListDate < DateTime.Now)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.DateLessThanToday);
            }

            if (!announcement.IsEffectiveList)
            {
                announcement.EffectiveListDate = null;
            }

            if (string.IsNullOrEmpty(announcement.AgencyAddress) || string.IsNullOrEmpty(announcement.AgencyFax) || string.IsNullOrEmpty(announcement.AgencyPhone) || string.IsNullOrEmpty(announcement.AgencyEmail))
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.AgencyRequired);
            }
        }

        public async Task AddPublicAnnouncementListToAgency(int announcementId, string agencyCode)
        {
            var announcement = await _announcementQueries.GetAnnouncementWithNoIncludsById(announcementId);
            announcement.AddPublicAnnouncementListToAgency(agencyCode);
            await _announcementCommands.UpdateAnnouncementSupplierTemplateAsync(announcement);
        }
        public async Task RemovePublicAnnouncementListFromAgency(int announcementId, string agencyCode)
        {
            var announcement = await _announcementQueries.GetAnnouncementWithLinkedAgencyById(announcementId);
            announcement.RemovePublicAnnouncementListFromAgency(agencyCode);
            await _announcementCommands.UpdateAnnouncementSupplierTemplateAsync(announcement);
        }
        public async Task<AnnouncementTemplateMainDetailsModel> SendJoinRequestToAnnouncment(AnnouncementTemplateMainDetailsModel announcementModel)
        {
            var model = new AnnouncementTemplateMainDetailsModel();
            var announcement = await _announcementQueries.GetAnnouncementWithJoinRequestsById(announcementModel.AnnouncementId);
            string cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            await IsValidToAddJoinRequestToAnnouncementTemplate(announcement, cr);
            List<AnnouncementTemplateJoinRequestAttachment> attachments = new List<AnnouncementTemplateJoinRequestAttachment>();
            if (announcementModel.Attachments.Count > 0)
            {
                foreach (var item in announcementModel.Attachments)
                {
                    attachments.Add(new AnnouncementTemplateJoinRequestAttachment(item.Name, item.FileNetReferenceId));
                }
            }
            announcement = announcement.AddJoinRequest(attachments, announcement.AnnouncementId, cr);

            MainNotificationTemplateModel mainNotificationTemplateModelForSupplier;
            NotificationArguments NotificationArguments = new NotificationArguments();
            NotificationArguments.BodyEmailArgs = new object[] { announcement.ReferenceNumber };
            NotificationArguments.SubjectEmailArgs = new object[] { };
            NotificationArguments.PanelArgs = new object[] { announcement.ReferenceNumber };
            NotificationArguments.SMSArgs = new object[] { announcement.ReferenceNumber };

            UserProfile userProfile = await _iDMQueries.FindUserProfileByIdAsync(announcement.CreatedById.Value);
            var branchid = userProfile.BranchUsers.Where(u => u.UserRoleId == (int)Enums.UserRole.NewMonafasat_DataEntry || u.UserRoleId == (int)Enums.UserRole.NewMonafasat_Auditer).FirstOrDefault().BranchId;
            mainNotificationTemplateModelForSupplier = new MainNotificationTemplateModel(NotificationArguments,
                $"AnnouncementSuppliersTemplate/AnnouncementSuppliersTemplateJoinRequestsDetails?AnnouncementId={Util.Encrypt(announcementModel.AnnouncementId)}",
                NotificationEntityType.Tender,
                announcement.AnnouncementId.ToString(),
               branchid);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.ReciveJoinRequest, branchid, mainNotificationTemplateModelForSupplier);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.ReciveJoinRequest, branchid, mainNotificationTemplateModelForSupplier);
            await _announcementCommands.UpdateAnnouncementSupplierTemplateAsync(announcement);
            return model;
        }



        private async Task IsValidToAddJoinRequestToAnnouncementTemplate(AnnouncementSupplierTemplate announcement, string cr)
        {
            if (announcement.StatusId != (int)Enums.AnnouncementStatus.Approved)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.AnnoucementNotApproved);
            }
            if (announcement.IsEffectiveList.HasValue && announcement.EffectiveListDate < DateTime.Now)
            {
                throw new BusinessRuleException("الاعلان غير سارى");
            }
            var hasSentOrAcceptedJoinRequest = announcement.AnnouncementJoinRequests.Any(q => q.Cr == cr && (q.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted || q.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Sent));
            if (hasSentOrAcceptedJoinRequest)
            {
                throw new BusinessRuleException(Resources.AnnouncementResources.ErrorMessages.ThierIsRequestUnderProcessing);

            }
        }

        public async Task<AnnouncementTemplateMainDetailsModel> AnnouncementFinalApprove(AnnouncementFinalApprovalModel approvalModel)
        {

            var announcementId = Util.Decrypt(approvalModel.AnnouncementTemplateIdString);
            if (approvalModel.VerificationCode == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ExpiredActivationCode);
            }
            var verified = await _announcementDomainService.CheckVerificationCode(announcementId, approvalModel.VerificationCode, (int)Enums.VerificationType.Announcement);


            var model = new AnnouncementTemplateMainDetailsModel();

            if (verified)
            {

                var announcementTemplate = await _announcementQueries.GetAnnouncementWithJoinRequestsById(announcementId);
                List<KeyValuePair<string, int>> ModifiedCrs = new List<KeyValuePair<string, int>>();

                if (approvalModel.SuppliersIdsString.Count > 0)
                {
                    List<int> suppliersIds = approvalModel.SuppliersIdsString.Select(r => Util.Decrypt(r)).ToList();
                    foreach (var request in announcementTemplate.AnnouncementJoinRequests.Where(r => suppliersIds.Contains(r.Id)))
                    {
                        ChangeRequestsStatus(request, ModifiedCrs);
                    }
                }
                await _announcementCommands.UpdateAnnouncementSupplierTemplateAsync(announcementTemplate);

                MainNotificationTemplateModel mainNotificationTemplateModelForSupplier;
                NotificationArguments NotificationArguments = new NotificationArguments();
                NotificationArguments.BodyEmailArgs = new object[] { announcementTemplate.ReferenceNumber };
                NotificationArguments.SubjectEmailArgs = new object[] { };
                NotificationArguments.PanelArgs = new object[] { announcementTemplate.ReferenceNumber };
                NotificationArguments.SMSArgs = new object[] { announcementTemplate.ReferenceNumber };
                mainNotificationTemplateModelForSupplier = new MainNotificationTemplateModel(NotificationArguments, $"AnnouncementSuppliersTemplate/AnnouncementSuppliersTemplateDetailsForSuppliers?announcmentIdString={approvalModel.AnnouncementTemplateIdString}", NotificationEntityType.Tender, announcementId.ToString(), 0);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.AcceptAnnouncementJoinRequest, ModifiedCrs.Where(cr => cr.Value == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted).Select(cr => cr.Key).ToList(), mainNotificationTemplateModelForSupplier);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.RejectAnnouncementJoinRequest, ModifiedCrs.Where(cr => cr.Value == (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected).Select(cr => cr.Key).ToList(), mainNotificationTemplateModelForSupplier);
            }
            return model;
        }

        private void ChangeRequestsStatus(AnnouncementJoinRequestSupplierTemplate request, List<KeyValuePair<string, int>> modifiedCrs)
        {
            if (request.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingAcceptance)
            {
                request.UpdateAnnouncementJoinRequest(_httpContextAccessor.HttpContext.User.UserId(), request.Id, (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted);
                modifiedCrs.Add(new KeyValuePair<string, int>(request.Cr, (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted));
            }
            if (request.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection)
            {
                request.UpdateAnnouncementJoinRequest(_httpContextAccessor.HttpContext.User.UserId(), request.Id, (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected, request.RejectionReason);
                modifiedCrs.Add(new KeyValuePair<string, int>(request.Cr, (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected));
            }
        }
    }
}
