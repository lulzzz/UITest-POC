using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using MOF.Etimad.Monafasat.ViewModel.User;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly IMapper _mapper;
        private readonly IIDMAppService _iDMAppService;
        private readonly IBranchAppService _IbranchAppService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMemoryCache _cache;


        public AccountController(INotificationAppService notificationAppService, IMemoryCache cache, IMapper mapper, IIDMAppService iDMAppService, IBranchAppService iBranchAppService, ISubscriptionService subscriptionService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _notificationAppService = notificationAppService;
            _mapper = mapper;
            _iDMAppService = iDMAppService;
            _IbranchAppService = iBranchAppService;
            _subscriptionService = subscriptionService;
            _rootConfiguration = rootConfiguration.Value;
            _cache = cache;
        }

        [HttpGet("GetNotificationSetting")]
        public async Task<List<UserCategoryNotificatioSettingModel>> GetNotificationSetting()
        {
            var userSetting = await _notificationAppService.GetUserNotificationSetting(User.UserId(), User.SupplierNumber(), User.GetUserType());
            return userSetting;
        }


        [HttpPost("SaveNotificationSetting")]
        public async Task<bool> SaveNotificationSetting([FromBody] List<UserNotificatioSettingModel> userNotificatioSettings)
        {
            await _notificationAppService.SaveNotificationSetting(userNotificatioSettings, User.UserId(), User.GetUserType(), User.SupplierNumber());
            return true;
        }
        [HttpPost("CreateMyProfile")]
        public async Task<UserProfileModel> CreateMyProfile()
        {
            var user = await _notificationAppService.GetUser(User.UserId());
            var result = _mapper.Map<UserProfileModel>(user, opt => opt.Items["SelectRoleName"] = User.GetUserType());
            return result;
        }

        [HttpPost("UpdateDefaultUserRole/{userRole}")]
        public async Task UpdateDefaultUserRole(string userRole)
        {
            await _iDMAppService.SetUserDefaultRole(userRole);
        }

        [HttpPost("UpdateNotificationSetting")]
        public IActionResult UpdateNotificationSetting()
        {
            return null;
        }

        [HttpGet("GetMyProfile")]
        public async Task<UserProfileModel> GetMyProfile()
        {
            var user = await _notificationAppService.GetUser(User.UserId());
            var result = _mapper.Map<UserProfileModel>(user, opt => opt.Items["SelectRoleName"] = User.GetUserType());
            return result;
        }



        [HttpGet("GetAllNotificationsAsync")]
        public async Task<QueryResult<NotificationPanelModel>> GetAllNotificationsAsync(SearchCriteria criteria)
        {
            var userId = User.UserId();
            int branchId = User.UserBranch();
            int committeeId = User.SelectedCommittee();
            var cr = User.SupplierNumber();
            QueryResult<NotificationPanelModel> notifications;
            if (!string.IsNullOrEmpty(cr) && User.UserRole() == RoleNames.supplier)
                notifications = await _notificationAppService.GetAllNotificationsForCrAsync(cr, criteria);
            else
                notifications = await _notificationAppService.GetAllNotificationsAsync(User.UserRole(), userId, branchId, committeeId, criteria);

            return notifications;
        }

        [HttpGet("GetNotificationPanel")]
        public async Task<List<NotificationPanelModel>> GetNotificationPanel()
        {
            var userId = User.UserId();
            int branchId = User.UserBranch();
            int committeeId = User.SelectedCommittee();
            var cr = User.SupplierNumber();
            List<NotificationPanelModel> notifications;
            if (!string.IsNullOrEmpty(cr) && User.UserRole() == RoleNames.supplier)
                notifications = await _notificationAppService.GetNotificationPanelForCr(cr);
            else
                notifications = await _notificationAppService.GetNotificationPanel(User.UserRole(), userId, branchId, committeeId);
            return notifications;
        }

        [HttpGet("GetNotificationPanelCount")]
        public async Task<int> GetNotificationPanelCount()
        {
            var userId = User.UserId();
            var branchId = User.UserBranch();
            var notificationscount = await _notificationAppService.GetNotificationPanelCount(userId, branchId);
            return notificationscount;
        }

        [HttpGet("GetNotificationPanelCountForSupplier")]
        public async Task<int> GetNotificationPanelCountForSupplier()
        {
            var cr = User.SupplierNumber();
            var notificationscount = await _notificationAppService.GetNotificationPanelCountForSupplier(cr);
            return notificationscount;
        }


        [HttpPost("SyncUserProfile")]
        public async Task<LoggedUserModel> SyncUserProfile()
        {
            var cr = User.SupplierNumber();
            if (!string.IsNullOrEmpty(cr))
            {
                List<string> CRs = new List<string>();
                CRs.Add(User.SupplierNumber());

                var isSubscription = _rootConfiguration.isSubscriptionConfiguration.isSubscription;
                var CrsList = new List<SubscriptionModel>();
                if (isSubscription)
                    CrsList = await _subscriptionService.GetCRsSubscriptionStatuses(CRs);
                else
                    CrsList.Add(new SubscriptionModel { IsSubscribed = true, SubscriptionURL = "", IsRenewal = true, CR = User.SupplierNumber() });
                if (CrsList != null && CrsList.Count > 0)
                {
                    bool isSubscribed = CrsList.FirstOrDefault().IsSubscribed;
                    string subscirpeUrl = CrsList.FirstOrDefault().SubscriptionURL;
                    var loggedUserModel = await _iDMAppService.SyncUserInfo(User.UserId(), User.UserName(), User.FullName(), User.Mobile(), User.Email(), User.SupplierNumber(), User.SupplierName(), User.SupplierAgency(), User.UserRoles(), User.UserRelatedAgencyCode(), User.VROUserRoles());

                    loggedUserModel.IsSubscripe = isSubscribed;
                    loggedUserModel.SubscirpeUrl = subscirpeUrl;
                    return loggedUserModel;
                }
                else
                {
                    return await _iDMAppService.SyncUserInfo(User.UserId(), User.UserName(), User.FullName(), User.Mobile(), User.Email(), User.SupplierNumber(), User.SupplierName(), User.SupplierAgency(), User.UserRoles(), User.UserRelatedAgencyCode(), User.VROUserRoles());
                }
            }
            else
            {
                return await _iDMAppService.SyncUserInfo(User.UserId(), User.UserName(), User.FullName(), User.Mobile(), User.Email(), User.SupplierNumber(), User.SupplierName(), User.SupplierAgency(), User.UserRoles(), User.UserRelatedAgencyCode(), User.VROUserRoles());
            }
        }

        [HttpGet("GetUserNewRoles/{branchId}/{committeeId}")]
        public async Task<List<string>> GetUserNewRoles(int branchId, int committeeId)
        {
            int userId = User.UserId();
            List<string> userRoles = new List<string>();
            if (branchId != 0)
                userRoles = await _IbranchAppService.GetUserRolesByBranch(userId, branchId);
            else if (committeeId != 0)
                userRoles = await _IbranchAppService.GetUserRoleByCommittee(userId, committeeId);
            return userRoles;
        }

        [HttpGet("SelectUserBranch")]
        public async Task<int> SelectUserBranch(int branchId)
        {
            int userId = User.UserId();
            var AgencyCode = User.UserAgency();
            if (branchId == 0)
                branchId = await _IbranchAppService.GetUserDefultBranch(userId, AgencyCode);
            return branchId;
        }

        [HttpGet("SelectUserCommittee")]
        public async Task<int> SelectUserCommittee(int committeeId)
        {
            int userId = User.UserId();
            string AgencyCode = User.UserAgency();
            if (committeeId == 0)
                committeeId = await _IbranchAppService.GetUserDefultCommittee(userId, AgencyCode);
            return committeeId;
        }

        [HttpGet("GetAllUserRoles")]
        public async Task<List<UserRolesModel>> GetAllUserRoles()
        {
            int userId = User.UserId();
            string AgencyCode = User.SemiGovAndServiceGovUserAgency();
            var roles = await _IbranchAppService.GetAllUserRoles(userId, AgencyCode);
            if (User.UserCategory() == 8 || User.UserIsSemiGovAgency())
            {
                var semiGovUserRoles = User.GetAllSemiGovAgencyUserRoles();
                foreach (var role in semiGovUserRoles)
                {
                    var fieldInfo = typeof(RoleNames).GetFields().FirstOrDefault(x => (x.GetValue(null) as string) == role);
                    var caption = ((PermissionCaptionAttribute)fieldInfo.GetCustomAttributes(typeof(PermissionCaptionAttribute), false).FirstOrDefault()).Caption;
                    roles.Add(new UserRolesModel("0,0," + role, caption));
                }
            }
            if (roles.Count() == 0)
                throw new UnHandledAccessException();
            else
                return roles;
        }

        [HttpGet("GetAllSemiGovrRoles")]
        public async Task<List<UserRolesModel>> GetAllSemiGovrRoles()
        {
            int userId = User.UserId();
            string AgencyCode = User.UserAgency();
            List<UserRolesModel> roles = await _IbranchAppService.GetAllUserRoles(userId, AgencyCode);
            if (roles.Count() == 0)
                throw new UnHandledAccessException();
            return roles;
        }

        [HttpGet("GetUserDefultBranch")]
        public async Task<int> GetUserDefultBranch()
        {
            int userId = User.UserId();
            string AgencyCode = User.UserAgency();
            int branchId = await _IbranchAppService.GetUserDefultBranch(userId, AgencyCode);
            return branchId;
        }

        [HttpGet("GetUserDefultCommittee")]
        public async Task<int> GetUserDefultCommittee()
        {
            int userId = User.UserId();
            string AgencyCode = User.UserAgency();
            int committeeId = await _IbranchAppService.GetUserDefultCommittee(userId, AgencyCode);
            return committeeId;
        }

        [HttpGet("GetAgenciesLogoes")]
        [AllowAnonymous]
        public async Task<Dictionary<string, string>> GetAgenciesLogoes(List<string> agencycodes)
        {
            Dictionary<string, string> result;
            if (string.IsNullOrEmpty(User.UserAgency()))
                result = await _iDMAppService.GetAgencyLogos(agencycodes);
            else
            {
                var baseUrl = _rootConfiguration.APIConfiguration.IDM + "/";

                result = new Dictionary<string, string> { { User.UserAgency(), User.AgencyLogo(baseUrl) } };
            }
            return result;
        }

        [HttpGet("GetOperationCode")]
        [AllowAnonymous]
        public List<OperationCodeModel> GetOperationCode() { return _notificationAppService.GetOperationCode(); }


        [HttpGet("GetNotificationStatus")]
        [AllowAnonymous]
        public List<NotificationStatusModel> GetNotificationStatus() { return _notificationAppService.GetNotificationStatus(); }



        [HttpGet("GetNotificationOperationCodebyId/{Id}")]
        [AllowAnonymous]
        public async Task<OperationCodeViewModel> GetNotificationOperationCodebyId(int Id)
        {

            var notificationscount = await _notificationAppService.GetOperationCodeDetails(Id);
            return notificationscount;
        }
        [HttpPost("SetReadStateToNotification")]
        public async Task SetReadStateToNotification([FromBody] int notificationId)
        {
            await _notificationAppService.SetReadStateToNotification(notificationId);
        }
        [HttpPost("SaveOperationCode")]
        public async Task SaveOperationCode([FromBody] OperationCodeViewModel operationCodeViewModel)
        {
            await _notificationAppService.SaveOperationCode(operationCodeViewModel);
        }

        [HttpGet("GetNotificationBySearchCriteria")]
        public async Task<QueryResult<OperationCodeViewModel>> GetNotificationBySearchCriteria(OperationCodeSearchCriteria tenderSearchCriteria)
        {

            var tenderList = await _notificationAppService.FindNotificationCodesBySearchCriteriaForPage(tenderSearchCriteria);
            return tenderList;
        }

        [HttpGet("ClearCache")]
        public async Task ClearCache()
        {
            _cache.Remove(CacheKeys.BasicStepCache + "_" + User.UserAgency());
            _cache.Remove(CacheKeys.PurchaseCommitteeCache + "_" + User.UserAgency());
        }
    }
}
