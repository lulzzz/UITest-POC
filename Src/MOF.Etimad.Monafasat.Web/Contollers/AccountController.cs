using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   [Route("account")]
   [Authorize]
   public class AccountController : BaseController
   {
      //private readonly IConfiguration _configuration;
      private readonly IHttpContextAccessor _httpcontext;
      private IApiClient _ApiClient;
      private readonly ILogger<AccountController> _logger;
      private IMemoryCache _cache;
      private const string KeyPrefix = "AuthSessionStore-";

      public AccountController(/*IConfiguration configuration, */IApiClient ApiClient, ILogger<AccountController> logger, IHttpContextAccessor httpContext, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         //_configuration = configuration;
         _ApiClient = ApiClient;
         _logger = logger;
         _httpcontext = httpContext;
         _cache = new MemoryCache(new MemoryCacheOptions());
      }

      [AllowAnonymous]
      [HttpGet("Logout")]
      public async Task Logout()
      {
         //var discoveryClient = new DiscoveryClient(_configuration.GetSection("Authority:AuthorityURL").Value);
         //var metaDataResponse = await discoveryClient.GetAsync();
         await HttpContext.SignOutAsync("Cookies");
         await HttpContext.SignOutAsync("oidc");
         var key = KeyPrefix + _httpcontext.HttpContext.Session.GetString("IdSession");
         _cache.Remove(key);
      }

      [HttpGet("changeComericalRegisteration")]
      public async Task<ActionResult> ChangeComericalRegisteration()
      {
         await HttpContext.SignOutAsync("Cookies");
         await HttpContext.SignOutAsync("oidc");
         var key = KeyPrefix + _httpcontext.HttpContext.Session.GetString("IdSession");
         _cache.Remove(key);
         var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/Dashboard/Index";
         // var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/account/logout";
         var authURl = _rootConfiguration.AuthorityConfiguration.AuthorityURL;//_configuration.GetSection("Authority:AuthorityURL").Value;
         return Redirect(authURl + "/suppliers?ReturnedUrl=" + baseUrl);
      }

      [AllowAnonymous]
      [HttpGet("UpdateIDMClaims")]
      public async Task<ActionResult> UpdateIDMClaims()
      {
         await HttpContext.SignOutAsync("Cookies");
         await HttpContext.SignOutAsync("oidc");
         var key = KeyPrefix + _httpcontext.HttpContext.Session.GetString("IdSession");
         _cache.Remove(key);
         //var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/Dashboard/Index";
         //// var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/account/logout";
         //var authURl = _configuration.GetSection("Authority:AuthorityURL").Value;
         return Redirect("/Dashboard/Index");
      }

      [HttpGet("SubscriptionUrlAsync")]
      public async Task<ActionResult> SubscriptionUrlAsync()
      {
         await HttpContext.SignOutAsync("Cookies");
         await HttpContext.SignOutAsync("oidc");
         var key = KeyPrefix + _httpcontext.HttpContext.Session.GetString("IdSession");
         _cache.Remove(key);
         return Redirect(User.NotSubscription());
      }

      [HttpGet("changeAgency")]
      public async Task<ActionResult> ChangeAgency()
      {
         await HttpContext.SignOutAsync("Cookies");
         await HttpContext.SignOutAsync("oidc");
         var key = KeyPrefix + _httpcontext.HttpContext.Session.GetString("IdSession");
         _cache.Remove(key);
         // var baseUrl = Request.Host;
         var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/Dashboard/Index";
         var authURl = _rootConfiguration.AuthorityConfiguration.AuthorityURL; //_configuration.GetSection("Authority:AuthorityURL").Value;
         return Redirect(authURl + "/agencyprofile?ReturnedUrl=" + baseUrl);
      }

      [HttpGet("RedirectToEtimad")]
      public RedirectResult RedirectToEtimad()
      {
         var baseUrl = Request.Host;
         var authURl = _rootConfiguration.AuthorityConfiguration.AuthorityURL; //_configuration.GetSection("Authority:AuthorityURL").Value;
         return Redirect(authURl);
      }

      [HttpGet("NotificationSetting")]
      public async Task<IActionResult> NotificationSetting()
      {
         try
         {
            var profile = await _ApiClient.GetListAsync<UserCategoryNotificatioSettingModel>("Account/GetNotificationSetting", null);
            return View(profile);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Logout));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Logout));
         }
      }

      [HttpPost("NotificationSetting")]
      public async Task<IActionResult> NotificationSetting(List<UserNotificatioSettingModel> userNotificatioSettings)
      {
         try
         {
            var profile = await _ApiClient.PostAsync<bool>("Account/SaveNotificationSetting", null, userNotificatioSettings);
            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(NotificationSetting));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Logout));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Logout));
         }
      }

      [HttpGet("MyProfile")]
      public async Task<IActionResult> MyProfile()
      {
         try
         {
            var profile = await _ApiClient.GetAsync<UserProfileModel>("Account/GetMyProfile", null);
            return View(profile);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Logout));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Logout));
         }
      }

      [AllowAnonymous]
      [HttpGet("Conditions")]
      public async Task<IActionResult> Conditions()
      {
         return View();
      }

      [HttpPost("SaveMyProfile")]
      public async Task<IActionResult> SaveMyProfile(UserProfileModel model)
      {
         try
         {
            var profile = await _ApiClient.PostAsync("Account/SaveMyProfile", null, model);
            return Json("done");
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(MyProfile));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(MyProfile));
         }
      }

      [HttpGet("CreateMyProfile")]
      public async Task<IActionResult> CreateMyProfile()
      {
         try
         {
            var profile = await _ApiClient.PostAsync("Account/CreateMyProfile", null, null);
            return RedirectToAction(nameof(MyProfile));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(MyProfile));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(MyProfile));
         }
      }

      [HttpGet("GetNotificationsCount")]
      public async Task<IActionResult> GetNotificationsCount()
      {
         try
         {
            var count = await _ApiClient.GetAsync<int>("Account/GetNotificationPanelCountForSupplier", null);
            return Json(count);
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

      [HttpGet("SetReadStateToNotification")]
      public async Task<IActionResult> SetReadStateToNotification(int notificationId, string link = null)
      {
         try
         {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            var profile = await _ApiClient.PostAsync("Account/SetReadStateToNotification", null, notificationId);
            if (link != null && !isAjax)
               return Redirect(link);
            if (isAjax && link != null)
               return Json(link);
            return Json("done");
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(MyProfile));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(MyProfile));
         }
      }

      [HttpGet("MyNotifications")]
      public async Task<IActionResult> MyNotifications()
      {
         try
         {
            var notifications = await _ApiClient.GetListAsync<NotificationPanelModel>("Account/GetNotificationPanel", null);
            return View(notifications);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(MyProfile));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(MyProfile));
         }
      }

      [HttpGet("GetAllUserRoles")]
      public List<UserRolesModel> GetAllUserRoles()
      {
         //List<UserRolesModel> roles = await _ApiClient.GetListAsync<UserRolesModel>("Account/GetAllUserRoles", null);
         //if (roles.Count() > 0 && User.SelectedCommittee() != 0 && User.UserBranch() != 0 && !User.IsSemiGovAgency() && User.UserCategory() != 8)
         //{
         //   int branchId = await _ApiClient.GetAsync<int>("Account/GetUserDefultBranch", null);
         //   if (branchId != 0)
         //      IdentityConfigs.AddUserBranchId(User, branchId.ToString());
         //   else
         //   {
         //      int committeeId = await _ApiClient.GetAsync<int>("Account/GetUserDefultCommittee", null); 
         //      IdentityConfigs.AddUserCommittee(User, committeeId);
         //   }
         //   AuthenticateResult authenticateInfo = await HttpContext.AuthenticateAsync("Cookies");
         //   await HttpContext.SignInAsync(User, authenticateInfo.Properties);
         //}
         return User.GetUserRolesList().Select(r => new UserRolesModel { RoleValue = r.Split("##")[0], RoleName = r.Split("##")[1] }).ToList();
      }

      [HttpPost("ChangeUserRole")]
      public async Task<ActionResult> ChangeUserRole(string roleValue)
      {
         string isBranch = roleValue.Split(",")[0];
         string userRole = roleValue.Split(",")[2];
         int id = int.Parse(roleValue.Split(",")[1]);
         IdentityConfigs.UpdateUserRoles(User, new List<string> { userRole });
         if (isBranch == "1")
         {
            IdentityConfigs.UpdateUserBranchId(User, id);
         }
         else if (isBranch == "2")
         {
            IdentityConfigs.UpdateUserCommittee(User, id);
         }
         else
         {
            IdentityConfigs.UpdateUserCommittee(User, 0);
         }
         AuthenticateResult authenticateInfo = await HttpContext.AuthenticateAsync("Cookies");
         await HttpContext.SignInAsync(User, authenticateInfo.Properties);
         return RedirectToAction("Index", "Dashboard");
      }

      [HttpPost("SelectDefaultRole")]
      public async Task<ActionResult> SelectDefaultRole(string roleValue)
      {
         string isBranch = roleValue.Split(",")[0];
         string userRole = roleValue.Split(",")[2];
         int id = int.Parse(roleValue.Split(",")[1]);
         IdentityConfigs.UpdateUserRoles(User, new List<string> { userRole });
         if (isBranch == "1")
         {
            IdentityConfigs.UpdateUserBranchId(User, id);
         }
         else if (isBranch == "2")
         {
            IdentityConfigs.UpdateUserCommittee(User, id);
         }
         else
         {
            IdentityConfigs.UpdateUserCommittee(User, 0);
         }
         AuthenticateResult authenticateInfo = await HttpContext.AuthenticateAsync("Cookies");
         await HttpContext.SignInAsync(User, authenticateInfo.Properties);
         await _ApiClient.PostAsync("Account/UpdateDefaultUserRole/" + roleValue, null, null);
         return RedirectToAction("Index", "Dashboard");
      }

      [HttpGet("GetAgenciesLogoes")]
      [AllowAnonymous]
      public async Task<Dictionary<string, string>> GetAgenciesLogoes(List<string> agencycodes)
      {
         return await _ApiClient.GetAsync<Dictionary<string, string>>("Account/GetAgenciesLogoes", new Dictionary<string, object> { { "agencycodes", agencycodes } });
      }

      [HttpGet("NavNotificationAsync")]
      public async Task<List<NotificationPanelModel>> NavNotificationAsync()
      {
         List<NotificationPanelModel> notifications = await _ApiClient.GetListAsync<NotificationPanelModel>("Account/GetNotificationPanel", null);
         return notifications;
      }

      [HttpGet("GetAllNotificationsAsync")]
      public async Task<ActionResult> GetAllNotificationsAsync(string Sort, int PageNumber = 1)
      {
         QueryResult<NotificationPanelModel> result = await _ApiClient.GetQueryResultAsync<NotificationPanelModel>("Account/GetAllNotificationsAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }

      [HttpGet("Home")]
      public async Task<ActionResult> Home()
      {
         return Redirect(_rootConfiguration.AuthorityConfiguration.AuthorityURL /*_configuration.GetSection("Authority:AuthorityURL").Value*/);
      }
      [HttpGet("NotificationOperationCodeCreate/{Id}")]
      public async Task<IActionResult> NotificationOperationCodeCreate(string Id = "")
      {
         var dataModel = new OperationCodeViewModel();
         var roles = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetUserRoles", null);
         var categories = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetNotificationCategories", null);
         try
         {
            int nId = string.IsNullOrEmpty(Id) ? 0 : Util.Decrypt(Id);
            if (nId != 0)
            {
               dataModel = await _ApiClient.GetAsync<OperationCodeViewModel>("Account/GetNotificationOperationCodebyId/" + nId, null);

            }
            dataModel.UserRoles = roles.Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = d.Name, Value = d.Id.ToString() }).ToList();
            dataModel.Categories = categories.Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = d.Name, Value = d.Id.ToString() }).ToList();
            return View(dataModel);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("INDEX", "Dashboard");
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Logout));
         }
      }


      [HttpPost("PostNotificationOperationCodeCreate")]
      public async Task<IActionResult> PostNotificationOperationCodeCreate(OperationCodeViewModel model)
      {
         try
         {
            var profile = await _ApiClient.PostAsync("Account/SaveOperationCode", null, model);
            return RedirectToAction("OperationCodes");
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(OperationCodes));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(OperationCodes));
         }
      }

      [HttpGet("OperationCodes")]
      public async Task<IActionResult> OperationCodes()
      {
         try
         {
            var roles = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetUserRoles", null);
            var modelData = roles.Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = d.Name, Value = d.Id.ToString() }).ToList();
            return View(modelData);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(MyProfile));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(MyProfile));
         }

      }

      [HttpGet("OperationCodesPagingAsync")]
      public async Task<ActionResult> OperationCodesPagingAsync(OperationCodeSearchCriteria operationCodeSearchCriteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<OperationCodeViewModel>("Account/GetNotificationBySearchCriteria", operationCodeSearchCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, operationCodeSearchCriteria.PageSize, result.PageNumber, null));
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

      [HttpGet("ClearCache")]
      public async Task ClearCache()
      {
         _cache.Remove(CacheKeys.BasicStepCache + "_" + User.UserAgency());
         _cache.Remove(CacheKeys.PurchaseCommitteeCache + "_" + User.UserAgency());
      }


   }


}
