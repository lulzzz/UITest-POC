using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("Api/[controller]")]
    public class InvitationController : BaseController
    {
        private readonly ITenderAppService _tenderAppService;
        private QueryResult<SupplierInvitationModel> tenderListModel;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        public InvitationController(ITenderAppService tenderAppService, IMapper mapper, ISupplierService supplierService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _tenderAppService = tenderAppService;
            _mapper = mapper;
            _supplierService = supplierService;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetTenderInformationByInvitationId")]
        public async Task<TenderInvitationDetailsModel> GetTenderInformationByInvitationId(int invitationId)
        {
            Tender tender = await _tenderAppService.FindTenderByInvitationId(invitationId);
            var model = _mapper.Map<TenderInvitationDetailsModel>(tender);
            return model;
        }

        [Authorize(RoleNames.UpdateInvitationStatusPolicy)]
        [HttpGet]
        [Route("UpdateInvitationStatus")]
        public async Task UpdateInvitationStatus(int invitationId, int statusId)
        {
            var userCR = User.SupplierNumber();
            await _tenderAppService.UpdateInvitationStatus(invitationId, statusId, userCR);
        }
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost]
        [Route("AcceptInvitationWithFees")]
        public async Task AcceptInvitationWithFees([FromBody] TenderFinantialCostModel costModel)
        {
            await _tenderAppService.AcceptInvitationWithFees(costModel);
        }
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost]
        [Route("JoinDirectPurchaseTender/{tenderIdString}")]
        public async Task JoinDirectPurchaseTender(string tenderIdString)
        {
            var tenderid = Util.Decrypt(tenderIdString);
            var cr = User.SupplierNumber();
            await _tenderAppService.JoinDirectPurchaseTender(tenderid, cr);
        }

        // Api / Tender / ApproveJoiningRequestStatus
        // Approve Joining Request 
        [Authorize(RoleNames.UpdateInvitationStatusPolicy)]
        [HttpGet]
        [Route("ApproveJoiningRequestStatus")]
        public async Task ApproveJoiningRequestStatus(int invitationId, int statusId)
        {
            await _tenderAppService.ApproveJoiningRequestStatus(invitationId, statusId);
        }

        // Api / Tender / UpdateInvitationStatus
        // Accept Or Reject Invitation Status
        //[Authorize(Roles = RoleNames.DataEntry)]
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("RejectJoiningRequestStatus")]
        public async Task RejectJoiningRequestStatus(int invitationId, int statusId, string rejectionReason)
        {
            await _tenderAppService.RejectJoiningRequestStatus(invitationId, statusId, rejectionReason);
        }

        // Api / Tender / SendInvitationByEmail
        // Send Invitations By Email For Unregistered Suppliers
        //[Authorize(Roles = RoleNames.DataEntry)]
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("SendInvitationByEmail")]
        public async Task<IActionResult> SendInvitationByEmail(int tenderId, List<string> emails)
        {
            await _tenderAppService.SendInvitationsByEmail(tenderId, emails);
            return Ok();
        }

        // Api / Tender / SendInvitationBySms
        // Send Invitations By SMS For Unregistered Suppliers
        //[Authorize(Roles = RoleNames.DataEntry)]
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("SendInvitationBySms")]
        public async Task<IActionResult> SendInvitationBySms(int tenderId, List<string> mobilNoList)
        {
            await _tenderAppService.SendInvitationBySms(tenderId, mobilNoList);
            return Ok();
        }


        //[Authorize(Roles = RoleNames.DataEntry)]
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetInvitation")]
        public async Task<List<Invitation>> GetInvitations(int tenderId, List<string> commericalRegisterNos)
        {
            List<Invitation> invitations = await _tenderAppService.GetInvitation(commericalRegisterNos, tenderId);
            Check.ArgumentNotNull(nameof(invitations), invitations);
            return invitations;
        }

        //[Authorize(Roles = RoleNames.supplier)]
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetNewInvitationsByCRNo")]
        public async Task<QueryResult<TenderInvitationDetailsModel>> GetNewInvitationsByCRNo(TenderSearchCriteria tenderSearchCriteria)
        {
            var invitations = await _tenderAppService.GetNewInvitationsByCRNo(tenderSearchCriteria);
            Check.ArgumentNotNull(nameof(invitations), invitations);
            var tenderListModel = _mapper.Map<QueryResult<TenderInvitationDetailsModel>>(invitations);
            return tenderListModel;
        }

        //[Authorize(Roles = RoleNames.supplier)]
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetSupplierTenders")]
        public async Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierTenders(TenderSearchCriteria tenderSearch)
        {
            var tenders = await _tenderAppService.GetSupplierTenders(tenderSearch);
            Check.ArgumentNotNull(nameof(Tender), tenders);
            return tenders;
        }

        [Authorize]
        //[Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetAllTenders")]
        public async Task<QueryResult<TenderInvitationDetailsModel>> GetAllTenders()
        {
            string cr = User.SupplierNumber();
            var tenders = await _tenderAppService.GetAllTenders(cr);
            Check.ArgumentNotNull(nameof(Tender), tenders);
            var tenderListModel = _mapper.Map<QueryResult<TenderInvitationDetailsModel>>(tenders);
            return tenderListModel;
        }

        [AllowAnonymous]
        //[Authorize]
        [HttpGet]
        [Route("GetAllSupplierTendersForVisitor")]
        public async Task<QueryResult<AllTendersForVisitorModel>> GetAllSupplierTendersForVisitor(TenderSearchCriteria criteria)
        {
            var tendersListModel = await _tenderAppService.GetAllSupplierTendersForVisitor(criteria);
            return tendersListModel;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllUnsubscribedSupplierTenders")]
        public async Task<QueryResult<TenderInvitationDetailsModel>> GetAllUnsubscribedSupplierTenders(TenderSearchCriteria criteria)
        {
            var tendersListModel = await _tenderAppService.GetAllUnsubscribedSupplierTenders(criteria);
            return tendersListModel;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetSuppliersJoiningRequestsByTenderId/{tenderId}")]
        public async Task<QueryResult<SupplierInvitationModel>> GetSuppliersJoiningRequestsByTenderId(int tenderId)
        {
            int BranchId = User.UserBranch();
            string agencyCode = User.UserAgency();
            var suppliersJoiningRequests = await _tenderAppService.GetSuppliersJoiningRequestsByTenderId(tenderId, agencyCode, BranchId);
            Check.ArgumentNotNull(nameof(Tender), suppliersJoiningRequests);
            var joiningRequestsListModel = _mapper.Map<QueryResult<SupplierInvitationModel>>(suppliersJoiningRequests);
            return joiningRequestsListModel;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetJoiningRequestByInvitationId/{invitationId}")]
        public async Task<TenderInvitationDetailsModel> GetJoiningRequestByInvitationId(int invitationId)
        {
            int BranchId = User.UserBranch();
            string agencyCode = User.UserAgency();
            var JoiningRequest = await _tenderAppService.GetJoiningRequestByInvitationId(invitationId, agencyCode, BranchId);
            Check.ArgumentNotNull(nameof(Tender), JoiningRequest);
            var joiningRequestModel = _mapper.Map<TenderInvitationDetailsModel>(JoiningRequest);
            return joiningRequestModel;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetAllInvitedUnRegisterSuppliers")]
        public async Task<QueryResult<InvitationCrModel>> GetAllInvitedUnRegisterSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetInvitedUnRegisterSuppliers(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetInvitedUnRegisterSuppliersForCreation")]
        public async Task<QueryResult<InvitationCrModel>> GetInvitedUnRegisterSuppliersForCreation(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetInvitedUnRegisterSuppliersForCreation(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetAllInvitedSuppliers")]
        public async Task<QueryResult<InvitationCrModel>> GetAllInvitedSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetInvitedSuppliers(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("GetAllInvitedSuppliersAndSendInvitation")]
        public async Task<bool> GetAllInvitedSuppliersAndSendInvitation([FromBody] SupplierSearchCretriaModel invitations)
        {
            return await _tenderAppService.ResendInvitationToSuppliersAsync(invitations);
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetAllSuppliersBySearchCretriaForInvitation")]
        public async Task<QueryResult<InvitationCrModel>> GetAllSuppliersBySearchCretriaForInvitation(SupplierSearchCretriaModel cretria)
        {
            cretria.AgencyCode = User.UserAgency();
            var suppliers = await _supplierService.GetAllSuppliersBySearchCretriaForInvitation(cretria);
            return suppliers;
        }

        [HttpGet]
        [Route("GetAllSuppliersData")]
        [Authorize(Roles = RoleNames.MonafasatAccountManager + "," + RoleNames.MonafasatAdmin + "," + RoleNames.CustomerService + "," + RoleNames.DataEntryPolicy)]
        public async Task<QueryResult<SupplierModel>> GetAllSuppliersData()
        {
            var suppliers = await _supplierService.GetSuppliersBySearchCretria(new SupplierSearchCretriaModel()
            {
                PageSize = 1000,
            });
            return suppliers;
        }

        #region Invitation-Step
        [Authorize(RoleNames.DataEntryAndSupplierPolicy)]
        [HttpGet]
        [Route("GetTenderDetailsForInvitationStep/{id}")]
        public async Task<InvitationStepModel> GetByTenderIdInvitationDetails(int id)
        {
            string cr = User.SupplierNumber();
            InvitationStepModel invitationStepModel = await _tenderAppService.GetTenderDetailsForInvitationStep(id, cr);
            Check.ArgumentNotNull(nameof(invitationStepModel), invitationStepModel);
            return invitationStepModel;
        }
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetAllSuppliersBySearchCretriaForInvitations")]
        public async Task<QueryResult<SupplierInvitationModel>> GetAllSuppliersBySearchCretriaForInvitations(SupplierSearchCretriaModel cretria)
        {
            cretria.AgencyCode = User.UserAgency();
            var suppliers = await _supplierService.GetAllSuppliersBySearchCretriaForInvitations(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetEmailsForUnregisteredSuppliers")]
        public async Task<QueryResult<string>> GetEmailsForUnregisteredSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetEmailsForUnregisteredSuppliers(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetMobileNumbersForUnregisteredSuppliers")]
        public async Task<QueryResult<string>> GetMobileNumbersForUnregisteredSuppliers(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetMobileForUnregisteredSuppliers(cretria);
            return suppliers;
        }
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetAccptedSuppliersInFirstStage")]
        public async Task<QueryResult<InvitationCrModel>> GetAccptedSuppliersInFirstStage(TenderIdSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetAcceptedSupplierInFirstStageTender(cretria);
            return suppliers;
        }
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetQualifiedSuppliers")]
        public async Task<QueryResult<InvitationCrModel>> GetQualifiedSuppliers(TenderIdSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetQualifiedSuppliers(cretria);
            return suppliers;
        }
        [Authorize(RoleNames.DataEntryAndAuditerAndSupplierPolicy)]
        [HttpGet]
        [Route("GetInvitedSuppliersForInvitationInTenderCreation")]
        public async Task<QueryResult<InvitationCrModel>> GetInvitedSuppliersForInvitationInTenderCreation(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetInvitedSuppliersForInvitationInTenderCreation(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("SendInvitationsInTenderCreation")]
        public async Task SendInvitationsInTenderCreation([FromBody] InvitationsInCreateTenderModel invitations)
        {
            await _tenderAppService.SendInvitationsInTenderCreation(invitations);
        }
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("SubmitTenderInvitationsStep/{tenderId}")]
        public async Task SubmitTenderInvitationsStep(int tenderId)
        {
            await _tenderAppService.SubmitTenderInvitationsStep(tenderId);
        }
        #endregion Invitation-Step

        [Authorize(RoleNames.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementPolicy)]
        [HttpGet]
        [Route("GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement")]
        public async Task<QueryResult<UnRegisterSupplierInvitationModel>> GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementPolicy)]
        [HttpGet]
        [Route("GetInvitedSuppliersForInvitationAfterTenderApprovement")]
        public async Task<QueryResult<SupplierInvitationModel>> GetInvitedSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await _supplierService.GetInvitedSuppliersForInvitationAfterTenderApprovement(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetAnnouncementTemplateSuppliers")]
        public async Task<QueryResult<InvitationCrModel>> GetAnnouncementTemplateSuppliers(AnnouncementSupplierTemplateInvitationSearchmodel cretria)
        {
            var suppliers = await _supplierService.GetAnnouncementTemplateSuppliers(cretria);
            return suppliers;
        }
    }
}

