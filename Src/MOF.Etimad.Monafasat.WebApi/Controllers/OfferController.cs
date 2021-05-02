using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.SearchCriterias;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Invitation;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using MOF.Etimad.Monafasat.ViewModel.Reports;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class OfferController : BaseController
    {
        private readonly IOfferAppService _offerAppService;
        private readonly ITenderAppService _tenderAppService;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IMapper _mapper;
        private readonly ISupplierService supplierService;

        public OfferController(IOfferAppService offerAppService, ITenderAppService tenderAppService, IVerificationCodeService verificationCodeService, IMapper mapper,
            ISupplierService _supplierService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _offerAppService = offerAppService;
            _tenderAppService = tenderAppService;
            _verificationCodeService = verificationCodeService;
            _mapper = mapper;
            supplierService = _supplierService;
        }

        #region Query

        [HttpGet]
        [Route("GetOffer")]
        public OfferModel GetOffer()
        {

            var offersModel = new OfferModel();
            return offersModel;
        }

        [HttpGet]
        [Route("GetOffers")]
        public async Task<QueryResult<OfferModel>> GetOffers(OfferSearchCriteriaModel offerSearchCriteriaModel)
        {
            var offerSearchCriteria = _mapper.Map<OfferSearchCriteria>(offerSearchCriteriaModel);
            var offers = await _offerAppService.Find(offerSearchCriteria);
            var offersModel = _mapper.Map<QueryResult<OfferModel>>(offers);
            return offersModel;
        }
        [HttpGet]
        [Route("GetOfferDetailsById/{tenderId}/{offerId}")]
        public async Task<OfferModel> GetOfferDetailsById(int tenderId, int offerId)
        {
            OfferModel offer = await _offerAppService.GeOfferByTenderIdAndOfferIdForChecking(offerId);
            return offer;
        }

        [HttpGet]
        [Route("GetOfferDetailsById/{offerId}")]
        public async Task<OfferCheckingDetailsModel> GetOfferDetailsById(int offerId)
        {
            OfferCheckingDetailsModel offer = await _offerAppService.FindOfferDetailsById(offerId);
            return offer;
        }
        [HttpGet]
        [Route("GetOfferById/{offerId}")]
        public async Task<OfferModel> GetOfferById(int offerId)
        {
            Offer offer = await _offerAppService.FindById(offerId);
            var _offer = _mapper.Map<OfferModel>(offer);
            return _offer;
        }
        #region QuantityTables     

        //[HttpGet]
        //[Route("GetQuantityTables")]
        //public async Task<QueryResult<SupplierQuantityTableModel>> GetQuantityTables(SupplierQuantityTableSearchCriteriaModel supplierQuantityTableSearchCriteriaModel)
        //{
        //    var supplierQuantityTableSearchCriteria = _mapper.Map<SupplierQuantityTableSearchCriteria>(supplierQuantityTableSearchCriteriaModel);
        //    var quantityTables = await _offerAppService.Find(supplierQuantityTableSearchCriteria);
        //    var quantityTablesModel = _mapper.Map<QueryResult<SupplierQuantityTableModel>>(quantityTables);
        //    return quantityTablesModel;
        //}

        //[HttpGet]
        //[Route("GetQuantityTableItems")]
        //public async Task<QueryResult<SupplierQuantityTableItemModel>> GetQuantityTableItems(SupplierQuantityTableItemSearchCriteriaModel supplierQuantityTableItemSearchCriteriaModel)
        //{
        //    var supplierQuantityTableItemSearchCriteria = _mapper.Map<SupplierQuantityTableItemsSearchCrateria>(supplierQuantityTableItemSearchCriteriaModel);
        //    var quantityTableItems = await _offerAppService.Find(supplierQuantityTableItemSearchCriteria);
        //    var quantityTableItemsModel = _mapper.Map<QueryResult<SupplierQuantityTableItemModel>>(quantityTableItems);
        //    return quantityTableItemsModel;
        //}

        //[HttpGet]
        //[Route("GetQuantityTableItemById/{id}")]
        //public async Task<SupplierQuantityTableItemModel> GetQuantityTableItemById(int id)
        //{
        //    SupplierQuantityTableItem supplierQuantityTableItem = await _offerAppService.FindQuantityTableItemById(id);
        //    var _supplierQuantityTableItem = _mapper.Map<SupplierQuantityTableItemModel>(supplierQuantityTableItem);
        //    return _supplierQuantityTableItem;
        //}
        #region New Apply Offer
        [HttpGet]
        [Route("GetAllSuppliersBySearchCretriaForCombined")]
        public async Task<QueryResult<SupplierCombinedModelView>> GetAllSuppliersBySearchCretriaForCombined(SupplierSearchCretriaModel cretria)
        {
            var suppliers = await supplierService.GetAllSuppliersBySearchCretriaForCombined(cretria, User.SupplierNumber());
            return suppliers;
        }
        //[HttpPost]
        //[Route("SaveTable")]
        //public async Task<OfferModel> SaveTable([FromBody]SaveTableModel modle)
        //{
        //    var suplierCR = User.SupplierNumber();
        //    var offer = await _offerAppService.UpdateOffer(modle, suplierCR);
        //    OfferModel offerModel = await _offerAppService.GeOfferByTenderIdAndCR(offer.TenderId, suplierCR);
        //    return offerModel;
        //}
        //[Authorize(RoleNames.ApplyOffersPolicy)]
        //[HttpGet]
        //[Route("ApplyTenderOffer/{tenderId}/{OfferId?}")]
        //public async Task<OfferModel> ApplyTenderOffer(int tenderId, int OfferId)
        //{
        //    var suplierCR = User.SupplierNumber();
        //    OfferModel offer = await _offerAppService.GetOfferByTenderIdAndCRAndOfferId(tenderId, suplierCR, OfferId);
        //    return offer;
        //}

        [HttpGet("GetCombinedSuppliersByOfferId")]
        [Authorize(Roles = RoleNames.supplier)]
        public async Task<QueryResult<SupplierCombinedModelView>> GetCombinedSuppliersByOfferId(CombinedSearchCretriaModel model)
        {
            var result = await supplierService.GetCombinedSuppliersByOfferId(model);
            return result;
        }

        [HttpPost("AddSuppliertoCombinedList")]
        [Authorize(Roles = RoleNames.supplier)]
        public AjaxResponse<string> AddSuppliertoCombinedList([FromBody] CombinedSupplierInsertModel model)
        {
            return null;
        }

        #endregion

        //[Authorize(Roles = RoleNames.supplier + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager)]
        //[HttpGet]
        //[Route("GetQuantityTablesByTenderId/{tenderId}")]
        //public async Task<OfferModel> GetQuantityTablesByTenderId(int tenderId)
        //{
        //    var suplierCR = User.SupplierNumber();
        //    OfferModel offer = await _offerAppService.FindOfferByTenderIdAndCRForSupplier(tenderId, suplierCR);


        //    //if (offer.OfferId == 0)
        //    //{

        //    //    offer = await _offerAppService.AddOffer(offer);
        //    //    offer.Combined.RemoveAll(x => x is CombinedOwner);

        //    //}
        //    //    var _offer = _mapper.Map<OfferModel>(offer);


        //    return offer;
        //}

        //[Authorize(RoleNames.SuppliersReportPolicy)]
        //[HttpGet]
        //[Route("FindOfferTableValuesByOfferId/{offerId}")]
        //public async Task<OfferModel> FindOfferTableValuesByOfferId(int offerId)
        //{
        //    OfferModel _supplierQuantityTableItem = await _offerAppService.GeOfferByOfferId(offerId);
        //    return _supplierQuantityTableItem;
        //}
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost]
        [Route("WithdrawOffer/{offerId}")]
        public async Task<string> WithdrawOffer(int offerId)
        {
            string supplierCR = User.SupplierNumber();
            Offer offer = await _offerAppService.WithdrawOffer(offerId, supplierCR);
            return "";
        }
        [Authorize(RoleNames.ApplyOffersPolicy)]
        [HttpGet]
        [Route("GeOfferByTenderIdAndOfferIdForOpening/{tenderId}/{offerId}")]
        public async Task<OfferModel> GeOfferByTenderIdAndOfferIdForOpening(int tenderId, int offerId)
        {
            OfferModel _supplierQuantityTableItem = await _offerAppService.GeOfferByTenderIdAndOfferIdForOpening(tenderId, offerId);
            return _supplierQuantityTableItem;
        }

        [HttpGet]
        [Route("GetTenderStatus/{tenderId}")]
        public async Task<OfferDetailModel> GetTenderStatus(int tenderId)
        {
            return await _offerAppService.GetTenderStatus(tenderId);
        }


        //[Authorize(RoleNames.ApplyOffersPolicy)]
        //[HttpGet]
        //[Route("FindOfferTableValuesByTenderIdAndOfferId/{tenderId}/{offerId}")]
        //public async Task<OfferModel> FindOfferTableValuesByTenderIdAndOfferId(int tenderId, int offerId)
        //{
        //    OfferModel _supplierQuantityTableItem = await _offerAppService.GeOfferByTenderIdAndOfferId(tenderId, offerId);
        //    return _supplierQuantityTableItem;
        //}


        //[Authorize(RoleNames.ApplyOffersPolicy)]
        //[HttpGet]
        //[Route("GetQuantityTablesByTenderAndOffer/{tenderId}/{offerId}")]
        //public async Task<OfferModel> GetQuantityTablesByTenderAndOffer(int tenderId, int offerId)
        //{
        //    OfferModel _supplierQuantityTableItem = await _offerAppService.GeOfferByTenderIdAndOfferId(tenderId, offerId);
        //    return _supplierQuantityTableItem;
        //}


        //[Authorize(RoleNames.ApplyOffersPolicy)]
        [Route("GetOpenOfferData/{offerid}")]
        [HttpGet]
        public async Task<OpenOfferModel> GetOpenOfferData(int offerid)
        {
            int userId = User.UserId();
            OpenOfferModel openOfferModel = await _offerAppService.GetOpenOfferbyId(offerid, userId);
            return openOfferModel;
        }

        [Authorize(RoleNames.CheckTenderOffersPolicy)]
        [Route("OpenOffersDetailsForAwarding/{offerid}")]
        [HttpGet]
        public async Task<OpenOfferModel> OpenOffersDetailsForAwarding(int offerid)
        {
            int userId = User.UserId();
            OpenOfferModel openOfferModel = await _offerAppService.OpenOffersDetailsForAwarding(offerid, userId);
            return openOfferModel;
        }

        [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
        [Route("OfferFinancialDetails/{offerId}")]
        public async Task<OfferFinancialDetailsModel> OfferFinancialDetails(int offerId)
        {
            OfferFinancialDetailsModel supplierQuantityTableItem = await _offerAppService.OfferFinancialDetails(offerId);
            return supplierQuantityTableItem;
        }

        [Authorize(RoleNames.CheckTenderOffersPolicy)]
        [Route("GetOfferLocalContentDetails/{offerIdString}")]
        public async Task<OfferLocalContentDetailsModel> GetOfferLocalContentDetails(string offerIdString)
        {
            var offerId = Util.Decrypt(offerIdString);
            OfferLocalContentDetailsModel model = await _offerAppService.GetOfferLocalContentDetails(offerId);
            return model;
        }

        #endregion QuantityTables
        //[Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.MonafasatAdmin + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager)]
        [Authorize(RoleNames.SuppliersReportPolicy)]
        [HttpGet]
        [Route("GetSuppliersReportByTenderId/{tenderId}")]
        public async Task<QueryResult<AppliedSuppliersReportModel>> GetSuppliersReportByTenderId(int tenderId)
        {
            Enums.CommitteeType committeeType = Enums.CommitteeType.PurchaseCommittee;
            if (User.IsInRole(RoleNames.OffersOppeningSecretary) || User.IsInRole(RoleNames.OffersOppeningManager))
                committeeType = Enums.CommitteeType.OpenOfferCommittee;
            if (User.IsInRole(RoleNames.OffersCheckManager) || User.IsInRole(RoleNames.OffersCheckSecretary))
                committeeType = Enums.CommitteeType.CheckOfferCommittee;
            if (User.IsInRole(RoleNames.OffersOpeningAndCheckManager) || User.IsInRole(RoleNames.OffersOpeningAndCheckSecretary))
                committeeType = Enums.CommitteeType.VROCommittee;
            if (User.IsInRole(RoleNames.OffersPurchaseSecretary) || User.IsInRole(RoleNames.OffersPurchaseManager))
                committeeType = Enums.CommitteeType.PurchaseCommittee;
            var basicTenderModel = await _offerAppService.FindSuppliersReportByTenderId(tenderId, User.UserBranch(), User.UserAgency(), User.SelectedCommittee(), committeeType);
            return basicTenderModel;
        }
        [Authorize(RoleNames.SuppliersReportPolicy)]
        [HttpGet]
        [Route("GetSuppliersReportByTenderId__/{tenderIdString}/{pageNumber}")]
        public async Task<QueryResult<AppliedSuppliersReportDetailsModel>> GetSuppliersReportByTenderId__(string tenderIdString, int pageNumber)
        {
            var tenderId = Util.Decrypt(tenderIdString);
            var basicTenderModel = await _offerAppService.FindSuppliersReportByTenderId__(tenderId, pageNumber);
            return basicTenderModel;
        }
        [Authorize(RoleNames.SuppliersReportPolicy)]
        [HttpGet]
        [Route("ExportAppliedSuppliersReport/{tenderIdString}")]
        public async Task<List<AppliedSuppliersReportDetailsModel>> ExportAppliedSuppliersReport(string tenderIdString)
        {
            var tenderId = Util.Decrypt(tenderIdString);
            var basicTenderModel = await _offerAppService.ExportAppliedSuppliersReport(tenderId);
            return basicTenderModel;
        }
        #endregion Query    

        #region Command

        //  [Authorize( RoleNames.supplier)]
        [HttpPost("AddOffer")]
        public async Task<IActionResult> AddOffer([FromBody] OfferModel offerModel)
        {
            var offer = _mapper.Map<Offer>(offerModel);
            var result = await _offerAppService.AddOffer(offer);
            return Ok(result);
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost("SendOffer")]
        public async Task<IActionResult> SendOffer([FromBody] int id)
        {
            string cr = User.SupplierNumber();

            var result = await _offerAppService.SendOffer(id, cr);
            return Ok();
        }
        [HttpPost("SendOfferToApprove")]
        public async Task<OfferSummaryStatusModel> SendOfferToApprove([FromBody] int id)
        {
            string cr = User.SupplierNumber();
            OfferSummaryStatusModel result = await _offerAppService.SendOfferToApprove(id, cr);
            return result;
        }

        //[Authorize(RoleNames.SupplierPolicy)]
        //[HttpPost("AddQuantityTable")]
        //public async Task<IActionResult> AddQuantityTable([FromBody]SupplierQuantityTableModel supplierQuantityTableModel)
        //{
        //    var supplierQuantityTable = _mapper.Map<SupplierQuantityTable>(supplierQuantityTableModel);
        //    var result = await _offerAppService.CreateAsync(supplierQuantityTable);
        //    return Ok(result);
        //}

        //[Authorize(RoleNames.SupplierPolicy)]
        //[HttpPost("AddQuantityTableItem")]
        //public async Task<IActionResult> AddQuantityTableItem([FromBody]SupplierQuantityTableItemModel supplierQuantityTableModel)
        //{
        //    var supplierQuantityTableItem = _mapper.Map<SupplierQuantityTableItem>(supplierQuantityTableModel);
        //    var result = await _offerAppService.CreateAsync(supplierQuantityTableItem);
        //    return Ok(result);
        //}

        //[Authorize(RoleNames.SupplierPolicy)]
        //[HttpPost("DeleteQuantityTable/{id}")]
        //public async Task<IActionResult> DeleteQuantityTable(int id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var result = await _offerAppService.DeActiveTableAsync(id);
        //    return Ok(result);
        //}

        //[Authorize(RoleNames.SupplierPolicy)]
        //[HttpPost("DeleteQuantityTableItem/{id}")]
        //public async Task<IActionResult> DeleteQuantityTableItem(int id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _offerAppService.DeActiveItemAsync(id);
        //    return Ok(result);
        //}
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost("ConfirmReceivedOffer/{offerId}")]
        public async Task<IActionResult> ConfirmReceivedOfferAsync(int offerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _offerAppService.ConfirmReceivedOfferAsync(offerId);
            return Ok(result);
        }

        [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
        [HttpPost("AddOfferAttachmentsDetails")]
        public async Task<IActionResult> AddOfferAttachmentsDetails(/*[FromBody]*/ OfferAttachmentsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string cr = User.SupplierNumber();
            var result = await _offerAppService.AddOfferAttachmentsDetails(model, cr);
            return Ok(result);
        }

        [Authorize(Roles = RoleNames.OffersOppeningSecretary)]
        [HttpPost("AddOfferDetails")]
        public async Task<IActionResult> AddOfferDetails([FromBody] OfferDetailModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _offerAppService.AddOfferDetails(model);
            var resultModel = _mapper.Map<OfferModel>(result);
            return Ok(resultModel);
        }

        //[Authorize(Roles = RoleNames.OffersOppeningSecretary + "," + RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpPost("SaveDiscountNotes")]
        public async Task<IActionResult> SaveDiscountNotes([FromBody] DiscountNotesModel model)
        {
            var result = await _offerAppService.SaveDiscountNotes(model);
            return Ok(result);
        }
        //[Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]


        [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
        [HttpPost("AddClassification")]
        public async Task<IActionResult> AddClassification([FromBody] SupplierSpecificationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Attachments = _mapper.Map<SupplierSpecificationAttachment>(model);
            var result = await _offerAppService.AddClassificationAttachmentsAsync(Attachments);
            return Ok(result);
        }
        #endregion Command
        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("OfferCheckingStatus")]
        public async Task<ActionResult> OfferCheckingStatus([FromBody] OfferCheckingDetailsModel offerModel)
        {
            offerModel.TechniciansReportAttachments = await PrepareTechniciansReportAttachmentsModel(offerModel.TechniciansReportAttachmentsRef, offerModel.OfferId);
            var offer = await _offerAppService.UpdateOfferCheckingStatus(offerModel);
            var result = await _offerAppService.FindOfferDetailsById(offer.OfferId);
            return Ok(result);
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetOffersForAwardingByTenderId/{tenderId}")]
        public async Task<QueryResult<OfferFinancialDetailsModel>> GetOffersForAwardingByTenderId(int tenderId, string crsuppliers)
        {
            var offers = await _offerAppService.GetOffersForAwardingByTenderId(tenderId, crsuppliers);
            return offers;
        }
        //  [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("OffersOpeningReport/{tenderId}")]
        public async Task<QueryResult<OfferFinancialDetailsModel>> OffersOpeningReport(int tenderId)
        {
            var offers = await _offerAppService.OfferFinancialDetailsForTender(tenderId);
            return offers;
        }

        [HttpGet]
        [Route("GetOfferStatusForOfferSummary/{offerId}")]
        public async Task<OfferSummaryStatusModel> GetOfferStatusForOfferSummary(int offerId)
        {
            OfferSummaryStatusModel summaryStatusModel = await _offerAppService.GetOfferStatusForOfferSummary(offerId, User.SupplierNumber());
            return summaryStatusModel;
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetAwardedOffersByTenderId/{tenderId}")]
        public async Task<QueryResult<OfferFinancialDetailsModel>> GetAwardedOffersByTenderId(int tenderId)
        {
            var invitationsOffers = await _offerAppService.OfferAwardeFinancialDetailsForTender(tenderId);
            return invitationsOffers;
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("SaveOfferAwardingValues")]
        public async Task<ActionResult> SaveOfferAwardingValues([FromBody] OfferAwardingModel offerAwardingObj)
        {
            await _offerAppService.SaveOfferAwardingValues(offerAwardingObj);
            return Ok();
        }

        [Authorize(RoleNames.SuppliersReportPolicy)]
        [HttpGet]
        [Route("GetOfferUploadedAttachmentsDetails/{offerId}/{combinedId}")]
        public async Task<OfferAttachmentsModel> GetOfferUploadedAttachmentsDetails(int offerId, int combinedId)
        {
            int userId = User.UserId();
            var details = await _offerAppService.GetOfferAttachmentsDetails(offerId, combinedId, userId);
            return details;
        }

        [HttpGet]
        [Route("GetOfferDetails/{combinedId}")]
        public async Task<OfferDetailModel> GetOfferDetails(int combinedId)
        {
            int userId = User.UserId();
            var details = await _offerAppService.GetOfferDetailsForOpening(combinedId, userId);
            Check.ArgumentNotNull(nameof(OfferDetailModel), details);
            return details;
        }




        [HttpGet]
        [Route("GetOfferDetailsDisplayOnly/{combinedId}")]
        public async Task<OfferDetailModel> GetOfferDetailsDisplayOnly(int combinedId)
        {
            int userId = User.UserId();
            var details = await _offerAppService.GetOfferDetailsDisplayOnly(combinedId, userId);
            Check.ArgumentNotNull(nameof(OfferDetailModel), details);
            return details;
        }

        [Authorize(RoleNames.SuppliersReportPolicy)]
        [HttpGet]
        [Route("GetOfferAttachmentsDetails/{OfferId}")]
        public async Task<OfferCheckingAttachmentsDetailsModel> GetOfferAttachmentsDetails(int OfferId)
        {
            var details = await _offerAppService.FindOfferDetailsForCheckById(OfferId);
            return details;
        }

        [HttpGet("OfferDeatilsReportForTender/{tenderId}")]
        //[Authorize(Roles = RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager + "," + RoleNames.supplier)]
        [Authorize(RoleNames.OpenOffersReportPolicy)]
        public async Task<List<OfferDeatilsReportForTenderModel>> OfferDeatilsReportForTender(int tenderId)
        {
            return await _offerAppService.OfferDetailsReportForTender(tenderId);
        }

        #region Check

        [HttpGet]
        [Authorize(RoleNames.CheckTenderOffersPolicy)]
        [Route("GetOpenOfferDataForCHeckStage/{offerid}")]
        public async Task<CheckOfferModel> GetOpenOfferDataForCHeckStage(int offerid)
        {
            CheckOfferModel openOfferModel = await _offerAppService.GetOpenOfferDataForCheckStage(offerid);
            return openOfferModel;
        }

        [Authorize(RoleNames.CheckTenderOffersPolicy)]
        [HttpGet]
        [Route("GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync/{offerid}")]
        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(int offerid)
        {
            int userId = User.UserId();
            QueryResult<CombinedSupplierModel> combinedSupplierModel = await _offerAppService.GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(offerid, userId);
            Check.ArgumentNotNull(nameof(CombinedSupplierModel), combinedSupplierModel);
            return combinedSupplierModel;
        }

        #endregion

        //[HttpGet]
        //[Route("GetCombinedSuppliers/{offerid}")]
        //public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(int offerid)
        //{
        //    int userId = User.UserId();
        //    QueryResult<CombinedSupplierModel> combinedSupplierModel = await _offerAppService.GetCombinedSuppliers(offerid, userId);
        //    Check.ArgumentNotNull(nameof(CombinedSupplierModel), combinedSupplierModel);
        //    return combinedSupplierModel;
        //}

        [HttpGet]
        [Route("GetTenderOfferIDsByOfferID/{offerid}")]
        public async Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerid)
        {
            CombinedSupplierModel combinedSupplierModel = await _offerAppService.GetTenderOfferIDsByOfferID(offerid);
            return combinedSupplierModel;
        }

        #region OffersChecking

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager)]
        [HttpGet]
        [Route("GetOfferDetailsForTenderChecking/{offerIdString}")]
        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForTenderChecking(string offerIdString)
        {
            int userId = User.UserId();
            var details = await _offerAppService.GetOfferDetailsForFinancialChecking(offerIdString, userId);
            Check.ArgumentNotNull(nameof(OfferDetailsForCheckingModel), details);
            return details;
        }


        [HttpGet]
        [Route("GetOfferTableQuantityItems")]
        public async Task<QueryResult<TableModel>> GetOfferTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetOfferTableQuantityItems(quantityTableSearchCretriaModel);
        }

        [Authorize(RoleNames.CheckTenderOffersPolicy)]
        [HttpGet]
        [Route("GetOfferDetailsForCombinedChecking/{combinedId}")]
        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForCombinedChecking(int combinedId)
        {
            int userId = User.UserId();
            var details = await _offerAppService.GetOfferDetailsForTenderChecking(combinedId, userId);
            Check.ArgumentNotNull(nameof(OfferDetailsForCheckingModel), details);
            return details;
        }


        [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        [HttpPost("AddTwoFilesFinancialDetails")]
        public async Task<IActionResult> AddTwoFilesFinancialDetails([FromBody] TenderTowFilesFinancialDetailsDetails model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _offerAppService.AddTwoFilesFinancialDetails(model);
            return Ok();
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        [HttpPost("AddTwoFilesFinancialCheck")]
        public async Task<IActionResult> AddTwoFilesFinancialCheck([FromBody] OfferDetailsForCheckingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _offerAppService.AddTwoFilesFinancialCheck(model);
            return Ok();
        }

        //[HttpGet]
        //[Route("GetOfferDetailsByCombinedId/{combinedId}")]
        //public async Task<OpenOfferModel> GetOfferDetailsByCombinedIdForCheckingStage(int combinedId)
        //{
        //    int userId = User.UserId();
        //    OpenOfferModel openOfferModel = await _offerAppService.GetOfferDetailsByCombinedIdForCheckingStage(combinedId, userId);
        //    return openOfferModel;
        //}

        [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        [HttpPost("AddTechnicalOfferResultForTwoFilesTender")]
        public async Task<IActionResult> AddTechnicalOfferResultForTwoFilesTender([FromBody] OfferDetailsForCheckingTwoFilesModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _offerAppService.AddTechnicalOfferResultForTwoFilesTender(model);
            return Ok();
        }

        //[Authorize(Roles = RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost("ValidateandSaveCheckingQuantitiesTable")]
        public async Task<QueryResult<TableModel>> ValidateandSaveCheckingQuantitiesTable([FromBody] Dictionary<string, string> AuthorList)
        {
            return await _offerAppService.ValidateandSaveCheckingQuantitiesTable(AuthorList);
        }
        #endregion

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("OfferCheckingDetailsForAwarding/{tenderId}/{offerId}")]
        public async Task<OfferModel> OfferCheckingDetailsForAwarding(int tenderId, int offerId)
        {
            OfferModel _supplierQuantityTableItem = await _offerAppService.OfferCheckingDetailsForAwarding(tenderId, offerId);
            _supplierQuantityTableItem.QuantityTableForDirectPurchase = await _offerAppService.GetQuantityTablesForDirectPurchase(tenderId, offerId);
            return _supplierQuantityTableItem;
        }

        [HttpGet]
        [Route("GetBayanTableAsync")]
        public async Task<QueryResult<TableModel>> GetBayanTableAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetBayanTableAsync(quantityTableSearchCretriaModel);
        }

        [HttpGet]
        [Route("GetBayanTableReadOnlyAsync")]
        public async Task<QueryResult<TableModel>> GetBayanTableReadOnlyAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetBayanTableReadOnlyAsync(quantityTableSearchCretriaModel);
        }

        //[Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("ValidateBayanQuantitiesTables")]
        public async Task<ValidationReturndTemplate> ValidateBayanQuantitiesTables([FromBody] Dictionary<string, string> AuthorList)
        {
            return await _offerAppService.ValidateBayanQuantitiesData(AuthorList);
        }

        //[Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("DeleteBayanQuantityItem")]
        public async Task<ValidationReturndTemplate> DeleteBayanQuantityItem([FromBody] Dictionary<string, string> authorList)
        {
            return await _offerAppService.DeleteOfferQuantityItem(authorList);
        }
        #region Checking Stage Direct Purchase 

        //[Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetCoastsTablForDirectPurchaseAsync")]
        public async Task<TableModel> GetCoastsTablForDirectPurchaseAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetCoastsTablForDirectPurchaseAsync(quantityTableSearchCretriaModel);
        }
        [HttpGet]
        [Route("GetCoastsTablForApplyOfferAsync")]
        public async Task<TableModel> GetCoastsTablForApplyOfferAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetCoastsTablForApplyOfferAsync(quantityTableSearchCretriaModel);
        }

        [HttpGet]
        [Route("GetCoastsTablForOpenDetails")]
        public async Task<TableModel> GetCoastsTablForOpenDetails(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetCoastsTablForOpenDetails(quantityTableSearchCretriaModel);
        }
        //[Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetOfferTableQuantityItemsForDirectPurchase")]
        public async Task<QueryResult<TableModel>> GetOfferTableQuantityItemsForDirectPurchase(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetOfferTableQuantityItemsForDirectPurchase(quantityTableSearchCretriaModel);
        }

        [HttpGet]
        [Route("GetApplyOfferTableQuantityItems")]
        public async Task<QueryResult<TableModel>> GetApplyOfferTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetApplyOfferTableQuantityItems(quantityTableSearchCretriaModel);
        }

        [HttpGet]
        [Route("GetTableQuantityItemsOpenDetails")]
        public async Task<QueryResult<TableModel>> GetTableQuantityItemsOpenDetails(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetTableQuantityItemsOpenDetails(quantityTableSearchCretriaModel);
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GeOfferByTenderIdAndOfferIdForDirectPurchase/{tenderId}/{offerId}")]
        public async Task<OfferModel> GeOfferByTenderIdAndOfferIdForDirectPurchase(int tenderId, int offerId)
        {
            OfferModel _supplierQuantityTableItem = await _offerAppService.GeOfferByTenderIdAndOfferIdForDirectPurchaseChecking(tenderId, offerId);
            _supplierQuantityTableItem.QuantityTableForDirectPurchase = await _offerAppService.GetQuantityTablesForDirectPurchase(tenderId, offerId);
            return _supplierQuantityTableItem;
        }

        [Authorize(RoleNames.CheckTenderOffersPolicy)]
        [HttpGet]
        [Route("GetOfferQuantityTableForAwarding/{tenderId}/{offerId}")]
        public async Task<QuantitiesTablesForAwardingModel> GetOfferQuantityTableForAwarding(int tenderId, int offerId)
        {
            QuantitiesTablesForAwardingModel quantitiesTablesForAwarding = await _offerAppService.GetOfferQuantityTableForAwarding(tenderId, offerId);
            return quantitiesTablesForAwarding;
        }
        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetOfferDetailsDirectPurchase/{combinedId}")]
        public async Task<OfferDetailModel> GetOfferDetailsDirectPurchase(int combinedId)
        {
            int userId = User.UserId();
            var details = await _offerAppService.GetOfferDetails(combinedId, userId);
            Check.ArgumentNotNull(nameof(OfferDetailModel), details);
            return details;
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetOfferDetailsForDirectPurchaseTenderChecking/{combinedId}")]
        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseTenderChecking(int combinedId)
        {
            int userId = User.UserId();
            var details = await _offerAppService.GetOfferDetailsForDirectPurchaseTenderChecking(combinedId, userId);
            Check.ArgumentNotNull(nameof(OfferDetailsForCheckingModel), details);
            return details;
        }
        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetOfferDetailsForDirectPurchaseTenderFinancialChecking/{offerId}")]
        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseTenderFinancialChecking(int offerId)
        {
            int userId = User.UserId();
            var details = await _offerAppService.GetOfferDetailsForDirectPurchaseTenderFinancialChecking(offerId, userId);
            Check.ArgumentNotNull(nameof(OfferDetailsForCheckingModel), details);
            return details;
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpPost("SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial")]
        public async Task<IActionResult> SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial([FromBody] OfferCheckingContainer container)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            OfferDetailModel model = container.model;
            CheckOfferResultModel checkingResult = container.checkingResult;
            if (container.checkingResult != null)
                container.checkingResult.TechniciansReportAttachments = await PrepareTechniciansReportAttachmentsModel(container.checkingResult.TechniciansReportAttachmentsRef, Util.Decrypt(container.model.OfferIdString));
            await _offerAppService.SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial(container);
            return Ok();
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpPost("SaveDirectPurchaseAttachmentsChecking")]
        public async Task<IActionResult> SaveDirectPurchaseAttachmentsChecking([FromBody] OfferDetailModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _offerAppService.SaveDirectPurchaseAttachmentsChecking(model);
            return Ok(result);
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpPost("SaveOneFileDirectPurchaseChecking")]
        public async Task<IActionResult> SaveOneFileDirectPurchaseChecking([FromBody] CheckOfferResultModel checkingResult)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (checkingResult != null)
                checkingResult.TechniciansReportAttachments = await PrepareTechniciansReportAttachmentsModel(checkingResult.TechniciansReportAttachmentsRef, Util.Decrypt(checkingResult.OfferIdString));
            await _offerAppService.SaveOneFileDirectPurchaseChecking(checkingResult);
            return Ok();
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost("SaveTwoFileTechnicalCheckingDataAndAttachemntsDetial")]
        public async Task<IActionResult> SaveTwoFileTechnicalCheckingDataAndAttachemntsDetial([FromBody] OfferCheckingContainer container)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            OfferDetailModel model = container.model;
            CheckOfferResultModel checkingResult = container.checkingResult;
            if (container.checkingResult != null)
                container.checkingResult.TechniciansReportAttachments = await PrepareTechniciansReportAttachmentsModel(container.checkingResult.TechniciansReportAttachmentsRef, Util.Decrypt(container.model.OfferIdString));
            await _offerAppService.SaveTwoFileTechnicalCheckingDataAndAttachemntsDetial(container);
            return Ok();
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost("SaveTwoFileFinancialCheckingDataAndAttachemntsDetial")]
        public async Task<IActionResult> SaveTwoFileFinancialCheckingDataAndAttachemntsDetial([FromBody] OfferCheckingContainer container)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _offerAppService.SaveTwoFileFinancialCheckingDataAndAttachemntsDetial(container);
            return Ok();
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost("SaveTwoFilesTechnicalDirectPurchaseChecking")]
        public async Task<IActionResult> SaveTwoFilesTechnicalDirectPurchaseChecking([FromBody] CheckOfferResultModel checkingResult)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (checkingResult != null)
                checkingResult.TechniciansReportAttachments = await PrepareTechniciansReportAttachmentsModel(checkingResult.TechniciansReportAttachmentsRef, Util.Decrypt(checkingResult.OfferIdString));
            await _offerAppService.SaveTwoFilesTechnicalDirectPurchaseChecking(checkingResult);
            return Ok();
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost("SaveTwoFilesFinancialDirectPurchaseChecking")]
        public async Task<IActionResult> SaveTwoFilesFinancialDirectPurchaseChecking([FromBody] CheckOfferResultModel checkingResult)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _offerAppService.SaveTwoFilesFinancialDirectPurchaseChecking(checkingResult);
            return Ok();
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost("SaveOfferCheckingOneFile")]
        public async Task<OfferModel> SaveOfferCheckingOneFile([FromBody] OfferCheckingContainer container)
        {
            OfferDetailModel model = container.model;
            CheckOfferResultModel checkingResult = container.checkingResult;
            if (container.checkingResult != null)
                container.checkingResult.TechniciansReportAttachments = await PrepareTechniciansReportAttachmentsModel(container.checkingResult.TechniciansReportAttachmentsRef, Util.Decrypt(container.model.OfferIdString));
            var offer = await _offerAppService.SaveOfferChecking(null, checkingResult);
            OfferModel offerModel = await _offerAppService.GeOfferByTenderId(offer.TenderId);
            return offerModel;
        }

        [Authorize(RoleNames.AddFinantialCheckingResultPolicy)]
        [HttpPost("AddFinantialCheckingResult")]
        public async Task<OfferModel> AddFinantialCheckingResult([FromBody] CheckOfferResultModel model)
        {
            var offer = await _offerAppService.AddFinantialCheckingResult(model);
            OfferModel offerModel = await _offerAppService.GeOfferByTenderId(offer.TenderId);
            return offerModel;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningSecretary)]
        [HttpPost("AddCheckingFinancialDetails")]
        public async Task AddCheckingFinancialDetails([FromBody] OfferDetailModel model)
        {
            await _offerAppService.AddCheckingFinancialDetails(model);
        }

        //[Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetOfferDetailsForDirectPurchase/{combinedId}")]
        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchase(int combinedId)
        {
            int userId = User.UserId();
            var details = await _offerAppService.GetOfferDetailsForTenderChecking(combinedId, userId);
            Check.ArgumentNotNull(nameof(OfferDetailModel), details);
            return details;
        }

        #endregion


        /// <summary>
        ///  User Story [7267,7270]
        /// </summary>
        /// <param name="attachmentReference"></param>
        /// <returns></returns>
        private async Task<List<TechniciansReportAttachmentModel>> PrepareTechniciansReportAttachmentsModel(string attachmentReference, int offerId)
        {
            List<string> attachmentReferences = new List<string>();
            if (!string.IsNullOrEmpty(attachmentReference))
            {
                string[] lst = attachmentReference.Split(',');
                foreach (var item in lst)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains("/GetFile/"))
                            attachmentReferences.Add(item.Split("/GetFile/")[item.Split("/GetFile/").Length - 1]);
                        else
                            attachmentReferences.Add(item);
                    }
                }
            }
            List<TechniciansReportAttachmentModel> techniciansReportAttachments = new List<TechniciansReportAttachmentModel>();
            foreach (var item in attachmentReferences)
            {
                var arr = item.Split(":");
                TechniciansReportAttachmentModel techniciansReportAttachment = new TechniciansReportAttachmentModel() { Name = arr[1], FileNetReferenceId = arr[0], OfferId = offerId, AttachmentTypeId = (int)Enums.AttachmentType.TechnicianReport };
                techniciansReportAttachments.Add(techniciansReportAttachment);
            }
            return techniciansReportAttachments;
        }
        //[Authorize(Roles = RoleNames.supplier)]
        //[HttpPost]
        //[Route("DeleteAlternativeItems")]
        //public async Task<AlternativeItemResponseModel> DeleteAlternativeItemee([FromBody]int alternativeItemId)
        //{
        //    //  var alternativeItemModel = await _offerAppService.SaveAlternativeItem(alternativeItem);
        //    var alternativeItemModel = await _offerAppService.DeleteAlternativeItem(alternativeItemId);
        //    return alternativeItemModel;
        //}

        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet]
        [Route("DeleteAlternativeItem/{TableId}/{ItemNumber}")]
        public async Task<AlternativeItemResponseModel> DeleteAlternativeItem(int TableId, int ItemNumber)
        {
            //  var alternativeItemModel = await _offerAppService.SaveAlternativeItem(alternativeItem);
            //int TableId = 0, ItemNumber = 0;

            var alternativeItemModel = await _offerAppService.DeleteAlternativeItem(ItemNumber, TableId);
            return alternativeItemModel;
        }
        //[Authorize(Roles = RoleNames.supplier)]
        //[HttpPost]
        //[Route("SaveAlternativeItem")]
        //public async Task<AlternativeItemResponseModel> SaveAlternativeItem([FromBody]SupplierQuantityTableItemModel alternativeItem)
        //{
        //    var alternativeItemModel = await _offerAppService.SaveAlternativeItem(alternativeItem);
        //    return alternativeItemModel;
        //}


        //[Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        //[HttpGet]
        //[Route("GetQuantitiesbyOfferId/{offerId}")]
        //public async Task<List<AlternativeSupplierQuantityTableForCheckingModel>> GetQuantitiesbyOfferId(int offerId)
        //{
        //    var quantities = await _offerAppService.GetQuantitiesbyOfferId(offerId);
        //    Check.ArgumentNotNull(nameof(AlternativeSupplierQuantityTableForCheckingModel), quantities);
        //    return quantities;
        //}

        //[Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        //[HttpPost]
        //[Route("SaveAlternativeChoices")]
        //public async Task<List<AlternativeSupplierQuantityTableForCheckingModel>> SaveAlternativeChoices([FromBody]AlternativeOfferCheckingChoiceSavingModel model)
        //{
        //    return await _offerAppService.SaveAlternativeChoices(model);
        //}

        [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpPost("AddFinantialOfferAttachmentsInVRO")]
        public async Task<IActionResult> AddFinantialOfferAttachmentsInVRO([FromBody] OfferDetailsForCheckingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _offerAppService.AddFinancialOfferAttachmentsInVRO(model);
            return Ok();
        }
        #region VRO 

        [HttpGet]
        [Route("GetVROOfferDetailsById/{offerIdString}")]
        public async Task<VROOffersTechnicalCheckingViewModel> GetVROOfferDetailsById(string offerIdString)
        {
            VROOffersTechnicalCheckingViewModel offer = await _offerAppService.FindVROOfferDetailsById(offerIdString);
            return offer;
        }

        [HttpGet]
        [Route("GetOfferTableQuantityItemsVRO")]
        public async Task<QueryResult<TableModel>> GetOfferTableQuantityItemsVRO(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _offerAppService.GetOfferTableQuantityItemsVRO(quantityTableSearchCretriaModel);
        }

        [HttpPost("ValidateandSaveVROCheckingQuantitiesTable")]
        public async Task<string> ValidateandSaveVROCheckingQuantitiesTable([FromBody] Dictionary<string, string> AuthorList)
        {
            return await _offerAppService.ValidateandSaveVROCheckingQuantitiesTable(AuthorList);
        }

        [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpPost]
        [Route("VROOffersTechnicalChecking")]
        public async Task<ActionResult> VROOffersTechnicalChecking([FromBody] VROOffersTechnicalCheckingViewModel offerModel)
        {
            var offer = await _offerAppService.UpdateVROOfferCheckingStatus(offerModel);
            return Ok(offer);
        }

        [HttpGet]
        [Route("GetOfferFinancialCheckingDetailsById/{tenderId}/{offerId}")]
        public async Task<VROFinantialCheckingModel> GetOfferFinancialCheckingDetailsById(int tenderId, int offerId)
        {
            VROFinantialCheckingModel offer = await _offerAppService.GeOfferByTenderIdAndOfferIdForFinantialChecking(tenderId, offerId);
            return offer;
        }

        //[Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpPost("SaveCombinedTechnicalDetailsVRO")]
        public async Task<IActionResult> SaveCombinedTechnicalDetailsVRO([FromBody] OfferDetailModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _offerAppService.SaveCombinedTechnicalDetailsVRO(model);
            return Ok(result);
        }
        #endregion VRO

        [HttpGet]
        [Route("GetOfferFullDetails/{OfferIdString}")]
        public async Task<OfferFullDetailsModel> GetOfferFullDetails(string OfferIdString)
        {
            var quantities = await _offerAppService.GetOfferFullDetails(Util.Decrypt(OfferIdString));
            Check.ArgumentNotNull(nameof(OfferFullDetailsModel), quantities);
            return quantities;
        }


        [HttpGet]
        [Route("GetOfferDetailsByCombinedIdForSupplier/{combinedId}")]
        public async Task<OfferDetailsForAcceptingSolidarityInviationsModel> GetOfferDetailsByCombinedId(int combinedId)
        {
            int userId = User.UserId();
            OfferDetailsForAcceptingSolidarityInviationsModel openOfferModel = await _offerAppService.GetOfferDetailsByCombinedId(combinedId, userId);
            return openOfferModel;
        }
        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet]
        [Route("GetAllCombinedInvitationForSupplier")]
        public async Task<QueryResult<CombinedListModel>> GetAllCombinedInvitationForSupplier(CombinedSearchCriteria criteria)
        {
            criteria.CommericalRegisterNo = User.SupplierNumber();
            var combinedList = await _offerAppService.GetAllCombinedInvitationForSupplier(criteria);
            return combinedList;
        }
        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet]
        [Route("GetCombinedInvitationDetails/{offerId}")]
        public async Task<CombinedInvitationDetailsModel> GetCombinedInvitationDetails(int offerId)
        {
            var combinedList = await _offerAppService.GetCombinedInvitationDetails(offerId);

            return combinedList;
        }

        [Authorize(Roles = RoleNames.supplier)]
        [HttpPost]
        [Route("AcceptCombinedInvitation/{combinedId}")]
        public async Task AcceptCombinedInvitation(int combinedId)
        {
            await _offerAppService.AcceptCombinedInvitation(combinedId);
        }
        [Authorize(Roles = RoleNames.supplier)]
        [HttpPost]
        [Route("RejectCombinedInvitation/{combinedId}")]
        public async Task RejectCombinedInvitation(int combinedId)
        {
            await _offerAppService.RejectCombinedInvitation(combinedId);
        }

        [HttpGet]
        [Route("GetQuantityTables/{offerId}")]
        public async Task<OfferQuantityTableHtmlVM> GetQuantityTables(int offerId)
        {
            OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM();
            offerQuantityTableHtmlVM = await _offerAppService.GetQuantityTables(offerId);
            return offerQuantityTableHtmlVM;
        }

        [HttpGet]
        [Route("GetOpeningQuantityTables/{offerId}/{allowEdit}")]
        public async Task<OfferQuantityTableHtmlVM> GetOpeningQuantityTables(int offerId, bool allowEdit)
        {
            OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM();
            offerQuantityTableHtmlVM = await _offerAppService.GetQuantityTablesForOpening(offerId, allowEdit);
            return offerQuantityTableHtmlVM;
        }
        [HttpGet]
        [Route("GetOpeningQuantityTablesTest/{offerId}/{allowEdit}")]
        public async Task<QuantitiesTemplateModel> GetOpeningQuantityTablesTest(int offerId, bool allowEdit)
        {
            return await _offerAppService.GetQuantityTablesForOpeningTest(offerId, allowEdit);
        }

        [HttpGet]
        [Route("GetQuantityTablesDisplay/{offerId}/{allowEdit}/{isNegotiation}")]
        public async Task<OfferQuantityTableHtmlVM> GetQuantityTablesDisplay(int offerId, bool allowEdit, bool isNegotiation = false)
        {
            OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM();
            offerQuantityTableHtmlVM = await _offerAppService.GetQuantityTablesDisplay(offerId, allowEdit, isNegotiation);
            return offerQuantityTableHtmlVM;
        }

        [HttpGet]
        [Route("GetEmptyForm/{offerId}/{tenderId}")]
        public async Task<string> GetEmptyForm(int offerId, int tenderId)
        {
            return (await _offerAppService.GetEmptyForm(offerId, tenderId));
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("ValidateQuantitiesTables")]
        public async Task<ValidationReturndTemplate> ValidateQuantitiesTables([FromBody] Dictionary<string, string> AuthorList)
        {
            return await _offerAppService.ValidateOfferQuantitiesData(AuthorList);
        }

        [HttpGet]
        [Route("GetOfferQuantityTableModelAsync/{offerId}")]
        public async Task<OfferQuantityTableModel> GetOfferQuantityTableModelAsync(int offerId)
        {


            OfferQuantityTableModel offerQuantityTableModel = new OfferQuantityTableModel();
            offerQuantityTableModel = await _offerAppService.GetOfferQuantityTableModel(offerId);
            return offerQuantityTableModel;
        }

        [HttpGet]
        [Route("GetApplyOfferQuantityTableModelAsync/{offerId}")]
        public async Task<OfferQuantityTableModel> GetApplyOfferQuantityTableModelAsync(int offerId)
        {


            OfferQuantityTableModel offerQuantityTableModel = new OfferQuantityTableModel();
            offerQuantityTableModel = await _offerAppService.GetApplyOfferQuantityTableModel(offerId);
            return offerQuantityTableModel;
        }
        [HttpGet]
        [Route("ApplyOfferQuantityTablesStep/{offerId}")]
        public async Task<OfferQuantityTableModel> ApplyOfferQuantityTablesStep(int offerId)
        {
            OfferQuantityTableModel offerQuantityTableModel = new OfferQuantityTableModel();
            offerQuantityTableModel = await _offerAppService.GetApplyOfferQuantityTableStepModel(offerId);
            return offerQuantityTableModel;
        }

        [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
        [HttpPost("ValidateandSaveQuantitiesTableForOpening")]
        public async Task<string> ValidateandSaveQuantitiesTableForOpening([FromBody] Dictionary<string, string> AuthorList)
        {
            return await _offerAppService.ValidateandSaveQuantitiesTableForOpening(AuthorList);
        }

        [HttpGet]
        [Route("GetQuantityTablesReadOnly/{offerId}")]
        public async Task<OfferQuantityTableHtmlVM> GetQuantityTablesReadOnly(int offerId)
        {
            OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM();
            offerQuantityTableHtmlVM = await _offerAppService.GetQuantityTablesReadOnly(offerId);
            return offerQuantityTableHtmlVM;
        }
        [HttpGet]
        [Route("OfferFilesAsync/{offerid}")]
        public async Task<OfferFileVModel> OfferFilesAsync(int offerid)
        {
            OfferFileVModel offerFileVModels = await _offerAppService.GetOfferFileVModel((offerid));
            return offerFileVModels;

        }

        [HttpGet]
        [Route("GetOfferFiles/{offerid}")]
        public async Task<SupplierAttachmentPartialVModel> GetOfferFiles(int offerid)
        {
            SupplierAttachmentPartialVModel offerFileVModels = await _offerAppService.GetSupplierAllFiles((offerid));
            return offerFileVModels;

        }
        [HttpPost]
        [Route("PostOfferFilesAsync")]
        public async Task<OfferFileVModel> PostOfferFilesAsync([FromBody] OfferFileVModel offerFileVModel)
        {
            return await _offerAppService.Addofferfiles((offerFileVModel));


        }
        [HttpGet]
        [Route("GetOfferMain/{offerid}/{tenderId}")]
        public async Task<OfferMainVModel> GetOfferMain(int offerid, int tenderId)
        {
            OfferMainVModel offerFileVModels = await _offerAppService.GetOfferMainVModel(offerid, tenderId);
            return offerFileVModels;

        }

        [HttpPost]
        [Route("PostOfferMain")]
        public async Task<OfferMainVModel> PostOfferMain([FromBody] OfferMainVModel mainVModel)
        {
            OfferMainVModel offerFileVModels = await _offerAppService.AddOfferMainVModel(mainVModel);
            return offerFileVModels;

        }
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("DeleteAttachment/{attachmentId}")]
        public async Task<bool> DeleteAttachment(int attachmentId)
        {
            var suplierCR = User.SupplierNumber();
            await _offerAppService.DeleteAttachement(attachmentId, suplierCR);
            return true;

        }


        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost("ValidateandSaveQuantitiesTable")]
        public async Task<string> ValidateandSaveQuantitiesTable([FromBody] Dictionary<string, string> AuthorList)
        {
            return await _offerAppService.ValidateandSaveQuantitiesTable(AuthorList);
        }


        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost("ValidateSupplierAlternativeItem")]
        public async Task<ValidationReturndTemplate> ValidateSupplierAlternativeItem([FromBody] Dictionary<string, string> AuthorList)
        {
            return await _offerAppService.ValidateSupplierAlternativeItem(AuthorList);
        }

        [Authorize(RoleNames.DataEntryAndSupplierPolicy)]
        [HttpGet]
        [Route("GetOfferSolidarity/{id}")]
        public async Task<OfferSolidarityModel> GetOfferSolidarity(int id)
        {
            string cr = User.SupplierNumber();
            OfferSolidarityModel invitationStepModel = await _offerAppService.GetOfferSolidarity(id);
            Check.ArgumentNotNull(nameof(invitationStepModel), invitationStepModel);
            return invitationStepModel;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetAllInvitedSuppliers")]
        public async Task<QueryResult<SolidarityInvitedRegisteredSupplierModel>> GetAllInvitedSuppliers(SolidaritySearchCriteria cretria)
        {
            //int BranchId = User.UserBranchId();
            //int AgencyCode = User.UserAgency();
            //var tender =  await _tenderAppService.GetBasicTenderDetailsById(cretria.InvitationTenderId, BranchId);
            //if (tender.BranchId != BranchId || tender.AgencyCode != AgencyCode)
            //    throw new SharedKernal.Exceptions.BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            var suppliers = await supplierService.GetSolidarityInvitedSuppliers(cretria);
            return suppliers;
        }
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetInvitedUnregisteredSuppliers")]
        public async Task<QueryResult<SolidarityInvitedUnRegisteredSupplierModel>> GetInvitedUnregisteredSuppliers(SolidarityUnregisteredSearchCriteria cretria)
        {
            var suppliers = await supplierService.GetInvitedUnregisteredSuppliers(cretria);
            return suppliers;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetAllSuppliersBySearchCretriaForInvitation")]
        public async Task<QueryResult<InvitationCrModel>> GetAllSuppliersBySearchCretriaForInvitation(SupplierSearchCretriaModel cretria)
        {
            cretria.AgencyCode = User.UserAgency();
            var suppliers = await supplierService.GetAllSuppliersBySearchCretriaForInvitation(cretria);
            return suppliers;
        }


        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetAllSuppliersBySearchCretriaForSolidarity")]
        public async Task<QueryResult<SolidarityInvitedRegisteredSupplierModel>> GetAllSuppliersBySearchCretriaForSolidarity(SolidaritySearchCriteria cretria)
        {
            cretria.CurrentSupplierCR = User.SupplierNumber();
            var suppliers = await supplierService.GetAllSuppliersBySearchCretriaForSolidarity(cretria);
            return suppliers;
        }


        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost]
        [Route("SendInvitations")]
        public async Task<IActionResult> SendInvitations([FromBody] List<SolidarityInvitedRegisteredSupplierModel> suppliers)
        {
            await _offerAppService.SendInvitationsAsync(suppliers);
            return Ok();
        }
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost]
        [Route("AddSolidaritySupplier")]
        public async Task<IActionResult> AddSolidaritySupplier([FromBody] SolidarityInvitationModel solidarity)
        {
            await _offerAppService.AddSolidaritySupplier(solidarity);
            return Ok();
        }
        [HttpPost]
        [Route("RemoveSolidaritySupplier")]
        public async Task<IActionResult> RemoveSolidaritySupplier([FromBody] SolidarityRemoveInvitationModel model)
        {
            await _offerAppService.RemoveSolidaritySupplier(model);
            return Ok();
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("SendInvitationBySms")]
        public async Task<IActionResult> SendInvitationBySms(int tenderId, List<string> mobilNoList)
        {
            await _offerAppService.SendInvitationBySms(tenderId, mobilNoList);
            return Ok();
        }

        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet]
        [Route("GetAllCombinedSuppliers")]
        public async Task<QueryResult<CombinedSuppliersListModel>> GetAllCombinedSuppliers(CombinedSearchCretriaModel criteria)
        {
            var combinedList = await _offerAppService.GetAllCombinedSuppliers(criteria);
            return combinedList;
        }
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("SendInvitationByEmail")]
        public async Task<IActionResult> SendInvitationByEmail(int tenderId, List<string> emails)
        {
            await _offerAppService.SendInvitationsByEmail(tenderId, emails);
            return Ok();
        }



        #region OpenOffers
        [HttpGet]
        [Route("GetCombinedSupplierByOfferid")]
        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSupplierByOfferid(CombinedSupplierModel model)
        {
            QueryResult<CombinedSupplierModel> CombinedSuppliers = await _offerAppService.GetCombinedSuppliersByOfferid(model);
            return CombinedSuppliers;
        }

        [HttpGet]
        [Route("GetCombinedSuppliers")]
        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(CombinedSupplierModel model)
        {
            int userId = User.UserId();
            QueryResult<CombinedSupplierModel> combinedSupplierModel = await _offerAppService.GetCombinedSuppliers(model, userId);
            Check.ArgumentNotNull(nameof(CombinedSupplierModel), combinedSupplierModel);
            return combinedSupplierModel;
        }

        [HttpGet]
        [Route("GetFinancialOfferDetailsForDisplay/{offerIdString}")]
        public async Task<OfferDetailsDisplayModel> GetFinancialOfferDetailsForDisplay(string offerIdString)
        {
            var result = await _offerAppService.GetFinancialOfferDetailsForDisplay(Util.Decrypt(offerIdString));
            return result;
        }
        #endregion


        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("AddExclusionReason")]
        public async Task<ActionResult> AddExclusionReason([FromBody] ExclusionReasonAwardingModel exclusionReasonAwardingModel)
        {
            exclusionReasonAwardingModel.OfferId = Util.Decrypt(exclusionReasonAwardingModel.offerIdString);
            await _offerAppService.AddExclusionReason(exclusionReasonAwardingModel);
            return Ok();
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("RemoveExclusionReason")]
        public async Task<ActionResult> RemoveExclusionReason([FromBody] ExclusionReasonAwardingModel exclusionReasonAwardingModel)
        {
            exclusionReasonAwardingModel.OfferId = Util.Decrypt(exclusionReasonAwardingModel.offerIdString);
            await _offerAppService.RemoveExclusionReason(exclusionReasonAwardingModel);
            return Ok();
        }


        [Route("GetCanIgnoreMandatoryValidationColumnsId")]
        public async Task<List<int>> GetCanIgnoreMandatoryValidationColumnsId()
        {
            try
            {
                return await _offerAppService.GetCanIgnoreMandatoryValidationColumnsId();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("UpdateIfCorporationSmallOrMedium/{offerIdString}")]
        public async Task<string> UpdateIfCorporationSmallOrMedium(string offerIdString)
        {
            int offerId = Util.Decrypt(offerIdString);
            var result = await _offerAppService.UpdateCorporationSize(offerId);
            return result;
        }

        [Route("UpdateIsCorporatioIncludedInMoneyMarket/{offerIdString}")]
        public async Task<string> UpdateIsCorporatioIncludedInMoneyMarket(string offerIdString)
        {
            int offerId = Util.Decrypt(offerIdString);
            var result = await _offerAppService.UpdateIsCorporatioExistedInMoneyMarket(offerId);
            return result;
        }

        [Route("UpdateLocalContentPercentage/{offerIdString}")]
        public async Task<string> UpdateLocalContentPercentage(string offerIdString)
        {
            int offerId = Util.Decrypt(offerIdString);
            var result = await _offerAppService.UpdateLocalContentBaseLine(offerId);
            return result;
        }

        [Route("UpdateLocalContentDesiredPercentage/{offerIdString}")]
        public async Task<string> UpdateLocalContentDesiredPercentage(string offerIdString)
        {
            int offerId = Util.Decrypt(offerIdString);
            var result = await _offerAppService.UpdateLocalContentDesiredPercentage(offerId);
            return result;
        }

        [Route("CalculateOffersPreferences/{tenderStringId}")]
        public async Task<bool> CalculateOffersPreferences(string tenderStringId)
        {
            try
            {
                int tenderId = Util.Decrypt(tenderStringId);
                await _offerAppService.CalculateOfferFinancialPreferences(tenderId);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

