using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.User;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class BlockController : BaseController
   {
      #region constructor
      private IApiClient _ApiClient;
      private readonly ILogger<BlockController> _logger;
      public BlockController(IApiClient ApiClient, ILogger<BlockController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _ApiClient = ApiClient;
         _logger = logger;
      }
      #endregion

      #region old
      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<ActionResult> Index(string supplierBlockId)
      {
         ViewData.Add("SupplierBlockId", supplierBlockId);
         return RedirectToAction(nameof(GetAdminBlockList));
      }

      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<IActionResult> IndexPagingAsync(BlockSearchCriteriaModel blockSearchCriteriaModel)
      {
         try
         {
            blockSearchCriteriaModel.FromDate = blockSearchCriteriaModel.FromDateString;
            blockSearchCriteriaModel.ToDate = blockSearchCriteriaModel.ToDateString;
            var blocks = await _ApiClient.GetQueryResultAsync<SupplierBlockModel>("Block/GetBlocks", blockSearchCriteriaModel.ToDictionary());
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

      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public ActionResult BlockDetails()
      {
         return View("BlockDetails");
      }

      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<ActionResult> DetailsAsync(string id)
      {
         try
         {
            int supplierBlockId = Util.Decrypt(id);
            var blockModel = await _ApiClient.GetAsync<SupplierBlockModel>("Block/GetBlockById/" + supplierBlockId, null);
            if (blockModel != null)
            {
               ViewData.Add("BlockId", supplierBlockId);
            }
            return View(blockModel);
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
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }


      [Authorize(Roles = RoleNames.supplier + "," + RoleNames.AccountsManagementSpecialist)]
      [HttpGet]
      public async Task<ActionResult> BlockDetailsAsync(string id)
      {
         try
         {
            int supplierBlockId = Util.Decrypt(id);
            var blockModel = await _ApiClient.GetAsync<SupplierBlockModel>("Block/GetBlockByIdForSupplier/" + supplierBlockId, null);
            if (blockModel != null)
            {
               ViewData.Add("BlockId", supplierBlockId);
            }
            return View(blockModel);
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
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpGet]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<ActionResult> CreateAsync(string Id, string name)
      {
         SupplierBlockModel supModel = new SupplierBlockModel() { CommercialRegistrationNo = Id, CommercialSupplierName = name };
         ViewData.Add("BlockId", supModel.CommercialRegistrationNo);
         return View(supModel);
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<ActionResult> CreateAsync([FromForm] SupplierBlockModel supplierBlockModel)
      {
         supplierBlockModel.SupplierTypeId = 1;
         supplierBlockModel.IsOldSystem = true;
         ModelState.Remove("SecretaryFileNetReferenceId");
         ModelState.Remove("CommercialRegistrationNoOrigin");
         ModelState.Remove("LicenseNumber");
         if (supplierBlockModel.BlockTypeId == 1)
         {

            ModelState.Remove("BlockEndDate");
         }

         else
         {
            if (supplierBlockModel.BlockEndDate <= supplierBlockModel.BlockStartDate)
            {

               ModelState.AddModelError("BlockEndDate", MOF.Etimad.Monafasat.Resources.SharedResources.ErrorMessages.EndDateLessThanStartDate);
            }

         }
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(supplierBlockModel);
         }
         try
         {
            string file;
            string[] arr;
            if (!string.IsNullOrEmpty(supplierBlockModel.AttachmentIdRef))
            {
               if (supplierBlockModel.AttachmentIdRef.Contains("/GetFile/"))
               {
                  string[] lst = supplierBlockModel.AttachmentIdRef.Split("/GetFile/");
                  file = lst[lst.Length - 1];
                  arr = file.Split(":");
               }
               else
                  arr = supplierBlockModel.AttachmentIdRef.Split(":");

               supplierBlockModel.FileNetReferenceId = arr[0];
               supplierBlockModel.FileName = arr[1];
            }
            await _ApiClient.PostAsync("Block/AddBlock", null, supplierBlockModel);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction("index");
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            ViewData.Add("BlockId", supplierBlockModel.CommercialRegistrationNo);
            return View(supplierBlockModel);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Block");
         }
      }


      [HttpGet]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<ActionResult> CreateMinistryBlockAsync(string Id, string name)
      {

         AdminSupplierBlockModel supModel = new AdminSupplierBlockModel() { CommercialRegistrationNo = Id, CommercialSupplierName = name };
         ViewData.Add("BlockId", supModel.CommercialRegistrationNo);

         return View(supModel);
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<ActionResult> CreateMinistryBlockAsync([FromForm] AdminSupplierBlockModel supplierBlockModel)
      {
         supplierBlockModel.SupplierTypeId = 1;
         ModelState.Remove("LicenseNumber");
         ModelState.Remove("CommercialRegistrationNoOrigin");
         ModelState.Remove("OrganizationTypeId");
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(supplierBlockModel);
         }
         try
         {
            string file;
            string[] arr;
            if (!string.IsNullOrEmpty(supplierBlockModel.AdminFileNetReferenceId))
            {
               if (supplierBlockModel.AdminFileNetReferenceId.Contains("/GetFile/"))
               {
                  string[] lst = supplierBlockModel.AdminFileNetReferenceId.Split("/GetFile/");
                  file = lst[lst.Length - 1];
                  arr = file.Split(":");
               }
               else
                  arr = supplierBlockModel.AdminFileNetReferenceId.Split(":");

               supplierBlockModel.AdminFileNetReferenceId = arr[0];
               supplierBlockModel.AdminFileName = arr[1];
            }
            await _ApiClient.PostAsync("Block/AddAdminBlock", null, supplierBlockModel);
            AddMessage(Resources.BlockResources.Messages.RequestSentSuccessfully);
            return RedirectToAction(nameof(GetAdminBlockList));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            ViewData.Add("BlockId", supplierBlockModel.CommercialRegistrationNo);
            return View(supplierBlockModel);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Block");
         }
      }

      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      [HttpGet]
      public async Task<ActionResult> GetCrNameAsync(string Cr)
      {
         try
         {
            var name = await _ApiClient.PostAsync("Block/GetCrName/" + Cr, null, null);
            return Ok(name);
         }
         catch (ArgumentNullException ex)
         {
            return JsonErrorMessage(Resources.BlockResources.ErrorMessages.EnterCommercialRegistrationNo);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(Resources.BlockResources.ErrorMessages.CrNotFound);
         }
         catch (Exception ex)
         {
            return JsonErrorMessage(Resources.SharedResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpGet]
      public async Task<ActionResult> GetAllManagers(string agencyCode, int categoryId)
      {
         UsersSearchCriteriaModel usersSearchCriteria = new UsersSearchCriteriaModel();
         usersSearchCriteria.AgencyId = agencyCode;
         usersSearchCriteria.RoleNames.Add(RoleNames.MonafasatAdmin);
         usersSearchCriteria.categoryId = categoryId;
         usersSearchCriteria.PageSize = 1000;
         var users = await _ApiClient.GetQueryResultAsync<ManageUsersAssignationModel>("ManageUsersAssignation/Find", usersSearchCriteria.ToDictionary());
         return Json(new PaginationModel(users.Items, users.TotalCount, users.PageSize, users.PageNumber, null));
      }

      [HttpGet]
      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<ActionResult> EditAsync(string Id)
      {
         try
         {
            int supplierBlockId = Util.Decrypt(Id);
            var blockModel = await _ApiClient.GetAsync<CommitteeModel>("Block/GetBlockById", new Dictionary<string, object> { { nameof(Id), Util.Decrypt(Id) } });
            if (blockModel != null)
            {
               ViewData.Add("BlockId", supplierBlockId);
            }
            return View(blockModel);
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

      [HttpPost]
      [ValidateAntiForgeryToken]
      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<ActionResult> EditAsync(SupplierBlockModel supplierBlockModel)
      {
         var bookletReferences = supplierBlockModel.AttachmentIdRef;
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(supplierBlockModel);
         }
         try
         {
            var blocks = await _ApiClient.PostAsync<SupplierBlockModel>("Block/UpdateBlock", null, supplierBlockModel);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index), new { SupplierBlockId = supplierBlockModel.SupplierBlockId });
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

      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<ActionResult> DeleteAsync(string Id)
      {
         try
         {
            int supplierBlockId = Util.Decrypt(Id);
            await _ApiClient.PostAsync("Block/DeactivateBlock/" + supplierBlockId, null, null);
            return Json(new { status = "success", message = Resources.BlockResources.Messages.DataRemoved });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return Json(new { status = "error", message = ex.Message });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            return Json(new { status = "error", message = Resources.TenderResources.ErrorMessages.UnexpectedError });
         }
      }

      [Authorize(RoleNames.AdminBlackListAccountManagerAndCustomerServicePolicy)]
      public ActionResult GetBlockListUsers(string CommertialRegistrationNo)
      {
         ViewData.Add("CommertialRegistrationNo", CommertialRegistrationNo);
         return View();
      }

      [Authorize(RoleNames.AdminBlackListAccountManagerAndCustomerServicePolicy)]
      public async Task<IActionResult> GetBlockListUsersPagingAsync(BlockCommitteeSearchCriteriaModel blockCommitteeSearchCriteriaModel)
      {
         try
         {
            var blocks = await _ApiClient.GetQueryResultAsync<UserProfileModel>("Block/GetBlockListUsers", blockCommitteeSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(blocks.Items, blocks.TotalCount, blocks.PageSize, blocks.PageNumber, null));
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

      [Authorize(RoleNames.AdminBlackListAccountManagerAndCustomerServicePolicy)]
      public ActionResult GetBlockCommittee(string CommertialRegistrationNo)
      {
         ViewData.Add("CommertialRegistrationNo", CommertialRegistrationNo);
         return View();
      }

      [Authorize(RoleNames.AdminBlackListAccountManagerAndCustomerServicePolicy)]
      public async Task<IActionResult> GetBlockCommitteePagingAsync(BlockCommitteeSearchCriteriaModel blockCommitteeSearchCriteriaModel)
      {
         try
         {
            var blocks = await _ApiClient.GetQueryResultAsync<BlockCommitteeModel>("Block/GetBlockCommittee", blockCommitteeSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(blocks.Items, blocks.TotalCount, blocks.PageSize, blocks.PageNumber, null));
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
      #endregion

      #region Supplier

      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<IActionResult> GetSupplier(SupplierSearchCretriaModel supplierSearchCriteriaModel)

      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<SupplierModel>("Block/GetAllSuppliers", supplierSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
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

      #endregion

      #region Admin

      // [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]

      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<IActionResult> GetAdminBlockList()
      {
         var agency = await _ApiClient.GetAsync<GovAgencyModel>("Block/GetAgency", null);
         ViewBag.IsOldSystem = agency.IsOldSystem;
         return View();
      }

      //[Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<IActionResult> GetAdminBlockListAsync(BlockSearchCriteriaModel blockSearchCriteriaModel)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<AdminSupplierBlockModel>("Block/GetAdminBlockList", blockSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(GetAdminBlockList));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetAdminBlockList));
         }
      }

      [HttpGet]
      //[Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<ActionResult> CreateAdminBlock(string Id, string name)
      {

         AdminSupplierBlockModel supModel = new AdminSupplierBlockModel() { CommercialRegistrationNo = Id, CommercialSupplierName = name };
         ViewData.Add("BlockId", supModel.CommercialRegistrationNo);

         return View(supModel);
      }

      [HttpPost]
      //[Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      public async Task<ActionResult> CreateAdminBlock([FromForm] AdminSupplierBlockModel supplierBlockModel)
      {

         if (supplierBlockModel.SupplierTypeId == 1)
         {
            ModelState.Remove("LicenseNumber");
            ModelState.Remove("CommercialRegistrationNoOrigin");
            ModelState.Remove("OrganizationTypeId");
         }
         else if (supplierBlockModel.SupplierTypeId == 2 && supplierBlockModel.OrganizationTypeId == 1)
         {
            supplierBlockModel.CommercialRegistrationNo = supplierBlockModel.CommercialRegistrationNoOrigin;
            ModelState.Remove("LicenseNumber");
            ModelState.Remove("CommercialRegistrationNo");
         }
         else if (supplierBlockModel.SupplierTypeId == 2 && supplierBlockModel.OrganizationTypeId == 2)
         {
            supplierBlockModel.CommercialRegistrationNo = supplierBlockModel.LicenseNumber;
            ModelState.Remove("CommercialRegistrationNoOrigin");
            ModelState.Remove("CommercialRegistrationNo");
         }
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(supplierBlockModel);
         }
         try
         {
            string file;
            string[] arr;
            if (!string.IsNullOrEmpty(supplierBlockModel.AdminFileNetReferenceId))
            {
               if (supplierBlockModel.AdminFileNetReferenceId.Contains("/GetFile/"))
               {
                  string[] lst = supplierBlockModel.AdminFileNetReferenceId.Split("/GetFile/");
                  file = lst[lst.Length - 1];
                  arr = file.Split(":");
               }
               else
                  arr = supplierBlockModel.AdminFileNetReferenceId.Split(":");

               supplierBlockModel.AdminFileNetReferenceId = arr[0];
               supplierBlockModel.AdminFileName = arr[1];
            }
            await _ApiClient.PostAsync("Block/AddAdminBlock", null, supplierBlockModel);
            AddMessage(Resources.BlockResources.Messages.RequestSentSuccessfully);
            return RedirectToAction(nameof(GetAdminBlockList));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(CreateAdminBlock));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(CreateAdminBlock));
         }
      }

      #endregion

      #region Secretary
      [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
      //[Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public IActionResult GetSecretaryBlockList()
      {
         return View();
      }

      //[Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
      public async Task<IActionResult> GetSecretaryBlockListAsync(BlockSearchCriteriaModel blockSearchCriteriaModel)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<SupplierBlockModel>("Block/GetSecretaryBlockList", blockSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(GetSecretaryBlockListAsync), "Block");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetSecretaryBlockListAsync), "Block");
         }
      }

      [HttpGet]
      [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
      public async Task<ActionResult> SecretaryBlockDetails(string Id)
      {
         try
         {
            int supplierBlockId = Util.Decrypt(Id);
            var blockModel = await _ApiClient.GetAsync<SupplierBlockModel>("Block/GetBlockById/" + supplierBlockId, null);
            if (blockModel != null)
            {
               ViewData.Add("BlockId", supplierBlockId);
            }
            return View(blockModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError("تم حذف الطلب");//ex.Message);
            //return RedirectToAction(nameof(GetSecretaryBlockListAsync));
            return RedirectToAction(nameof(Index), "Dashboard");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetSecretaryBlockListAsync));
         }
      }

      [HttpGet]
      [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
      public async Task<ActionResult> CreateSecretaryBlock(string SupplierBlockId, string SupplierId, string name)
      {
         SupplierBlockModel blockModel = new SupplierBlockModel();
         try
         {
            if (SupplierBlockId != null)
            {
               int supplierBlockId = Util.Decrypt(SupplierBlockId);
               blockModel = await _ApiClient.GetAsync<SupplierBlockModel>("Block/GetBlockById/" + supplierBlockId, null);
               if (blockModel != null)
               {
                  ViewData.Add("BlockId", supplierBlockId);
               }
               return View(blockModel);
            }
            else
            {
               blockModel = new SupplierBlockModel() { CommercialRegistrationNo = SupplierId, CommercialSupplierName = name };
               ViewData.Add("BlockId", blockModel.CommercialRegistrationNo);
               return View(blockModel);
            }
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return View(blockModel);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetSecretaryBlockListAsync));
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
      public async Task<ActionResult> CreateSecretaryBlock(SupplierBlockModel supplierBlockModel)
      {
         ModelState.Remove("Violation");
         ModelState.Remove("AttachmentIdRef");
         ModelState.Remove("BlockDetails");
         if (supplierBlockModel.SupplierTypeId == 1)
         {
            ModelState.Remove("LicenseNumber");
            ModelState.Remove("CommercialRegistrationNoOrigin");
            ModelState.Remove("OrganizationTypeId");

         }
         else if (supplierBlockModel.SupplierTypeId == 2 && supplierBlockModel.OrganizationTypeId == 1)
         {
            ModelState.Remove("LicenseNumber");
            ModelState.Remove("CommercialRegistrationNo");
         }
         else if (supplierBlockModel.SupplierTypeId == 2 && supplierBlockModel.OrganizationTypeId == 2)
         {
            ModelState.Remove("CommercialRegistrationNoOrigin");
            ModelState.Remove("CommercialRegistrationNo");
         }
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(supplierBlockModel);
         }
         try
         {
            string file;
            string[] arr;
            if (!string.IsNullOrEmpty(supplierBlockModel.SecretaryFileNetReferenceId))
            {
               if (supplierBlockModel.SecretaryFileNetReferenceId.Contains("/GetFile/"))
               {
                  string[] lst = supplierBlockModel.SecretaryFileNetReferenceId.Split("/GetFile/");
                  file = lst[lst.Length - 1];
                  arr = file.Split(":");
               }
               else
                  arr = supplierBlockModel.SecretaryFileNetReferenceId.Split(":");

               supplierBlockModel.AdminFileNetReferenceId = arr[0];
               supplierBlockModel.AdminFileName = arr[1];
            }
            await _ApiClient.PostAsync("Block/AddSecretaryBlock", null, supplierBlockModel);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(GetSecretaryBlockList));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return View(supplierBlockModel);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetSecretaryBlockList));
         }
      }

      #endregion

      #region Manager

      [Authorize(Roles = RoleNames.MonafasatBlackListCommittee)]
      public IActionResult GetManagerBlockList()
      {
         return View();
      }

      [Authorize(Roles = RoleNames.MonafasatBlackListCommittee)]
      public async Task<IActionResult> GetManagerBlockListAsync(BlockSearchCriteriaModel blockSearchCriteriaModel)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<SupplierBlockModel>("Block/GetManagerBlockList", blockSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(GetManagerBlockList), "Block");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetManagerBlockList), "Block");
         }
      }

      [HttpGet]
      [Authorize(Roles = RoleNames.MonafasatBlackListCommittee)]
      public async Task<ActionResult> ManagerBlockDetails(string Id)
      {
         try
         {
            int supplierBlockId = Util.Decrypt(Id);

            var blockModel = await _ApiClient.GetAsync<SupplierBlockModel>("Block/GetBlockById/" + supplierBlockId, null);

            return View(blockModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(GetManagerBlockList));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetManagerBlockList));
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.MonafasatBlackListCommittee)]
      public async Task<ActionResult> ManagerRejectionReason(SupplierBlockModel blockSearchCriteria)
      {
         try
         {
            blockSearchCriteria.SupplierBlockId = Util.Decrypt(blockSearchCriteria.SupplierBlockIdString);
            await _ApiClient.PostAsync<TenderChangeRequestModel>("Block/ManagerRejectionReason", null, blockSearchCriteria);
            return RedirectToAction(nameof(GetManagerBlockList));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(GetManagerBlockList));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetManagerBlockList));
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
      public async Task<ActionResult> SecretaryRejectionReason(SupplierBlockModel blockSearchCriteria)
      {
         try
         {
            blockSearchCriteria.SupplierBlockId = Util.Decrypt(blockSearchCriteria.SupplierBlockIdString);
            await _ApiClient.PostAsync<TenderChangeRequestModel>("Block/SecretaryRejectionReason", null, blockSearchCriteria);
            return RedirectToAction(nameof(GetSecretaryBlockList));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(GetSecretaryBlockList));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetSecretaryBlockList));
         }
      }

      [HttpPost]
      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<ActionResult> ManagerApproval(string Id, int verificationCode)
      {
         try
         {
            int supplierBlockId = Util.Decrypt(Id);
            await _ApiClient.PostAsync("Block/ManagerApproval/" + supplierBlockId + "/" + verificationCode, null, null);
            return RedirectToAction(nameof(GetManagerBlockList));
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
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetManagerBlockList));
         }
      }

      [HttpGet]
      [Authorize(Roles = RoleNames.MonafasatBlockListSecritary)]
      public async Task<ActionResult> SecretaryApproval(string Id)
      {
         try
         {
            int supplierBlockId = Util.Decrypt(Id);
            await _ApiClient.PostAsync("Block/SecretaryApproval/" + supplierBlockId, null, null);
            return RedirectToAction(nameof(GetSecretaryBlockList));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(GetSecretaryBlockList));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetSecretaryBlockList));
         }
      }

      #endregion

      #region General

      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<IActionResult> GetIDMSupplier(SupplierSearchCretriaModel supplierSearchCriteriaModel)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<SupplierIntegrationViewModel>("Block/GetAllIDMSuppliers", supplierSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(GetAdminBlockListAsync));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetAdminBlockListAsync));
         }
      }


      [Authorize(Roles = RoleNames.MonafasatBlockListSecritary + "," + RoleNames.MonafasatBlackListCommittee)]
      public IActionResult ReviewBlockingList()
      {
         return View();
      }

      [Authorize(Roles = RoleNames.MonafasatBlockListSecritary + "," + RoleNames.MonafasatBlackListCommittee)]
      public async Task<IActionResult> ReviewBlockingListAsync(BlockSearchCriteriaModel supplierSearchCriteriaModel)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<SupplierBlockModel>("Block/GetBlockingListDetails", supplierSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(ReviewBlockingListAsync));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetAdminBlockListAsync));
         }
      }





      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.AccountsManagementSpecialist)]
      public IActionResult AdminReviewBlockingList()
      {
         return View();
      }
      [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.AccountsManagementSpecialist)]
      public async Task<IActionResult> AdminReviewBlockingListAsync(BlockSearchCriteriaModel supplierSearchCriteriaModel)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<SupplierBlockModel>("Block/GetAdminBlockingListDetails", supplierSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AdminReviewBlockingListAsync));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(AdminReviewBlockingListAsync));
         }
      }

      #endregion

      #region  Verification code  

      [HttpPost]
      public async Task<ActionResult> CreateVerificationCode(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Block/CreateVerificationCode/" + tenderId, null, null);
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

      #region  Agency Configuration  

      [Authorize(RoleNames.MonafasatAccountManagerPolicy)]
      public async Task<ActionResult> GetAgencyExceptedModel()
      {
         return View();
      }

      [Authorize(RoleNames.MonafasatAccountManagerPolicy)]
      public async Task<IActionResult> GetAgencyExceptedModelAsync(BlockSearchCriteriaModel blockSearchCriteriaModel)
      {
         try
         {
            var profile = await _ApiClient.GetAsync<QueryResult<AgencyExceptedModel>>("Block/GetAgencyExceptedModel", blockSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(profile.Items, profile.TotalCount, profile.PageSize, profile.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(RoleNames.MonafasatAccountManagerPolicy)]
      public async Task<ActionResult> EditAgency(string Id)
      {
         try
         {
            if (!string.IsNullOrEmpty(Id))
            {
               TenderSearchCriteriaModel tenderSearchCriteriaModel = new TenderSearchCriteriaModel();
               tenderSearchCriteriaModel.AgencyCode = Id;
               var result = await _ApiClient.GetAsync<int[]>("Tender/FindTenderByAgencyCodeForPurchaseMethod/", tenderSearchCriteriaModel.ToDictionary());


               var agencyModel = await _ApiClient.GetAsync<AgencyExceptedModel>("Block/GetAgencyExceptedById/" + Id, null);
               ViewBag.AgencyId = Id;
               ViewData.Add("AgencyIdString", Id);

               agencyModel.IsRelatedToTender = agencyModel.PurchaseMethods.Any(x => result.Contains(x));

               var oldGroup = new SelectListGroup { Name = "اساليب شراء نظام قديم" };
               var newGroup = new SelectListGroup { Name = "اساليب شراء نظام جديد" };
               var vroGroup = new SelectListGroup { Name = "VRO" };
               List<SelectListItem> tenderType = new List<SelectListItem>();


               foreach (PurchaseMethodsModel purchaseMethod in agencyModel.TenderTypes)
               {
                  if (purchaseMethod.PurchaseMethodId == (int)Enums.TenderType.CurrentDirectPurchase || purchaseMethod.PurchaseMethodId == (int)Enums.TenderType.CurrentTender)
                     tenderType.Add(new SelectListItem
                     {
                        Value = purchaseMethod.PurchaseMethodId.ToString(),
                        Text = purchaseMethod.PurchaseMethodName.ToString(),
                        Group = oldGroup,

                     });
                  //else if (purchaseMethod.PurchaseMethodId == (int)Enums.TenderType.NationalTransformationProjects && (agencyModel.IsRelated == true || agencyModel.IsVro == true))
                  //   tenderType.Add(new SelectListItem
                  //   {
                  //      Value = purchaseMethod.PurchaseMethodId.ToString(),
                  //      Text = purchaseMethod.PurchaseMethodName.ToString(),
                  //      Group = vroGroup,
                  //     // Disabled = true
                  //   });
                  else if (purchaseMethod.PurchaseMethodId != (int)Enums.TenderType.NationalTransformationProjects)
                  {
                     tenderType.Add(new SelectListItem
                     {
                        Value = purchaseMethod.PurchaseMethodId.ToString(),
                        Text = purchaseMethod.PurchaseMethodName.ToString(),
                        Group = newGroup,

                     });
                  }

               }
               ViewBag.PurchaseTypes = tenderType;

               return View(agencyModel);
            }
            else return View();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(GetAgencyExceptedModel));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetAgencyExceptedModel));
         }
      }

      [Authorize(RoleNames.MonafasatAccountManagerPolicy)]
      [HttpPost]
      public async Task<ActionResult> SaveAgency(AgencyExceptedModel agencyExceptedModel)
      {
         try
         {
            var agencyModel = await _ApiClient.PostAsync<bool>("Block/SaveAgency", null, agencyExceptedModel);
            return RedirectToAction(nameof(GetAgencyExceptedModel));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(EditAgency), new { Id = agencyExceptedModel.AgencyIdString });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(GetAgencyExceptedModelAsync));
         }
      }

      [Authorize(RoleNames.MonafasatAccountManagerPolicy)]
      public async Task<IActionResult> GetAgencyById(string id)
      {
         try
         {
            AgencyExceptedModel profile = await _ApiClient.PostAsync<AgencyExceptedModel>("Block/GetAgencyExceptedById/" + id, null, null);

            return Json(profile);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(RoleNames.MonafasatAccountManagerPolicy)]
      public async Task<IActionResult> UpdateAgencyAsync()
      {
         try
         {
            var profile = await _ApiClient.GetAsync<List<TenderTypeModel>>("Lookup/GetTenderTypes", null);
            return Json(profile);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [Authorize(RoleNames.AdminAndBlackListCommitteePolicy)]
      public async Task<ActionResult> ResetSupplier(string Id)
      {
         try
         {
            int supplierBlockId = Util.Decrypt(Id);
            await _ApiClient.PostAsync("Block/ResetSupplier/" + supplierBlockId, null, null);
            return Json(new { status = "success", message = Resources.BlockResources.Messages.ResetSupplier });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return Json(new { status = "error", message = ex.Message });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            return Json(new { status = "error", message = Resources.TenderResources.ErrorMessages.UnexpectedError });
         }
      }


      public async Task<IActionResult> DownloadSupplierReportForAdmin(BlockSearchCriteriaModel supplierSearchCretriaModel)
      {

         var downloadFileContents = await _ApiClient.GetAsync<byte[]>("Block/DownloadSupplierBlockedAccordingAgency", supplierSearchCretriaModel.ToDictionary());
         if (downloadFileContents == null || downloadFileContents.Length == 0)
         {
            return NotFound();
         }
         return File(
            fileContents: downloadFileContents,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: Resources.BlockResources.DisplayInputs.BlockSuppliersBlockedReport + ".xlsx"
         );
      }

      public async Task<IActionResult> DownloadSupplierReport(BlockSearchCriteriaModel supplierSearchCretriaModel)
      {

         var downloadFileContents = await _ApiClient.GetAsync<byte[]>("Block/DownloadSupplierBlocked", supplierSearchCretriaModel.ToDictionary());
         if (downloadFileContents == null || downloadFileContents.Length == 0)
         {
            return NotFound();
         }
         return File(
            fileContents: downloadFileContents,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: Resources.BlockResources.DisplayInputs.BlockSuppliersBlockedReport + ".xlsx"
         );
      }

      #endregion
   }
}

