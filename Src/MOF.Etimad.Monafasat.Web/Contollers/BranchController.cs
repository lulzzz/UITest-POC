using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{

   [Authorize(RoleNames.MonafasatAdminPolicy)]
   public class BranchController : BaseController
   {
      private readonly IApiClient _ApiClient;
      private readonly ILogger<BranchController> _logger;
      private readonly IMemoryCache _cache;
      public BranchController(IApiClient ApiClient, ILogger<BranchController> logger, IMemoryCache cache, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _ApiClient = ApiClient;
         _logger = logger;
         _cache = cache;
      }

      public IActionResult Index()
      {
         return View();
      }

      public async Task<IActionResult> IndexPagingAsync(BranchSearchCriteriaModel branchSearchCriteria)
      {
         try
         {
            var blocks = await _ApiClient.GetQueryResultAsync<BranchModel>("Branch/Find", branchSearchCriteria.ToDictionary());
            return Json(new PaginationModel(blocks.Items, blocks.TotalCount, blocks.PageSize, blocks.PageNumber, null));
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

      public ActionResult CreateAsync(string BranchId)
      {
         return View(new BranchModel());
      }

      public async Task<ActionResult> Create(string BranchId)
      {
         BranchModel branchModel = await _ApiClient.GetAsync<BranchModel>("Lookup/GetBranchAddressTypesAndCommitteesByAgency", null);
         branchModel.BranchIdString = BranchId;
         return View(branchModel);
      }
      [HttpGet]
      public async Task<JsonResult> GetBranchDetails(string BranchId)
      {
         if (string.IsNullOrEmpty(BranchId))
            return Json(new BranchModel { BranchIdString = BranchId });
         BranchModel result = await _ApiClient.GetAsync<BranchModel>("Branch/GetById/" + Util.Decrypt(BranchId), null);
         return Json(result);
      }


      [HttpPost]
      public async Task<ActionResult> SaveBranchList(BranchModel Model)
      {
         if (!ModelState.IsValid)
         {
            Model.AddressesList = Model.AddressesList ?? new List<OtherBranchAddressModel>();
            Model.MainBranchAddress = Model.MainBranchAddress ?? new MainBranchAddressModel();
            Model.techcommitteeIds = new List<int>();
            Model.checkcommitteeIds = new List<int>();
            Model.opencommitteeIds = new List<int>();
            Model.purchaseCommitteeIds = new List<int>();
            Model.preQualificationCommitteeIds = new List<int>();
            Model.vrocommitteeIds = new List<int>();
            return Json(new
            {
               IsSuccess = false,
               data = Model,
               Message = Resources.SharedResources.ErrorMessages.ModelDataError
            });
         }
         try
         {
            var message = "";

            var model = await _ApiClient.PostAsync<BranchModel>("Branch/AddBranch", null, Model);

            if (string.IsNullOrEmpty(Model.BranchIdString))
            {
               message = Resources.BranchResources.Messages.BranchAddedSuccessfully;
            }
            else
            {
               message = Resources.BranchResources.Messages.BranchUpdatedSuccessfully;
            }
            return Json(new
            {
               IsSuccess = true,
               data = model,
               Message = message
            });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return Json(new
            {
               IsSuccess = false,
               data = Model,
               Message = ex.Message
            });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return Json(new
            {
               IsSuccess = false,
               data = Model,
               Message = Resources.TenderResources.ErrorMessages.UnexpectedError
            });
         }
      }

      [HttpPost]
      public async Task<ActionResult> CreateAsync(BranchModel branchModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.SharedResources.ErrorMessages.ModelDataError);
            return View(branchModel);
         }
         try
         {
            ClearCommitteesCache();
            await _ApiClient.PostAsync("Branch/AddBranch", null, branchModel);
            AddMessage(Resources.SharedResources.Messages.BranchAddSuccessfully);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return View(branchModel);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      public async Task<ActionResult> EditAsync(string id)
      {
         try
         {
            BranchModel branchModel = await _ApiClient.GetAsync<BranchModel>("Branch/GetById/" + Util.Decrypt(id).ToString(), null);
            if (branchModel != null)
            {
               ViewData.Add("BranchIdString", branchModel.BranchIdString);
               return View(branchModel);
            }
            return RedirectToAction(nameof(Index));
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

      [HttpPost]
      public async Task<ActionResult> EditAsync(BranchModel branchModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.SharedResources.ErrorMessages.ModelDataError);
            return View(branchModel);
         }
         try
         {
            ClearCommitteesCache();
            branchModel.BranchId = Util.Decrypt(branchModel.BranchIdString);
            await _ApiClient.PostAsync("Branch/Update", null, branchModel);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index));
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

      public async Task<ActionResult> DeleteAsync(string branchIdString)
      {
         try
         {
            ClearCommitteesCache();
            int branchId = Util.Decrypt(branchIdString);
            await _ApiClient.GetStringAsync("Branch/Delete/" + branchId.ToString(), null);
            return Json(new { status = "success", message = Resources.TenderResources.Messages.DeletedSuccessfully });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return Json(new { status = "error", message = ex.Message });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return Json(new { status = "error", message = ex.Message });
         }
      }

      [HttpGet]
      public async Task<List<UserLookupModel>> AssignUsersToBranchAsync(BranchUserSearchCriteriaModel criteria)
      {
         criteria.RoleNames = new List<string>();
         if (User.UserIsVRO())
         {
            criteria.RoleNames.Add(RoleNames.PurshaseSpecialist);
            criteria.RoleNames.Add(RoleNames.EtimadOfficer);
         }
         else
         {
            criteria.RoleNames.Add(RoleNames.Auditer);
            criteria.RoleNames.Add(RoleNames.DataEntry);
            criteria.RoleNames.Add(RoleNames.PrePlanningCreator);
            criteria.RoleNames.Add(RoleNames.PrePlanningAuditor);
            criteria.RoleNames.Add(RoleNames.ApproveTenderAward);
         }
         List<UserLookupModel> branchUserModel = await _ApiClient.PostAsync<List<UserLookupModel>>("Branch/GetIDMUsersSearch/", null, criteria);
         return branchUserModel;
      }

      [HttpPost]
      public async Task<ActionResult> GetUserRolesById(string userName)
      {
         try
         {
            var roles = await _ApiClient.GetListAsync<RoleModel>("Branch/GetUserRolesById/" + userName, null);
            return Json(roles);
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
      public async Task<ActionResult> AssignUsersToBranch(string branchIdString)
      {
         try
         {
            BranchUserSearchCriteriaModel criteria = new BranchUserSearchCriteriaModel() { BranchId = Util.Decrypt(branchIdString) };
            BranchUserModel branchUserModel = await _ApiClient.PostAsync<BranchUserModel>("Branch/GetBranchUserModel/", null, criteria);
            return View(branchUserModel);
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
      [HttpPost]
      public async Task<ActionResult> AssignUsersToBranch(BranchUserModel branchUserModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return RedirectToAction(nameof(AssignUsersToBranch), new { branchIdString = Util.Encrypt(branchUserModel.BranchId) });
         }
         try
         {
            ClearCommitteesCache();
            await _ApiClient.PostAsync("Branch/AssignUsersBranch", null, branchUserModel);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(AssignUsersToBranch), new { branchIdString = Util.Encrypt(branchUserModel.BranchId) });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);

            return RedirectToAction(nameof(AssignUsersToBranch), new { branchIdString = Util.Encrypt(branchUserModel.BranchId) });

         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpGet]
      public async Task<ActionResult> GetBranchUsersAsync(BranchUserSearchCriteriaModel criteria)
      {
         try
         {
            var usersList = await _ApiClient.GetQueryResultAsync<BranchUserModel>("Branch/GetBranchUsers", criteria.ToDictionary());
            return Json(new PaginationModel(usersList.Items, usersList.TotalCount, usersList.PageSize, usersList.PageNumber, null));
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
      public async Task<ActionResult> RemoveAssignedUserAsync(int userId, int branchId, int roleName)
      {
         try
         {
            ClearCommitteesCache();
            await _ApiClient.PostAsync("Branch/RemoveAssignedUser", new Dictionary<string, object> { { nameof(userId), userId }, { nameof(branchId), branchId }, { nameof(roleName), roleName } }, null);
            AddMessage(Resources.TenderResources.Messages.DeletedSuccessfully);
            return RedirectToAction(nameof(AssignUsersToBranch), new { branchIdString = Util.Encrypt(branchId) });
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
      public async Task<ActionResult> AssignCommitteesToBranch(string branchIdString)
      {
         BranchCommitteeModel branchUserModel = new BranchCommitteeModel();
         try
         {
            branchUserModel.BranchId = Util.Decrypt(branchIdString);
            BranchModel branch = await _ApiClient.GetAsync<BranchModel>("Branch/GetById/" + branchUserModel.BranchId.ToString(), null);
            branchUserModel.BranchName = branch.BranchName;
            branchUserModel.Committees = await _ApiClient.GetListAsync<CommitteeModel>("Branch/GetCommitteesNotAssignedToBranch/" + branchUserModel.BranchId, null);
            return View(branchUserModel);
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
      public async Task<ActionResult> AssignCommitteesToBranch(BranchCommitteeModel branchUserModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return RedirectToAction(nameof(AssignCommitteesToBranch), new { branchIdString = Util.Encrypt(branchUserModel.BranchId) });
         }
         try
         {
            ClearCommitteesCache();
            await _ApiClient.PostAsync("Branch/AssignCommitteesBranch", null, branchUserModel);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(AssignCommitteesToBranch), new { branchIdString = Util.Encrypt(branchUserModel.BranchId) });
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

      [HttpGet]
      public async Task<ActionResult> GetBranchCommitteesAsync(BranchCommitteeSearchCriteriaModel criteria)
      {
         try
         {
            var usersList = await _ApiClient.GetQueryResultAsync<LookupModel>("Branch/GetBranchCommittees", criteria.ToDictionary());
            return Json(new PaginationModel(usersList.Items, usersList.TotalCount, usersList.PageSize, usersList.PageNumber, null));
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
      public async Task<ActionResult> RemoveAssignedCommitteeAsync(int committeeId, int branchId)
      {
         try
         {
            ClearCommitteesCache();
            await _ApiClient.PostAsync("Branch/RemoveAssignedCommittee", new Dictionary<string, object> { { nameof(committeeId), committeeId }, { nameof(branchId), branchId } }, null);
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

      public void ClearCommitteesCache()
      {
         _cache.Remove(CacheKeys.BasicStepCache + "_" + User.UserAgency());
         _cache.Remove(CacheKeys.PurchaseCommitteeCache + "_" + User.UserAgency());
         _cache.Remove(CacheKeys.VROCommitteeCache + "_" + User.UserAgency());
      }
   }

}
