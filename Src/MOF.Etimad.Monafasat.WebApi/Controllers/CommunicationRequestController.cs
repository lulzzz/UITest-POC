using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Negotiation;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CommunicationRequestController : BaseController
    {
        private readonly ICommunicationAppService _communicationAppService;
        private readonly INegotiationAppService _negotiationAppService;
        private readonly IVerificationService _verification;
        public CommunicationRequestController(INegotiationAppService negotiationAppService, ICommunicationAppService communicationAppService, IVerificationService verification, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _communicationAppService = communicationAppService;
            _negotiationAppService = negotiationAppService;
            _verification = verification;
        }

        #region Tender Communication Request
        [HttpGet]
        [Route("GetSuppliersCommunicationRequestsGrid")]
        public async Task<QueryResult<CommunicationRequestGrid>> GetSuppliersCommunicationRequestsGrid(SimpleTenderSearchCriteria criteria)
        {
            if (User.IsInRole(RoleNames.supplier)) criteria.CR = User.SupplierNumber(); else criteria.CR = string.Empty;
            var suppliersCommunicationRequests = await _communicationAppService.GetSuppliersCommunicationRequestsGridAsync(criteria);
            Check.ArgumentNotNull(nameof(AgencyCommunicationRequest), suppliersCommunicationRequests);
            return suppliersCommunicationRequests;
        }

        [HttpGet]
        [Route("GetAgencyCommunicationRequestsGrid")]
        public async Task<QueryResult<CommunicationRequestGrid>> GetAgencyCommunicationRequestsGrid(SimpleTenderSearchCriteria criteria)
        {
            if (User.IsInRole(RoleNames.supplier))
            {
                criteria.CR = User.SupplierNumber();
            }
            else
            {
                criteria.SelectedCommittee = User.SelectedCommittee();
                criteria.userId = User.UserId();
            }
            var agencyCommunicationRequests = await _communicationAppService.GetAgencyCommunicationRequestsGridAsync(criteria);
            Check.ArgumentNotNull(nameof(AgencyCommunicationRequest), agencyCommunicationRequests);
            return agencyCommunicationRequests;
        }
        #endregion

        #region Extend Offers Validity
        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpGet]
        [Route("GetExtendOffersValidity/{agencyRequestId}/{tenderId?}")]
        public async Task<ExtendOffersValidityModel> GetExtendOffersValidity(int agencyRequestId, int? tenderId)
        {
            int UserId = User.UserId();
            ExtendOffersValidityModel extendOffersValidityModel = await _communicationAppService.GetExtendOffersValidity(agencyRequestId, tenderId == null ? 0 : tenderId.Value, UserId);
            return extendOffersValidityModel;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser)]
        [HttpGet]
        [Route("GetTenderOffersPagingAsync/{tenderId}/{extendOfferValidityId}/{extendofferValidityStatusId}")]
        public async Task<QueryResult<ExtendOffersGridModel>> GetTenderOffersPagingAsync(int tenderId, int extendOfferValidityId, int extendofferValidityStatusId)
        {
            var invitationsOffers = await _communicationAppService.GetTenderOffersPagingAsync(tenderId, extendOfferValidityId, extendofferValidityStatusId);
            Check.ArgumentNotNull(nameof(Offer), invitationsOffers);
            return invitationsOffers;
        }

        //[Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        //[HttpGet]
        //[Route("GeOfferByTenderIdAndOfferIdForOpening/{tenderId}/{offerId}/{combinedIdString?}")]
        //public async Task<OfferDetailModel> GeOfferDetailModel(int tenderId, int offerId, int combinedIdString)
        //{
        //    OfferDetailModel offerDetailModel = await _communicationAppService.GeOfferDetailModel(tenderId, offerId);
        //    return offerDetailModel;
        //}

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpGet]
        [Route("GetOfferToExtendbyId/{offerid}")]
        public async Task<ExtendOffersDisplayFilesModel> GetOfferToExtendbyId(int offerid)
        {
            int userId = User.UserId();
            ExtendOffersDisplayFilesModel extendOffersDisplayFilesModel = await _communicationAppService.GetOfferToExtendbyId(offerid, userId);
            return extendOffersDisplayFilesModel;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.UnitManagerUser)]
        [HttpGet]
        [Route("GetOfferFilesById/{offerid}")]
        public async Task<OfferNegotiationFileModel> GetOfferFilesById(int offerid)
        {
            int userId = User.UserId();
            OfferNegotiationFileModel extendOffersDisplayFilesModel = await _communicationAppService.GetOfferFilesbyId(offerid);
            return extendOffersDisplayFilesModel;
        }
        [Authorize(Roles = RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpGet]
        [Route("GetOfferDetails/{combinedId}")]
        public async Task<OfferDetailModel> GetOfferDetails(int combinedId)
        {
            int userId = User.UserId();
            var details = await _communicationAppService.GetOfferDetails(combinedId, userId);
            Check.ArgumentNotNull(nameof(OfferDetailModel), details);
            return details;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpGet]
        [Route("GetCombinedSuppliers/{offerid}")]
        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(int offerid)
        {
            int userId = User.UserId();
            QueryResult<CombinedSupplierModel> combinedSupplierModel = await _communicationAppService.GetCombinedSuppliers(offerid, userId);
            Check.ArgumentNotNull(nameof(CombinedSupplierModel), combinedSupplierModel);
            return combinedSupplierModel;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpGet]
        [Route("GetTenderOfferIDsByOfferID/{offerid}")]
        public async Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerid)
        {
            CombinedSupplierModel combinedSupplierModel = await _communicationAppService.GetTenderOfferIDsByOfferID(offerid);
            return combinedSupplierModel;
        }

        [HttpPost]
        [Route("AddExtendOffersValidityRequest")]
        public async Task<string> AddExtendOffersValidityRequest([FromBody] ExtendOffersValiditySavingModel model)
        {
            string agencyRequestIdString = await _communicationAppService.AddExtendOffersValidity(model);
            return agencyRequestIdString;
        }

        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet]
        [Route("GetSupplierExtendOffersValidityForSupplier/{supplierExtendOffersValidtyId}")]
        public async Task<SupplierExtendOffersValidityViewModel> GetSupplierExtendOffersValidityForSupplier(int supplierExtendOffersValidtyId)
        {
            var Cr = User.SupplierNumber();
            SupplierExtendOffersValidityViewModel supplierExtendOffersValidity = await _communicationAppService.GetSupplierExtendOffersValidityForSupplier(supplierExtendOffersValidtyId, Cr);
            return supplierExtendOffersValidity;
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("AcceptExtendOffersValidityBySupplier/{extendOffersValidtyId}")]
        public async Task AcceptExtendOffersValidityBySupplier(int extendOffersValidtyId)
        {
            string userCr = User.SupplierNumber();
            string supplierName = User.SupplierName();
            await _communicationAppService.AcceptExtendOffersValidityBySupplier(extendOffersValidtyId, userCr, supplierName);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("AddInitialGuaranteeAttachmentToOfferAsync/{tenderId}")]
        public async Task AddInitialGuaranteeAttachmentToOfferAsync([FromBody] ExtendOffersValidityAttachementViewModel extendOffersValidityAttachementViewModel, int tenderId)
        {
            string userCr = User.SupplierNumber();
            string supplierName = User.SupplierName();
            await _communicationAppService.AddInitialGuaranteeAttachmentToEXV(extendOffersValidityAttachementViewModel, tenderId, userCr, supplierName);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("RejectExtendOffersValidityBySupplier/{extendOffersValidtyId}")]
        public async Task RejectExtendOffersValidityBySupplier(int extendOffersValidtyId)
        {
            string userCr = User.SupplierNumber();
            string SupplierName = User.SupplierName();
            await _communicationAppService.RejectExtendOffersValidityBySupplier(extendOffersValidtyId, userCr, SupplierName);
        }
        [HttpPost]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("CancelSupplierExtendOfferValidity/{extendOffersValidtyId}")]
        public async Task CancelSupplierExtendOfferValidity(int extendOffersValidtyId)
        {
            string userCr = User.SupplierNumber();
            await _communicationAppService.CancelSupplierExtendOfferValidity(extendOffersValidtyId, userCr);
        }
        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("SendToApproveExtendOffersRequestAsync/{AgencyRequestId}")]
        public async Task SendToApproveExtendOffersRequestAsync(int AgencyRequestId)
        {
            await _communicationAppService.SendToApproveExtendOffersRequest(AgencyRequestId);
        }

        [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpPost]
        [Route("RejectExtendOffersRequestAsync")]
        public async Task RejectExtendOffersRequestAsync([FromBody] RejectNegotiation model)
        {
            await _communicationAppService.RejectExtendOffersRequest(model.NegotiationId, model.RejectionReason);
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
        [HttpPost]
        [Route("DeleteExtendOffersRequestAsync/{AgencyRequestId}")]
        public async Task DeleteExtendOffersRequestAsync(int AgencyRequestId)
        {
            await _communicationAppService.DeleteExtendOffersRequestAsync(AgencyRequestId);
        }

        //[Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager+","+ RoleNames.OffersPurchaseSecretary)]
        [Authorize(RoleNames.ApproveExtendOffersRequestPolicy)]
        [HttpPost]
        [Route("ApproveExtendOffersRequestAsync/{tenderIdString}/{AgencyRequestId}/{verificationCode}")]
        public async Task ApproveExtendOffersRequestAsync(int tenderIdString, int AgencyRequestId, string verificationCode)
        {
            int typeId = (int)Enums.VerificationType.AgencyCommunication;
            bool check = await _verification.CheckForVerificationCode(tenderIdString, verificationCode, typeId);
            await _communicationAppService.ApproveExtendOffersRequestAsync(AgencyRequestId);
        }
        #endregion

        #region[Negotiation]


        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet]
        [Route("GetSupplierNegotiation/{TenderIdString}/{NegotiationIdString}")]
        public async Task<NegotiationSupplierViewModel> GetSupplierNegotiation(string TenderIdString, string NegotiationIdString)
        {
            NegotiationSupplierViewModel dataModel = await _negotiationAppService.GetSupplierNegotiationPageInfo(TenderIdString, NegotiationIdString, User.SupplierNumber());
            return dataModel;
        }

        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet]
        [Route("GetSupplierNegotiationFirstStageInfo/{TenderIdString}/{NegotiationIdString}")]
        public async Task<NegotiationSupplierViewModel> GetSupplierNegotiationFirstStageInfo(string TenderIdString, string NegotiationIdString)
        {
            NegotiationSupplierViewModel dataModel = await _negotiationAppService.GetSupplierNegotiationFirstStageInfo(TenderIdString, NegotiationIdString, User.SupplierNumber());
            return dataModel;
        }

        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet]
        [Route("CheckIfHasPendingRequests")]
        public async Task<List<PendingAgencyRequestAlarm>> CheckIfHasPendingRequests()
        {
            List<PendingAgencyRequestAlarm> result = await _communicationAppService.GetPendingRequests(User.SupplierNumber());
            return result;
        }
        [HttpGet]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("AgreeOnOfferNegotiationFirstStage/{TenderIdString}/{NegotiationIdString}")]
        public async Task<bool> AgreeOnOfferNegotiationFirstStage(string TenderIdString, string NegotiationIdString)
        {
            await _negotiationAppService.AgreeOnOfferNegotiationFirstStage(NegotiationIdString, TenderIdString, User.SupplierNumber(), User.SupplierName());
            return true;
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("AgreeOnFirstStageNegotiationBySupplier/{TenderIdString}/{NegotiationIdString}")]
        public async Task<bool> AgreeOnFirstStageNegotiationBySupplier(string TenderIdString, string NegotiationIdString)
        {
            await _negotiationAppService.AgreeOnFirstStageNegotiationBySupplier(NegotiationIdString, TenderIdString, User.SupplierNumber(), User.SupplierName());
            return true;
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("AgreeWithExtraDiscountOnNegotiationFirstStage")]
        public async Task<bool> AgreeWithExtraDiscountOnNegotiationFirstStage([FromBody] AcceptNegotiationWithExtraDiscountModel extraDiscountModel)
        {
            await _negotiationAppService.AgreeOnNegotiationFirstStageWithExtraDiscount(extraDiscountModel);
            return true;
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("DisAgreeOfferAfterNegotiationFirstStage/{TenderIdString}/{NegotiationIdString}")]
        public async Task<bool> DisAgreeOfferAfterNegotiationFirstStage(string TenderIdString, string NegotiationIdString)
        {
            await _negotiationAppService.DisAgreeOfferAfterNegotiationFirstStage(NegotiationIdString, TenderIdString, User.SupplierNumber());
            return true;
        }

        // [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        [HttpGet]
        [Route("GetNegotiationData/{TenderIdString}/{NegotiationIdString}")]
        public async Task<NegotiationAgencyPageModel> GetNegotiationData(string TenderIdString, string NegotiationIdString)
        {
            NegotiationAgencyPageModel dataModel = await _communicationAppService.GetNegotiationPageData(Util.Decrypt(NegotiationIdString), Util.Decrypt(TenderIdString));

            return dataModel;
        }

        [HttpGet]
        [Route("GetNegotiationDataFirstStage/{TenderIdString}/{NegotiationIdString}")]
        public async Task<NegotiationAgencyPageModel> GetNegotiationDataFirstStage(string TenderIdString, string NegotiationIdString)
        {
            NegotiationAgencyPageModel dataModel = await _communicationAppService.GetNegotiationDataFirstStage(Util.Decrypt(NegotiationIdString), Util.Decrypt(TenderIdString));

            return dataModel;
        }
        [HttpGet]
        [Route("isAllowedToApplySecondStageNegotiation/{TenderIdString}")]
        public async Task<bool> isAllowedToApplySecondStageNegotiation(string TenderIdString)
        {
            bool dataModel = await _communicationAppService.isAllowedToApplySecondStageNegotiation(Util.Decrypt(TenderIdString));

            return dataModel;
        }
        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager)]
        [HttpGet]
        [Route("PreviewFirstStageNegotiationOffers/{TenderId}/{Discount}")]
        public async Task<List<NegotiationOfferModel>> PreviewFirstStageNegotiationOffers(string TenderId, decimal Discount)
        {


            var result = await _communicationAppService.PreviewOfferListDiscount(TenderId, Discount);
            return result;


        }



        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpGet]
        [Route("GetOffersListForFirstStageNegotiation")]
        public async Task<QueryResult<NegotiationOfferModel>> GetOffersListForFirstStageNegotiation([FromHeader] NegotiationOffersSearchModel negotiationOffersSearchModel)
        {


            var result = await _communicationAppService.GetOffersListForFirstStageNegotiation(negotiationOffersSearchModel);
            return result;


        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpGet]
        [Route("GetOffersForFirstStageNegotiation")]
        public async Task<QueryResult<NegotiationOfferModel>> GetOffersForFirstStageNegotiation([FromHeader] NegotiationOffersSearchModel negotiationOffersSearchModel)
        {
            var result = await _communicationAppService.GetOffersForFirstStageNegotiation(negotiationOffersSearchModel);
            return result;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpGet]
        [Route("GetNotNegotitedOffersForFirstStageNegotiation")]
        public async Task<QueryResult<NegotiationOfferModel>> GetNotNegotitedOffersForFirstStageNegotiation([FromHeader] NegotiationOffersSearchModel negotiationOffersSearchModel)
        {
            var result = await _communicationAppService.GetNotNegotitedOffersForFirstStageNegotiation(negotiationOffersSearchModel);
            return result;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("PreviewFirstStageNegotiationOffers")]
        public async Task<List<NegotiationOfferModel>> PreviewFirstStageNegotiationOffers([FromBody] NegotiationAgencyFirstStageEditModel negotiationModel)
        {


            var result = await _communicationAppService.PreviewOfferListDiscount(negotiationModel);
            return result;


        }



        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpGet]
        [Route("GetNegotiationFirstStageDatabyId/{NegotiationId}")]
        public async Task<NegotiationAgencyFirstStageEditModel> GetNegotiationFirstStageDatabyId(string NegotiationId)
        {


            return await _communicationAppService.GetNegotiationFirstStageDatabyId(NegotiationId);



        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("CreateNegotiationRequest")]
        public async Task<string> CreateNegotiationRequest([FromBody] StartNegotiationModel Model)
        {
            var result = await _communicationAppService.CreateNegotiationRequest(Model);
            return result;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.supplier + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser)]
        [HttpGet]
        [Route("GetSecondNegotiation/{NegotiationIdString}")]
        public async Task<SecondStageNegotiationModelcs> GetSecondNegotiation(string NegotiationIdString)
        {
            int NegotiationId = Util.Decrypt(NegotiationIdString);
            SecondStageNegotiationModelcs model = await _communicationAppService.GetSecondNegotiation(NegotiationId);
            return model;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.supplier + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser)]
        [HttpGet]
        [Route("GetSecondNegotiation_NEW/{NegotiationIdString}")]
        public async Task<SecondStageNegotiationModelcs> GetSecondNegotiation_NEW(string NegotiationIdString)
        {
            int NegotiationId = Util.Decrypt(NegotiationIdString);
            SecondStageNegotiationModelcs model = await _communicationAppService.GetSecondNegotiation_NEW(NegotiationId);
            return model;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.supplier + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
        [HttpGet]
        [Route("GetSecondStageNegotiation/{NegotiationIdString}")]
        public async Task<SecondStageNegotiationModelcs> GetSecondStageNegotiation(string NegotiationIdString)
        {
            int NegotiationId = Util.Decrypt(NegotiationIdString);
            SecondStageNegotiationModelcs model = await _communicationAppService.GetSecondStageNegotiation(NegotiationId);
            return model;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("CreateFirstStageNegotiation")]
        public async Task<bool> CreateFirstStageNegotiation([FromBody] NegotiationAgencyFirstStageEditModel negotiationModel)
        {
            await _communicationAppService.CreateFirstStageNegotiation(negotiationModel);
            return true;


        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("UpdateFirstStageNegotiation")]
        public async Task<bool> UpdateFirstStageNegotiation([FromBody] NegotiationAgencyFirstStageEditModel negotiationModel)
        {
            await _communicationAppService.UpdateFirstStageNegotiation(negotiationModel);
            return true;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("UpdateFirstStageNegotiationData")]
        public async Task<bool> UpdateFirstStageNegotiationData([FromBody] CreateNegotiationFirstStageDataModel negotiationModel)
        {
            await _communicationAppService.UpdateFirstStageNegotiationData(negotiationModel);
            return true;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("SendSecondFirstStageNegotiationToApprove")]
        public async Task<bool> SendSecondFirstStageNegotiationToApprove([FromBody] CreateNegotiationFirstStageDataModel negotiationModel)
        {
            await _communicationAppService.SendSecondFirstStageNegotiationToApprove(negotiationModel);
            return true;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("UpdateFirstStageNegotiationR")]
        public async Task<bool> UpdateFirstStageNegotiationR([FromBody] NegotiationAgencyFirstStageEditModel negotiationModel)
        {
            await _communicationAppService.UpdateFirstStageNegotiation(negotiationModel);
            return true;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("SaveSecondStageNegotiation")]
        public async Task<AjaxResponse<string>> SaveSecondStageNegotaion([FromBody] NegotiationAgencySecondStageEditModel model)
        {
            AjaxResponse<string> response = new AjaxResponse<string>();
            bool Result = await _communicationAppService.SaveSecondStageNegotaion(new SecondStageNegotiationModel { NegotiationId = model.NegotiationId, NegotiationReasonId = model.NegotiationReasonId, TenderIdString = model.TenderIdString }, model.NegotiationQuantityTableModels, model.IsSend);
            if (Result)
            {
                response.enMessageType = Enums.enAjaxResponseMessageType.success;
                response.Message = Resources.SharedResources.Messages.DataSaved;
            }
            else
            {
                response.enMessageType = Enums.enAjaxResponseMessageType.danger;
                response.Message = Resources.CommunicationRequest.ErrorMessages.SupplierOrederMustbeTheSameBeforeandAfterEdit;
            }
            return response;
        }



        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpPost]
        [Route("ChangeNegotiationFirstStageStatus")]
        public async Task<bool> ChangeNegotiationFirstStageStatus([FromBody] NegotiationAgencyActionStatusModel negotiationActionStatusModel)
        {
            await _negotiationAppService.ChangeCommunicationRequestStatus(negotiationActionStatusModel);
            return true;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpPost]
        [Route("ApproveNegotiationRequestFirstStage")]
        public async Task<bool> ApproveNegotiationRequestFirstStage([FromBody] NegotiationAgencyActionStatusModel negotiationActionStatusModel)
        {
            await _negotiationAppService.ApproveNegotiationRequestFirstStage(negotiationActionStatusModel);
            return true;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpPost]
        [Route("RejectNegotiationRequestFirstStage")]
        public async Task<bool> RejectNegotiationRequestFirstStage([FromBody] NegotiationAgencyActionStatusModel negotiationActionStatusModel)
        {
            await _negotiationAppService.RejectNegotiationRequestFirstStage(negotiationActionStatusModel);
            return true;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("SendToApproveNegotiationSecondStageByCheckManagerAsync/{NegotiationId}/{Days}/{Hours}")]
        public async Task SendToApproveNegotiationSecondStageByCheckManagerAsync(int NegotiationId, int Days, int Hours)
        {
            await _negotiationAppService.SendToApproveNegotiationSecondStageByCheckManagerAsync(NegotiationId, Days, Hours);
        }

        [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost]
        [Route("ApproveNegotiationSecondStageByCheckManagerAsync/{NegotiationId}/{verficationCode}/{Days}/{Hours}")]
        public async Task ApproveNegotiationSecondStageByCheckManagerAsync(int NegotiationId, string verficationCode, int Days, int Hours)
        {
            await _negotiationAppService.ApproveNegotiationSecondStageByCheckManagerAsync(NegotiationId, verficationCode, Days, Hours);
        }

        [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpPost]
        [Route("RejectNegotiationSecondStageByCheckManagerAsync")]
        public async Task RejectNegotiationSecondStageByCheckManagerAsync([FromBody] RejectNegotiation rejectNegotiation)
        {
            int NegotiationId = rejectNegotiation.NegotiationId;
            string RejectionReason = rejectNegotiation.RejectionReason;
            await _negotiationAppService.RejectNegotiationSecondStageByCheckManagerAsync(NegotiationId, RejectionReason);
        }

        [Authorize(Roles = RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
        [HttpPost]
        [Route("ApproveNegotiationSecondStageByUnitSecretaryAsync/{NegotiationId}/{verficationCode}")]
        public async Task ApproveNegotiationSecondStageByUnitSecretaryAsync(int NegotiationId, string verficationCode)
        {
            await _negotiationAppService.ApproveNegotiationSecondStageByUnitSecretaryAsync(NegotiationId, verficationCode);
        }

        #region Negotiation second stage
        [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckSecretary)]
        [HttpPost]
        [Route("UpdateNegotiationQuantityTableItem")]
        public async Task<NegotiationQuantityTableItemModel> UpdateNegotiationQuantityTableItem([FromBody] NegotiationQuantityTableItemModel negotiationQuantityTableItemModel)
        {
            negotiationQuantityTableItemModel = await _negotiationAppService.UpdateNegotiationQuantityTableItem(negotiationQuantityTableItemModel);
            return negotiationQuantityTableItemModel;
        }

        [HttpGet]
        [Route("DeleteNegotiationTable/{tableId}")]
        public async Task<AjaxResponse<OffersCompareViewModel>> DeleteNegotiationTable(string tableId)
        {
            return await _negotiationAppService.DeleteNegotiationTenderQuantityTable(int.Parse(tableId));
        }

        [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckSecretary)]
        [HttpGet]
        [Route("DeleteNegotiationQuantityItem/{QuantityItemId}")]
        public async Task<NegotiationOffersModel> DeleteNegotiationQuantityItem(string QuantityItemId)
        {
            await _negotiationAppService.DeleteNegotiationQuantityItem(QuantityItemId);
            return new NegotiationOffersModel();
        }

        [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckSecretary)]
        [HttpPost]
        [Route("UpdateNegotiationQuantityItem")]
        public async Task<NegotiationOffersModel> UpdateNegotiationQuantityItem(string QuantityItemId, int QTY)
        {
            await _negotiationAppService.UpdateNegotiationQuantityItem(QuantityItemId, QTY);
            return new NegotiationOffersModel();
        }


        [HttpGet]
        [Route("GetNegotiationTableQuantityTables/{negotiationId}")]
        public async Task<OfferQuantityTableHtmlVM> GetNegotiationTableQuantityTables(int negotiationId)
        {
            OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM();
            offerQuantityTableHtmlVM = await _negotiationAppService.GetNegotiationQuantityTables(negotiationId);
            return offerQuantityTableHtmlVM;
        }



        #endregion Negotiation second stage


        [Authorize(Roles = RoleNames.UnitSecretaryUser + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
        [HttpPost]
        [Route("RejectNegotiationSecondStageByUnitSecretaryAsync")]
        public async Task RejectNegotiationSecondStageByUnitSecretaryAsync([FromBody] RejectNegotiation rejectNegotiation)
        {
            int NegotiationId = rejectNegotiation.NegotiationId;
            string RejectionReason = rejectNegotiation.RejectionReason;
            await _negotiationAppService.RejectNegotiationSecondStageByUnitSecretaryAsync(NegotiationId, RejectionReason);
        }

        [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckSecretary)]
        [HttpPost]
        [Route("EditNegotiationInfoAsync/{NegotiationId}")]
        public async Task EditNegotiationInfoAsync(int NegotiationId)
        {
            await _negotiationAppService.EditNegotiationInfoAsync(NegotiationId);
        }

        [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckSecretary)]
        [HttpPost]
        [Route("FinishNegotiationAsync/{NegotiationId}")]
        public async Task FinishNegotiationAsync(int NegotiationId)
        {
            await _negotiationAppService.FinishNegotiationAsync(NegotiationId);
        }

        [Authorize(Roles = RoleNames.supplier)]
        [HttpPost]
        [Route("ApproveNegotiationSecondStageBySupplierAsync/{NegotiationId}")]
        public async Task<String> ApproveNegotiationSecondStageBySupplierAsync(int NegotiationId)
        {
            return await _negotiationAppService.ApproveNegotiationSecondStageBySupplierAsync(NegotiationId);
        }

        [Authorize(Roles = RoleNames.supplier)]
        [HttpPost]
        [Route("RejectNegotiationSecondStageBySupplierAsync/{NegotiationId}")]
        public async Task RejectNegotiationSecondStageBySupplierAsync(int NegotiationId)
        {
            await _negotiationAppService.RejectNegotiationSecondStageBySupplierAsync(NegotiationId);
        }

        //[Authorize( RoleNames.OffersPurchaseSecretary + ","  + RoleNames.OffersCheckSecretaryPolicy)]

        [HttpGet]
        [Route("ReopenSecondNegotiation/{negotiationId}")]
        public async Task<bool> ReopenSecondNegotiation(string negotiationId)
        {
            await _negotiationAppService.ReopenNegotiationSecondStageAsync(Util.Decrypt(negotiationId));
            return true;
        }

        [Route("ResetSecondNegotiation/{negotiationId}")]
        public async Task<bool> ResetSecondNegotiation(string negotiationId)
        {
            await _negotiationAppService.ResetSecondNegotiation(Util.Decrypt(negotiationId));
            return true;
        }
        [HttpGet]
        [Route("GetQuantityTables/{offerId}")]
        public async Task<OfferQuantityTableHtmlVM> GetQuantityTables(int offerId)
        {
            OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM();
            return offerQuantityTableHtmlVM;
        }



        #endregion

        #region Plaint

        [Authorize(Roles = RoleNames.supplier)]
        [HttpPost("AddPlaintRequest")]
        public async Task<TenderPlaintCommunicationRequestModel> AddPlaintRequest([FromBody] TenderPlaintCommunicationRequestModel model)
        {
            TenderPlaintCommunicationRequestModel response = await _communicationAppService.CreateCommunicationRequest(model, User.SupplierNumber());
            return response;
        }

        [HttpGet("FindTenderForPlaintRequestById/{tenderId}")]
        [Authorize(RoleNames.PlaintRequestDataPolicy)]
        public async Task<TenderPlaintDatailsModel> FindTenderForPlaintRequestById(string tenderId)
        {
            TenderPlaintDatailsModel model = await _communicationAppService.FindTenderForPlaintRequestById(Util.Decrypt(tenderId), User.SupplierNumber());
            return model;
        }
        [HttpGet("FindTenderForEscalationRequestById/{tenderId}")]
        [Authorize(RoleNames.PlaintRequestDataPolicy)]
        public async Task<TenderPlaintDatailsModel> FindTenderForEscalationRequestById(string tenderId)
        {
            TenderPlaintDatailsModel model = await _communicationAppService.FindTenderForEscalationRequestById(Util.Decrypt(tenderId), User.SupplierNumber());
            return model;
        }
        [HttpGet("FindSupplierPlaintRequestByTenderIdAndCR/{tenderId}")]
        [Authorize(Roles = RoleNames.supplier)]
        public async Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintRequestByTenderIdAndCR(string tenderId)
        {
            TenderPlaintCommunicationRequestModel model = await _communicationAppService.FindSupplierPlaintRequestByTenderIdAndCR(Util.Decrypt(tenderId), User.SupplierNumber());
            return model;
        }

        [HttpGet("FindPlaintRequestById/{plaintId}")]
        [Authorize(RoleNames.PlaintRequestDataPolicy)]
        public async Task<PlaintRequestModel> FindPlaintRequestById(string plaintId)
        {
            PlaintRequestModel model = await _communicationAppService.FindPlaintRequestModelById(Util.Decrypt(plaintId), User.UserAgency(), User.SelectedCommittee());
            return model;
        }

        [HttpGet("FindEscalatedPlaintRequestById/{plaintId}")]
        [Authorize(RoleNames.PlaintRequestDataPolicy)]
        public async Task<PlaintRequestModel> FindEscalatedPlaintRequestById(string plaintId)
        {
            PlaintRequestModel model = await _communicationAppService.FindEscalationRequestModelById(Util.Decrypt(plaintId));
            return model;
        }
        [HttpGet("GetEscalatedTenders")]
        [Authorize(RoleNames.EscalatedTendersIndexPolicy)]
        public async Task<QueryResult<EscalatedTendersModel>> GetEscalatedTenders(EscalationSearchCriteria searchCriteria)
        {
            QueryResult<EscalatedTendersModel> model = await _communicationAppService.GetEscalatedTenders(searchCriteria);
            return model;
        }
        [HttpGet("FindTenderPlaintRequestsByTenderId")]
        [Authorize(RoleNames.PlaintRequestDataPolicy)]
        public async Task<QueryResult<TenderPlaintCheckingModel>> FindTenderPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria)
        {
            QueryResult<TenderPlaintCheckingModel> model = await _communicationAppService.FindTenderPlaintRequestsByTenderId(plaintSearchCriteria);
            return model;
        }
        [HttpGet("FindTenderEscalatedPlaintRequestsByTenderId")]
        [Authorize(RoleNames.EscalatedTendersIndexPolicy)]
        public async Task<QueryResult<TenderPlaintCheckingModel>> FindTenderEscalatedPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria)
        {
            QueryResult<TenderPlaintCheckingModel> model = await _communicationAppService.FindTenderEscalatedPlaintRequestsByTenderId(plaintSearchCriteria);
            return model;
        }
        [HttpGet("FindTenderPlaintCommunicationByTenderId/{agencyRequestId}")]
        [Authorize(RoleNames.CheckPlaintDataPolicy)]
        public async Task<TenderPLaintCommunicationModel> FindTenderPlaintCommunicationByTenderId(int agencyRequestId)
        {
            TenderPLaintCommunicationModel model = await _communicationAppService.FindAgencyCommunicationPalintRequestsByTenderId(agencyRequestId, User.UserAgency(), User.SelectedCommittee());
            return model;
        }

        [HttpGet("FindTenderPlaintEscalationsByTenderId/{requestId}")]
        [Authorize(RoleNames.PlaintRequestDataPolicy)]
        public async Task<TenderEscalatedPLaintModel> FindTenderPlaintEscalationsByTenderId(int requestId)
        {
            TenderEscalatedPLaintModel model = await _communicationAppService.FindAgencyCommunicationEscalatedPalintsByTenderId(requestId);
            return model;
        }
        #endregion

        #region Extend offer presentation dates request
        [HttpPost("CreateSupplierExtendOfferDatesRequest")]
        [Authorize(Roles = RoleNames.supplier)]
        public async Task CreateSupplierExtendOfferDatesRequest([FromBody] ExtendOffersDateRequestModel extendOffersDateRequestModel)
        {
            extendOffersDateRequestModel.Cr = User.SupplierNumber();
            extendOffersDateRequestModel.TenderId = Util.Decrypt(extendOffersDateRequestModel.TenderIdString);
            await _communicationAppService.CreateSupplierExtendOfferDatesAgencyRequest(extendOffersDateRequestModel);
        }

        [HttpPost("ApproveSupplierExtendOfferDatesRequest/{SupplierExtendDatesAgencyCommunicationRequestId}")]
        [Authorize(RoleNames.ApproveSupplierExtendOfferDatesPolicy)]
        public async Task<SupplierExtendOfferDatesAgencyRequestModel> ApproveSupplierExtendOfferDatesRequest(int SupplierExtendDatesAgencyCommunicationRequestId)
        {
            var res = await _communicationAppService.ApproveSupplierExtendOfferDatesRequest(SupplierExtendDatesAgencyCommunicationRequestId);
            return await FindSupplierExtendOfferDatesAgencyRequestById(res.AgencyRequestId);
        }

        [HttpPost("RejectSupplierExtendOfferDatesRequest/{SupplierExtendDatesAgencyCommunicationRequestId}")]
        [Authorize(RoleNames.ApproveSupplierExtendOfferDatesPolicy)]
        public async Task RejectSupplierExtendOfferDatesRequest(int SupplierExtendDatesAgencyCommunicationRequestId)
        {
            await _communicationAppService.RejectSupplierExtendOfferDatesRequest(SupplierExtendDatesAgencyCommunicationRequestId);
        }

        [Authorize(RoleNames.SupplierExtendOfferDatesPolicy)]
        [HttpGet]
        [Route("FindSupplierExtendOfferDatesAgencyRequestById/{agencyRequestId}")]
        public async Task<SupplierExtendOfferDatesAgencyRequestModel> FindSupplierExtendOfferDatesAgencyRequestById(int agencyRequestId)
        {
            return await _communicationAppService.FindSupplierExtendOfferDatesAgencyRequestRequestById(agencyRequestId);
        }

        [HttpGet("FindSupplierExtendOfferDatesAgencyRequestsById/{agencyRequestId}")]
        [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.supplier)]
        public async Task<List<SupplierExtendOfferDatesAgencyRequestModel>> FindSupplierExtendOfferDatesAgencyRequestsById(int agencyRequestId)
        {
            return await _communicationAppService.FindSupplierExtendOfferDatesRequestsByTenderId(agencyRequestId);
        }

        [HttpGet]
        [Route("GetTenderExtendDatesByTenderId/{id}/{agencyRequestId}")]
        public async Task<ExtendOfferPresentationDatesModel> GetTenderExtendDatesByTenderId(int id, int agencyRequestId)
        {
            ExtendOfferPresentationDatesModel tenderDatesModel = await _communicationAppService.FindTenderExtendDatesByTenderId(id, agencyRequestId);
            tenderDatesModel.AgencyRequestIdString = Util.Encrypt(tenderDatesModel.AgencyRequestId);
            if (Check.IsNull(tenderDatesModel))
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.FillTenderBasicInformation);
            }
            if (tenderDatesModel.AgencyId != User.UserAgency())
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            return tenderDatesModel;
        }

        [Authorize(RoleNames.SupplierExtendOfferDatesPolicy)]
        [HttpPost("UpdateTenderExtendDates")]
        public async Task UpdateTenderExtendDates([FromBody] ExtendOfferPresentationDatesModel tenderDatesModel)
        {
            var userRole = User.UserRole();
            await (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PreQualification || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PostQualification
                ? _communicationAppService.UpdateQualificationExtendDates(tenderDatesModel, userRole, User.UserAgency())
                : _communicationAppService.UpdateTenderExtendDates(tenderDatesModel, userRole, User.UserAgency()));
        }



        [Authorize(RoleNames.ApprovePlaintDataPolicy)]
        [HttpPost]
        [Route("RejectPlaintCommunicationRequest/{requestId}/{rejectionReason}")]
        public async Task RejectPlaintCommunicationRequest(string requestId, string rejectionReason)
        {
            await _communicationAppService.RejectPlaintCommunicationRequest(Util.Decrypt(requestId), rejectionReason, User.UserAgency(), User.SelectedCommittee());
        }
        [Authorize(RoleNames.ApprovePlaintDataPolicy)]
        [HttpPost]
        [Route("ApprovePlaintCommunicationRequest/{requestId}/{verficationCode}")]
        public async Task ApprovePlaintCommunicationRequest(string requestId, string verficationCode)
        {
            int typeId = (int)Enums.VerificationType.AgencyCommunication;
            await _communicationAppService.ApprovePlaintCommunicationRequest(Util.Decrypt(requestId), verficationCode, User.UserAgency(), User.SelectedCommittee(), typeId);
        }
        [Authorize(RoleNames.CheckPlaintDataPolicy)]
        [HttpPost]
        [Route("RejectPlaint/{CommunicationRequestId}/{rejectionReason}")]
        public async Task RejectPlaint(string CommunicationRequestId, string rejectionReason)
        {
            await _communicationAppService.RejectPlaint(CommunicationRequestId, rejectionReason, User.UserAgency(), User.SelectedCommittee(), false);
        }
        [Authorize(RoleNames.CheckPlaintDataPolicy)]
        [HttpPost]
        [Route("AcceptPlaint/{communicationRequestId}/{procedureId}/{details?}")]
        public async Task AcceptPlaint(string communicationRequestId, int procedureId, string details)
        {
            await _communicationAppService.AcceptPlaint(communicationRequestId, procedureId, details, User.UserAgency(), false, User.SelectedCommittee());
        }

        [Authorize(RoleNames.CreateVerificationCodePolicy)]
        [HttpPost]
        [Route("CreateVerificationCode")]
        public async Task CreateVerificationCode([FromBody] BasicTenderModel basicTender)
        {
            var userEmail = User.Email();
            var userMobile = User.Mobile();
            await _verification.CreateVerificationCode(basicTender.TenderId, userMobile, userEmail, User.UserId(), (int)Enums.VerificationType.AgencyCommunication);
        }
        [Authorize(RoleNames.CreateVerificationCodePolicy)]
        [HttpPost]
        [Route("CreateNegotiationVerificationCode")]
        public async Task CreateNegotiationVerificationCode([FromBody] BasicTenderModel basicTender)
        {
            var userEmail = User.Email();
            var userMobile = User.Mobile();
            await _verification.CreateVerificationCode(basicTender.TenderId, userMobile, userEmail, User.UserId(), (int)Enums.VerificationType.Negotiation);
        }
        [Authorize(Roles = RoleNames.SecretaryGrievanceCommittee)]
        [HttpPost]
        [Route("AcceptEscalationCommunicationRequest/{communicationRequestId}/{procedureId}/{details?}")]
        public async Task AcceptEscalationCommunicationRequest(int communicationRequestId, int procedureId, string details)
        {
            await _communicationAppService.AcceptEscalationCommunicationRequest(communicationRequestId, procedureId, details);
        }
        [Authorize(Roles = RoleNames.ManagerGrievanceCommittee)]
        [HttpPost]
        [Route("ApproveEscalationCommunicationRequest/{requestId}/{verficationCode}")]
        public async Task ApproveEscalationCommunicationRequest(int requestId, string verficationCode)
        {
            int typeId = (int)Enums.VerificationType.AgencyCommunication;
            await _communicationAppService.ApproveEscalationCommunicationRequest(requestId, verficationCode, User.UserAgency(), User.UserId(), typeId);
        }
        [Authorize(Roles = RoleNames.ManagerGrievanceCommittee)]
        [HttpPost]
        [Route("RejectEscalationCommunicationRequestApproval/{requestId}/{rejectionReason}")]
        public async Task RejectEscalationCommunicationRequestApproval(int requestId, string rejectionReason)
        {
            await _communicationAppService.RejectEscalationCommunicationRequestApproval(requestId, rejectionReason);
        }
        [Authorize(Roles = RoleNames.SecretaryGrievanceCommittee)]
        [HttpPost]
        [Route("RejectEscalationCommunicationRequest/{requestId}/{rejectionReason}")]
        public async Task RejectEscalationCommunicationRequest(int requestId, string rejectionReason)
        {
            await _communicationAppService.RejectEscalationCommunicationRequest(requestId, rejectionReason);
        }
        [Authorize(Roles = RoleNames.supplier)]
        [HttpPost]
        [Route("EscalatePlaintCommunicationRequest/{plaintId}/{attachmentId}/{attachmentName}")]
        public async Task EscalatePlaintCommunicationRequest(string plaintId, string attachmentId, string attachmentName)
        {
            await _communicationAppService.EscalatePlaint(plaintId, attachmentId, attachmentName, User.SupplierNumber());
        }

        [HttpGet("FindSupplierPlaintDetailsPlaintId/{agencyRequestIdString}")]
        [Authorize(Roles = RoleNames.supplier)]
        public async Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintDetailsPlaintId(int agencyRequestIdString)
        {
            TenderPlaintCommunicationRequestModel model = await _communicationAppService.FindSupplierPlaintDetailsByPlaintIdAndCR(agencyRequestIdString, User.SupplierNumber());
            return model;
        }
        [Authorize(RoleNames.CheckPlaintDataPolicy)]
        [HttpPost("SavePlaintNotes")]
        public async Task<PlaintNotesModel> SavePlaintNotes([FromBody] PlaintNotesModel model)
        {
            PlaintNotesModel response = await _communicationAppService.SavePlaintNotes(model, User.UserAgency(), User.SelectedCommittee());
            return response;
        }

        #endregion Extend offer presentation dates request
        [HttpPost("SaveEscalationNotes")]
        public async Task<PlaintRequestModel> SaveEscalationNotes([FromBody] PlaintRequestModel model)
        {
            PlaintRequestModel response = await _communicationAppService.SaveEscalationNotes(model, User.UserAgency());
            return response;
        }
        #region [NEGO]


        [HttpGet]
        [Route("GetNegotiationTableQuantityItems")]
        public async Task<QueryResult<TableModel>> GetNegotiationTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _communicationAppService.GetNegotiationTableQuantityItems(quantityTableSearchCretriaModel);
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpPost("SaveNegotiationQitems")]
        public async Task<AjaxResponse<OffersCompareViewModel>> SaveNegotiationQitems([FromBody] Dictionary<string, string> AuthorList)
        {

            return await _negotiationAppService.SaveNegotiationQitems(AuthorList);
        }
        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
        [HttpGet]
        [Route("DeleteNegotiationQitems_NEW/{tenderId}/{negotiationId}/{tableId}/{rowNumber}")]
        public async Task<AjaxResponse<OffersCompareViewModel>> DeleteNegotiationQitems_NEW(int tenderId, int negotiationId, int tableId, int rowNumber)
        {
            return await _communicationAppService.DeleteNegotiationQitems(tenderId, negotiationId, tableId, rowNumber);
        }
        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
        [HttpPost("SaveNegotiationQitems_NEW")]
        public async Task<AjaxResponse<OffersCompareViewModel>> SaveNegotiationQitems_NEW([FromBody] Dictionary<string, string> AuthorList)
        {

            return await _communicationAppService.SaveNegotiationQitems(AuthorList);
        }





        #endregion
    }
}