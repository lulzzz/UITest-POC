using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BranchController : BaseController
    {
        private readonly IBranchAppService _branchService;
        private readonly IIDMAppService _iDMAppService;

        public BranchController(IBranchAppService branchService, IIDMAppService iDMAppService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _iDMAppService = iDMAppService;
            _branchService = branchService;
        }

        [HttpGet]
        [Route("Find")]
        public async Task<QueryResult<BranchModel>> Find(BranchSearchCriteriaModel branchSearchCriteriaModel)
        {
            branchSearchCriteriaModel.AgencyCode = User.UserAgency();
            var branchList = await _branchService.Find(branchSearchCriteriaModel);
            return branchList;
        }

        [HttpPost("AddBranch")]
        public async Task<BranchModel> AddBranch([FromBody] BranchModel branchModel)
        {
            branchModel.AgencyCode = User.UserAgency();
            branchModel.RelatedAgencyCode = User.UserRelatedAgencyCode();
            var result = await _branchService.AddBranch(branchModel);
            return result;
        }

        [HttpGet("GetById/{id}")]
        public async Task<BranchModel> GetById(int id)
        {
            var branchObject = await _branchService.FindById(id);
            return branchObject;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<BranchModel> Update([FromBody] BranchModel branchModel)
        {
             await _branchService.UpdateAsync(branchModel);
            return branchModel;
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _branchService.DeActiveAsync(id);
        }

        [HttpGet("GetIDMUsers/")]
        public async Task<List<UserLookupModel>> GetIDMUsers()
        {
            BranchUserSearchCriteriaModel criteria = new BranchUserSearchCriteriaModel();
            criteria = GetUserAgencyTypeAndIdWithFlags(criteria);
            return await _branchService.GetIDMUsers(criteria);
        }

        [HttpPost]
        [Route("GetIDMUsersSearch")]
        public async Task<List<UserLookupModel>> GetIDMUsersSearch([FromBody] BranchUserSearchCriteriaModel criteria)
        {
            criteria = GetUserAgencyTypeAndIdWithFlags(criteria);
            return await _branchService.GetIDMUsers(criteria);
        }

        private BranchUserSearchCriteriaModel GetUserAgencyTypeAndIdWithFlags(BranchUserSearchCriteriaModel userSearchCriteriaModel)
        {
            if (User.IsInRole(RoleNames.MonafasatAdmin))
            {
                userSearchCriteriaModel.isGovVendor = User.UserIsGovVendor();
                userSearchCriteriaModel.isSemiGovAgency = User.UserIsSemiGovAgency();
                if (User.UserIsVRO())
                {
                    userSearchCriteriaModel.AgencyType = Enums.AgencyType.VRO;
                }
                else
                {
                    userSearchCriteriaModel.AgencyType = userSearchCriteriaModel.isSemiGovAgency ? AgencyType.SemiGovAgency : (userSearchCriteriaModel.isGovVendor ? AgencyType.GovVendor : AgencyType.Agency);
                }
                userSearchCriteriaModel.AgencyId = User.UserAgency();
            }
            else
            {
                userSearchCriteriaModel.AgencyType = AgencyType.None;
            }
            return userSearchCriteriaModel;
        }

        [HttpPost]
        [Route("GetBranchUserModel")]
        public async Task<BranchUserModel> GetBranchUserModel([FromBody] BranchUserSearchCriteriaModel criteria)
        {
            BranchUserModel branchUserModel = new BranchUserModel();
            BranchModel branchObject = await _branchService.FindById(criteria.BranchId);
            branchUserModel.BranchId = branchObject.BranchId;
            branchUserModel.BranchName = branchObject.BranchName;
            branchUserModel.Users = await GetIDMUsersSearch(criteria);
            return branchUserModel;
        }

        [HttpGet]
        [Route("GetUserRolesById/{userName}")]
        public async Task<List<RoleModel>> GetUserRolesById(string userName)
        {
            UsersSearchCriteriaModel userSearchCriteriaModel = new UsersSearchCriteriaModel();
            userSearchCriteriaModel = GetUserAgencyTypeAndIdWithFlags(userSearchCriteriaModel);
            return await _branchService.GetUserRolesById(userName, userSearchCriteriaModel.AgencyId, 0, userSearchCriteriaModel);
        }

        private UsersSearchCriteriaModel GetUserAgencyTypeAndIdWithFlags(UsersSearchCriteriaModel userSearchCriteriaModel)
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
            {
                userSearchCriteriaModel.AgencyType = (int)AgencyType.None;
            }
            return userSearchCriteriaModel;
        }

        [HttpPost("AssignUsersBranch")]
        public async Task AssignUsersBranch([FromBody] BranchUserModel branchUserModel)
        {
            branchUserModel.RelatedAgencyCode = User.UserRelatedAgencyCode();
            await _branchService.AddUserAsyn(branchUserModel, User.UserAgency());
        }

        [HttpGet]
        [Route("GetBranchUsers")]
        public async Task<QueryResult<BranchUserModel>> GetBranchUsers(BranchUserSearchCriteriaModel criteria)
        {
            var users = await _branchService.GetBranchUsers(criteria);
            return users;
        }

        [HttpPost]
        [Route("RemoveAssignedUser")]
        public async Task RemoveAssignedUser(int userId, int branchId, int roleName)
        {
            await _branchService.RemoveAssignedUser(userId, branchId, roleName);
        }

        [HttpGet("GetCommitteesNotAssignedToBranch/{branchId}")]
        public async Task<List<CommitteeModel>> GetCommitteesNotAssignedToBranch(int branchId)
        {
            string agencyCode = User.UserAgency();
            var tenderStatusList = await _iDMAppService.FindCommitteesNotAssignedToBranch(agencyCode, branchId);
            return tenderStatusList;
        }

        [HttpPost("AssignCommitteesBranch")]
        public async Task AssignCommitteesBranch([FromBody] BranchCommitteeModel branchCommitteeModel)
        {
            await _branchService.AddCommittee(branchCommitteeModel);
        }

        [HttpGet]
        [Route("GetAssignedCommitteeByIdAndBranch")]
        public async Task<BranchCommittee> GetAssignedCommitteeByIdAndBranch(int committeeId, int branchId)
        {
            return await _branchService.GetAssignedCommitteeByIdAndBranch(committeeId, branchId);
        }

        [HttpGet("GetBranchCommittees")]
        public async Task<QueryResult<LookupModel>> GetBranchCommittees(BranchCommitteeSearchCriteriaModel criteria)
        {
            var users = await _branchService.GetBranchCommittees(criteria);
            return users;
        }

        [HttpPost]
        [Route("RemoveAssignedCommittee")]
        public async Task RemoveAssignedCommittee(int committeeId, int branchId)
        {
            await _branchService.RemoveAssignedCommittee(committeeId, branchId);
        }

        [HttpGet]
        [Route("GetIDMRoleNameById/{roleId}")]
        private async Task<IDMRolesModel> GetIDMRoleNameById(int roleId)
        {
            List<IDMRolesModel> roles = _iDMAppService.GetIDMRoles();
            return roles.Where(r => r.Id == roleId).FirstOrDefault();
        }

        [HttpGet]
        [Route("GetBranchByAgency/{AgencyCode}")]
        public async Task<List<BranchModel>> GetBranchByAgency(string AgencyCode)
        {
            if (AgencyCode == "" || AgencyCode == "0")
                AgencyCode = User.UserAgency();
            return await _branchService.GetBranchByAgency(AgencyCode);
        }
    }
}