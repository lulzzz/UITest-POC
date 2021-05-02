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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementAppService _announcementAppService;
        public AnnouncementController(IOptionsSnapshot<RootConfigurations> rootConfiguration, IAnnouncementAppService announcementAppService) : base(rootConfiguration)
        {
            _rootConfiguration = rootConfiguration.Value;
            _announcementAppService = announcementAppService;
        }

        [HttpGet]
        [Route("GetAnnouncementDetails/{announcementIdString}")]
        [Authorize(RoleNames.GetAnnouncementDetailsPolicy)]
        public async Task<AnnouncementDetailsModel> GetAnnouncementDetails(string announcementIdString)
        {
            return await _announcementAppService.FindAnnouncementDetailsByAnnouncementId(Util.Decrypt(announcementIdString));
        }

        [HttpGet]
        [Route("GetAnnouncementNameByAnnouncementId/{announcementId}")]
        public async Task<LookupModel> GetAnnouncementNameByAnnouncementId(int announcementId)
        {
            var announcementName = await _announcementAppService.GetAnnouncementNameByAnnouncementId(announcementId);
            return announcementName;
        }

        [HttpGet]
        [Route("GetSupplierAnnouncementDetails/{announcementIdString}")]
        [AllowAnonymous]
        public async Task<SupplierAnnouncementDetailsModel> GetSupplierAnnouncementDetails(string announcementIdString)
        {
            return await _announcementAppService.GetAnnouncementDetailsForSupplierByAnnouncementId(Util.Decrypt(announcementIdString));
        }
        #region Supplier
        //[HttpGet]
        //[Route("GetSupplierAnnouncementDetails/{announcementIdString}")]
        //[Authorize(RoleNames.GetAnnouncementDetailsPolicy)]
        //public async Task<SupplierAnnouncementDetailsModel> GetSupplierAnnouncementDetails(string announcementIdString)
        //{
        //    return await _announcementAppService.FindAnnouncementDetailsForSupplierByAnnouncementId(Util.Decrypt(announcementIdString));
        //}
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost]
        [Route("JoinAnnouncement/{id}")]
        public async Task JoinAnnouncement(int id)
        {
            await _announcementAppService.JoinAnnouncement(id);
        }
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost]
        [Route("WithdrawJoinRequest/{id}")]
        public async Task WithdrawJoinRequest(int id)
        {
            await _announcementAppService.WithdrawJoinRequest(id);
        }
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetSupplierAnnouncements")]
        public async Task<QueryResult<SupplierGridAnnouncementModel>> GetSupplierAnnouncements(SupplierAnnouncementSearchCriteria cretria)
        {
            var suppliers = await _announcementAppService.GetSupplierAnnouncements(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetAllSupplierAnnouncements")]
        public async Task<QueryResult<AllAnouuncementsForSupplierVisitorModel>> GetAllSupplierAnnouncements(AllSupplierAnnouncementSearchCriteria criteria)
        {
            criteria.CommericalRegisterNo = User.SupplierNumber();
            var result = await _announcementAppService.GetAllSupplierAnnouncements(criteria);
            return result;
        }

        [HttpGet]
        [Route("GetAllAgencyAnnouncements")]
        [Authorize(RoleNames.GetAnnouncementDetailsPolicy)]
        public async Task<QueryResult<AllAnouuncementsForAgencyModel>> GetAllAgencyAnnouncements(AllAgencyAnnouncementSearchCriteriaModel criteria)
        {
            criteria.CurrentAgencyCode = User.UserAgency();
            criteria.CurrentBranchId = User.UserBranch();
            criteria.CurrentRoleName = User.UserRole();
            var result = await _announcementAppService.GetAllAgencyAnnouncements(criteria);
            return result;
        }

        [HttpGet]
        [Route("GetUnderOperationAgencyAnnouncements")]
        [Authorize(RoleNames.GetUnderOperationAgencyAnnouncementPolicy)]
        public async Task<QueryResult<UnderOperationAnouuncementsForAgencyModel>> GetUnderOperationAgencyAnnouncements(UnderOperationAgencyAnnouncementSearchCriteria criteria)
        {
            criteria.CurrentAgencyCode = User.UserAgency();
            criteria.CurrentBranchId = User.UserBranch();
            criteria.CurrentRoleName = User.UserRole();
            var result = await _announcementAppService.GetUnderOperationAgencyAnnouncements(criteria);
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllVisitorAnnouncements")]
        public async Task<QueryResult<AllAnouuncementsForSupplierVisitorModel>> GetAllVisitorAnnouncements(AllSupplierAnnouncementSearchCriteria criteria)
        {
            var suppliers = await _announcementAppService.GetAllSupplierAnnouncements(criteria);
            return suppliers;
        }
        #endregion

        #region Creat Announcement

        [HttpPost]
        [Authorize(Policy = PolicyNames.CreateAnnouncementPolicy)]
        [Route("CreateNewAnnouncement")]
        public async Task CreateNewAnnouncement([FromBody] CreateAnnouncementModel announcementModel)
        {
            announcementModel.BranchId = User.UserBranch();
            announcementModel.AgencyCode = User.UserAgency();
            await _announcementAppService.CreateNewAnnouncement(announcementModel);
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.CreateAnnouncementPolicy)]
        [Route("GetAnnouncementByIdForEdit/{announcementIdString}")]
        public async Task<CreateAnnouncementModel> GetAnnouncementByIdForEdit(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            var announcement = await _announcementAppService.GetAnnouncementByIdForEdit(announcementId);
            return announcement;
        }

        #endregion Create Announcement



        #region Approval Process

        [HttpPost]
        [Route("CreateVerificationCode")]
        public async Task CreateVerificationCode([FromBody] CreateVerificationCodeModel createVerification)
        {
            createVerification.Id = Util.Decrypt(createVerification.IdString);
            await _announcementAppService.CreateVerificationCode(createVerification);
        }

        [HttpPost]
        [Authorize(Policy = PolicyNames.SendAnnouncementForApprovalPolicy)]
        [Route("SendAnnouncementForApproval/{announcementIdString}")]
        public async Task SendAnnouncementForApproval(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            await _announcementAppService.SendAnnouncementForApproval(announcementId);
        }

        [HttpPost]
        [Authorize(Policy = PolicyNames.ApproveAnnouncementPolicy)]
        [Route("ApproveAnnouncement")]
        public async Task<ApproveAnnouncemntModel> ApproveAnnouncement([FromBody] VerificationModel verificationModel)
        {
            verificationModel.Id = Util.Decrypt(verificationModel.IdString);
            var result = await _announcementAppService.ApproveAnnouncement(verificationModel);
            return result;
        }

        [HttpPost]
        [Authorize(Policy = PolicyNames.RejectApproveAnnouncementPolicy)]
        [Route("RejectApproveAnnouncement")]
        public async Task RejectApproveAnnouncement([FromBody] RejectionReasonModel rejectionReasonModel)
        {
            rejectionReasonModel.Id = Util.Decrypt(rejectionReasonModel.IdString);
            await _announcementAppService.RejectApproveAnnouncement(rejectionReasonModel);
        }

        [HttpPost]
        [Authorize(Policy = PolicyNames.ReopenAnnouncementPolicy)]
        [Route("ReOpenAnnouncement/{announcementIdString}")]
        public async Task ReopenAnnouncement(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            await _announcementAppService.ReOpenAnnouncement(announcementId);
        }


        #endregion Approval Process

        #region Delete-Announcement

        [HttpPost]
        [Authorize(Policy = PolicyNames.DeleteAnnouncementPolicy)]
        [Route("DeleteAnnouncement/{announcementIdString}")]
        public async Task DeleteAnnouncement(string announcementIdString)
        {
            var announcementId = Util.Decrypt(announcementIdString);
            await _announcementAppService.DeleteAnnouncement(announcementId);
        }

        #endregion Delete-Announcement

        #region Lookups
        [HttpGet]
        [Route("GetTenderTypes")]
        [AllowAnonymous]
        public async Task<List<TenderTypeModel>> GetTenderTypes()
        {
            return await _announcementAppService.GetTenderTypes();
        }
        #endregion Lookups

    }
}
