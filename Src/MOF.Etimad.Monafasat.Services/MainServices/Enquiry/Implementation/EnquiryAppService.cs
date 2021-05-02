using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class EnquiryAppService : IEnquiryAppService
    {
        private readonly IEnquiryQueries _enquiryQueries;
        private readonly IEnquiryCommands _enquiryCommands;
        private readonly IEnquiryDomainService _enquiryDomain;
        private readonly IMapper _mapper;
        private readonly ITenderAppService _tenderAppService;
        private readonly IIDMAppService _iDMAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationAppService _notificationAppService;
        private readonly RootConfigurations _rootConfiguration;

        public EnquiryAppService(IIDMAppService iDMAppService, INotificationAppService notificationAppService, IMapper mapper, IEnquiryQueries enquiryQueries, IEnquiryCommands enquiryCommands, ITenderAppService tenderAppService, IEnquiryDomainService enquiryDomain, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _iDMAppService = iDMAppService;
            _enquiryCommands = enquiryCommands;
            _enquiryQueries = enquiryQueries;
            _mapper = mapper;
            _notificationAppService = notificationAppService;
            _tenderAppService = tenderAppService;
            _enquiryDomain = enquiryDomain;
            _rootConfiguration = rootConfiguration.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Enquiry> SendEnquiry(EnquiryModel enquiryModel)
        {
            Enquiry enquiry = _mapper.Map<Enquiry>(enquiryModel);
            Tender tender = await _tenderAppService.FindTenderByTenderId(enquiry.TenderId);
            if (tender != null)
                await _enquiryDomain.CheckCanAddEnquiry(tender, enquiry.CommericalRegisterNo);

            #region 
            NotificationArguments NotificationArgument = new NotificationArguments();
            NotificationArgument.BodyEmailArgs = new object[] { "", tender?.TenderName, tender?.ReferenceNumber, enquiryModel.EnquiryName };
            NotificationArgument.SubjectEmailArgs = new object[] { };
            NotificationArgument.PanelArgs = new object[] { tender?.ReferenceNumber };
            NotificationArgument.SMSArgs = new object[] { tender?.ReferenceNumber };
            #endregion

            MainNotificationTemplateModel mainNotificationTemplateModel;
            if (tender != null)
            {
                mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArgument,
                              $"Enquiry/SupplierEnquiriesOnTender?id={Util.Encrypt(tender.TenderId)}",
                              NotificationEntityType.Tender,
                              tender.TenderId.ToString(),
                              tender.TechnicalOrganizationId);

                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.TechnicalCommitteeSecretary.InquiriesAboutTender.vendoraskquestion, tender?.TechnicalOrganizationId, mainNotificationTemplateModel);

                tender.AddActionHistory(tender.TenderStatusId, TenderActions.AskQuestion, "", _httpContextAccessor.HttpContext.User.UserId());

                var request = new AgencyCommunicationRequest(tender.TenderId, (int)Enums.AgencyCommunicationRequestType.Enquiry, (int)Enums.AgencyCommunicationRequestStatus.PendingEnquiryForReply, RoleNames.supplier);
                tender.AddAgencyCommunicationRequest(request);
                enquiry.SetCommunicationRequest(request);
            }
            return await _enquiryCommands.CreateAsync(enquiry);
        }

        public async Task<EnquiryReply> AddEnquiryReply(EnquiryReplyModel enquiryReplyModel)
        {
            EnquiryReply enquiryReply = new EnquiryReply(enquiryReplyModel.EnquiryReplyMessage, enquiryReplyModel.EnquiryId, (int)Enums.EnquiryReplyStatus.Pending, enquiryReplyModel.CommitteeId, false);
            await _enquiryDomain.UserCanAddReplyToEnquiry(enquiryReplyModel.EnquiryId, _httpContextAccessor.HttpContext.User.UserId());
            return await _enquiryCommands.CreateReplyAsync(enquiryReply);
        }

        public async Task AddEnquiryComment(EnquiryReplyModel enquiryReplyModel)
        {
            EnquiryReply enquiryReply = new EnquiryReply(enquiryReplyModel.EnquiryReplyMessage, enquiryReplyModel.EnquiryId, (int)Enums.EnquiryReplyStatus.Pending, enquiryReplyModel.CommitteeId, true);
            await _enquiryCommands.CreateReplyAsync(enquiryReply);
        }

        public async Task<EnquiryReply> EditEnquiryReply(EnquiryReplyModel enquiryReplyModel)
        {
            EnquiryReply enquiryReply = await _enquiryQueries.GetEnquiryReplyByReplyId(enquiryReplyModel.EnquiryReplyId);
            await _enquiryDomain.UserCanAddReplyToEnquiry(enquiryReply.EnquiryId, _httpContextAccessor.HttpContext.User.UserId());
            enquiryReply.Update(enquiryReplyModel.EnquiryReplyId, enquiryReplyModel.EnquiryReplyStatusId, enquiryReplyModel.EnquiryReplyMessage, enquiryReplyModel.CommitteeId, enquiryReplyModel.IsComment);
            return await _enquiryCommands.UpdateReplyAsync(enquiryReply);
        }

        public async Task<QueryResult<JoinTechnicalCommitteeModel>> GetInvitedCommitesByEnquiryId(SimpleSearchCretriaModel criteria)
        {
            var result = await _enquiryQueries.GetInvitedCommitesByEnquiryId(criteria);
            return result;
        }

        public async Task<EnquiryModel> GetEnquiryById(int enquiryId, int userId)
        {
            var result = await _enquiryQueries.FindEnquiryById(enquiryId, userId);
            return result;
        }

        public async Task<EnquiryReplyModel> GetEnquiryReplyById(int enquiryReplyId)
        {
            var result = await _enquiryQueries.FindEnquiryReplyById(enquiryReplyId);
            return result;
        }

        public async Task<QueryResult<Enquiry>> GetAllPendingEnquiriesByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var result = await _enquiryQueries.GetAllPendingEnquiriesByTenderId(tenderId);
            return result;
        }

        public async Task<QueryResult<EnquiryReplyModel>> GetAllEnquiryRepliesByEnquiryId(SimpleSearchCretriaModel criteria)
        {
            var result = await _enquiryQueries.GetAllEnquiryRepliesByEnquiryId(criteria);
            return result;
        }

        public async Task<QueryResult<Enquiry>> GetAllEnquiryRepliesByTenderId(SimpleSearchCretriaModel criteria)
        {
            var result = await _enquiryQueries.GetAllEnquiryRepliesByTenderId(criteria);
            return result;
        }

        public async Task<List<Enquiry>> GetAllEnquiryRepliesByTenderId(int tenderId, int userId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var result = await _enquiryQueries.GetAllEnquiryRepliesByTenderId(tenderId, userId);
            return result;
        }


        public async Task<EnquiryReply> DeleteReply(int enquiryReplyId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(enquiryReplyId), enquiryReplyId.ToString());
            var enquiryReply = await _enquiryQueries.GetEnquiryReplyByReplyId(enquiryReplyId);
            IsReplyApproved(enquiryReply);
            enquiryReply.DeActive();
            return await _enquiryCommands.UpdateReplyAsync(enquiryReply);
        }

        public async Task<EnquiryReply> ApproveEnquiryReply(int enquiryReplyId)
        {
            EnquiryReply enquiryReply = await _enquiryQueries.GetEnquiryReplyWithTender(enquiryReplyId);
            IsReplyApproved(enquiryReply);
            enquiryReply.ApproveEnquiryReply();

            await SendNotificationAfterApproveReply(enquiryReply);
            return await _enquiryCommands.UpdateReplyAsync(enquiryReply);
        }

        private async Task SendNotificationAfterApproveReply(EnquiryReply enquiryReply)
        {
            var tender = enquiryReply.Enquiry.Tender;
            List<string> crs = (tender.TenderTypeId == (int)Enums.TenderType.PreQualification || tender.TenderTypeId == (int)Enums.TenderType.PostQualification) ? await _iDMAppService.QualificationToSendInvitation(tender.TenderId) : await _iDMAppService.GetAllSupplierOnTender(tender.TenderId);

            NotificationArguments NotificationArgument = new NotificationArguments();

            if (tender.TenderTypeId == (int)Enums.TenderType.PreQualification || tender.TenderTypeId == (int)Enums.TenderType.PostQualification)
            {
                NotificationArgument.BodyEmailArgs = new object[] { "", tender.TenderName, tender.ReferenceNumber, tender?.LastEnqueriesDate, tender?.LastOfferPresentationDate };
            }
            else
            {
                NotificationArgument.BodyEmailArgs = new object[] { "", tender.TenderName, tender.ReferenceNumber, tender.Purpose, tender?.LastEnqueriesDate, tender?.LastOfferPresentationDate, tender.OffersOpeningDate, tender?.OffersOpeningDate?.Hour };
            }

            NotificationArgument.SubjectEmailArgs = new object[] { };
            NotificationArgument.PanelArgs = new object[] { tender.ReferenceNumber };
            NotificationArgument.SMSArgs = new object[] { tender.ReferenceNumber };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArgument, $"Enquiry/SupplierEnquiriesOnTender?id={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.TechnicalOrganizationId);

            if (tender.TenderTypeId == (int)Enums.TenderType.PreQualification || tender.TenderTypeId == (int)Enums.TenderType.PostQualification)
            {
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.InquiriesAboutTender.publishfaqanswerforQualification, crs, mainNotificationTemplateModel);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.TechnicalCommitteeSecretary.InquiriesAboutTender.PublishfaqanswerforQualification, tender.TechnicalOrganizationId, mainNotificationTemplateModel);

            }
            else
            {
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.InquiriesAboutTender.publishfaqanswer, crs, mainNotificationTemplateModel);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.TechnicalCommitteeSecretary.InquiriesAboutTender.publishfaqanswerbackend, tender.TechnicalOrganizationId, mainNotificationTemplateModel);
            }

            enquiryReply.Enquiry.Tender.AddActionHistory(tender.TenderStatusId, TenderActions.ApproveFAQ, "", _httpContextAccessor.HttpContext.User.UserId());
            var comunicationRequest = await _enquiryQueries.GetEnquiryReplyWithCommunicationRequest(enquiryReply.EnquiryReplyId);

            AgencyCommunicationRequest agencyCommunicationRequest = await _enquiryDomain.GetEnquiryCommunicationRequestByRequestId(comunicationRequest.Enquiry.AgencyCommunicationRequest.AgencyRequestId);

            if (agencyCommunicationRequest.StatusId == (int)Enums.AgencyCommunicationRequestStatus.PendingEnquiryForReply && enquiryReply.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Approved)
            {
                agencyCommunicationRequest.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.ReplyOnEnquiry);
                await _enquiryDomain.UpdateCommunicationRequest(agencyCommunicationRequest);
            }
        }
        private void IsReplyApproved(EnquiryReply enquiryReply)
        {
            if (enquiryReply.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Approved)
                throw new BusinessRuleException(MOF.Etimad.Monafasat.Resources.EnquiryResources.ErrorMessages.EnquiryReplyIsApproved);
        }

        public async Task<JoinTechnicalCommitteeModel> GetJoinTechnicalCommitteeByEnquiryId(int enquiryId)
        {
            var result = await _enquiryQueries.GetJoinCommitteeByEnquiryId(enquiryId);
            return result;
        }

        public async Task<JoinTechnicalCommitteeModel> SendInvitationsToJoinCommittee(JoinTechnicalCommitteeModel joinModel)
        {
            if (joinModel.SelectedUserCommitteeId == joinModel.JoinedCommitteeId)
                throw new BusinessRuleException("لا يمكن للجهة ان تدعوا نفسها للرد على الاستفسار");

            await _enquiryDomain.CheckCanAddCommittee(joinModel.JoinedCommitteeId);
            await HasJoinCommitteeForEnquiry(joinModel.EnquiryId, joinModel.JoinedCommitteeId);
            var enquiry = await _enquiryQueries.FindEnquiryByEnquiryId(joinModel.EnquiryId);
            var tender = enquiry.Tender;

            await SendInvitationsToJoinCommitteeNotification(joinModel, tender);

            tender.AddActionHistory(tender.TenderStatusId, TenderActions.AddFAQInvitation, "", _httpContextAccessor.HttpContext.User.UserId());

            enquiry.JoinTechnicalCommitee(joinModel.EnquiryId, joinModel.JoinedCommitteeId, joinModel.SelectedUserCommitteeId, joinModel.EnquiryComment);

            await _enquiryCommands.UpdateAsync(enquiry);

            return joinModel;
        }

        private async Task HasJoinCommitteeForEnquiry(int enquiryId, int joinedCommitteeId)
        {
            var result = await _enquiryQueries.GetJoinCommitteeByEnquiryIdAndCommitteeId(enquiryId, joinedCommitteeId);
            if (result)
                throw new BusinessRuleException(MOF.Etimad.Monafasat.Resources.EnquiryResources.Messages.CannotJoinCommittee);
        }

        private async Task SendInvitationsToJoinCommitteeNotification(JoinTechnicalCommitteeModel joinModel, Tender tender)
        {
            var DetailsUrl = _rootConfiguration?.MonafasatURLConfiguration?.MonafasatURL;

            NotificationArguments NotificationArgument = new NotificationArguments();
            NotificationArgument.BodyEmailArgs = new object[] { tender.TenderName, joinModel.CommitteeName, tender.ReferenceNumber, tender?.Agency?.NameArabic, tender?.Branch?.BranchName, tender?.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate, tender.OffersOpeningDate?.Hour, DetailsUrl + $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}" };

            NotificationArgument.SubjectEmailArgs = new object[] { };
            NotificationArgument.PanelArgs = new object[] { tender.ReferenceNumber };
            NotificationArgument.SMSArgs = new object[] { tender.ReferenceNumber };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArgument,
                $"Enquiry/EnquiryDetails?enquiryIdString={Util.Encrypt(joinModel.EnquiryId)}",
                NotificationEntityType.Tender, tender.TenderId.ToString(), 0, joinModel.JoinedCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.TechnicalCommitteeSecretary.InquiriesAboutTender.changetendergatid, joinModel.JoinedCommitteeId, mainNotificationTemplateModel);

        }

        public async Task RemoveInvitedCommittee(int joinTechnicalCommitteeId, int replyId, int enquiryId)
        {
            var enquiry = await _enquiryQueries.FindEnquiryByEnquiryId(enquiryId);
            enquiry.RemoveJoinedCommittee(enquiry.JoinTechnicalCommittees, enquiry.EnquiryReplies, joinTechnicalCommitteeId, replyId);
            enquiry.Tender.AddActionHistory(enquiry.Tender.TenderStatusId, TenderActions.DeleteFAQInvitation, "", _httpContextAccessor.HttpContext.User.UserId());
            await _enquiryCommands.UpdateAsync(enquiry);
        }
    }
}