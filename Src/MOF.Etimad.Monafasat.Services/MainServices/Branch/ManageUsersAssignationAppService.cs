using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class ManageUsersAssignationAppService : IManageUsersAssignationAppService
    {
        private readonly IIDMQueries _iDMQueries;
        private readonly INotificationAppService _notificationAppService;
        private readonly IBranchServiceDomain _branchServiceDomain;
        private readonly ICommitteeDomainService _committeeDomainService;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly IIDMAppService _iDMAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ManageUsersAssignationAppService(IBranchServiceDomain branchServiceDomain, ICommitteeDomainService committeeDomainService, IGenericCommandRepository genericCommandRepository, IIDMAppService iDMAppService, IIDMQueries iDMQueries, INotificationAppService notificationAppService, IHttpContextAccessor httpContextAccessor)
        {

            _branchServiceDomain = branchServiceDomain;
            _genericCommandRepository = genericCommandRepository;
            _iDMAppService = iDMAppService;
            _iDMQueries = iDMQueries;
            _committeeDomainService = committeeDomainService;
            _notificationAppService = notificationAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddUserToBranchAsyn(BranchUserAssignModel branchUserModel, string agencyCode)
        {

            Check.ArgumentNotNull(nameof(branchUserModel), branchUserModel);
            var user = await _iDMAppService.FindUserProfileByUserNameAsync(branchUserModel.NationalIdString);
            Enums.UserRole userRole = ((Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), branchUserModel.RoleName));
            if (user == null)
            {
                ManageUsersAssignationModel emp = await _iDMAppService.GetMonafastatEmployeeDetailById(agencyCode, branchUserModel.NationalIdString, "");
                UserProfile userProfile = new UserProfile(emp.userId, emp.nationalId, emp.fullName, emp.mobileNumber, emp.email, await _notificationAppService.GetDefaultSettingByUserType(userRole));
                Check.ArgumentNotNull(nameof(userProfile), userProfile);
                await _genericCommandRepository.CreateAsync(userProfile);
                branchUserModel.UserId = userProfile.Id;
            }
            else
            {

                if (!user.NotificationSetting.Any(x => x.UserRoleId == (int)userRole))
                {
                    await _branchServiceDomain.CheckUserExist(user.Id, branchUserModel.BranchId, branchUserModel.RoleName);
                    user.AddNotificationSettings(await _notificationAppService.GetDefaultSettingByUserType(userRole));
                    _genericCommandRepository.Update(user);
                }
                branchUserModel.UserId = user.Id;
            }
            var branchUser = new BranchUser(branchUserModel.BranchId, branchUserModel.UserId.Value, (int)userRole, branchUserModel.RelatedAgencyCode, branchUserModel.EstimatedValueFrom, branchUserModel.EstimatedValueTo);
            await _genericCommandRepository.CreateAsync(branchUser);
            await _genericCommandRepository.SaveAsync();

            if (user == null)
            {
                if (!string.IsNullOrEmpty(branchUser.UserProfile.Email) || !string.IsNullOrEmpty(branchUser.UserProfile.Mobile))
                {
                    await _notificationAppService.SendNotificationByEmailAndSmsForRolesChanged(branchUser.UserProfile.Id, branchUser.UserProfile.Email, branchUser.UserProfile.Mobile);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(user.Email) || !string.IsNullOrEmpty(user.Mobile))
                {
                    await _notificationAppService.SendNotificationByEmailAndSmsForRolesChanged(user.Id, user.Email, user.Mobile);
                }
            }
        }

        public async Task AddUserToCommitteeAsyn(CommitteeUserAssignModel committeeUserModel, string agencyCode)
        {
            Check.ArgumentNotNull(nameof(committeeUserModel), committeeUserModel);
            var user = await _iDMAppService.FindUserProfileByUserNameAsync(committeeUserModel.NationalIdString);
            List<IDMRolesModel> roles = _iDMAppService.GetIDMRoles();
            IDMRolesModel iDMRolesModel = roles.FirstOrDefault(r => r.Name == committeeUserModel.RoleName);
            committeeUserModel.RoleName = iDMRolesModel.Name;
            committeeUserModel.RoleNameAr = iDMRolesModel.NormalizedName;
            Enums.UserRole userType = (Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), committeeUserModel.RoleName, true);

            if (user == null)
            {
                UserProfile userProfile = await _iDMAppService.GetUserProfileByEmployeeId(committeeUserModel.NationalIdString, agencyCode, userType);
                await _committeeDomainService.CheckUserExist(userProfile.Id, committeeUserModel.CommitteeId.Value);
                Check.ArgumentNotNull(nameof(userProfile), userProfile);
                await _genericCommandRepository.CreateAsync(userProfile);
                committeeUserModel.UserId = userProfile.Id;
            }
            else
            {
                if (!user.NotificationSetting.Any(x => x.UserRoleId == (int)userType))
                {
                    await _committeeDomainService.CheckUserExist(user.Id, committeeUserModel.CommitteeId.Value);
                    user.AddNotificationSettings(await _notificationAppService.GetDefaultSettingByUserType(userType));
                    _genericCommandRepository.Update(user);
                }
                committeeUserModel.UserId = user.Id;
            }
            CommitteeUser committeeUser = new CommitteeUser(committeeUserModel.CommitteeId.Value, committeeUserModel.UserId.Value, (int)((Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), committeeUserModel.RoleName)), committeeUserModel.RelatedAgencyCode);
            await _genericCommandRepository.CreateAsync(committeeUser);
            await _genericCommandRepository.SaveAsync();

            if (user == null)
            {
                if (!string.IsNullOrEmpty(committeeUser.UserProfile.Email) || !string.IsNullOrEmpty(committeeUser.UserProfile.Mobile))
                {
                    await _notificationAppService.SendNotificationByEmailAndSmsForRolesChanged(committeeUser.UserProfile.Id, committeeUser.UserProfile.Email, committeeUser.UserProfile.Mobile);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(user.Email) || !string.IsNullOrEmpty(user.Mobile))
                {
                    await _notificationAppService.SendNotificationByEmailAndSmsForRolesChanged(user.Id, user.Email, user.Mobile);
                }
            }
        }

        public List<RoleModel> GetIDMRoles()
        {
            string[] allowedRoles;
            System.Security.Claims.ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            if (user.UserIsVRO())
            {
                allowedRoles = RoleNames.GetVROMonafasatRoles();
            }
            else
            {
                allowedRoles = RoleNames.GetMonafasatRoles();
            }

            var result = _iDMAppService.GetIDMRoles();
            return result.Where(alw => allowedRoles.Contains(alw.Name)).Select(r => new RoleModel() { RoleId = r.Id, RoleName = r.Name, RoleNameAr = r.NormalizedName }).ToList();
        }

        public async Task UserPermissionRest(int UserId)
        {
            UserProfile userProfile = await _iDMQueries.FindUserProfileByIdAsync(UserId);
            if (userProfile != null)
            {
                userProfile.RestPermission();
                _genericCommandRepository.Update(userProfile);
                await _genericCommandRepository.SaveAsync();
            }
        }
    }
}
