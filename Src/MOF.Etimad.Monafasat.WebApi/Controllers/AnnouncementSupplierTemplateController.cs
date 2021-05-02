using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.User;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AnnouncementSupplierTemplateController : BaseController
    {
        private readonly IAnnouncementTemplateJoinRequestAppService _joinRequestAppService;
        private readonly IAnnouncementSupplierTemplateAppService _announcementSupplierTemplateAppService;

        public AnnouncementSupplierTemplateController(IOptionsSnapshot<RootConfigurations> rootConfiguration, IAnnouncementSupplierTemplateAppService announcementSupplierTemplateAppService, IAnnouncementTemplateJoinRequestAppService joinRequestAppService) : base(rootConfiguration)
        {
            _rootConfiguration = rootConfiguration.Value;
            _joinRequestAppService = joinRequestAppService;
            _announcementSupplierTemplateAppService = announcementSupplierTemplateAppService;
        }

        [HttpGet]
        [Route("GetAllAnnouncementSupplierTemplates")]
        [Authorize(RoleNames.GetAnnouncementSupplierTemplatePolicy)]
        public async Task<QueryResult<AllAnnouncementSupplierTemplateAgencyModel>> GetAllAnnouncementSupplierTemplates(AgencyAnnouncementSupplierTemplateSearchCriteria criteria)
        {
            criteria.CurrentAgencyCode = User.UserAgency();
            criteria.CurrentBranchId = User.UserBranch();
            criteria.CurrentRoleName = User.UserRole();
            var result = await _announcementSupplierTemplateAppService.GetAllAnnouncementSupplierTemplates(criteria);
            return result;
        }

        [HttpGet]
        [Route("GetJoinRequestsSuppliersByAnnouncementId")]
        public async Task<QueryResult<JoinRequestModel>> GetJoinRequestsSuppliersByAnnouncementId(JoinRequestSuppliersSearchCriteria criteria)
        {
            criteria.CurrentAgencyCode = User.UserAgency();
            criteria.UserRole = User.UserRole();
            criteria.announcementId = Util.Decrypt(criteria.announcementIdString);
            var result = await _announcementSupplierTemplateAppService.GetJoinRequestsSuppliersByAnnouncementIdAsync(criteria);
            return result;
        }




        [HttpGet]
        [Route("GetAllAnnouncementSupplierTemplatesForSupplier")]
        [AllowAnonymous]
        public async Task<QueryResult<AnnouncementSupplierTemplateSupplierGridModel>> GetAllAnnouncementSupplierTemplatesForSupplier(AnnouncementSupplierTemplateSupplierSearchCriteriaModel criteria)
        {
            var result = await _announcementSupplierTemplateAppService.GetAllAnnouncementSupplierTemplatesForSupplier(criteria);
            return result;
        }

        [HttpPost]
        [Route("CreateNewAnnouncementSupplierTemplate")]
        public async Task<CreateAnnouncementSupplierTemplateModel> CreateNewAnnouncementSupplierTemplate([FromBody] CreateAnnouncementSupplierTemplateModel announcementModel)
        {
            var result = await _announcementSupplierTemplateAppService.CreateNewAnnouncementSupplierTemplate(announcementModel);
            return result;
        }


        [HttpPost]
        [Route("UpdateAnnouncementSupplierTemplateList")]
        public async Task<UpdateAnnouncementSupplierTemplateModel> UpdateAnnouncementSupplierTemplateList([FromBody] UpdateAnnouncementSupplierTemplateModel announcementModel)
        {
            var result = await _announcementSupplierTemplateAppService.UpdateAnnouncementSupplierTemplateList(announcementModel);
            return result;
        }

        [HttpPost]
        [Route("ExtendAnnouncementTemplate")]
        public async Task<ExtendAnnouncementSupplierTemplateModel> ExtendAnnouncementTemplate([FromBody] ExtendAnnouncementSupplierTemplateModel announcementModel)
        {
            var result = await _announcementSupplierTemplateAppService.ExtendAnnouncementTemplate(announcementModel);
            return result;
        }




        [HttpGet]
        [Route("GetAnnouncementSupplierTemplateByIdForEdit/{announcementIdString}")]
        public async Task<CreateAnnouncementSupplierTemplateModel> GetAnnouncementSupplierTemplateByIdForEdit(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            var announcement = await _announcementSupplierTemplateAppService.GetAnnouncementByIdForEdit(announcementId);
            return announcement;
        }


        [HttpGet]
        [Route("GetAnnouncementByIdForExtendAnnouncement/{announcementIdString}")]
        public async Task<ExtendAnnouncementSupplierTemplateModel> GetAnnouncementByIdForExtendAnnouncement(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            var announcement = await _announcementSupplierTemplateAppService.GetAnnouncementByIdForExtend(announcementId);
            return announcement;
        }



        [HttpGet]
        [Route("GetAnnouncementSupplierTemplateByIdForEditApproval/{announcementIdString}")]
        public async Task<UpdateAnnouncementSupplierTemplateModel> GetAnnouncementSupplierTemplateByIdForEditApproval(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            var announcement = await _announcementSupplierTemplateAppService.GetAnnouncementByIdForEditApproval(announcementId);
            return announcement;
        }



        [Authorize(RoleNames.GetAnnouncementSupplierTemplatePolicy)]
        [HttpGet]
        [Route("GetJoinRequestsByAnnouncementIdAsync")]
        public async Task<QueryResult<JoinRequestModel>> GetJoinRequestsByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            criteria.AgencyCode = User.UserAgency();
            criteria.UserRole = User.UserRole();
            criteria.announcementId = Util.Decrypt(criteria.announcementIdString);
            var result = await _announcementSupplierTemplateAppService.GetJoinRequestsByAnnouncementIdAsync(criteria);
            return result;
        }

        [HttpGet]
        [Route("GetApprovedSuppliersJoinRequestsByAnnouncementId")]
        public async Task<QueryResult<JoinRequestModel>> GetApprovedSuppliersJoinRequestsByAnnouncementId(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            criteria.announcementId = Util.Decrypt(criteria.announcementIdString);
            var result = await _announcementSupplierTemplateAppService.GetApprovedSuppliersJoinRequestsByAnnouncementId(criteria);
            return result;
        }

        [HttpGet]
        [Route("GetAnnouncementSupplierTemplateByIdForCancelation/{announcementId}")]
        public async Task<AnnouncementSuppliersTemplateCancelModel> GetAnnouncementSupplierTemplateByIdForCancelation(int announcementId)
        {
            var announcement = await _announcementSupplierTemplateAppService.FindAnnouncementDetailsByAnnouncementIdForCancelation(announcementId);
            return announcement;
        }

        [HttpPost]
        [Route("SaveDraft")]
        public async Task<CreateAnnouncementSupplierTemplateModel> SaveDraft([FromBody] CreateAnnouncementSupplierTemplateModel announcementModel)
        {
            var result = await _announcementSupplierTemplateAppService.SaveDraft(announcementModel);
            return result;
        }


        [HttpGet]
        [Route("AnnouncementSupplierTemplateForApproval/{announcementId}")]
        public async Task<ApproveAnnouncemntSupplierTemplateModel> AnnouncementSupplierTemplateForApproval(int announcementId)
        {
            ApproveAnnouncemntSupplierTemplateModel approveAnnouncemntSupplierTemplateModel = await _announcementSupplierTemplateAppService.GetAnnouncementSupplierTemplateDetailsByannouncementId(announcementId);
            return approveAnnouncemntSupplierTemplateModel;
        }


        [HttpGet]
        [Route("GetAnnouncementSupplierTemplateDetails/{id}")]
        public async Task<AnnouncementSuppliersTemplateDetailsModel> GetAnnouncementSupplierTemplateDetails(int Id)
        {
            AnnouncementSuppliersTemplateDetailsModel detailsModel = await _announcementSupplierTemplateAppService.FindAnnouncementDetailsByAnnouncementId(Id);
            return detailsModel;
        }
        [HttpGet]
        [Route("GetAnnouncementBasicDetails/{id}")]
        public async Task<AnnouncementBasicDetailModel> GetAnnouncementBasicDetails(int Id)
        {
            AnnouncementBasicDetailModel detailsModel = await _announcementSupplierTemplateAppService.GetAnnouncementBasicDetailsByAnnouncementId(Id);
            return detailsModel;
        }

        [HttpGet]
        [Route("GetAnnouncementTemplateListDetail/{id}")]
        public async Task<AnnouncementTemplateListDetailsModel> GetAnnouncementTemplateListDetail(int Id)
        {
            AnnouncementTemplateListDetailsModel detailsModel = await _announcementSupplierTemplateAppService.GetAnnouncementTemplateListDetailsByAnnouncementId(Id);
            return detailsModel;
        }


        [HttpGet]
        [Route("GetTendersByAnnouncementIdAsync")]
        public async Task<QueryResult<TenderAnnouncementSuppliersTemplateDetails>> GetTendersByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            var result = await _announcementSupplierTemplateAppService.GetTendersByAnnouncementIdAsync(criteria);
            return result;
        }



        [HttpGet]
        [Route("GetAnnouncementDescriptionDetails/{id}")]
        [AllowAnonymous]
        public async Task<AnnouncementDescriptionModel> GetAnnouncementDescriptionDetails(int Id)
        {
            AnnouncementDescriptionModel detailsModel = await _announcementSupplierTemplateAppService.GetAnnouncementDescriptionDetailsByAnnouncementId(Id);
            return detailsModel;
        }

        [HttpGet]
        [Route("GetAnnouncementJoinRequestDetails/{announcementId}")]
        [AllowAnonymous]
        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementJoinRequestDetails(int announcementId)
        {
            AnnouncementSuppliersTemplateJoinRequestsDetailsModel detailsModel = await _joinRequestAppService.GetAnnouncementJoinRequestDetails(announcementId, User.SupplierNumber());
            return detailsModel;
        }
        [HttpGet]
        [Route("GetAnnouncementTemplateActivityAndAddressDetails/{announcementId}")]
        [AllowAnonymous]
        public async Task<AnnouncementTemplateActivityAndAreaDetailsModel> GetAnnouncementTemplateActivityAndAddressDetails(int announcementId)
        {
            AnnouncementTemplateActivityAndAreaDetailsModel detailsModel = await _announcementSupplierTemplateAppService.GetAnnouncementTemplateActivityAndAddressDetails(announcementId);
            return detailsModel;
        }

        [HttpGet]
        [Route("GettAnnouncementTemplateDetailsData/{announcementId}")]
        public async Task<AnnouncementSuppliersTemplateDetailsModel> GettAnnouncementTemplateDetailsData(int announcementId)
        {
            return await _announcementSupplierTemplateAppService.GettAnnouncementTemplateDetails(announcementId);
        }

        [HttpGet]
        [Route("GetAnnouncementTemplateDetailsForSuppliers/{announcementId}")]
        [AllowAnonymous]
        public async Task<AnnouncementTemplateMainDetailsModel> GetAnnouncementTemplateDetailsForSuppliers(int announcementId)
        {

            return await _announcementSupplierTemplateAppService.GetAnnouncementTemplateDetailsForSuppliers(announcementId, User.SupplierNumber());
        }

        [HttpGet]
        [Route("GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists/{announcementId}")]
        [AllowAnonymous]
        public async Task<AddPublicListToAgencyAnnouncementListsModel> GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(int announcementId)
        {

            return await _announcementSupplierTemplateAppService.GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(announcementId, User.UserAgency());
        }

        [HttpGet]
        [Route("GetAnnouncementTemplateJoinRequestDetails/{joinRequestId}")]
        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetails(int joinRequestId)
        {

            return await _announcementSupplierTemplateAppService.GetAnnouncementTemplateJoinRequestDetails(joinRequestId, User.SupplierNumber());
        }
        [HttpGet]
        [Route("GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId/{announcementId}")]
        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(int announcementId)
        {

            return await _announcementSupplierTemplateAppService.GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(announcementId, User.UserAgency(), User.UserRole());
        }

        [HttpPost]
        [Route("CreateVerificationCode")]
        public async Task CreateVerificationCode([FromBody] CreateVerificationCodeModel createVerification)
        {
            createVerification.Id = Util.Decrypt(createVerification.IdString);
            await _announcementSupplierTemplateAppService.CreateVerificationCode(createVerification);
        }

        [HttpPost]
        [Route("ApproveAnnouncement")]
        public async Task<ApproveAnnouncemntSupplierTemplateModel> ApproveAnnouncement([FromBody] VerificationModel verificationModel)
        {
            verificationModel.Id = Util.Decrypt(verificationModel.IdString);
            var result = await _announcementSupplierTemplateAppService.ApproveAnnouncement(verificationModel);
            return result;
        }

        [HttpPost]
        [Route("ApproveAnnouncementCancelRequestAsync")]
        public async Task<AnnouncementSuppliersTemplateCancelModel> ApproveAnnouncementCancelRequestAsync([FromBody] AnnouncementSuppliersTemplateCancelModel cancelModel)
        {
            cancelModel.AnnouncementId = Util.Decrypt(cancelModel.AnnouncementIdString);
            var result = await _announcementSupplierTemplateAppService.CancelAnnouncement(cancelModel);
            return result;
        }

        [HttpPost]
        [Route("SendJoinRequestToAnnouncment")]
        public async Task<AnnouncementTemplateMainDetailsModel> SendJoinRequestToAnnouncment([FromBody] AnnouncementTemplateMainDetailsModel announcementModel)
        {
            var result = await _announcementSupplierTemplateAppService.SendJoinRequestToAnnouncment(announcementModel);
            return result;
        }

        [HttpPost]
        [Route("SaveJoinRequestResult")]
        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> SaveJoinRequestResult([FromBody] AnnouncementSuppliersTemplateJoinRequestsDetailsModel joinModel)
        {
            joinModel.UserId = User.UserId();
            var result = await _joinRequestAppService.SaveJoinRequestResult(joinModel);
            return result;
        }

        [HttpPost]
        [Route("WithdrawFromAnnouncementTemplate/{joinRequestId}")]
        public async Task WithdrawFromAnnouncementTemplate(int joinRequestId)
        {
            await _joinRequestAppService.WithdrawFromAnnouncementTemplate(User.UserId(), joinRequestId);
        }

        [HttpPost]
        [Route("DeleteSupplierFromAnnouncementTemplate")]
        public async Task DeleteSupplierFromAnnouncementTemplate([FromBody] DeleteSupplierFromAnnouncementModel deleteModel)
        {
            deleteModel.UserId = User.UserId();
            await _joinRequestAppService.DeleteSupplierFromAnnouncementTemplate(deleteModel);
        }


        [HttpPost]
        [Route("DeleteAnnouncementTemplate/{announcementIdString}")]
        public async Task DeleteAnnouncement(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            await _announcementSupplierTemplateAppService.DeleteAnnouncement(announcementId);
        }


        [HttpPost]
        [Route("AddListToAgencyAnnouncementLists/{announcementIdString}")]
        public async Task AddListToAgencyAnnouncementLists(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            await _announcementSupplierTemplateAppService.AddPublicAnnouncementListToAgency(announcementId, User.UserAgency());
        }
        [HttpPost]
        [Route("RemoveListFromAgencyAnnouncementLists/{announcementIdString}")]
        public async Task RemoveListFromAgencyAnnouncementLists(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            await _announcementSupplierTemplateAppService.RemovePublicAnnouncementListFromAgency(announcementId, User.UserAgency());
        }


        [HttpPost]
        [Route("FinalApproveAnnouncemntSupplierTemplate")]
        public async Task FinalApproveAnnouncemntSupplierTemplate([FromBody] AnnouncementFinalApprovalModel approvalModel)
        {
            await _announcementSupplierTemplateAppService.AnnouncementFinalApprove(approvalModel);
        }



        [HttpGet]
        [Route("GetBeneficiaryAgencyPagingAsync")]
        public async Task<QueryResult<LinkedAgenciesAnnouncementTemplateModel>> GetBeneficiaryAgencyPagingAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            var result = await _announcementSupplierTemplateAppService.GetBeneficiaryAgencyByAnnouncementIdAsync(criteria);
            return result;
        }


        [HttpGet("AnnouncementSuppliersTemplateJoinRequestsDetailsReport/{announcementId}")]
        [AllowAnonymous]
        public async Task<AnnouncementTemplateDetailsForPrintModel> AnnouncementSuppliersTemplateJoinRequestsDetailsReport(int announcementId)
        {
            return await _announcementSupplierTemplateAppService.GetAnnouncementDetailsByAnnouncementIdForPrint(announcementId);
        }

        [HttpGet("GetAnnouncementTemplateJoinRequestsDetailsReportForSupplier/{announcementId}")]
        [AllowAnonymous]
        public async Task<AnnouncementTemplateDetailsForSupplierForPrintModel> GetAnnouncementTemplateJoinRequestsDetailsReportForSupplier(int announcementId)
        {
            return await _announcementSupplierTemplateAppService.GetAnnouncementDetailsForSupplierForPrint(announcementId, User.SupplierNumber());
        }
    }

}