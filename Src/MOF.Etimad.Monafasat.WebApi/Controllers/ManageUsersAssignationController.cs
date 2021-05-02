
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ManageUsersAssignationController : BaseController
    {
        private readonly IManageUsersAssignationAppService _usersService;
        private readonly IIDMAppService _iDMAppService;
        private readonly IBranchAppService _branchService;
        private readonly ICommitteeAppService _committeeApplication;


        public ManageUsersAssignationController(IManageUsersAssignationAppService usersService, IIDMAppService iDMAppService, ICommitteeAppService committeeApplication, IBranchAppService branchService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _iDMAppService = iDMAppService;
            _usersService = usersService;
            _branchService = branchService;
            _committeeApplication = committeeApplication;
        }

        [HttpGet]
        [Route("Find")]
        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
        public async Task<QueryResult<ManageUsersAssignationModel>> Find(UsersSearchCriteriaModel userSearchCriteriaModel)
        {
            if (User.IsInRole(RoleNames.UnitSpecialistLevel1) || User.IsInRole(RoleNames.UnitSpecialistLevel2) || User.IsInRole(RoleNames.UnitManagerUser) || User.IsInRole(RoleNames.UnitBusinessManagement))
                userSearchCriteriaModel.RoleName = RoleNames.OffersCheckSecretary;

            userSearchCriteriaModel = await GetUserAgencyTypeAndIdWithFlags(userSearchCriteriaModel);
            var employeeList = await _iDMAppService.GetMonafasatUsers(userSearchCriteriaModel);
            return employeeList;
        }

        private async Task<UsersSearchCriteriaModel> GetUserAgencyTypeAndIdWithFlags(UsersSearchCriteriaModel userSearchCriteriaModel)
        {
            if (User.IsInRole(RoleNames.MonafasatAdmin))
            {
                userSearchCriteriaModel.isGovVendor = User.UserIsGovVendor();
                userSearchCriteriaModel.isSemiGovAgency = User.UserIsSemiGovAgency();
                if (User.UserIsVRO())
                {
                    userSearchCriteriaModel.AgencyType = (int)Enums.AgencyType.VRO;
                }
                else
                {
                    userSearchCriteriaModel.AgencyType = userSearchCriteriaModel.isSemiGovAgency ? (int)AgencyType.SemiGovAgency : (userSearchCriteriaModel.isGovVendor ? (int)AgencyType.GovVendor : (int)AgencyType.Agency);
                }
                userSearchCriteriaModel.AgencyId = User.UserAgency();
            }
            else
            {//Account manager + Customer service
                if (!string.IsNullOrEmpty(userSearchCriteriaModel.AgencyId))
                {
                    var agencyCategory = (await _iDMAppService.FindGovAgencyByIdAsync(userSearchCriteriaModel.AgencyId)).CategoryId;
                    userSearchCriteriaModel.AgencyType = agencyCategory.Value;// agencyCategory == (int)IDMUserCategory.GovVendor ? (int)AgencyType.GovVendor : (agencyCategory == (int)IDMUserCategory.PrivateSector ? (int)AgencyType.SemiGovAgency : (int)AgencyType.Agency);
                }
                else
                    userSearchCriteriaModel.AgencyType = (int)AgencyType.None;
            }
            return userSearchCriteriaModel;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2 + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement)]
        [HttpGet("GetById/{nationalId}")]
        public async Task<ManageUsersAssignationModel> GetById(string nationalId)
        {
            UsersSearchCriteriaModel userSearchCriteriaModel = new UsersSearchCriteriaModel();
            userSearchCriteriaModel = await GetUserAgencyTypeAndIdWithFlags(userSearchCriteriaModel);
            var employee = await _iDMAppService.GetMonafastatEmployeeDetailById(userSearchCriteriaModel.AgencyId, nationalId, string.Empty, userSearchCriteriaModel.AgencyType);
            return employee;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService)]
        [HttpGet("GetCommitteeNotAssignedToUser/{userId}")]
        public async Task<List<CommitteeModel>> GetCommitteeNotAssignedToUser(int userId)
        {
            string agencyCode = User.UserAgency();
            var tenderStatusList = await _iDMAppService.FindCommitteeNotAssignedToUser(agencyCode, userId);
            return tenderStatusList;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService)]
        [HttpGet("GetCommitteeNotAssignedByTypeAsync/{userId}/{committeeTypeId}")]
        public async Task<List<CommitteeModel>> GetCommitteeNotAssignedByTypeAsync(int userId, int committeeTypeId)
        {
            string agencyId = User.UserAgency();
            var CommitteeList = await _iDMAppService.FindCommitteeNotAssignedToUser(agencyId, userId);
            List<CommitteeModel> filteredList = new List<CommitteeModel>();
            foreach (var item in CommitteeList)
            {
                if (item.CommitteeTypeId == committeeTypeId)
                    filteredList.Add(item);
            }
            return filteredList;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService)]
        [HttpGet("GetBranchesNotAssignedToUser/{userId}")]
        public async Task<List<BranchModel>> GetBranchesNotAssignedToUser(int userId)
        {
            string agencyCode = User.UserAgency();
            var tenderStatusList = await _iDMAppService.FindBranchesNotAssignedToUser(agencyCode, userId);
            return tenderStatusList;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService)]
        [HttpGet("GetBranchesNotAssignedByUserAndRole/{userId}/{role}")]
        public async Task<List<BranchModel>> GetBranchesNotAssignedByUserAndRole(int userId, string role)
        {
            string agencyCode = User.UserAgency();
            var tenderStatusList = await _iDMAppService.FindBranchesNotAssignedByUserAndRole(agencyCode, userId, role);
            return tenderStatusList;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
        [HttpGet("GetIDMRolesAsync")]
        public async Task<List<RoleModel>> GetIDMRolesAsync()
        {
            var result = _usersService.GetIDMRoles();
            return result;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.UnitManagerUser + "," + RoleNames.UnitBusinessManagement + "," + RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
        [HttpGet("GetUserCommitteeBranchesModel")]
        public async Task<QueryResult<UserCommitteeBranchesModel>> GetUserCommitteeBranchesModel(BranchCommitteeUserSearchCriteriaModel userSearchCriteriaModel)
        {
            if (userSearchCriteriaModel.PageNumber <= 0 && userSearchCriteriaModel.PageSize <= 0) { userSearchCriteriaModel.PageNumber = 1; userSearchCriteriaModel.PageSize = 10; }
            string agencyCode = User.UserAgency();
            if (User.UserRole() == RoleNames.CustomerService || User.UserRole() == RoleNames.MonafasatAccountManager)
                agencyCode = null;
            var userCommitteeBranchesList = await _iDMAppService.GetUserCommitteeBranchesModel(agencyCode, userSearchCriteriaModel);
            return userCommitteeBranchesList;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin)]
        [HttpPost("AssignUsersBranch")]
        public async Task AssignUsersBranch([FromBody] BranchUserAssignModel branchUserModel)
        {
            branchUserModel.RelatedAgencyCode = User.UserRelatedAgencyCode();
            await _usersService.AddUserToBranchAsyn(branchUserModel, User.UserAgency());
        }
        [Authorize(Roles = RoleNames.MonafasatAdmin)]
        [HttpPost("AssignUsersCommittee")]
        public async Task AssignUsersCommittee([FromBody] CommitteeUserAssignModel committeeUserModel)
        {
            committeeUserModel.RelatedAgencyCode = User.UserRelatedAgencyCode();
            await _usersService.AddUserToCommitteeAsyn(committeeUserModel, User.UserAgency());
        }

        [HttpPost]
        [Route("RemoveBranchAssignedUser")]
        [Authorize(Roles = RoleNames.MonafasatAdmin)]
        public async Task RemoveBranchAssignedUser(int userId, int branchId, int roleId)
        {
            await _branchService.RemoveAssignedUserForMang(userId, branchId, roleId);
        }

        [HttpPost]
        [Route("RemoveCommitteeAssignedUser")]
        [Authorize(Roles = RoleNames.MonafasatAdmin)]
        public async Task RemoveCommitteeAssignedUser(int userId, int committeeId)
        {
            await _committeeApplication.RemoveAssignedUser(userId, committeeId);

        }

        [HttpGet("GetALlAgencies")]
        public async Task<List<GovAgencyModel>> GetAllAgenceies()
        {
            return await _iDMAppService.GetALlAgencies();
        }

        [HttpPost]
        [Route("UserPermissionRest")]
        public async Task UserPermissionRest(int uId)
        {
            await _usersService.UserPermissionRest(uId);
        }
    }
}
