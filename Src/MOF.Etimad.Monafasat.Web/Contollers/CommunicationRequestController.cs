using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Negotiation;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Helpers;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class CommunicationRequestController : BaseController
   {
      private readonly int _pageSize = (int)PageSize.Twelve;
      private readonly IWebHostEnvironment _hostingEnvironment;
      private IApiClient _ApiClient;
      private readonly ILogger<TenderController> _logger;

      public CommunicationRequestController(IWebHostEnvironment hostingEnvironment, IApiClient ApiClient, ILogger<TenderController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _hostingEnvironment = hostingEnvironment;
         _ApiClient = ApiClient;
         _logger = logger;
      }

      [Authorize(Roles = RoleNames.UnitManagerUser + "," + RoleNames.UnitSecretaryUser)]
      public ActionResult TenderIndexUnitStage(string Type)
      {
         return View();
      }

      public ActionResult Index()
      {
         var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
         if (User.IsInRole(RoleNames.supplier))
            return RedirectToAction("AllSuppliersTenders", "Tender");
         if (!((User.IsInRole(RoleNames.TechnicalCommitteeUser)) || (User.IsInRole(RoleNames.MonafasatAccountManager) && roles.Count() == 1) || (User.IsInRole(RoleNames.UnitManagerUser) || User.IsInRole(RoleNames.UnitSecretaryUser))))
            return View();
         if (User.IsInRole(RoleNames.TechnicalCommitteeUser))
            return RedirectToAction("SupplierEnquiryList", "Enquiry");
         if (User.IsInRole(RoleNames.MonafasatAccountManager) && roles.Count() == 1)
            return RedirectToAction(nameof(Index), "Block");
         if (User.IsInRole(RoleNames.UnitManagerUser) || User.IsInRole(RoleNames.UnitSecretaryUser))
            return RedirectToAction(nameof(TenderIndexUnitStage));
         if (User.IsInRole(RoleNames.OffersCheckSecretary))
            return RedirectToAction("Tender", "TenderIndexAwardingStage");
         return View();
      }

      #region Tender Communication Request
      public async Task<ActionResult> GetSuppliersCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<CommunicationRequestGrid>("CommunicationRequest/GetSuppliersCommunicationRequestsGrid", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }
      public async Task<ActionResult> GetAgencyCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<CommunicationRequestGrid>("CommunicationRequest/GetAgencyCommunicationRequestsGrid", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }
      #endregion

      #region Extend Offers Validity
      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpPost]
      [Route("CommunicationRequest/AddExtendOffersValidity")]
      public async Task<ActionResult> AddExtendOffersValidity(ExtendOffersValiditySavingModel model)
      {
         string _RequestIdString = Util.Encrypt(model.AgencyRequestId);
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(model);
         }
         try
         {
            string _agencyRequestIdString = await _ApiClient.PostAsync("CommunicationRequest/AddExtendOffersValidityRequest", null, model);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction("ExtendOffersValidity", "CommunicationRequest", new { agencyRequestIdString = _agencyRequestIdString, TenderIdString = model.TenderIdString });
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("ExtendOffersValidity", "CommunicationRequest", new { agencyRequestIdString = _RequestIdString, TenderIdString = model.TenderIdString });
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Tender", "TenderIndexAwardingStage");
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }
      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpGet("CommunicationRequest/ExtendOffersValidity/{agencyRequestIdString}/{TenderIdString?}")]
      public async Task<ActionResult> ExtendOffersValidity(string agencyRequestIdString, string TenderIdString)
      {
         try
         {
            ExtendOffersValidityModel model = await _ApiClient.GetAsync<ExtendOffersValidityModel>("CommunicationRequest/GetExtendOffersValidity/" + Util.Decrypt(agencyRequestIdString) + "/" + Util.Decrypt(TenderIdString), null);
            if (model.StatusId != (int)Enums.TenderStatus.OffersAwarding)
               AddError(Resources.CommunicationRequest.ErrorMessages.TenderNotinOfferAwarding);
            return View(model);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser)]
      public async Task<ActionResult> GetTenderOffersPagingAsync(string tenderIdString, int extendOfferValidityId, int extendofferValidityStatusId)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetQueryResultAsync<ExtendOffersGridModel>("CommunicationRequest/GetTenderOffersPagingAsync/ " + tenderId + "/" + extendOfferValidityId + "/" + extendofferValidityStatusId, null);
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpGet("CommunicationRequest/OfferFiles/{offerid}")]
      public async Task<ActionResult> OfferFiles(string offerid)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            ExtendOffersDisplayFilesModel model = await _ApiClient.GetAsync<ExtendOffersDisplayFilesModel>("CommunicationRequest/GetOfferToExtendbyId/" + Util.Decrypt(offerid), null);
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }
      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.UnitManagerUser)]
      [HttpGet("CommunicationRequest/OfferNegotiationFiles/{offerid}")]
      public async Task<ActionResult> OfferNegotiationFiles(string offerid)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            OfferNegotiationFileModel model = await _ApiClient.GetAsync<OfferNegotiationFileModel>("CommunicationRequest/GetOfferFilesById/" + Util.Decrypt(offerid), null);
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitSecretaryUser)]
      [HttpGet("CommunicationRequest/OfferDetail/{combinedId}")]
      public async Task<ActionResult> OfferDetail(string combinedId)

      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            OfferDetailModel model = await _ApiClient.GetAsync<OfferDetailModel>("CommunicationRequest/GetOfferDetails/" + Util.Decrypt(combinedId), null);
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> GetCombinedSuppliersGridAsync(string offerid)
      {
         try
         {
            // get all data from api at once 
            var result = await _ApiClient.GetQueryResultAsync<CombinedSupplierModel>("CommunicationRequest/GetCombinedSuppliers/" + Util.Decrypt(offerid), null);
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpGet("CommunicationRequest/CombinedSuppliers/{offeridString}/{tenderIdString}/{CombinedIdString?}")]
      public async Task<ActionResult> CombinedSuppliers(string offeridString, string tenderIdString, string CombinedIdString = null)
      {
         try
         {


            int offerId = Util.Decrypt(offeridString);
            int tenderId = Util.Decrypt(tenderIdString);
            int? combinedIdString = null;
            if (!string.IsNullOrEmpty(CombinedIdString))
            {
               combinedIdString = Util.Decrypt(CombinedIdString);
            }
            OfferDetailModel model = await _ApiClient.GetAsync<OfferDetailModel>("CommunicationRequest/GeOfferByTenderIdAndOfferIdForOpening/" + tenderId + "/" + offerId + "/" + combinedIdString, null);
            return View(model);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      [HttpGet("CommunicationRequest/ExtendOffersValiditySupplier/{agencyCommunicationRequestId}")]
      public async Task<ActionResult> ExtendOffersValiditySupplier(string agencyCommunicationRequestId)
      {
         try
         {
            SupplierExtendOffersValidityViewModel model = await _ApiClient.GetAsync<SupplierExtendOffersValidityViewModel>("CommunicationRequest/GetSupplierExtendOffersValidityForSupplier/" + Util.Decrypt(agencyCommunicationRequestId), null);
            if (model.SupplierExtendOffersValidityStatusId != (int)Enums.SupplierExtendOffersValidityStatus.Sent)
               await GetSupplierExtendOffersValidityModeAttahmentsData(model);
            return View(model);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return View();
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      private async Task GetSupplierExtendOffersValidityModeAttahmentsData(SupplierExtendOffersValidityViewModel model)
      {
         var item = model.ExtendOffersValidityAttachementViewModel ?? new ExtendOffersValidityAttachementViewModel();
         model.InitialGuaranteerefId = "";
         model.InitialGuaranteerefId += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.Name;
         model.InitialGuaranteerefId = model.InitialGuaranteerefId.Remove(0, 1);
      }

      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost]
      public async Task<ActionResult> AcceptExtendOffersValidityBySupplierAsync(string extendOffersValidtyId)
      {
         try
         {
            var eOVId = Util.Decrypt(extendOffersValidtyId);
            var result = await _ApiClient.PostAsync("CommunicationRequest/AcceptExtendOffersValidityBySupplier/" + eOVId, null, null);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            AddMessage(Resources.CommunicationRequest.Messages.YouHaveToUploadInitialGuarantee);
            return Ok(result);
         }
         catch (NullReferenceException ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost]
      public async Task<ActionResult> AddInitialGuaranteeAttachmentToOfferAsync(string guaranteeReferenceId, string extendOffersValiditySupplierString)
      {
         try
         {
            var extendOffersValiditySupplierId = Util.Decrypt(extendOffersValiditySupplierString);
            ExtendOffersValidityAttachementViewModel extendOffersValidityAttachement = PrepareExtendOfferValidityAttachmentForInsert(guaranteeReferenceId);
            var result = await _ApiClient.PostAsync("CommunicationRequest/AddInitialGuaranteeAttachmentToOfferAsync/" + extendOffersValiditySupplierId, null, extendOffersValidityAttachement);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return Ok(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (ArgumentNullException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (ArgumentException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost]
      public async Task<ActionResult> RejectExtendOffersValidityBySupplierAsync(string extendOffersValidtyId)
      {
         try
         {
            int eOVId = Util.Decrypt(extendOffersValidtyId);
            var result = await _ApiClient.PostAsync("CommunicationRequest/RejectExtendOffersValidityBySupplier/" + eOVId, null, null);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return Ok(result);
         }
         catch (NullReferenceException ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }



      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost]
      public async Task<ActionResult> CancelSupplierExtendOfferValidity(string extendOffersValidtyId)
      {
         try
         {
            int eOVId = Util.Decrypt(extendOffersValidtyId);
            var result = await _ApiClient.PostAsync("CommunicationRequest/CancelSupplierExtendOfferValidity/" + eOVId, null, null);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return Ok(result);
         }
         catch (NullReferenceException ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }









      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      public async Task<ActionResult> SendToApproveExtendOffersRequestAsync(string AgencyRequestId)
      {
         try
         {
            int agencyRequestId = Util.Decrypt(AgencyRequestId);
            await _ApiClient.PostAsync("CommunicationRequest/SendToApproveExtendOffersRequestAsync/" + agencyRequestId, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> RejectExtendOffersRequestAsync(string AgencyRequestId, string RejectionReason)
      {
         try
         {
            RejectNegotiation model = new RejectNegotiation
            {
               RejectionReason = RejectionReason,
               NegotiationId = Util.Decrypt(AgencyRequestId)
            };
            if (RejectionReason.Length > 1000)
            {
               return JsonErrorMessage(Resources.CommunicationRequest.ErrorMessages.RejectionReasonMustbeLessThan1000Caracter);
            }
            int agencyRequestId = Util.Decrypt(AgencyRequestId);
            await _ApiClient.PostAsync("CommunicationRequest/RejectExtendOffersRequestAsync", null, model);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);

         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> DeleteExtendOffersRequestAsync(string AgencyRequestId)
      {
         try
         {
            int agencyRequestId = Util.Decrypt(AgencyRequestId);
            await _ApiClient.PostAsync("CommunicationRequest/DeleteExtendOffersRequestAsync/" + agencyRequestId, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(RoleNames.ApproveExtendOffersRequestPolicy)]
      public async Task<ActionResult> ApproveExtendOffersRequestAsync(string tenderIdString, string AgencyRequestId, string verificationCode)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/ApproveExtendOffersRequestAsync/" + Util.Decrypt(tenderIdString) + "/" + Util.Decrypt(AgencyRequestId) + "/" + verificationCode, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      #endregion

      #region Negotiation

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpGet("CommunicationRequest/CreateNegotiationRequest/{TenderIdString}")]
      public async Task<ActionResult> CreateNegotiationRequest(string TenderIdString)
      {
         try
         {
            var lookups = await _ApiClient.GetListAsync<LookupModel>("LookUp/GetNegotiationTypes", null);
            var isAllowed = await _ApiClient.GetAsync<bool>("CommunicationRequest/isAllowedToApplySecondStageNegotiation/" + TenderIdString, null);

            var model = new StartNegotiationModel { enNegotiationTypeId = 0, TenderIdString = TenderIdString, NegotaitionTypes = lookups.Where(d => d.Id == (!isAllowed ? (int)Enums.enNegotiationType.FirstStage : d.Id)).Select(f => new SelectListItem { Text = f.Name, Value = f.Id.ToString() }).ToList() };
            return View(model);
         }

         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(CreateNegotiationRequest), new { TenderIdString = TenderIdString });
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(CreateNegotiationRequest), new { TenderIdString = TenderIdString });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.supplier + "," + RoleNames.UnitSecretaryUser)]
      [HttpGet]
      [Route("CommunicationRequest/GetNegotiationQuantityTable_NewNEGO/{negotiationId}")]
      public async Task<ActionResult> GetNegotiationQuantityTable_NewNEGO(string negotiationId)
      {
         try
         {
            var model = await _ApiClient.GetAsync<SecondStageNegotiationViewModel>("CommunicationRequest/GetNegotiationQuantityTable/" + negotiationId, null);
            return Json(model);
         }
         catch (Exception ex)
         {
            throw;
         }
      }


      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.supplier + "," + RoleNames.UnitSecretaryUser)]
      [HttpGet]
      [Route("CommunicationRequest/GetNegotiationQuantityTable/{negotiationId}")]
      public async Task<ActionResult> GetNegotiationQuantityTable(string negotiationId)
      {
         try
         {
            var model = await _ApiClient.GetAsync<SecondStageNegotiationViewModel>("CommunicationRequest/GetNegotiationQuantityTable/" + negotiationId, null);
            return Json(model);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      [HttpPost]
      [Route("CommunicationRequest/RemoveQuantityTableItem/{negotiationQuantityTableItemId}")]
      public async Task<AjaxResponse<NegotiationQuantityTableItemModel>> RemoveQuantityTableItem(string negotiationQuantityTableItemId)
      {
         var response = new AjaxResponse<NegotiationQuantityTableItemModel>();

         try
         {
            NegotiationQuantityTableItemModel editedItem = await _ApiClient.PostAsync<NegotiationQuantityTableItemModel>("CommunicationRequest/DeleteNegotiationQuantityItem", null, negotiationQuantityTableItemId);
            response.enMessageType = enAjaxResponseMessageType.success;
            response.DataList.Add(editedItem);
            response.Message = Resources.SharedResources.Messages.DataSaved;
            return response;
         }
         catch (BusinessRuleException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (UnHandledAccessException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }

      }
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      [HttpGet]
      [Route("CommunicationRequest/DeleteNegotiationQuantityItem/{negotiationQuantityTableItemId}")]
      public async Task<AjaxResponse<NegotiationQuantityTableItemModel>> DeleteNegotiationQuantityItem(string negotiationQuantityTableItemId)
      {
         var response = new AjaxResponse<NegotiationQuantityTableItemModel>();

         try
         {
            NegotiationQuantityTableItemModel editedItem = await _ApiClient.GetAsync<NegotiationQuantityTableItemModel>("CommunicationRequest/DeleteNegotiationQuantityItem/" + negotiationQuantityTableItemId, null);
            response.enMessageType = enAjaxResponseMessageType.success;
            response.DataList.Add(editedItem);
            response.Message = Resources.SharedResources.Messages.DataSaved;
            return response;
         }
         catch (BusinessRuleException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (UnHandledAccessException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }

      }


      [HttpGet]
      [Route("CommunicationRequest/DeleteNegotiationTable/{tableId}")]
      public async Task<IActionResult> DeleteNegotiationTable(string tableId)
      {
         var response = new AjaxResponse<NegotiationQuantityTableItemModel>();

         try
         {
            response = await _ApiClient.GetAsync<AjaxResponse<NegotiationQuantityTableItemModel>>("CommunicationRequest/DeleteNegotiationTable/" + tableId, null);
            return Json(response);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);

         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            return JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);

         }

      }


      //[Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      //[HttpGet]
      //[Route("CommunicationRequest/DeleteNegotiationItemFromTable/{TableId}/{ItemNumber}")]
      public async Task<AjaxResponse<NegotiationQuantityTableItemModel>> DeleteNegotiationItemFromTable(string TableId, string ItemNumber)
      {
         var response = new AjaxResponse<NegotiationQuantityTableItemModel>();

         try
         {
            NegotiationQuantityTableItemModel editedItem = await _ApiClient.GetAsync<NegotiationQuantityTableItemModel>("CommunicationRequest/DeleteNegotiationQuantityItem/" + TableId, null);
            response.enMessageType = enAjaxResponseMessageType.success;
            response.DataList.Add(editedItem);
            response.Message = Resources.SharedResources.Messages.DataSaved;
            return response;
         }
         catch (BusinessRuleException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (UnHandledAccessException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }

      }


      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      [HttpPost]
      [Route("CommunicationRequest/RemoveQuantityTable/{negotiationId}")]
      public async Task<AjaxResponse<NegotiationQuantityTableModel>> RemoveQuantityTable(string negotiationQuantityTableId)
      {
         var response = new AjaxResponse<NegotiationQuantityTableModel>();

         try
         {
            NegotiationQuantityTableModel editedItem = await _ApiClient.PostAsync<NegotiationQuantityTableModel>("CommunicationRequest/UpdateNegotiationQuantityTableItem", null, negotiationQuantityTableId);
            response.enMessageType = enAjaxResponseMessageType.success;
            response.DataList.Add(editedItem);
            response.Message = Resources.SharedResources.Messages.DataSaved;
            return response;
         }
         catch (BusinessRuleException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (UnHandledAccessException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }

      }


      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      [HttpPost]
      [Route("CommunicationRequest/SaveEditedItem")]
      public async Task<AjaxResponse<NegotiationQuantityTableItemModel>> SaveEditedItem(NegotiationQuantityTableItemModel QuantityItem)
      {
         var response = new AjaxResponse<NegotiationQuantityTableItemModel>();

         try
         {
            NegotiationQuantityTableItemModel editedItem = await _ApiClient.PostAsync<NegotiationQuantityTableItemModel>("CommunicationRequest/UpdateNegotiationQuantityTableItem", null, QuantityItem);
            response.enMessageType = enAjaxResponseMessageType.success;
            response.DataList.Add(editedItem);
            response.Message = Resources.SharedResources.Messages.DataSaved;
            return response;
         }
         catch (BusinessRuleException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (UnHandledAccessException ex)
         {
            JsonErrorMessage(ex.Message);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);
            response.enMessageType = enAjaxResponseMessageType.danger;
            return response;
         }
      }
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpPost("CommunicationRequest/CreateNegotiationRequestd")]
      public async Task<ActionResult> CreateNegotiationRequest(StartNegotiationModel model)
      {

         try
         {
            if (!ModelState.IsValid)
            {
               AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
               return View(model);
            }

            var negotiationId = await _ApiClient.PostAsync("CommunicationRequest/CreateNegotiationRequest", null, model);
            if ((int)enNegotiationType.SecondStage == model.enNegotiationTypeId)
               return RedirectToAction(nameof(CreateSecondNegotiationRequestAsync), new { NegotiationIdString = negotiationId });
            return RedirectToAction(nameof(CreateNegotiationFirstStage), new { TenderIdString = model.TenderIdString, NegotiationIdString = negotiationId });
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(CreateNegotiationRequest), new { TenderIdString = model.TenderIdString });
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(TenderController.TenderIndexAwardingStage), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex
         )
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      [HttpPost("CommunicationRequest/SaveSecondStageNegotiation")]
      public async Task<ActionResult> SaveSecondStageNegotiation(SecondStageNegotiationModel NogotiationObj, List<NegotiationQuantityTableModel> QuantityTable, bool IsSend)
      {
         try
         {

            var model = new NegotiationAgencySecondStageEditModel();
            model.NegotiationId = NogotiationObj.NegotiationId;
            model.TenderIdString = NogotiationObj.TenderIdString;
            model.NegotiationReasonId = NogotiationObj.NegotiationReasonId;
            model.NegotiationQuantityTableModels = QuantityTable;
            model.IsSend = IsSend;
            AjaxResponse<string> response = await _ApiClient.PostAsync<AjaxResponse<string>>("CommunicationRequest/SaveSecondStageNegotiation", null, model);

            return Json(response);
         }
         catch (BusinessRuleException ex)
         {
            JsonErrorMessage(ex.Message);
            return BadRequest();
         }
         catch (UnHandledAccessException ex)
         {
            JsonErrorMessage(ex.Message);
            return BadRequest();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);
            return BadRequest();
         }

      }
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.supplier + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
      [HttpGet("CommunicationRequest/CreateSecondStageNegotiation/{NegotiationIdString}")]
      public async Task<ActionResult> CreateSecondStageNegotiation(string NegotiationIdString)
      {
         try
         {
            //var lookups = await _ApiClient.GetListAsync<LookupModel>("LookUp/GetReductionReasons", null);
            // OfferModel secondStageNegotiation = await _ApiClient.GetAsync<OfferModel>("CommunicationRequest/GetSecondNegotiation/" + NegotiationId, null, "");
            SecondStageNegotiationModelcs secondStageNegotiation = await _ApiClient.GetAsync<SecondStageNegotiationModelcs>("CommunicationRequest/GetSecondStageNegotiation/" + NegotiationIdString, null);
            //secondStageNegotiation.ReductionReasons = lookups.Where(w => w.Id == (int)enNegotiationFirstStageRejectionReasons.HighPriceThanBudget).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            //secondStageNegotiation.SupplierList = lookups.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();

            return View(secondStageNegotiation);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.supplier + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.supplier + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.UnitManagerUser)]

      //[Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," +
      //   RoleNames.OffersPurchaseManager + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," +
      //   RoleNames.OffersCheckManager + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.supplier)]
      [HttpGet("CommunicationRequest/CreateSecondNegotiation/{NegotiationIdString}")]
      public async Task<ActionResult> CreateSecondNegotiationAsync(string NegotiationIdString)
      {
         try
         {
            SecondStageNegotiationModelcs secondStageNegotiation = await _ApiClient.GetAsync<SecondStageNegotiationModelcs>("CommunicationRequest/GetSecondNegotiation/" + NegotiationIdString, null);
            return View(secondStageNegotiation);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.supplier + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.supplier + "," + RoleNames.UnitSecretaryUser + "," + RoleNames.UnitManagerUser)]
      [HttpGet("CommunicationRequest/CreateSecondNegotiationRequest/{NegotiationIdString}")]
      public async Task<ActionResult> CreateSecondNegotiationRequestAsync(string NegotiationIdString)
      {
         try
         {
            SecondStageNegotiationModelcs secondStageNegotiation = await _ApiClient.GetAsync<SecondStageNegotiationModelcs>("CommunicationRequest/GetSecondNegotiation_NEW/" + NegotiationIdString, null);
            return View(secondStageNegotiation);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }


      //[Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      //[HttpPost("CommunicationRequest/UpdateInSupplierQuantityTable")]
      //public async Task<ActionResult> UpdateInSupplierQuantityTable(SaveTableModel table)
      //{
      //   try
      //   {
      //      table.TenderId = Util.Decrypt(table.TenderId).ToString();
      //      var result = await _ApiClient.PostAsync<OfferModel>("CommunicationRequest/UpdateInSupplierQuantityTable", null, table);
      //      return Json(result);
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      return JsonErrorMessage(ex.Message);
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //   }
      //}

      [Authorize(Roles = RoleNames.supplier)]
      [HttpGet("CommunicationRequest/SupplierNegotiation/{TenderIdString}/{NegotiationIdString}")]
      public async Task<IActionResult> SupplierNegotiation(string TenderIdString, string NegotiationIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<NegotiationSupplierViewModel>("CommunicationRequest/GetSupplierNegotiation/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);


            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      [HttpGet("CommunicationRequest/SupplierNegotiationDetails/{TenderIdString}/{NegotiationIdString}")]
      public async Task<IActionResult> SupplierNegotiationDetails(string TenderIdString, string NegotiationIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<NegotiationSupplierViewModel>("CommunicationRequest/GetSupplierNegotiationFirstStageInfo/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);


            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
      }



      [Authorize(Roles = RoleNames.supplier)]
      [HttpGet("CommunicationRequest/AgreeOnFirstStageOffer")]
      public async Task<IActionResult> AgreeOnFirstStageOffer(string TenderIdString, string NegotiationIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<bool>("CommunicationRequest/AgreeOnOfferNegotiationFirstStage/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));

         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
      }
      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost("CommunicationRequest/AgreeOnFirstStageNegotiationBySupplier")]
      public async Task<IActionResult> AgreeOnFirstStageNegotiationBySupplier(string TenderIdString, string NegotiationIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<bool>("CommunicationRequest/AgreeOnFirstStageNegotiationBySupplier/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);
            return Json(new { success = true, message = Resources.SharedResources.Messages.DataSaved });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost("CommunicationRequest/AgreeWithExtraDiscountOnNegotiationFirstStage")]
      public async Task<IActionResult> AgreeWithExtraDiscountOnNegotiationFirstStage(AcceptNegotiationWithExtraDiscountModel extraDiscountModel)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/AgreeWithExtraDiscountOnNegotiationFirstStage/", null, extraDiscountModel);
            return Json(new { success = true, message = Resources.SharedResources.Messages.DataSaved });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      [HttpGet("CommunicationRequest/DisAgreeOnFirstStageOffer")]
      public async Task<IActionResult> DisAgreeOnFirstStageOffer(string TenderIdString, string NegotiationIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<bool>("CommunicationRequest/DisAgreeOfferAfterNegotiationFirstStage/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));

         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost("CommunicationRequest/DisAgreeFirstStageOfferNegotiation")]
      public async Task<IActionResult> DisAgreeFirstStageOfferNegotiation(string TenderIdString, string NegotiationIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<bool>("CommunicationRequest/DisAgreeOfferAfterNegotiationFirstStage/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);
            return Json(new { success = true, message = Resources.SharedResources.Messages.DataSaved });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpGet("CommunicationRequest/Negotiation/{NegotiationIdString}")]
      public async Task<IActionResult> Negotiation(string NegotiationIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<NegotiationAgencyPageModel>("CommunicationRequest/GetNegotiationData/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);
            var lookups = await _ApiClient.GetListAsync<LookupModel>("LookUp/GetReductionReasons", null);
            result.NegotiationFirstStageModel.ReductionReasons = lookups.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();


            if (result.NegotiationFirstStageModel.StatusId == (int)Enums.enNegotiationStatus.UnderUpdate && (User.IsInRole(RoleNames.OffersCheckSecretary) || User.IsInRole(RoleNames.OffersPurchaseSecretary)))
            {
               return RedirectToAction("CreateNegotiation", "CommunicationRequest", new { TenderIdString = result.NegotiationFirstStageModel.TenderIdString, NegotiationIdString = NegotiationIdString });
            }
            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Index", "Tender");
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Index", "Tender");
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("Index", "Tender");
         }
      }
      private async Task GetEditModeAttahmentsData(NegotiationAgencyFirstStageEditModel model)
      {
         var item = model.negotiationFirstStageViewModel.attachmentViewModel;
         model.ReductionLetterrefId = "";
         if (item != null && item.FileNetReferenceId != null)
         {
            model.ReductionLetterrefId += item.FileNetReferenceId + ":" + item.Name;
            //  model.ReductionLetterrefId = model.ReductionLetterrefId.Remove(0, 1);

         }

      }
      private async Task GetEditModeAttahmentsDataForFirstStageNegotiation(CreateNegotiationFirstStageDataModel model)
      {
         var item = model.negotiationFirstStageViewModel.attachmentViewModel;
         model.ReductionLetterrefId = "";
         if (item != null && item.FileNetReferenceId != null)
         {
            model.ReductionLetterrefId += item.FileNetReferenceId + ":" + item.Name;
            //  model.ReductionLetterrefId = model.ReductionLetterrefId.Remove(0, 1);

         }

      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      [HttpGet("CommunicationRequest/CreateNegotiation/{TenderIdString}/{NegotiationIdString}")]
      public async Task<IActionResult> CreateNegotiation(string TenderIdString, string NegotiationIdString)
      {
         try

         {
            var result = await _ApiClient.GetAsync<NegotiationAgencyPageModel>("CommunicationRequest/GetNegotiationData/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);
            var lookups = await _ApiClient.GetListAsync<LookupModel>("LookUp/GetReductionReasons", null);
            var firstOffer = result.NegotiationOfferModels.OrderByDescending(d => d.OfferAmount).FirstOrDefault();
            //if (result.NegotiationTenderModel.Amount >= (firstOffer == null ? 0 : firstOffer.OfferAmount))
            //{
            // lookups = lookups.Where(d => d.Id != (int)Enums.enNegotiationFirstStageRejectionReasons.HighPriceThanBudget).ToList();
            //   }

            if (result.NegotiationFirstStageModel.StatusId != (int)Enums.enNegotiationStatus.New
               && result.NegotiationFirstStageModel.StatusId != (int)Enums.enNegotiationStatus.UnderUpdate
               && result.NegotiationFirstStageModel.StatusId != (int)Enums.enNegotiationStatus.UnitSpecialistReject
               && result.NegotiationFirstStageModel.StatusId != (int)Enums.enNegotiationStatus.CheckManagerReject
               && (User.IsInRole(RoleNames.OffersCheckSecretary) || User.IsInRole(RoleNames.OffersPurchaseSecretary)))
            {
               return RedirectToAction("Negotiation", "CommunicationRequest", new { TenderIdString = TenderIdString, NegotiationIdString = NegotiationIdString });
            }



            result.NegotiationFirstStageModel.ReductionReasons = lookups.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            await GetEditModeAttahmentsData(result.NegotiationFirstStageModel);
            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Index", "Tender");
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Index", "Tender");
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("Index", "Tender");
         }
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpGet("CommunicationRequest/CreateNegotiationFirstStage/{TenderIdString}/{NegotiationIdString}")]
      public async Task<IActionResult> CreateNegotiationFirstStage(string TenderIdString, string NegotiationIdString)
      {
         try

         {
            var result = await _ApiClient.GetAsync<NegotiationAgencyPageModel>("CommunicationRequest/GetNegotiationDataFirstStage/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);
            var lookups = await _ApiClient.GetListAsync<LookupModel>("LookUp/GetReductionReasons", null);
            // var firstOffer = result.NegotiationOfferModels.OrderByDescending(d => d.OfferAmount).FirstOrDefault();

            //if (result.NegotiationFirstStageModel.StatusId != (int)Enums.enNegotiationStatus.New
            //   && result.NegotiationFirstStageModel.StatusId != (int)Enums.enNegotiationStatus.UnderUpdate
            //   && result.NegotiationFirstStageModel.StatusId != (int)Enums.enNegotiationStatus.UnitSpecialistReject
            //   && result.NegotiationFirstStageModel.StatusId != (int)Enums.enNegotiationStatus.CheckManagerReject
            //   && (User.IsInRole(RoleNames.OffersCheckSecretary) || User.IsInRole(RoleNames.OffersPurchaseSecretary)))
            //{
            //   return RedirectToAction("Negotiation", "CommunicationRequest", new { TenderIdString = TenderIdString, NegotiationIdString = NegotiationIdString });
            //}
            result.CreateNegotiationFirstStageData.ReductionReasons = lookups.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            await GetEditModeAttahmentsDataForFirstStageNegotiation(result.CreateNegotiationFirstStageData);
            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Index", "Tender");
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Index", "Tender");
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("Index", "Tender");
         }
      }


      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager)]
      [HttpGet("CommunicationRequest/Negotiation/{TenderIdString}/{NegotiationIdString}")]
      public async Task<IActionResult> Negotiation(string TenderIdString, string NegotiationIdString)
      {
         try

         {
            var result = await _ApiClient.GetAsync<NegotiationAgencyPageModel>("CommunicationRequest/GetNegotiationData/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);
            var lookups = await _ApiClient.GetListAsync<LookupModel>("LookUp/GetReductionReasons", null);
            result.NegotiationFirstStageModel.ReductionReasons = lookups.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            await GetEditModeAttahmentsData(result.NegotiationFirstStageModel);


            if (result.NegotiationFirstStageModel.StatusId == (int)Enums.enNegotiationStatus.UnderUpdate && (User.IsInRole(RoleNames.OffersCheckSecretary) || User.IsInRole(RoleNames.OffersPurchaseSecretary)))
            {
               return RedirectToAction("CreateNegotiation", "CommunicationRequest", new { TenderIdString = result.NegotiationFirstStageModel.TenderIdString, NegotiationIdString = NegotiationIdString });
            }


            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("DetailsForAll", "Tender", new { STenderId = TenderIdString });
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("DetailsForAll", "Tender", new { STenderId = TenderIdString });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("DetailsForAll", "Tender", new { STenderId = TenderIdString });
         }
      }
      //Offline


      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckManager)]
      [HttpGet("CommunicationRequest/NewNegotiation/{TenderIdString}/{NegotiationIdString}")]
      public async Task<IActionResult> NewNegotiation(string TenderIdString, string NegotiationIdString)
      {
         try

         {
            var result = await _ApiClient.GetAsync<NegotiationAgencyPageModel>("CommunicationRequest/GetNegotiationDataFirstStage/" + TenderIdString + "/" + ((string.IsNullOrEmpty(NegotiationIdString) ? string.Empty : NegotiationIdString)), null);
            var lookups = await _ApiClient.GetListAsync<LookupModel>("LookUp/GetReductionReasons", null);
            result.CreateNegotiationFirstStageData.ReductionReasons = lookups.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            await GetEditModeAttahmentsDataForFirstStageNegotiation(result.CreateNegotiationFirstStageData);


            if (result.CreateNegotiationFirstStageData.StatusId == (int)Enums.enNegotiationStatus.UnderUpdate && (User.IsInRole(RoleNames.OffersCheckSecretary) || User.IsInRole(RoleNames.OffersPurchaseSecretary) || result.CreateNegotiationFirstStageData.IsUserHasAccessToLowBudgetTender == true))
            {
               return RedirectToAction("CreateNegotiationFirstStage", "CommunicationRequest", new { TenderIdString = result.CreateNegotiationFirstStageData.TenderIdString, NegotiationIdString = NegotiationIdString });
            }


            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("DetailsForAll", "Tender", new { STenderId = TenderIdString });
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("DetailsForAll", "Tender", new { STenderId = TenderIdString });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("DetailsForAll", "Tender", new { STenderId = TenderIdString });
         }
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      [HttpGet]
      [Route("CommunicationRequest/LoadNegotiationFirstStageDataAsync/{Id}")]
      public async Task<IActionResult> LoadNegotiationFirstStageDataAsync(string Id)
      {
         var negotiationModel = await _ApiClient.GetAsync<NegotiationAgencyFirstStageEditModel>("CommunicationRequest/GetNegotiationFirstStageDatabyId/" + Id, null);
         var lookups = await _ApiClient.GetListAsync<LookupModel>("LookUp/GetReductionReasons", null);
         negotiationModel.ReductionReasons = lookups.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
         negotiationModel.StatusId = (int)enNegotiationStatus.CheckManagerPendingApprove;
         return PartialView("~/Views/CommunicationRequest/Partial/_CreateFirstStageNegotiation.cshtml", negotiationModel);


      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      [HttpPost]
      [Route("CommunicationRequest/CreateFirstStageNegotiation")]
      public async Task<ActionResult> CreateFirstStageNegotiation(NegotiationAgencyFirstStageEditModel negotiationModel)
      {

         string errorMessage = Resources.SharedResources.ErrorMessages.ModelDataError;

         try
         {
            if (!ModelState.IsValid || (string.IsNullOrEmpty(negotiationModel.ProjectNumber) && negotiationModel.NegotiationReasonId == (int)Enums.NegotiationFirstStageRejectionReasons.HighPriceThanBudget))
            {
               foreach (var modelState in ModelState.Values)
               {
                  foreach (var error in modelState.Errors)
                  {
                     errorMessage += error.ErrorMessage;
                  }
               }
               return Json(new { success = false, message = errorMessage });
            }
            else
            {
               bool isSaved = false;
               string succesMessage = Resources.SharedResources.Messages.DataSaved;
               switch (negotiationModel.ActionType)
               {
                  case enSubmitActionType.SaveAndSend:

                     negotiationModel.StatusId = (int)enNegotiationStatus.CheckManagerPendingApprove;
                     switch (negotiationModel.EnOperationType)
                     {
                        case enOperationType.Add:

                           negotiationModel.Attachment = PrepareAttachmentForInsert(negotiationModel);
                           isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/CreateFirstStageNegotiation", null, negotiationModel);
                           return Json(new { success = isSaved, message = isSaved ? succesMessage : errorMessage });
                        case enOperationType.Update:

                           negotiationModel.Attachment = PrepareAttachmentForInsert(negotiationModel);
                           isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/UpdateFirstStageNegotiation/", null, negotiationModel);
                           return Json(new { success = isSaved, message = isSaved ? succesMessage : errorMessage });
                        default:
                           return Json(new { success = isSaved, message = errorMessage });
                     }

                  case enSubmitActionType.SaveOnly:

                     switch (negotiationModel.EnOperationType)
                     {

                        case enOperationType.Add:

                           negotiationModel.StatusId = (int)enNegotiationStatus.UnderUpdate;
                           negotiationModel.Attachment = PrepareAttachmentForInsert(negotiationModel);
                           isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/CreateFirstStageNegotiation", null, negotiationModel);
                           return Json(new { success = isSaved, message = isSaved ? succesMessage : errorMessage });
                        case enOperationType.Update:

                           negotiationModel.Attachment = PrepareAttachmentForInsert(negotiationModel);
                           isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/UpdateFirstStageNegotiation/", null, negotiationModel);
                           return Json(new { success = isSaved, message = isSaved ? succesMessage : errorMessage });
                        default:
                           return Json(new { success = isSaved, message = errorMessage });
                     }
                  case enSubmitActionType.Preview:
                     var negotiationOffers = await _ApiClient.PostAsync<List<NegotiationOfferModel>>("CommunicationRequest/PreviewFirstStageNegotiationOffers", null, negotiationModel);
                     return PartialView("~/Views/CommunicationRequest/Partial/_OffersAfterDiscountPreview.cshtml", negotiationOffers);
                  default:
                     return Json(new { success = false });

               }
            }

         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            //    return RedirectToAction(nameof(Index));
            return Json(new
            {
               success = false,
               message = ex.Message
            });

         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            //            return RedirectToAction(nameof(Index));
            return Json(new { success = false, message = ex.Message });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            //   return RedirectToAction(nameof(Index));
            return Json(new { success = false, message = Resources.TenderResources.ErrorMessages.UnexpectedError });

         }
      }

      private static NegotiationAttachmentViewModel PrepareAttachmentForInsert(NegotiationAgencyFirstStageEditModel negotiationModel)
      {
         List<string> attachmentReferences = new List<string>();
         if (negotiationModel.ReductionLetterrefId != null)
         {
            string item = negotiationModel.ReductionLetterrefId;

            if (!string.IsNullOrEmpty(item))
            {
               if (item.Contains("/GetFile/"))
                  attachmentReferences.Add(item.Split("/GetFile/")[item.Split("/GetFile/").Length - 1]);
               else
                  attachmentReferences.Add(item);
            }
            var arr = item.Split(":");
            NegotiationAttachmentViewModel tenderAttachment = new NegotiationAttachmentViewModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.Negotiation, FileNetReferenceId = arr[0].StartsWith(',') ? arr[0].TrimStart(',') : arr[0] };
            negotiationModel.Attachment = tenderAttachment;
            return negotiationModel.Attachment;
         }
         return new NegotiationAttachmentViewModel();
      }
      private static NegotiationAttachmentViewModel PrepareAttachmentForInsertNegotiationFirstStage(CreateNegotiationFirstStageDataModel negotiationModel)
      {
         List<string> attachmentReferences = new List<string>();
         if (negotiationModel.ReductionLetterrefId != null)
         {
            string item = negotiationModel.ReductionLetterrefId;

            if (!string.IsNullOrEmpty(item))
            {
               if (item.Contains("/GetFile/"))
                  attachmentReferences.Add(item.Split("/GetFile/")[item.Split("/GetFile/").Length - 1]);
               else
                  attachmentReferences.Add(item);
            }
            var arr = item.Split(":");
            NegotiationAttachmentViewModel tenderAttachment = new NegotiationAttachmentViewModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.Negotiation, FileNetReferenceId = arr[0].StartsWith(',') ? arr[0].TrimStart(',') : arr[0] };
            negotiationModel.Attachment = tenderAttachment;
            return negotiationModel.Attachment;
         }
         return new NegotiationAttachmentViewModel();
      }

      private static ExtendOffersValidityAttachementViewModel PrepareExtendOfferValidityAttachmentForInsert(string guaranteeReferenceId)
      {
         List<string> attachmentReferences = new List<string>();
         if (guaranteeReferenceId != null)
         {
            if (!string.IsNullOrEmpty(guaranteeReferenceId))
            {
               if (guaranteeReferenceId.Contains("/GetFile/"))
                  attachmentReferences.Add(guaranteeReferenceId.Split("/GetFile/")[guaranteeReferenceId.Split("/GetFile/").Length - 1]);
               else
                  attachmentReferences.Add(guaranteeReferenceId);
            }
            var arr = guaranteeReferenceId.Split(":");
            ExtendOffersValidityAttachementViewModel extendOffersValidityAttachement = new ExtendOffersValidityAttachementViewModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.InitialGuarantee, FileNetReferenceId = arr[0] };
            return extendOffersValidityAttachement;
         }
         return new ExtendOffersValidityAttachementViewModel();
      }


      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpGet]
      public async Task<ActionResult> GetOffersListForFirstStageNegotiation([FromQuery] NegotiationOffersSearchModel negotiationOffersSearchModel)
      {

         try
         {
            var negotiationOffers = await _ApiClient.GetQueryResultAsync<NegotiationOfferModel>("CommunicationRequest/GetOffersListForFirstStageNegotiation", negotiationOffersSearchModel.ToDictionary());
            return Json(new PaginationModel(negotiationOffers.Items, negotiationOffers.TotalCount, negotiationOffers.PageSize, negotiationOffers.PageNumber, null));
         }

         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpGet]
      public async Task<ActionResult> GetOffersForFirstStageNegotiation([FromQuery] NegotiationOffersSearchModel negotiationOffersSearchModel)
      {

         try
         {
            var negotiationOffers = await _ApiClient.GetQueryResultAsync<NegotiationOfferModel>("CommunicationRequest/GetOffersForFirstStageNegotiation", negotiationOffersSearchModel.ToDictionary());
            return Json(new PaginationModel(negotiationOffers.Items, negotiationOffers.TotalCount, negotiationOffers.PageSize, negotiationOffers.PageNumber, null));
         }

         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpGet]
      public async Task<ActionResult> GetNotNegotitedOffersForFirstStageNegotiation([FromQuery] NegotiationOffersSearchModel negotiationOffersSearchModel)
      {

         try
         {
            var negotiationOffers = await _ApiClient.GetQueryResultAsync<NegotiationOfferModel>("CommunicationRequest/GetNotNegotitedOffersForFirstStageNegotiation", negotiationOffersSearchModel.ToDictionary());
            return Json(new PaginationModel(negotiationOffers.Items, negotiationOffers.TotalCount, negotiationOffers.PageSize, negotiationOffers.PageNumber, null));
         }

         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }



      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpGet]
      [Route("CommunicationRequest/PreviewOffersAfterDiscount/{DiscountValue}/{TenderIdString}")]
      public async Task<ActionResult> PreviewOffersAfterDiscount(decimal DiscountValue, string TenderIdString)
      {

         try
         {
            var negotiationOffers = await _ApiClient.GetListAsync<NegotiationOfferModel>("CommunicationRequest/PreviewFirstStageNegotiationOffers/" + TenderIdString + "/" + DiscountValue, null); ;
            return PartialView("~/Views/CommunicationRequest/Partial/_OffersAfterDiscountPreview.cshtml", negotiationOffers);


         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      [HttpGet]
      [Route("CommunicationRequest/OffersAfterDiscount/{TenderIdString}/{Discount}")]
      public PartialViewResult OffersAfterDiscount(string TenderIdString, decimal Discount) => PartialView("~/Views/CommunicationRequest/Partial/_OffersAfterDiscountPreview.cshtml", new List<NegotiationOfferModel>());

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpPost("CommunicationRequest/ChangeStatus")]
      public async Task<IActionResult> ChangeStatus(NegotiationAgencyActionStatusModel negotiationActionStatusModel)
      {
         try
         {

            bool isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/ChangeNegotiationFirstStageStatus", null, negotiationActionStatusModel);
            return Json(new { success = true, message = Resources.SharedResources.Messages.DataSaved });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }

      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpPost("CommunicationRequest/RejectFirstStageNegotiationRequest")]
      public async Task<IActionResult> RejectFirstStageNegotiationRequest(NegotiationAgencyActionStatusModel negotiationActionStatusModel)
      {
         try
         {
            negotiationActionStatusModel.Status = Enums.enNegotiationStatus.CheckManagerReject;
            if (negotiationActionStatusModel.RejectionReason == null || negotiationActionStatusModel.RejectionReason.Trim() == "")
            {
               return JsonErrorMessage("سبب الرفض مطلوب");
            }
            bool isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/ChangeNegotiationFirstStageStatus", null, negotiationActionStatusModel);
            return Json(new { success = true, message = Resources.SharedResources.Messages.DataSaved });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }

      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpPost("CommunicationRequest/ApproveFirstStageNegotiationRequest")]
      public async Task<IActionResult> ApproveFirstStageNegotiationRequest(NegotiationAgencyActionStatusModel negotiationActionStatusModel)
      {
         try
         {
            negotiationActionStatusModel.Status = Enums.enNegotiationStatus.SentToSuppliers;
            if (negotiationActionStatusModel.VerificationCode == null || string.IsNullOrEmpty(negotiationActionStatusModel.VerificationCode.Trim()))
            {
               return JsonErrorMessage("أدخل  كود التحقق ");


            }
            bool isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/ChangeNegotiationFirstStageStatus", null, negotiationActionStatusModel);
            return Json(new { success = true, message = Resources.SharedResources.Messages.DataSaved });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }

      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpPost("CommunicationRequest/ApproveNegotiationRequestFirstStage")]
      public async Task<IActionResult> ApproveNegotiationRequestFirstStage(NegotiationAgencyActionStatusModel negotiationActionStatusModel)
      {
         try
         {
            bool isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/ApproveNegotiationRequestFirstStage", null, negotiationActionStatusModel);
            return Json(new { success = true, message = Resources.SharedResources.Messages.DataSaved });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }

      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpPost("CommunicationRequest/RejectNegotiationRequestFirstStage")]
      public async Task<IActionResult> RejectNegotiationRequestFirstStage(NegotiationAgencyActionStatusModel negotiationActionStatusModel)
      {
         try
         {
            bool isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/RejectNegotiationRequestFirstStage", null, negotiationActionStatusModel);
            return Json(new { success = true, message = Resources.SharedResources.Messages.DataSaved });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
            //  return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }

      }

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      public async Task<ActionResult> SendToApproveNegotiationSecondStageByCheckManagerAsync(string NegotiationIdString, int Days, int Hours)
      {
         try
         {
            int negotiationId = Util.Decrypt(NegotiationIdString);
            await _ApiClient.PostAsync("CommunicationRequest/SendToApproveNegotiationSecondStageByCheckManagerAsync/" + negotiationId + "/" + Days + "/" + Hours, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary)]
      public async Task<ActionResult> ApproveNegotiationSecondStageByCheckManagerAsync(string NegotiationIdString, string verficationCode, int Days, int Hours)
      {
         try
         {
            int negotiationId = Util.Decrypt(NegotiationIdString);
            await _ApiClient.PostAsync("CommunicationRequest/ApproveNegotiationSecondStageByCheckManagerAsync/" + negotiationId + "/" + verficationCode + "/" + Days + "/" + Hours, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> RejectNegotiationSecondStageByCheckManagerAsync(string NegotiationIdString, string RejectionReason)
      {
         try
         {
            RejectNegotiation rejectNegotiation = new RejectNegotiation
            {
               RejectionReason = RejectionReason,
               NegotiationId = Util.Decrypt(NegotiationIdString)
            };
            int negotiationId = Util.Decrypt(NegotiationIdString);
            await _ApiClient.PostAsync("CommunicationRequest/RejectNegotiationSecondStageByCheckManagerAsync", null, rejectNegotiation);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
      public async Task<ActionResult> ApproveNegotiationSecondStageByUnitSecretaryAsync(string NegotiationIdString, string verficationCode)
      {
         try
         {
            int negotiationId = Util.Decrypt(NegotiationIdString);
            await _ApiClient.PostAsync("CommunicationRequest/ApproveNegotiationSecondStageByUnitSecretaryAsync/" + negotiationId + "/" + verficationCode, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.UnitSecretaryUser + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
      public async Task<ActionResult> RejectNegotiationSecondStageByUnitSecretaryAsync(string NegotiationIdString, string RejectionReason)
      {
         try
         {
            RejectNegotiation rejectNegotiation = new RejectNegotiation
            {
               RejectionReason = RejectionReason,
               NegotiationId = Util.Decrypt(NegotiationIdString)
            };
            int negotiationId = Util.Decrypt(NegotiationIdString);
            await _ApiClient.PostAsync("CommunicationRequest/RejectNegotiationSecondStageByUnitSecretaryAsync", null, rejectNegotiation);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      //[HttpPost]
      //[Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      //public async Task<JsonResult> SaveSecondStageNegotiationQuantityTables(SecondStageNegotiationViewModel Model)
      //{
      //   try
      //   {
      //      var response = await _ApiClient.PostAsync<OffersCompareViewModel>("CommunicationRequest/SaveSecondStageNegotiationQuantityTables", null, Model);
      //      return Json(response);
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      return JsonErrorMessage(ex.Message);
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //   }
      //}
      //[HttpPost]
      //[Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      //public async Task<JsonResult> SaveSecondStageNegotiationQuantityTables_NEWNEGO(SecondStageNegotiationViewModel Model)
      //{
      //   try
      //   {
      //      var response = await _ApiClient.PostAsync<OffersCompareViewModel>("CommunicationRequest/SaveSecondStageNegotiationQuantityTables_NEWNEGO", null, Model);
      //      return Json(response);
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      return JsonErrorMessage(ex.Message);
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //   }
      //}

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]

      public async Task<ActionResult> EditNegotiationInfoAsync(string NegotiationIdString)
      {
         try
         {
            int negotiationId = Util.Decrypt(NegotiationIdString);
            await _ApiClient.PostAsync("CommunicationRequest/EditNegotiationInfoAsync/" + negotiationId, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      public async Task<ActionResult> FinishNegotiationAsync(string NegotiationIdString)
      {
         try
         {
            int negotiationId = Util.Decrypt(NegotiationIdString);
            await _ApiClient.PostAsync("CommunicationRequest/FinishNegotiationAsync/" + negotiationId, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [HttpPost]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> ApproveNegotiationSecondStageBySupplierAsync(string NegotiationIdString)
      {
         try
         {
            int negotiationId = Util.Decrypt(NegotiationIdString);
            var newOfferAmount = await _ApiClient.PostAsync<string>("CommunicationRequest/ApproveNegotiationSecondStageBySupplierAsync/" + negotiationId, null, null);
            return Ok(newOfferAmount);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> RejectNegotiationSecondStageBySupplierAsync(string NegotiationIdString)
      {
         try
         {
            int negotiationId = Util.Decrypt(NegotiationIdString);
            await _ApiClient.PostAsync("CommunicationRequest/RejectNegotiationSecondStageBySupplierAsync/" + negotiationId, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      private async Task<decimal> GetAvilableCash(string agencyCode, string projectNumber, bool isGfs)
      {
         //var projectBudget = await _commonService.GetProjectWithBudget(projectNumber, isGfs, agencyCode);
         var projectBudget = await _ApiClient.GetAsync<decimal>("CommunicationRequest/CheckCashAvilablity/" + projectNumber, null);
         return projectBudget;
      }

      /// <summary>
      /// التحقق من وجود سيولة فى البنك تكفى لحجز قيمة الطلب 
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      public async Task<string> CheckAvilableCashAsync(string ProjectItemNumber, bool IsGfs)
      {
         string agencyCode = User.UserAgency();
         var projectBudget = await _ApiClient.GetAsync<decimal>("CommunicationRequest/CheckCashAvilablity/" + ProjectItemNumber, null);
         decimal availableCash = await GetAvilableCash(agencyCode, ProjectItemNumber, IsGfs);

         return availableCash.ToString();


      }

      // [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> GetNegotiationQuantityTablesComponent(string negotiationId)
      {
         return ViewComponent("NegotiationQuantityTablesViewComponent", negotiationId);

      }
      [HttpGet]
      public async Task<ActionResult> ReopenSecondNegotiation(string negotiationId)
      {

         try
         {
            bool dd = await _ApiClient.GetAsync<bool>("CommunicationRequest/ReopenSecondNegotiation/" + negotiationId, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpGet]
      public async Task<ActionResult> ResetSecondNegotiation(string negotiationId)
      {

         try
         {
            bool dd = await _ApiClient.GetAsync<bool>("CommunicationRequest/ResetSecondNegotiation/" + negotiationId, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      #endregion
      #region Plaint

      [Authorize(Roles = RoleNames.supplier)]
      [HttpGet("CommunicationRequest/AddPlaintRequest/{tenderIdString}")]
      public async Task<ActionResult> AddPlaintRequest(string tenderIdString)
      {
         try
         {
            var model = await _ApiClient.GetAsync<TenderPlaintCommunicationRequestModel>("CommunicationRequest/FindSupplierPlaintRequestByTenderIdAndCR/" + tenderIdString, null);
            var attachments = model.AttachmentList == null ? new List<CommunicationAttachmentModel>() : model.AttachmentList;

            if (!string.IsNullOrEmpty(model.AttachmentReference))
            {
               foreach (var item in attachments)
               {
                  model.AttachmentReference += ',' + "/Upload/GetFile?id=" + item./*Attachment.*/FileNetReferenceId + ":" + item/*.Attachment*/.Name;

               }
               if (model.AttachmentReference.Length > 0)
                  model.AttachmentReference = model.AttachmentReference.Remove(0, 1);
            }
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(RoleNames.PlaintRequestDataPolicy)]
      [HttpGet]
      public async Task<ActionResult> CheckPlaintRequestsGrid(PlaintSearchCriteria plaintSearchCriteria)
      {
         try
         {
            int tenderId = Util.Decrypt(plaintSearchCriteria.TenderIdString);
            var result = await _ApiClient.GetQueryResultAsync<TenderPlaintCheckingModel>("CommunicationRequest/FindTenderPlaintRequestsByTenderId", plaintSearchCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [Authorize(Roles = RoleNames.ManagerGrievanceCommittee + "," + RoleNames.SecretaryGrievanceCommittee)]
      [HttpGet]
      public async Task<ActionResult> CheckEscalatedPlaintRequestsGrid(PlaintSearchCriteria plaintSearchCriteria)
      {
         try
         {
            int tenderId = Util.Decrypt(plaintSearchCriteria.TenderIdString);
            var result = await _ApiClient.GetQueryResultAsync<TenderPlaintCheckingModel>("CommunicationRequest/FindTenderEscalatedPlaintRequestsByTenderId", plaintSearchCriteria.ToDictionary());
            result.Items.ToList().ForEach(a => a.tenderHistory = null);
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }
      public IActionResult GetTenderDetailsForPlaintViewComponenet(string tenderIdString)
      {
         return ViewComponent("TenderDetailsForPlaint", new { tenderIdString = tenderIdString });
      }

      [Authorize(RoleNames.CheckPlaintDataPolicy)]
      [HttpGet("CommunicationRequest/CheckPlaintRequests/{agencyRequestIdString}")]
      public async Task<ActionResult> CheckPlaintRequests(string agencyRequestIdString)
      {
         try
         {

            var result = await _ApiClient.GetAsync<TenderPLaintCommunicationModel>("CommunicationRequest/FindTenderPlaintCommunicationByTenderId/" + Util.Decrypt(agencyRequestIdString), null);
            return View(result);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(RoleNames.PlaintRequestDataPolicy)]
      [HttpGet("CommunicationRequest/CheckEscalationRequests/{AgencyRequestIdString}")]
      public async Task<ActionResult> CheckEscalationRequests(string AgencyRequestIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<TenderEscalatedPLaintModel>("CommunicationRequest/FindTenderPlaintEscalationsByTenderId/" + Util.Decrypt(AgencyRequestIdString), null);
            return View(result);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }




      [Authorize(Roles = RoleNames.supplier)]
      [HttpGet("CommunicationRequest/VendorPlaintRequestData/{agencyRequestIdString}")]
      public async Task<ActionResult> VendorPlaintRequestData(string agencyRequestIdString)
      {
         try
         {
            var model = await _ApiClient.GetAsync<TenderPlaintCommunicationRequestModel>("CommunicationRequest/FindSupplierPlaintDetailsPlaintId/" + Util.Decrypt(agencyRequestIdString), null);
            return View(model);
            //return View(new EmptyModel { TenderId = tenderId });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(RoleNames.CheckPlaintDataPolicy)]
      [HttpGet("CommunicationRequest/CheckPlaintRequestData/{plaintId}")]
      public async Task<ActionResult> CheckPlaintRequestData(string plaintId)
      {
         try
         {
            var model = await _ApiClient.GetAsync<PlaintRequestModel>("CommunicationRequest/FindPlaintRequestById/" + plaintId, null);
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }
      [Authorize(RoleNames.CheckPlaintDataPolicy)]
      [HttpPost]
      public async Task<ActionResult> CheckPlaintRequestData([FromForm] PlaintRequestModel model)
      {
         try
         {
            ModelState.Remove("EscalationNotes");
            PlaintNotesModel obj = new PlaintNotesModel() { Notes = model.Notes, plaintId = model.PlainRequestId };
            await _ApiClient.PostAsync<PlaintNotesModel>("CommunicationRequest/SavePlaintNotes", null, obj);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(CheckPlaintRequests), new { agencyRequestIdString = model.EncryptedAgencyCommunicationRequestId });
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("CheckPlaintRequestData", new { plaintId = model.PlainRequestId });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }
      [Authorize(RoleNames.PlaintRequestDataPolicy)]
      [HttpGet("CommunicationRequest/CheckPlaintEscalationData/{plaintId}")]
      public async Task<ActionResult> CheckPlaintEscalationData(string plaintId)
      {
         try
         {

            var model = await _ApiClient.GetAsync<PlaintRequestModel>("CommunicationRequest/FindEscalatedPlaintRequestById/" + plaintId, null);
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(Roles = RoleNames.SecretaryGrievanceCommittee)]
      [HttpPost]
      public async Task<ActionResult> CheckPlaintEscalationData([FromForm] PlaintRequestModel model)
      {
         try
         {
            ModelState.Remove("Notes");
            PlaintRequestModel response = await _ApiClient.PostAsync<PlaintRequestModel>("CommunicationRequest/SaveEscalationNotes", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(CheckEscalationRequests), new { AgencyRequestIdString = model.EncryptedAgencyCommunicationRequestId });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost]
      public async Task<ActionResult> AddPlaintRequest(TenderPlaintCommunicationRequestModel model)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(model);
         }
         try
         {
            List<string> attachmentReferences = new List<string>();
            if (model.AttachmentReference != null)
            {
               string[] lst = model.AttachmentReference.Split(',');
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
            //else
            //{
            //   throw new BusinessRuleException("يجب ارفاق ملف التظلم");
            //   //     attachmentReferences.Add("");
            //}
            //List<CommunicationAttachmentModel> attachList = new List<CommunicationAttachmentModel>(); 
            foreach (var item in attachmentReferences)
            {
               var arr = item.Split(":");
               CommunicationAttachmentModel tenderAttachment = new CommunicationAttachmentModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.PlainRequest, FileNetReferenceId = arr[0] };
               model.AttachmentList.Add(tenderAttachment);
            }
            TenderPlaintCommunicationRequestModel response = await _ApiClient.PostAsync<TenderPlaintCommunicationRequestModel>("CommunicationRequest/AddPlaintRequest", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index), "Tender");

         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return View(model);
      }
      [HttpPost]
      [Authorize(RoleNames.CheckPlaintDataPolicy)]
      public async Task<ActionResult> AcceptPlaintAsync(string communicationRequestId, int procedureId, string details = "")
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/AcceptPlaint/" + communicationRequestId + "/" + procedureId + "/" + details, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [HttpPost]
      [Authorize(Roles = RoleNames.SecretaryGrievanceCommittee)]
      public async Task<ActionResult> AcceptEscalationAsync(string requestId, int procedureId, string details = "")
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/AcceptEscalationCommunicationRequest/" + Util.Decrypt(requestId) + "/" + procedureId + "/" + details, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [HttpPost]
      [Authorize(RoleNames.CheckPlaintDataPolicy)]
      public async Task<ActionResult> RejectPlaintAsync(string CommunicationRequestId, string rejectionReason)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/RejectPlaint/" + CommunicationRequestId + "/" + rejectionReason, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [HttpPost]
      [Authorize(Roles = RoleNames.SecretaryGrievanceCommittee)]
      public async Task<ActionResult> RejecetEscalationAsync(string requestId, string rejectionReason)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/RejectEscalationCommunicationRequest/" + Util.Decrypt(requestId) + "/" + rejectionReason, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(RoleNames.ApprovePlaintDataPolicy)]
      public async Task<ActionResult> RejectPlaintCommunicationRequest(string requestId, string rejectionReason)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/RejectPlaintCommunicationRequest/" + requestId + "/" + rejectionReason, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(RoleNames.ApprovePlaintDataPolicy)]
      public async Task<ActionResult> ApprovePlaintCommunicationRequest(string requestId, string verficationCode)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/ApprovePlaintCommunicationRequest/" + requestId + "/" + verficationCode, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [Authorize(RoleNames.CreateVerificationCodePolicy)]
      [HttpPost]
      public async Task<ActionResult> CreateVerificationCode(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.PostAsync("CommunicationRequest/CreateVerificationCode", null, new BasicTenderModel
            {
               TenderId = tenderId
            });
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [Authorize(RoleNames.CreateVerificationCodePolicy)]
      [HttpPost]
      public async Task<ActionResult> CreateNegotiationVerificationCode(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.PostAsync("CommunicationRequest/CreateNegotiationVerificationCode", null, new BasicTenderModel
            {
               TenderId = tenderId
            });
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(RoleNames.CreateVerificationCodePolicy)]
      [HttpPost]
      public async Task<ActionResult> CreateSecondNegotiationVerificationCode(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.PostAsync("CommunicationRequest/CreateNegotiationVerificationCode", null, new BasicTenderModel
            {
               TenderId = tenderId

            });
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [Authorize(RoleNames.EscalatedTendersIndexPolicy)]
      public ActionResult EscalatedTenders()
      {
         return View();
      }

      [Authorize(RoleNames.EscalatedTendersIndexPolicy)]
      public async Task<ActionResult> EscalatedTendersAsync(EscalationSearchCriteria searchCriteria)
      {
         try
         {
            if (string.IsNullOrEmpty(searchCriteria.Sort))
            {
               searchCriteria.Sort = "CreatedAt";
               searchCriteria.SortDirection = "Asc";
            }
            var result = await _ApiClient.GetQueryResultAsync<EscalatedTendersModel>("CommunicationRequest/GetEscalatedTenders", searchCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, searchCriteria.PageSize, result.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> EscalatePlaintCommunicationRequest(string plaintId, string attachmentId, string attachmentName)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/EscalatePlaintCommunicationRequest/" + plaintId + "/" + attachmentId + "/" + attachmentName, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [HttpPost]
      [Authorize(Roles = RoleNames.ManagerGrievanceCommittee)]
      public async Task<ActionResult> RejectEscalationCommunicationRequest(string requestId, string rejectionReason)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/RejectEscalationCommunicationRequestApproval/" + Util.Decrypt(requestId) + "/" + rejectionReason, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }



      [HttpPost]
      [Authorize(Roles = RoleNames.ManagerGrievanceCommittee)]
      public async Task<ActionResult> ApproveEscalationCommunicationRequest(string requestId, string verficationCode)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/ApproveEscalationCommunicationRequest/" + Util.Decrypt(requestId) + "/" + verficationCode, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      #endregion

      #region Extend offer presentation dates request
      [HttpGet]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> CreateSupplierExtendOfferDatesRequest(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            SupplierExtendOfferDatesAgencyRequestModel supplierExtendOfferDatesAgencyRequestModel = new SupplierExtendOfferDatesAgencyRequestModel
            {
               TenderIdString = tenderIdString,
            };
            return View(supplierExtendOfferDatesAgencyRequestModel);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> CreateSupplierExtendOfferDatesRequestAsync(ExtendOffersDateRequestModel extendOffersDateRequestModel)
      {
         try
         {
            extendOffersDateRequestModel.RequestedDate += DateHelper.GetTimePart(extendOffersDateRequestModel.RequestedTime);
            await _ApiClient.PostAsync("CommunicationRequest/CreateSupplierExtendOfferDatesRequest", null, extendOffersDateRequestModel);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction("Details", nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")), new { tenderIdString = extendOffersDateRequestModel.TenderIdString });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpPost]
      [Authorize(RoleNames.ApproveSupplierExtendOfferDatesPolicy)]
      public async Task<ActionResult> ApproveSupplierExtendOfferDatesRequestAsync(int SupplierExtendDatesAgencyCommunicationRequestId)
      {
         try
         {
            var result = await _ApiClient.PostAsync<SupplierExtendOfferDatesAgencyRequestModel>("CommunicationRequest/ApproveSupplierExtendOfferDatesRequest/" + SupplierExtendDatesAgencyCommunicationRequestId, null, null);
            return RedirectToAction(nameof(ExtendTenderDates), new { tenderIdString = result.TenderIdString, agencyRequestId = Util.Encrypt(result.AgencyRequestId) });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
      }

      [HttpPost]
      [Authorize(RoleNames.ApproveSupplierExtendOfferDatesPolicy)]
      public async Task<ActionResult> RejectSupplierExtendOfferDatesRequestAsync(int SupplierExtendDatesAgencyCommunicationRequestId)
      {
         try
         {
            await _ApiClient.PostAsync("CommunicationRequest/RejectSupplierExtendOfferDatesRequest/" + SupplierExtendDatesAgencyCommunicationRequestId, null, null);
            return Ok();
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
      }

      [HttpGet]
      [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.supplier)]
      public async Task<ActionResult> FindSupplierExtendOfferDatesAgencyRequestByIdAsync(int agencyRequestId)
      {
         try
         {
            var result = await _ApiClient.GetAsync<SupplierExtendOfferDatesAgencyRequestModel>("CommunicationRequest/FindSupplierExtendOfferDatesAgencyRequestById/" + agencyRequestId, null);
            return Ok(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
      }

      [HttpGet("CommunicationRequest/SupplierExtendOfferDates/{agencyRequestId}")]
      [Authorize(RoleNames.SupplierExtendOfferDatesPolicy)]
      public async Task<ActionResult> SupplierExtendOfferDates(string agencyRequestId)
      {
         try
         {
            int requestId = Util.Decrypt(agencyRequestId);
            var result = await _ApiClient.GetAsync<SupplierExtendOfferDatesAgencyRequestModel>("CommunicationRequest/FindSupplierExtendOfferDatesAgencyRequestById/" + requestId, null);
            //if (result.StatusId == (int)Enums.AgencyCommunicationRequestStatus.UnderRevision && User.UserRole() == RoleNames.DataEntry)
            //{
            //   return RedirectToAction(nameof(ExtendTenderDates), new { tenderIdString = result.TenderIdString });
            //}
            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
      }

      [HttpGet]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.supplier)]
      public async Task<ActionResult> FindSupplierExtendOfferDatesAgencyRequestsByIdAsync(int agencyRequestId)
      {
         try
         {
            var result = await _ApiClient.GetListAsync<SupplierExtendOfferDatesAgencyRequestModel>("CommunicationRequest/FindSupplierExtendOfferDatesAgencyRequestsById/" + agencyRequestId, null);
            return Ok(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
      }

      [HttpGet]
      [Authorize(RoleNames.SupplierExtendOfferDatesPolicy)]
      public async Task<ActionResult> ExtendTenderDates(string tenderIdString, string agencyRequestId)
      {
         try
         {
            ExtendOfferPresentationDatesModel response = await _ApiClient.GetAsync<ExtendOfferPresentationDatesModel>("CommunicationRequest/GetTenderExtendDatesByTenderId/" + Util.Decrypt(tenderIdString) + "/" + Util.Decrypt(agencyRequestId), null);
            List<VacationsDateModel> vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            response.TenderIdString = tenderIdString;
            response.Vacations = vacations;
            return View(response);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(TenderController.TenderIndexUnderOperationsStage), "Tender");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(TenderController.TenderIndexUnderOperationsStage), "Tender");
         }
      }

      [HttpPost]
      [Authorize(RoleNames.SupplierExtendOfferDatesPolicy)]
      public async Task<ActionResult> ExtendTenderDates(ExtendOfferPresentationDatesModel model)
      {
         if (model.LastOfferPresentationDate != null)
         {
            model.LastOfferPresentationDate += DateHelper.GetTimePart(model.LastOfferPresentationTime);
         }
         if (model.OffersOpeningDate != null)
         {
            model.OffersOpeningDate += DateHelper.GetTimePart(model.OffersOpeningTime);
         }
         if (model.OffersCheckingDate != null)
         {
            model.OffersCheckingDate += DateHelper.GetTimePart(model.OffersCheckingTime);
         }
         //if ((model.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase &&  model.OffersOpeningAddressId == null) || model.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
         //{
         //   ModelState.Remove("OffersOpeningDate");

         //   ModelState.Remove("OffersOpeningTime");
         //}

         if (model.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || model.TenderTypeId == (int)Enums.TenderType.PostQualification || model.TenderTypeId == (int)Enums.TenderType.PreQualification || model.TenderTypeId == (int)Enums.TenderType.FirstStageTender || model.TenderTypeId == (int)Enums.TenderType.Competition)
         {
            ModelState.Remove("OffersOpeningDate");
            ModelState.Remove("OffersOpeningTime");
         }
         else
         {
            ModelState.Remove("OffersCheckingDate");
            ModelState.Remove("OffersCheckingTime");
         }
         if (!ModelState.IsValid)
         {
            string idString = model.TenderIdString;
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            model = await _ApiClient.GetAsync<ExtendOfferPresentationDatesModel>("CommunicationRequest/GetTenderExtendDatesByTenderId/" + Util.Decrypt(idString) + "/" + Util.Decrypt(model.AgencyRequestIdString), null);
            List<VacationsDateModel> vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            model.TenderIdString = idString;
            model.Vacations = vacations;
            return View(model);
         }
         try
         {
            if (model.LastOfferPresentationDate != null)
            {
               model.LastOfferPresentationDate += DateHelper.GetTimePart(model.LastOfferPresentationTime);
            }
            if (model.OffersOpeningDate != null)
            {
               model.OffersOpeningDate += DateHelper.GetTimePart(model.OffersOpeningTime);
            }
            if (model.OffersCheckingDate != null)
            {
               model.OffersCheckingDate += DateHelper.GetTimePart(model.OffersCheckingTime);
            }

            model.TenderId = Util.Decrypt(model.TenderIdString);
            await _ApiClient.PostAsync("CommunicationRequest/UpdateTenderExtendDates", null, model);

            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return (model.TenderTypeId == (int)Enums.TenderType.PostQualification || model.TenderTypeId == (int)Enums.TenderType.PreQualification ? RedirectToAction(nameof(QualificationController.QualificationExtendDateApprovement), "Qualification", new { tenderIdString = model.TenderIdString }) : RedirectToAction(nameof(TenderController.TenderExtendDateApprovement), "Tender", new { tenderIdString = model.TenderIdString }));
         }
         catch (AuthorizationException ex)
         {
            AddError(ex.Message);
            model.TenderIdString = Util.Encrypt(model.TenderId);
            List<VacationsDateModel> vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            model.Vacations = vacations;
            return View(model);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            model.TenderId = Util.Decrypt(model.TenderIdString);
            model.TenderIdString = Util.Encrypt(model.TenderId);
            List<VacationsDateModel> vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            model.Vacations = vacations;
            return View(model);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return View(model);
         }
      }
      #endregion

      #region [NEGO]

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      public async Task<ActionResult> SaveNegotiationQitems(IFormCollection collection)
      {

         try
         {

            //string TenderId = "";
            Dictionary<string, string> AuthorList = new Dictionary<string, string>();
            foreach (var item in collection.Keys)
            {
               AuthorList.Add(item, collection[item].ToString());
            }
            //int tenderId = 21;
            //AuthorList["TenderId"] = tenderId.ToString();
            AjaxResponse<OffersCompareViewModel> ajaxResponse = await _ApiClient.PostAsync<AjaxResponse<OffersCompareViewModel>>("CommunicationRequest/SaveNegotiationQitems", null, AuthorList);

            return Ok(ajaxResponse);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }


      }

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> SaveNegotiationQitems_NEW(IFormCollection collection)
      {

         try
         {

            //string TenderId = "";
            Dictionary<string, string> AuthorList = new Dictionary<string, string>();
            foreach (var item in collection.Keys)
            {
               AuthorList.Add(item, collection[item].ToString());
            }
            //int tenderId = 21;
            //AuthorList["TenderId"] = tenderId.ToString();
            AjaxResponse<OffersCompareViewModel> ajaxResponse = await _ApiClient.PostAsync<AjaxResponse<OffersCompareViewModel>>("CommunicationRequest/SaveNegotiationQitems_NEW", null, AuthorList);

            return Ok(ajaxResponse);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }


      }
      [HttpGet]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> DeleteNegotiationQitems_NEW(int tenderId, int negotiationId, int tableId, int rowNumber)
      {

         try
         {
            AjaxResponse<OffersCompareViewModel> ajaxResponse = await _ApiClient.GetAsync<AjaxResponse<OffersCompareViewModel>>("CommunicationRequest/DeleteNegotiationQitems_NEW/" + tenderId + "/" + negotiationId + "/" + tableId + "/" + rowNumber + "", null);

            return Ok(ajaxResponse);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }


      }
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpPost]
      [Route("CommunicationRequest/CreateFirstStageNegotiationR")]
      public async Task<ActionResult> CreateFirstStageNegotiationR(NegotiationAgencyFirstStageEditModel negotiationModel)
      {

         string errorMessage = Resources.SharedResources.ErrorMessages.ModelDataError;

         try
         {
            if (!ModelState.IsValid /*|| (string.IsNullOrEmpty(negotiationModel.ProjectNumber) && negotiationModel.NegotiationReasonId == (int)Enums.NegotiationFirstStageRejectionReasons.HighPriceThanBudget)*/)
            {
               foreach (var modelState in ModelState.Values)
               {
                  foreach (var error in modelState.Errors)
                  {
                     errorMessage += error.ErrorMessage;
                  }
               }
               return Json(new { success = false, message = errorMessage });
            }
            else
            {
               bool isSaved = false;
               string succesMessage = Resources.SharedResources.Messages.DataSaved;
               negotiationModel.Attachment = PrepareAttachmentForInsert(negotiationModel);
               isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/UpdateFirstStageNegotiationR/", null, negotiationModel);
               return Json(new { success = isSaved, message = isSaved ? succesMessage : errorMessage });
            }
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);

         }
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpPost]
      [Route("CommunicationRequest/CreateFirstStageNegotiationData")]
      public async Task<ActionResult> CreateFirstStageNegotiationData(CreateNegotiationFirstStageDataModel negotiationModel)
      {

         string errorMessage = Resources.SharedResources.ErrorMessages.ModelDataError;

         try
         {
            if (!ModelState.IsValid)
            {
               foreach (var modelState in ModelState.Values)
               {
                  foreach (var error in modelState.Errors)
                  {
                     errorMessage += error.ErrorMessage;
                  }
               }
               return Json(new { success = false, message = errorMessage });
            }
            else
            {
               bool isSaved = false;
               string succesMessage = Resources.SharedResources.Messages.DataSaved;
               negotiationModel.Attachment = PrepareAttachmentForInsertNegotiationFirstStage(negotiationModel);
               isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/UpdateFirstStageNegotiationData/", null, negotiationModel);
               return Json(new { success = isSaved, message = isSaved ? succesMessage : errorMessage });
            }
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);

         }
      }


      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersCheckManager)]
      [HttpPost("CommunicationRequest/SendSecondFirstStageNegotiationToApprove")]
      public async Task<IActionResult> SendSecondFirstStageNegotiationToApprove(CreateNegotiationFirstStageDataModel negotiationModel)
      {
         try
         {
            bool isSaved = await _ApiClient.PostAsync<bool>("CommunicationRequest/SendSecondFirstStageNegotiationToApprove", null, negotiationModel);
            return Json(new { success = true, message = Resources.SharedResources.Messages.DataSaved });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (UnHandledAccessException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }

      }



      [HttpPost]
      public IActionResult GetNegotiationQuantityTableViewComponent(int tenderId, int offerId, int tableId, int formId, bool isReadOnly, int negotiationId)
      {
         return ViewComponent("NegotiationQuantityTablesPaging", new { tenderId, offerId, tableId = tableId.ToString(), formId, isReadOnly, negotiationId });
      }
      [HttpGet]
      public async Task<IActionResult> GetNegotiationTableQuantityItemsAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("CommunicationRequest/GetNegotiationTableQuantityItems", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      #endregion
   }

}
