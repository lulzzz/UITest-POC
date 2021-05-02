using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class MandatoryListController : BaseController
   {
      private readonly IApiClient _apiClient;
      private readonly ILogger<MandatoryListController> _logger;


      public MandatoryListController(IApiClient apiClient, ILogger<MandatoryListController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _apiClient = apiClient;
         _logger = logger;
      }

      [Authorize(PolicyNames.ViewMandatoryListPolicy)]
      public IActionResult Index()
      {
         return View();
      }

      [Authorize(PolicyNames.ViewMandatoryListPolicy)]
      public async Task<IActionResult> IndexPagingAsync(MandatoryListSearchViewModel searchViewModel)
      {
         try
         {
            var queryResult = await _apiClient.GetQueryResultAsync<MandatoryListIndexViewModel>("MandatoryList/Search", searchViewModel.ToDictionary());

            return Json(new PaginationModel(queryResult.Items, queryResult.TotalCount, queryResult.PageSize, queryResult.PageNumber, null));
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

      [Authorize(PolicyNames.AddMandatoryListPolicy)]
      public IActionResult Add()
      {
         return View();
      }

      [Authorize(PolicyNames.AddMandatoryListPolicy)]
      [HttpPost]
      public async Task<IActionResult> Add(InputMandatoryListViewModel viewModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(FormatErrorMessage(GetModalStateErrors()));
            return View(viewModel);
         }
         try
         {
            var entity = await _apiClient.PostAsync<MandatoryListViewModel>("MandatoryList/Add", null, viewModel);
            if (viewModel.SendToApproval)
               await _apiClient.PostAsync("MandatoryList/SendToApproval/" + entity.Id, null, null);

            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(ex.Message);
         }

         return View(viewModel);
      }

      [Authorize(PolicyNames.EditMandatoryListPolicy)]
      public async Task<IActionResult> Edit(string id)
      {
         try
         {
            var viewModel = await _apiClient.GetAsync<InputMandatoryListViewModel>("MandatoryList/FindForEdit/" + id, null);
            return View(viewModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(PolicyNames.EditMandatoryListPolicy)]
      [HttpPost]
      public async Task<IActionResult> Edit(InputMandatoryListViewModel viewModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(FormatErrorMessage(GetModalStateErrors()));
            return View(viewModel);
         }

         try
         {
            await _apiClient.PostAsync("MandatoryList/Update", null, viewModel);
            if (viewModel.SendToApproval)
               await _apiClient.PostAsync("MandatoryList/SendToApproval/" + viewModel.Id, null, null);

            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return View(viewModel);
         }
         catch (AuthorizationException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(ex.Message);
            return View(viewModel);
         }
      }

      [Authorize(PolicyNames.ViewMandatoryListPolicy)]
      [HttpGet]
      public async Task<IActionResult> Find(string id)
      {
         try
         {
            var viewModel = await _apiClient.GetAsync<MandatoryListViewModel>("MandatoryList/Find/" + id, null);

            return Json(viewModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(PolicyNames.ViewMandatoryListPolicy)]
      public async Task<ActionResult> Details(string id)
      {
         try
         {
            var viewModel = await _apiClient.GetAsync<MandatoryListViewModel>("MandatoryList/Find/" + id, null);

            return View(viewModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
      public async Task<ActionResult> Approval(string encryptedMandatoryListId)
      {
         try
         {
            var model = await _apiClient.GetAsync<MandatoryListApprovalViewModel>("MandatoryList/GetMandatoryListDetailsForApproval/" + encryptedMandatoryListId, null);

            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(PolicyNames.EditMandatoryListPolicy)]
      public async Task<ActionResult> SendToApproval(string encryptedMandatoryListId)
      {
         try
         {
            var model = await _apiClient.GetAsync<MandatoryListApprovalViewModel>("MandatoryList/GetMandatoryListDetailsForApproval/" + encryptedMandatoryListId, null);

            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(PolicyNames.EditMandatoryListPolicy)]
      [HttpPost]
      public async Task<ActionResult> SendMandatoryListToApproveAsync(string encryptedId)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/SendToApproval/" + encryptedId, null, null);
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

      [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
      [HttpPost]
      public async Task<ActionResult> ApproveMandatoryListAsync(string encryptedId, string verificationCode)
      {
         try
         {
            //await _apiClient.PostAsync("MandatoryList/Approve/" + encryptedId, null, null);
            await _apiClient.PostAsync("MandatoryList/Approve/" + encryptedId + "/" + verificationCode, null, null);
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

      [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
      [HttpPost]
      public async Task<IActionResult> ApproveEdit(string id, string verificationCode)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/ApproveEdit/" + id + "/" + verificationCode, null, null);
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

      [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
      [HttpPost]
      public async Task<IActionResult> RejectEdit(MandatoryListRejectionViewModel viewModel)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/RejectEdit", null, viewModel);
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

      [Authorize(PolicyNames.EditMandatoryListPolicy)]
      [HttpPost]
      public async Task<IActionResult> CloseEdit(string id)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/CloseEdit/" + id, null, null);
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

      [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
      [HttpPost]
      public async Task<ActionResult> Reject(MandatoryListRejectionViewModel rejectionModel)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/Reject", null, rejectionModel);
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

      [Authorize(PolicyNames.EditMandatoryListPolicy)]
      public async Task<IActionResult> Delete(string id)
      {
         try
         {
            var viewModel = await _apiClient.GetAsync<MandatoryListViewModel>("MandatoryList/Find/" + id, null);
            return View(viewModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(PolicyNames.EditMandatoryListPolicy)]
      [HttpPost]
      public async Task<ActionResult> DeleteAsync(string id)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/Delete/" + id, null, null);
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

      [Authorize(PolicyNames.EditMandatoryListPolicy)]
      [HttpPost]
      public async Task<ActionResult> RequestDelete(string id)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/RequestDelete/" + id, null, null);
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

      [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
      [HttpPost]
      public async Task<ActionResult> ApproveDelete(string id, string verificationCode)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/ApproveDelete/" + id + "/" + verificationCode, null, null);
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
            return JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(PolicyNames.MandatoryListApprovalPolicy)]
      [HttpPost]
      public async Task<ActionResult> RejectDelete(MandatoryListRejectionViewModel viewModel)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/RejectDelete", null, viewModel);
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
            return JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(PolicyNames.EditMandatoryListPolicy)]
      [HttpPost]
      public async Task<ActionResult> ReopenMandatoryListAsync(string encryptedId)
      {
         try
         {
            await _apiClient.PostAsync("MandatoryList/Reopen/" + encryptedId, null, null);
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
      public async Task<IActionResult> CreateVerificationCode(string id)
      {
         try
         {
            var result = await _apiClient.PostAsync("MandatoryList/CreateVerificationCode/" + id, null, null);
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
      public async Task<CSICodeDetailsModel> GetMandatoryListCSICodeInfo(string code)
      {

         var result = await _apiClient.GetAsync<CSICodeDetailsModel>("MandatoryList/GetMandatoryListCSICodeInfo/" + code, null);
         return result;
      }
   }
}
