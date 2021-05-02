using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class ManageUsersAssignationController : BaseController
   {
      private IApiClient _ApiClient;
      private readonly ILogger<ManageUsersAssignationController> _logger;

      public ManageUsersAssignationController(IApiClient ApiClient, ILogger<ManageUsersAssignationController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _ApiClient = ApiClient;
         _logger = logger;
      }

      public async Task<IActionResult> Index()
      {
         ViewBag.Roles = await _ApiClient.GetAsync<List<RoleModel>>("ManageUsersAssignation/GetIDMRolesAsync", null);
         return View();
      }

      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
      public async Task<IActionResult> IndexPagingAsync(UsersSearchCriteriaModel usersSearchCriteria)
      {
         try
         {
            if (User.IsInRole(RoleNames.UnitManagerUser) || User.IsInRole(RoleNames.UnitBusinessManagement) || User.IsInRole(RoleNames.UnitSpecialistLevel1) || User.IsInRole(RoleNames.UnitSpecialistLevel2))
               usersSearchCriteria.RoleName = RoleNames.OffersCheckSecretary;
            var users = await _ApiClient.GetQueryResultAsync<ManageUsersAssignationModel>("ManageUsersAssignation/Find", usersSearchCriteria.ToDictionary());
            return Json(new PaginationModel(users.Items, users.TotalCount, users.PageSize, users.PageNumber, null));
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

      #region EditAsync
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<ActionResult> EditAsync(string userIdString, string nationalId)
      {
         try
         {
            ManageUsersAssignationModel userModel = await _ApiClient.GetAsync<ManageUsersAssignationModel>("ManageUsersAssignation/GetById/" + nationalId, null);
            if (userModel != null)
            {
               var committeeNotAssignToUser = await _ApiClient.GetAsync<List<CommitteeModel>>("ManageUsersAssignation/GetCommitteeNotAssignedToUser/" + Util.Decrypt(userIdString), null);
               var branchesNotAssignToUser = await _ApiClient.GetAsync<List<BranchModel>>("ManageUsersAssignation/GetBranchesNotAssignedToUser/" + Util.Decrypt(userIdString), null);
               EditUserViewModel viewModel = new EditUserViewModel()
               {
                  AgencyNames = userModel.AgencyNames,
                  UserId = userModel.userId,
                  UserIdString = Util.Encrypt(userModel.userId),
                  UserName = userModel.fullName,
                  MobileNumber = userModel.mobileNumber,
                  Email = userModel.email,
                  NationalId = userModel.nationalId,
                  Roles = userModel.roles,
                  CommitteeNotAssignToUser = committeeNotAssignToUser,
                  BranchesNotAssignToUser = branchesNotAssignToUser,
                  BranchRoles = userModel.BranchRoles,
                  AllCommitteeRoles = userModel.AllCommitteeRoles,

                  CommitteeOpenOfferRoles = userModel.CommitteeOpenOfferRoles,
                  CommitteeAuditOfferRoles = userModel.CommitteeAuditOfferRoles,
                  CommitteeTechnicalRoles = userModel.CommitteeTechnicalRoles,
                  CommitteeBlockRoles = userModel.CommitteeBlockRoles,
                  CommitteeOpenAndCheckRoles = userModel.CommitteeOpenAndCheckRoles,
                  CommitteePurchaseRoles = userModel.CommitteePurchaseRoles,
                  CommitteePreQualificationRoles = userModel.CommitteePreQualificationRoles,
               };
               return View(viewModel);
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
      #endregion

      #region Details
      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
      public async Task<ActionResult> DetailsAsync(string nationalId)
      {
         try
         {
            ManageUsersAssignationModel userModel = await _ApiClient.GetAsync<ManageUsersAssignationModel>("ManageUsersAssignation/GetById/" + nationalId, null);
            if (userModel != null)
            {
               EditUserViewModel viewModel = new EditUserViewModel()
               {
                  AgencyNames = userModel.AgencyNames,
                  UserId = userModel.userId,
                  Roles = userModel.roles,
                  UserIdString = Util.Encrypt(userModel.userId),
                  UserName = userModel.fullName,
                  MobileNumber = userModel.mobileNumber,
                  Email = userModel.email,
                  NationalId = userModel.nationalId,
               };
               return View(viewModel);
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
      #endregion

      #region Users To Branch
      [HttpPost]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<ActionResult> AssignUsersToBranchAsync(BranchUserAssignModel branchUserModel)
      {
         if (!ModelState.IsValid)
         {
            _logger.LogError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.ModelDataError);
         }
         try
         {
            IsUserToBranchRelationValid(branchUserModel);
            branchUserModel.UserId = Util.Decrypt(branchUserModel.UserIdString);
            await _ApiClient.PostAsync("ManageUsersAssignation/AssignUsersBranch", null, branchUserModel);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [HttpPost]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<List<BranchModel>> GetBranchesByUserAndRoleId(int userName, string roleId)
      {
         {
            var branchesNotAssignToUser = await _ApiClient.GetListAsync<BranchModel>("ManageUsersAssignation/GetBranchesNotAssignedByUserAndRole/" + userName + "/" + roleId, null);
            return branchesNotAssignToUser;
         }

      }

      private void IsUserToBranchRelationValid(BranchUserAssignModel branchUserModel)
      {
         string[] allowedRoles = { Enum.GetName(typeof(UserRole), UserRole.NewMonafasat_Auditer), Enum.GetName(typeof(UserRole), UserRole.NewMonafasat_DataEntry), Enum.GetName(typeof(UserRole), UserRole.NewMonafasat_PlanningOfficer), Enum.GetName(typeof(UserRole), UserRole.NewMonafasat_PlanningApprover), Enum.GetName(typeof(UserRole), UserRole.NewMonafasat_ApproveTenderAward), Enum.GetName(typeof(UserRole), UserRole.NewMonafasat_PurshaseSpecialist), Enum.GetName(typeof(UserRole), UserRole.NewMonafasat_EtimadOfficer) };
         if (branchUserModel.BranchId <= 0 || String.IsNullOrEmpty(branchUserModel.UserIdString) || String.IsNullOrEmpty(branchUserModel.RoleName) || string.IsNullOrEmpty(branchUserModel.RoleName))
            throw new BusinessRuleException(Resources.UsersResources.ErrorMessages.AddUserBranchErrorMessage);
         else if (Array.IndexOf(allowedRoles, branchUserModel.RoleName) == -1)
         {
            throw new BusinessRuleException(Resources.UsersResources.ErrorMessages.UserBranchNotMatchErrorMessage);
         }
      }

      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      [HttpPost]
      public async Task<ActionResult> RemoveBranchAssignedUserAsync(string branchUserId, string userIdString, int branchId, string roleName, int roleId)
      {
         try
         {
            if (string.IsNullOrEmpty(userIdString)) { throw new ArgumentNullException("userIdString is null"); }
            var userId = Util.Decrypt(userIdString);
            await _ApiClient.PostAsync("ManageUsersAssignation/RemoveBranchAssignedUser", new Dictionary<string, object> { { nameof(userId), userId }, { nameof(branchId), branchId }, { nameof(roleId), roleId } }, null);
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

      #region Users To Committees
      [HttpPost]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<ActionResult> AssignUsersToCommitteesAsync(CommitteeUserAssignModel committeeUserModel)
      {
         if (!ModelState.IsValid)
         {
            _logger.LogError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.ModelDataError);
         }
         try
         {
            committeeUserModel.UserId = Util.Decrypt(committeeUserModel.UserIdString);
            await _ApiClient.PostAsync("ManageUsersAssignation/AssignUsersCommittee", null, committeeUserModel);
            return Json(committeeUserModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      [HttpPost]
      public async Task<ActionResult> RemoveCommitteeAssignedUserAsync(string committeeUserId, string userIdString, int committeeId, string roleName)
      {
         try
         {
            if (string.IsNullOrEmpty(userIdString)) { throw new ArgumentNullException("userIdString is null"); }
            var userId = Util.Decrypt(userIdString);
            await _ApiClient.PostAsync("ManageUsersAssignation/RemoveCommitteeAssignedUser", new Dictionary<string, object> { { nameof(userId), userId }, { nameof(committeeId), committeeId }, { nameof(roleName), roleName } }, null);
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

      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
      [HttpGet]
      public async Task<ActionResult> GetBranchAndCommitteeUsersAsync(BranchCommitteeUserSearchCriteriaModel model)
      {
         try
         {
            var committeeBrannchesAssignToUser = await _ApiClient.GetQueryResultAsync<UserCommitteeBranchesModel>("ManageUsersAssignation/GetUserCommitteeBranchesModel", model.ToDictionary());
            return Json(new PaginationModel(committeeBrannchesAssignToUser.Items, committeeBrannchesAssignToUser.TotalCount, model.PageSize, model.PageNumber, null));
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

      #region Lookups
      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
      [HttpGet]
      public async Task<List<GovAgencyModel>> GetAllAgenciesAsync()
      {
         var result = await _ApiClient.GetListAsync<GovAgencyModel>("ManageUsersAssignation/GetALlAgencies/", null);
         return result;
      }
      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
      [HttpGet]
      public async Task<List<LookupModel>> GetAllBranchesByAgencyCode(string agency)
      {
         if (!(User.IsInRole(RoleNames.MonafasatAccountManager) || User.IsInRole(RoleNames.CustomerService)))
            agency = User.UserAgency();
         var result = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllBranchesByAgencyCode/" + agency, null);
         return result;
      }
      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService)]
      [HttpGet]
      public async Task<List<CommitteeModel>> GetCommitteeNotAssignedByTypeAsync(string userIdString, int committeeTypeId)
      {
         var committeeNotAssignToUser = await _ApiClient.GetAsync<List<CommitteeModel>>("ManageUsersAssignation/GetCommitteeNotAssignedByTypeAsync/" + Util.Decrypt(userIdString) + "/" + committeeTypeId, null);
         return committeeNotAssignToUser;
      }
      #endregion


      public async Task<ActionResult> RestPermission(string uId)
      {
         try
         {
            await _ApiClient.PostAsync("ManageUsersAssignation/UserPermissionRest", new Dictionary<string, object> { { nameof(uId), uId } }, null);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), "Block");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Block");
         }
      }
   }
}
