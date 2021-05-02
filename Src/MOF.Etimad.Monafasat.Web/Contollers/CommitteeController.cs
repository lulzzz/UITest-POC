using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Resources.CommitteeResources;
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
   public class CommitteeController : BaseController
   {
      private IApiClient _ApiClient;
      private readonly ILogger<CommitteeController> _logger;
      private IMemoryCache _cache;
      public CommitteeController(IApiClient ApiClient, ILogger<CommitteeController> logger, IMemoryCache cache, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _ApiClient = ApiClient;
         _logger = logger;
         _cache = cache;
      }

      public ActionResult Index(string committeeTypeId)
      {
         ViewData.Add("CommitteeTypeId", committeeTypeId);
         ViewBag.Title = CommitteeTypeTitleValue(Util.Decrypt(committeeTypeId));
         return View();
      }
      private string CommitteeTypeTitleValue(int committeeTypeId)
      {
         if (committeeTypeId == (int)Enums.CommitteeType.TechincalCommittee)
         {
            return DisplayInputs.TechnicalCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee)
         {
            return DisplayInputs.OpenOffersCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee)
         {
            return DisplayInputs.CheckOffersCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee)
         {
            return DisplayInputs.PreQualificationCommittee;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.BlockCommittee)
         {
            return DisplayInputs.BlockCommittee;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.PurchaseCommittee)
         {
            return DisplayInputs.PurchaseCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.VROCommittee)
         {
            return DisplayInputs.VROCommittee;
         }
         return DisplayInputs.TechnicalCommitteeName;
      }

      public async Task<IActionResult> IndexPagingAsync(CommitteeSearchCriteriaModel committeeSearchCriteriaModel)
      {
         try
         {
            committeeSearchCriteriaModel.CommitteeTypeId = Util.Decrypt(committeeSearchCriteriaModel.CommitteeTypeIdString);
            committeeSearchCriteriaModel.AgencyCode = User.UserAgency();
            var committees = await _ApiClient.GetQueryResultAsync<CommitteeModel>("Committee/Get", committeeSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(committees.Items, committees.TotalCount, committees.PageSize, committees.PageNumber, null));
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
      public async Task<ActionResult> AssignUsersToCommittees(string committeeIdString, string roleName)
      {
         CommitteeUserModel committeeUserModel = new CommitteeUserModel();
         try
         {
            committeeUserModel.CommitteeId = Util.Decrypt(committeeIdString);
            committeeUserModel.RoleName = roleName;
            committeeUserModel.Users = await _ApiClient.GetListAsync<UserLookupModel>("Lookup/GetUsersNotAssignedToCommitteeByRoleName/" + roleName + "/" + committeeUserModel.CommitteeId, null);
            committeeUserModel.CommitteeModel = await _ApiClient.GetAsync<CommitteeTenderModel>("Committee/GetCommitteeTendersAsync/" + Util.Decrypt(committeeIdString).ToString(), null);
            return View(committeeUserModel);
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
      public async Task<ActionResult> AssignUsersToCommittees(CommitteeUserModel committeeUserModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return RedirectToAction(nameof(AssignUsers), new { committeeIdString = Util.Encrypt(committeeUserModel.CommitteeId) });
         }
         try
         {
            await _ApiClient.PostAsync("Committee/AssignUsersCommittee", null, committeeUserModel);
            return RedirectToAction(nameof(AssignUsers), new { committeeIdString = Util.Encrypt(committeeUserModel.CommitteeId) });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AssignUsers), new { committeeIdString = Util.Encrypt(committeeUserModel.CommitteeId) });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return View();
         }
      }

      [HttpGet]
      public async Task<ActionResult> AssignUsers(string committeeIdString)
      {
         CommitteeUserModel committeeUserModel = new CommitteeUserModel();
         try
         {
            committeeUserModel.CommitteeId = Util.Decrypt(committeeIdString);
            committeeUserModel.CommitteeModel = await _ApiClient.GetAsync<CommitteeTenderModel>("Committee/GetCommitteeTendersAsync/" + Util.Decrypt(committeeIdString).ToString(), null);
            CommitteeUserSearchCriteriaModel criteria = new CommitteeUserSearchCriteriaModel();
            criteria.CommitteeId = committeeUserModel.CommitteeId;
            criteria.CommitteeTypeId = committeeUserModel.CommitteeModel.CommitteeTypeId;
            committeeUserModel.CommitteeTypeIdString = Util.Encrypt(criteria.CommitteeTypeId);
            committeeUserModel.Users = await _ApiClient.PostAsync<List<UserLookupModel>>("Committee/GetUsersFilteredbyCommitteeTypeId/", null, criteria);
            return View(committeeUserModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AssignUsers), new { committeeIdString = committeeIdString });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      public async Task<ActionResult> AssignUsers(CommitteeUserModel committeeUserModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
         }
         try
         {
            await _ApiClient.PostAsync("Committee/AssignUsersCommittee", null, committeeUserModel);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return RedirectToAction(nameof(AssignUsers), new { committeeIdString = Util.Encrypt(committeeUserModel.CommitteeId) });
      }
      [HttpGet]
      public async Task<ActionResult> LinkTendersToCommittees(string committeeIdString)
      {
         var committeeTenderModel = new CommitteeTenderModel();
         try
         {
            int commiteeId = Util.Decrypt(committeeIdString);
            committeeTenderModel = await _ApiClient.GetAsync<CommitteeTenderModel>("Committee/GetCommitteeTendersAsync/" + commiteeId, null);
            return View(committeeTenderModel);
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
      public async Task<JsonResult> JoinCommitteeToAllTenders(CommitteeTenderModel committeeTenderModel)
      {
         if (!ModelState.IsValid)
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.ModelDataError);
         try
         {
            await _ApiClient.PostAsync("Tender/JoinCommitteeToAllTenders", null, committeeTenderModel);
            ClearCommitteesCache();
            return Json(Resources.CommitteeResources.ErrorMessages.TenderJoinedSuccess);
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
      public async Task<JsonResult> JoinCommitteeToTenders(CommitteeTenderModel committeeTenderModel)
      {
         if (!ModelState.IsValid)
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.ModelDataError);
         try
         {
            await _ApiClient.PostAsync("Tender/JoinCommitteeToTenders", null, committeeTenderModel);
            ClearCommitteesCache();
            return Json(Resources.CommitteeResources.ErrorMessages.TenderJoinedSuccess);
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
      public async Task<JsonResult> JoinCommitteeToBranchTenders(CommitteeTenderModel committeeTenderModel)
      {
         if (!ModelState.IsValid)
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.ModelDataError);
         try
         {
            await _ApiClient.PostAsync("Tender/JoinCommitteeToBranchTenders", null, committeeTenderModel);
            ClearCommitteesCache();
            return Json(Resources.CommitteeResources.ErrorMessages.TenderJoinedSuccess);
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
      public async Task<JsonResult> CancelJoinCommitteeToTender(CommitteeTenderModel committeeTenderModel)
      {
         if (!ModelState.IsValid)
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.ModelDataError);
         try
         {
            await _ApiClient.PostAsync("Tender/CancelJoinCommitteeToTender", null, committeeTenderModel);
            ClearCommitteesCache();
            return Json(Resources.CommitteeResources.ErrorMessages.CancelTenderJoinedSuccess);
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
      public async Task<ActionResult> DetailsAsync(string id)
      {
         try
         {
            var committeeModel = await _ApiClient.GetAsync<CommitteeModel>("Committee/GetById", new Dictionary<string, object> { { nameof(id), Util.Decrypt(id) } });
            return View(committeeModel);
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
      public ActionResult CreateAsync(string committeeTypeId)
      {
         ViewData.Add("CommitteeTypeId", committeeTypeId);
         ViewBag.Title = CommitteeTypeCreateTitleValue(Util.Decrypt(committeeTypeId));
         return View(new CommitteeModel() { CommitteeTypeIdString = committeeTypeId });
      }
      private string CommitteeTypeCreateTitleValue(int committeeTypeId)
      {
         if (committeeTypeId == (int)Enums.CommitteeType.TechincalCommittee)
         {
            return DisplayInputs.AddTechnicalCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee)
         {
            return DisplayInputs.AddOpenOffersCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee)
         {
            return DisplayInputs.AddCheckOffersCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.BlockCommittee)
         {
            return DisplayInputs.AddBlockCommittee;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.PurchaseCommittee)
         {
            return DisplayInputs.AddPurchaseCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee)
         {
            return DisplayInputs.PreQualificationCommitteeAdd;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.VROCommittee)
         {
            return DisplayInputs.VROCommittee;
         }
         return DisplayInputs.TechnicalCommitteeName;
      }

      private string CommitteeTypeEditTitleValue(int committeeTypeId)
      {
         if (committeeTypeId == (int)Enums.CommitteeType.TechincalCommittee)
         {
            return DisplayInputs.EditTechnicalCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee)
         {
            return DisplayInputs.EditOpenOffersCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee)
         {
            return DisplayInputs.EditCheckOffersCommittees;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.BlockCommittee)
         {
            return DisplayInputs.EditBlockCommittee;
         }
         else if (committeeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee)
         {
            return DisplayInputs.EditPreQualificationCommittee;
         }
         return DisplayInputs.EditCommittee;
      }

      [HttpPost]
      public async Task<ActionResult> CreateAsync(CommitteeModel committeeModel)
      {
         ModelState.Remove("CommitteeTypeId");
         if (string.IsNullOrEmpty(committeeModel.CommitteeName))
         {
            ViewData.Add("CommitteeTypeId", committeeModel.CommitteeTypeIdString);
            return View(committeeModel);
         }
         else
         {
            committeeModel.CommitteeName = committeeModel.CommitteeName.Trim();
         }
         if (!ModelState.IsValid)
         {
            AddError(Resources.SharedResources.ErrorMessages.ModelDataError);
            return View(committeeModel);
         }
         try
         {
            committeeModel.CommitteeTypeId = Util.Decrypt(committeeModel.CommitteeTypeIdString);
            committeeModel.AgencyCode = User.UserAgency();
            committeeModel.RelatedAgencyCode = User.UserRelatedAgencyCode();
            await _ApiClient.PostAsync("Committee", null, committeeModel);
            ClearCommitteesCache();
            var message = "";
            switch ((Enums.CommitteeType)Util.Decrypt(committeeModel.CommitteeTypeIdString))
            {
               case Enums.CommitteeType.TechincalCommittee:
                  message = Messages.TechnicalCommitteeAdded;
                  break;
               case Enums.CommitteeType.OpenOfferCommittee:
                  message = Messages.OpenOfferCommitteeAdded;
                  break;
               case Enums.CommitteeType.CheckOfferCommittee:
                  message = Messages.CheckOfferCommitteeAdded;
                  break;
               case Enums.CommitteeType.BlockCommittee:
                  message = Messages.BlockCommitteeAdded;
                  break;
               case Enums.CommitteeType.PurchaseCommittee:
                  message = Messages.PurchaseCommitteeAdded;
                  break;
               case Enums.CommitteeType.PreQualificationCommittee:
                  message = Messages.PreQualificationCommitteeAdd;
                  break;
               default:
                  message = string.Format(Messages.CommitteeSaved, committeeModel.CommitteeName);
                  break;
            }
            AddMessage(message);
            return RedirectToAction(nameof(Index), new { CommitteeTypeId = committeeModel.CommitteeTypeIdString });

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {

            AddError(ex.Message);
            ViewData.Add("CommitteeTypeId", Util.Encrypt(committeeModel.CommitteeTypeId));
            return View(committeeModel);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), new { CommitteeTypeId = committeeModel.CommitteeTypeIdString });
         }
      }

      public async Task<ActionResult> EditAsync(string id)
      {
         try
         {
            var committeeModel = await _ApiClient.GetAsync<CommitteeModel>("Committee/GetById", new Dictionary<string, object> { { nameof(id), Util.Decrypt(id) } });
            if (committeeModel != null)
            {
               ViewData.Add("CommitteeTypeId", committeeModel.CommitteeTypeIdString);
               ViewData.Add("CommitteeId", id);
               ViewBag.Title = CommitteeTypeEditTitleValue(committeeModel.CommitteeTypeId);
               return View(committeeModel);
            }
            return RedirectToAction("index", "tender");
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
      public async Task<ActionResult> EditAsync(CommitteeModel committeeModel)
      {
         if (string.IsNullOrEmpty(committeeModel.CommitteeName))
         {
            ViewData.Add("CommitteeTypeId", committeeModel.CommitteeTypeIdString);
            return View(committeeModel);
         }
         else
         {
            committeeModel.CommitteeName = committeeModel.CommitteeName.Trim();
         }
         if (!ModelState.IsValid)
         {
            AddError(Resources.SharedResources.ErrorMessages.ModelDataError);
            return View(committeeModel);
         }
         try
         {
            committeeModel.CommitteeTypeId = Util.Decrypt(committeeModel.CommitteeTypeIdString);
            committeeModel.CommitteeId = Util.Decrypt(committeeModel.CommitteeIdString);
            var committee = await _ApiClient.PostAsync<CommitteeModel>("Committee/Update", null, committeeModel);
            ClearCommitteesCache();

            AddMessage(string.Format(Resources.CommitteeResources.Messages.CommitteeEdited, committeeModel.CommitteeTypeName));
            return RedirectToAction(nameof(Index), new { CommitteeTypeId = committeeModel.CommitteeTypeIdString });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            ViewData.Add("CommitteeTypeId", Util.Encrypt(committeeModel.CommitteeTypeId));
            return View(committeeModel);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), new { CommitteeTypeId = committeeModel.CommitteeTypeIdString });
         }
      }

      [HttpPost]
      public async Task<ActionResult> DeleteAsync(string committeeIdString, string committeeTypeIdString)
      {
         try
         {
            int committeeId = Util.Decrypt(committeeIdString);
            await _ApiClient.GetStringAsync("Committee/Delete/" + committeeId.ToString(), null);
            ClearCommitteesCache();

            return Json(new { status = "success", message = Resources.TenderResources.Messages.DeletedSuccessfully });
         }
         catch (AuthorizationException ex)
         {
            return Json(new { status = "error", message = ex.Message });
         }
         catch (BusinessRuleException ex)
         {
            return Json(new { status = "error", message = ex.Message });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return Json(new { status = "error", message = ex.Message });
         }
      }
      [HttpPost]
      public async Task<ActionResult> GetUserRolesByIdAndCommitteeType(string userName, int CommitteeTypeId)
      {
         try
         {
            var roles = await _ApiClient.GetListAsync<RoleModel>("Committee/GetUserRolesByIdAndCommitteeType/" + userName + "/" + CommitteeTypeId, null);
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
      public async Task<ActionResult> GetCommitteeUsersAsync(CommitteeUserSearchCriteriaModel criteria)
      {
         try
         {
            criteria.AgencyId = User.UserAgency();
            var usersList = await _ApiClient.GetQueryResultAsync<CommitteeUserViewModel>("Committee/GetCommitteeUsers", criteria.ToDictionary());
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
      public async Task<ActionResult> RemoveAssignedUserAsync(string userId, int committeeId)
      {
         try
         {
            await _ApiClient.PostAsync("Committee/RemoveAssignedUser", new Dictionary<string, object> { { nameof(userId), Util.Decrypt(userId) }, { nameof(committeeId), committeeId } }, null);
            ClearCommitteesCache();
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
      public async Task<List<UserLookupModel>> GetUsersByCommitteeType(CommitteeUserSearchCriteriaModel criteria)
      {
         List<UserLookupModel> committeeUserModel = await _ApiClient.PostAsync<List<UserLookupModel>>("Committee/GetUsersFilteredbyCommitteeTypeId/", null, criteria);
         return committeeUserModel;
      }

      public void ClearCommitteesCache()
      {
         _cache.Remove(CacheKeys.BasicStepCache + "_" + User.UserAgency());
         _cache.Remove(CacheKeys.PurchaseCommitteeCache + "_" + User.UserAgency());
      }
   }
}
