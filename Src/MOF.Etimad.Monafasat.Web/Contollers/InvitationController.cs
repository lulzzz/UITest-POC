using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class InvitationController : BaseController
   {
      private readonly int _pageSize = (int)PageSize.Ten;
      private readonly IApiClient _ApiClient;
      private readonly ILogger<TenderController> _logger;

      public InvitationController(IApiClient ApiClient, ILogger<TenderController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _ApiClient = ApiClient;
         _logger = logger;
      }
      #region Invitations


      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> GetAllSuppliersBySearchCretriaForInvitationAsync(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<InvitationCrModel>("Invitation/GetAllSuppliersBySearchCretriaForInvitation", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
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
      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<IActionResult> GetInvitedSuppliersForInvitationInTenderCreationAsync(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<InvitationCrModel>("Invitation/GetInvitedSuppliersForInvitationInTenderCreation", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, _pageSize, suppliers.PageNumber, null));
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

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> GetQualifiedSuppliersAsync(TenderIdSearchCretriaModel searchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<InvitationCrModel>("Invitation/GetQualifiedSuppliers", searchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, _pageSize, suppliers.PageNumber, null));
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

      [HttpGet]
      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<IActionResult> GetAccptedSuppliersInFirstStageAsync(TenderIdSearchCretriaModel searchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<InvitationCrModel>("Invitation/GetAccptedSuppliersInFirstStage", searchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, _pageSize, suppliers.PageNumber, null));
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

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> GetEmailsForUnregisteredSuppliersAsync(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<string>("Invitation/GetEmailsForUnregisteredSuppliers", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, 100, 1, null));
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> GetMobileNumbersForUnregisteredSuppliersAsync(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<string>("Invitation/GetMobileNumbersForUnregisteredSuppliers", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, 100, 1, null));
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> SendInvitationsInTenderCreationAsync(InvitationsInCreateTenderModel invitations)
      {
         try
         {
            var tenderList = await _ApiClient.PostAsync("Invitation/SendInvitationsInTenderCreation", null, invitations);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> SubmitTenderInvitationsStepAsync(int tenderId)
      {
         try
         {
            var tenderList = await _ApiClient.PostAsync("Invitation/SubmitTenderInvitationsStep/" + tenderId, null, null);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> SendInvitationsByEmailAsync(int tenderId, List<string> emails)
      {
         try
         {
            var emailslist = await _ApiClient.GetQueryResultAsync<TenderInvitationDetailsModel>("Invitation/SendInvitationByEmail", new Dictionary<string, object> { { "tenderId", tenderId }, { "emails", emails } });
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> SendInvitationsBySmsAsync(int tenderId, List<string> mobileNoList)
      {
         try
         {
            var mobileList = await _ApiClient.GetAsync<TenderInvitationDetailsModel>("Invitation/SendInvitationBySms", new Dictionary<string, object> { { "tenderId", tenderId }, { "mobilNoList", mobileNoList } });
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
      //GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementRequirement
      [HttpGet]
      [Authorize(RoleNames.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementPolicy)]
      public async Task<IActionResult> GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<UnRegisterSupplierInvitationModel>("Invitation/GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, _pageSize, suppliers.PageNumber, null));
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

      [HttpGet]
      [Authorize(RoleNames.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementPolicy)]
      public async Task<IActionResult> GetInvitedSuppliersForInvitationAfterTenderApprovementAsync(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<SupplierInvitationModel>("Invitation/GetInvitedSuppliersForInvitationAfterTenderApprovement", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, _pageSize, suppliers.PageNumber, null));
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


      [HttpGet]
      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<IActionResult> GetAnnouncementTemplateSuppliersAsync(AnnouncementSupplierTemplateInvitationSearchmodel searchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<InvitationCrModel>("Invitation/GetAnnouncementTemplateSuppliers", searchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, _pageSize, suppliers.PageNumber, null));
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
   }
}
