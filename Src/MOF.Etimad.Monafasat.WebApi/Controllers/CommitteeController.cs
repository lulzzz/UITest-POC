using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Api.Controllers
{
    [Route("api/[controller]")]
    public class CommitteeController : BaseController
    {
        private readonly ICommitteeAppService _committeeApplication;
        private readonly IBranchAppService _branchService;
        private readonly IIDMAppService _iDMAppService;
        private readonly IMapper _mapper;
        public CommitteeController(IBranchAppService branchService, IIDMAppService iDMAppService, ICommitteeAppService committeeApplication, IMapper mapper, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _committeeApplication = committeeApplication;
            _branchService = branchService;
            _iDMAppService = iDMAppService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get")]
        [Authorize(RoleNames.MonafasatAdminPolicy)]
        public async Task<QueryResult<CommitteeModel>> Get(CommitteeSearchCriteriaModel committeeSearchCriteriaModel)
        {
            var committeeSearchCriteria = _mapper.Map<CommitteeSearchCriteria>(committeeSearchCriteriaModel);
            var committeeList = await _committeeApplication.Find(committeeSearchCriteria);
            var committeeModel = _mapper.Map<QueryResult<CommitteeModel>>(committeeList);
            return committeeModel;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(RoleNames.MonafasatAdminPolicy)]
        public async Task<List<CommitteeModel>> GetAll(string agencyCode)
        {
            var committeeList = await _committeeApplication.FindAll(agencyCode);
            var committeeModel = _mapper.Map<List<CommitteeModel>>(committeeList);
            return committeeModel;
        }

        [HttpGet]
        [Route("GetByCommitteeType")]
        //[Authorize(RoleNames.AdminAndDataEntryPolicy)]
        public async Task<List<CommitteeModel>> GetByCommitteeType(int committeeTypeId, string agencyCode)
        {
            var committeeList = await _committeeApplication.FindByCommitteeTypeId(committeeTypeId, agencyCode);
            var committeeModel = _mapper.Map<List<CommitteeModel>>(committeeList);
            return committeeModel;
        }

        [HttpGet]
        [Route("GetById")]
        [Authorize(RoleNames.AdminAndDataEntryPolicy)]
        public async Task<CommitteeModel> GetById(int id)
        {
            var committeeObject = await _committeeApplication.GetById(id);
            var committeeModel = _mapper.Map<CommitteeModel>(committeeObject);
            return committeeModel;
        }

        [HttpGet]
        [Route("GetCommitteeTendersAsync/{committeeId}")]
        [Authorize(RoleNames.MonafasatAdminPolicy)]
        public async Task<CommitteeTenderModel> GetCommitteeTendersAsync(int committeeId)
        {
            var committeeObject = await _committeeApplication.GetById(committeeId);
            var committeeModel = _mapper.Map<CommitteeTenderModel>(committeeObject);
            return committeeModel;
        }

        [HttpGet]
        [Route("GetCommitteeUsers")]
        [Authorize(RoleNames.MonafasatAdminPolicy)]
        public async Task<QueryResult<CommitteeUserViewModel>> GetCommitteeUsers(CommitteeUserSearchCriteriaModel criteria)
        {
            var users = await _committeeApplication.GetCommitteeUsers(criteria);
            return users;
        }

        [Authorize(RoleNames.MonafasatAdminPolicy)]
        [HttpPost("RemoveAssignedUser")]
        public async Task RemoveAssignedUser(int userId, int committeeId)
        {
            await _committeeApplication.RemoveAssignedUser(userId, committeeId);
        }

        [HttpPost]
        [Authorize(RoleNames.MonafasatAdminPolicy)]
        public async Task<CommitteeModel> Post([FromBody] CommitteeModel committeeModel)
        {
            var committeeObject = _mapper.Map<Committee>(committeeModel);
            var committeeResult = await _committeeApplication.CreateAsync(committeeObject);
            committeeModel = _mapper.Map<CommitteeModel>(committeeResult);
            return committeeModel;
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(RoleNames.MonafasatAdminPolicy)]
        public async Task<CommitteeModel> Update([FromBody] CommitteeModel committeeModel)
        {
            committeeModel.AgencyCode = User.UserAgency();
            var committeeResult = await _committeeApplication.UpdateAsync(committeeModel);
            committeeModel = _mapper.Map<CommitteeModel>(committeeResult);
            return committeeModel;
        }

        [HttpGet]
        [Route("Delete/{id}")]
        [Authorize(RoleNames.MonafasatAdminPolicy)]
        public async Task Delete(int id)
        {
            await _committeeApplication.DeActiveAsync(id);
        }

        [Authorize(RoleNames.MonafasatAdminPolicy)]
        [HttpPost("AssignUsersCommittee")]
        public async Task AssignUsersCommittee([FromBody] CommitteeUserModel committeeUserModel)
        {
            committeeUserModel.RelatedAgencyCode = User.UserRelatedAgencyCode();
            await _committeeApplication.AddUserAsyn(committeeUserModel, User.UserAgency());
        }
        [HttpGet]
        [Route("GetUserRolesByIdAndCommitteeType/{userName}/{CommitteeTypeId}")]
        public async Task<List<RoleModel>> GetUserRolesByIdAndCommitteeType(string userName, int CommitteeTypeId)
        {
            UsersSearchCriteriaModel userSearchCriteriaModel = new UsersSearchCriteriaModel();
            userSearchCriteriaModel = GetUserAgencyTypeAndIdWithFlags(userSearchCriteriaModel);
            return await _branchService.GetUserRolesById(userName, userSearchCriteriaModel.AgencyId, CommitteeTypeId, userSearchCriteriaModel);
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

        [HttpPost]
        [Route("GetUsersFilteredbyCommitteeTypeId")]
        public async Task<List<UserLookupModel>> GetUsersFilteredbyCommitteeTypeId([FromBody] CommitteeUserSearchCriteriaModel userSearchCriteriaModel)
        {
            userSearchCriteriaModel = GetCommitteeUserAgencyTypeAndIdWithFlags(userSearchCriteriaModel);
            if (userSearchCriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee)
            {
                userSearchCriteriaModel.RoleNames.Add(RoleNames.OffersOppeningSecretary);
                userSearchCriteriaModel.RoleNames.Add(RoleNames.OffersOppeningManager);
            }
            else if (userSearchCriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee)
            {
                userSearchCriteriaModel.RoleNames.Add(RoleNames.OffersCheckSecretary);
                userSearchCriteriaModel.RoleNames.Add(RoleNames.OffersCheckManager);
            }
            else if (userSearchCriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee)
            {
                userSearchCriteriaModel.RoleNames.Add(RoleNames.PreQualificationCommitteeSecretary);
                userSearchCriteriaModel.RoleNames.Add(RoleNames.PreQualificationCommitteeManager);
            }
            else if (userSearchCriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee)
            {
                userSearchCriteriaModel.RoleNames.Add(RoleNames.OffersPurchaseSecretary);
                userSearchCriteriaModel.RoleNames.Add(RoleNames.OffersPurchaseManager);
            }
            else if (userSearchCriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee)
            {
                userSearchCriteriaModel.RoleNames.Add(RoleNames.TechnicalCommitteeUser);
            }
            var usersForCommitteeTypeId = await _iDMAppService.GetUsersbyCommitteeTypeId(userSearchCriteriaModel);
            return usersForCommitteeTypeId;
        }

        private CommitteeUserSearchCriteriaModel GetCommitteeUserAgencyTypeAndIdWithFlags(CommitteeUserSearchCriteriaModel userSearchCriteriaModel)
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
                    userSearchCriteriaModel.AgencyType = (AgencyType)(userSearchCriteriaModel.isSemiGovAgency ? (int)AgencyType.SemiGovAgency : (userSearchCriteriaModel.isGovVendor ? (int)AgencyType.GovVendor : (int)AgencyType.Agency));
                }
                userSearchCriteriaModel.AgencyId = User.UserAgency();
            }
            else
            {
                userSearchCriteriaModel.AgencyType = (int)AgencyType.None;
            }
            return userSearchCriteriaModel;
        }
    }
}
