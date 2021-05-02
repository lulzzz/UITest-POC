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

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class ManageVendorsController : BaseController
   {
      private IApiClient _ApiClient;
      private readonly ILogger<ManageVendorsController> _logger;

      public ManageVendorsController(IApiClient ApiClient, ILogger<ManageVendorsController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _ApiClient = ApiClient;
         _logger = logger;
      }

      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.OffersOppeningManager + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.DataEntry + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.PurshaseSpecialist + "," + RoleNames.EtimadOfficer + "," + RoleNames.TechnicalCommitteeUser + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary)]
      public async Task<IActionResult> Index()
      {
         return View();
      }

      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.OffersOppeningManager + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.DataEntry + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.PurshaseSpecialist + "," + RoleNames.EtimadOfficer + "," + RoleNames.TechnicalCommitteeUser + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary)]
      public async Task<IActionResult> IndexPagingAsync(SupplierSearchCretriaModel suppliersSearchCriteria)
      {
         try
         {
            suppliersSearchCriteria.AgencyCode = User.UserAgency();
            var vendors = await _ApiClient.GetQueryResultAsync<SupplierInvitationModel>("ManageVendors/Find", suppliersSearchCriteria.ToDictionary());
            return Json(new PaginationModel(vendors.Items, vendors.TotalCount, vendors.PageSize, vendors.PageNumber, null));
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
      public async Task<List<LookupModel>> GetMainActivitiesByParentId(int? ParentId = 0)
      {
         var result = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetMainActivitiesByParentId/" + ParentId, null);
         return result;
      }

      #region Details
      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.OffersOppeningManager + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.DataEntry + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement)]
      public async Task<ActionResult> DetailsAsync(string userIdString)
      {
         try
         {
            ManageUsersAssignationModel userModel = await _ApiClient.GetAsync<ManageUsersAssignationModel>("ManageUsersAssignation/GetById/" + Util.Decrypt(userIdString), null);
            if (userModel != null)
            {
               EditUserViewModel viewModel = new EditUserViewModel()
               {
                  AgencyNames = userModel.AgencyNames,
                  agencies = userModel.agencies,
                  UserId = userModel.userId,
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
   }
}
