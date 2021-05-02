using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommitteeAppService : ICommitteeAppService
    {
        private readonly ICommitteeDomainService _committeeDomainService;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly ICommitteeQueries _committeeQueries;
        private readonly IIDMAppService _iDMAppService;
        private readonly INotificationAppService _notificationAppService;

        public CommitteeAppService(ICommitteeDomainService committeeDomainService, IGenericCommandRepository genericCommandRepository, ICommitteeQueries committeeQueries, IIDMAppService iDMAppService, INotificationAppService notificationAppService)
        {
            _committeeDomainService = committeeDomainService;
            _committeeQueries = committeeQueries;
            _genericCommandRepository = genericCommandRepository;
            _iDMAppService = iDMAppService;
            _notificationAppService = notificationAppService;
        }

        #region Service Queries ======================================================
        public async Task<QueryResult<Committee>> Find(CommitteeSearchCriteria criteria)
        {
            return await _committeeQueries.Find(criteria, x => x.CommitteeType, d => d.CommitteeType);
        }
        public async Task<List<Committee>> FindAll(string agencyCode)
        {
            return await _committeeQueries.FindAll(agencyCode);
        }
        public async Task<List<Committee>> FindByCommitteeTypeId(int committeeTypeId, string agencyCode)
        {
            return await _committeeQueries.FindByCommitteeTypeId(committeeTypeId, agencyCode);
        }

        public async Task<QueryResult<CommitteeUserViewModel>> GetCommitteeUsers(CommitteeUserSearchCriteriaModel criteria)
        {
            var CommitteeTypeId = (await GetById(criteria.CommitteeId)).CommitteeTypeId; criteria.RoleNames = CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee ? new List<string> { RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOpeningAndCheckManager } : CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee ? new List<string> { RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary } : CommitteeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee ? new List<string> { RoleNames.PreQualificationCommitteeManager, RoleNames.PreQualificationCommitteeSecretary } : CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee ? new List<string> { RoleNames.OffersCheckManager, RoleNames.OffersCheckSecretary } :
    (CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee ? new List<string> { RoleNames.OffersOppeningManager, RoleNames.OffersOppeningSecretary } : new List<string> { RoleNames.TechnicalCommitteeUser });
            return await _committeeQueries.GetCommitteeUsers(criteria);
        }

        public async Task<Committee> GetById(int id)
        {
            return await _committeeQueries.GetById(id);
        }
        #endregion

        #region  Service Commands =====================================================
        public async Task<Committee> CreateAsync(Committee committee)
        {
            Check.ArgumentNotNull(nameof(committee), committee);
            await _committeeDomainService.CheckNameExistbyType(committee.CommitteeName, committee.AgencyCode, committee.CommitteeTypeId);
            var res = await _genericCommandRepository.CreateAsync<Committee>(committee);
            await _genericCommandRepository.SaveAsync();
            return res;
        }

        public async Task<Committee> UpdateAsync(CommitteeModel committeeModel)
        {
            Check.ArgumentNotNull(nameof(committeeModel), committeeModel);
            await _committeeDomainService.CheckNameExist(committeeModel.CommitteeName, committeeModel.AgencyCode, committeeModel.CommitteeId);
            var committee = await _committeeQueries.GetById(committeeModel.CommitteeId);
            committee.Update(committeeModel.CommitteeName, committeeModel.Address, committeeModel.Phone, committeeModel.Fax, committeeModel.Email, committeeModel.PostalCode, committeeModel.ZipCode);
            var result = _genericCommandRepository.Update(committee);
            await _genericCommandRepository.SaveAsync();
            return result;
        }

        public async Task DeActiveAsync(int committeeId)
        {
            var committee = await _committeeQueries.GetById(committeeId);
            Check.EntityNotNull(nameof(committee), committee, Resources.CommitteeResources.ErrorMessages.CommitteeNotFound);
            await _committeeDomainService.CheckIfHasUsers(committeeId);
            await _committeeDomainService.CheckIfHasTenders(committeeId);
            await _committeeDomainService.CheckIfHasEnqiryReplies(committeeId);
            committee.DeActive();
            committee.CommitteeUsers.ForEach(x => x.DeActive());
            _genericCommandRepository.Update(committee);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task AddUserAsyn(CommitteeUserModel committeeUserModel, string agencyCode)
        {
            Check.ArgumentNotNull(nameof(committeeUserModel), committeeUserModel);
            var user = await _iDMAppService.FindUserProfileByUserNameAsync(committeeUserModel.UserName);
            List<IDMRolesModel> roles = _iDMAppService.GetIDMRoles();
            IDMRolesModel iDMRolesModel = roles.FirstOrDefault(r => r.Name == committeeUserModel.RoleName);
            Enums.UserRole userType = (Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), iDMRolesModel.Name, true);
            UserProfile userProfile = new UserProfile();
            if (user == null)
            {
                userProfile = await _iDMAppService.GetUserProfileByEmployeeId(committeeUserModel.UserName, agencyCode, userType);
                Check.ArgumentNotNull(nameof(userProfile), userProfile);
                await _committeeDomainService.CheckUserExist(userProfile.Id, committeeUserModel.CommitteeId);
                await _genericCommandRepository.CreateAsync(userProfile);
                committeeUserModel.UserId = userProfile.Id;
            }
            else
            {
                var defaultSettingsForUserType = await _notificationAppService.GetDefaultSettingByUserType(userType);
                if (user.NotificationSetting.Count(x => x.UserRoleId == (int)userType) < defaultSettingsForUserType.Count)
                {
                    await _committeeDomainService.CheckUserExist(user.Id, committeeUserModel.CommitteeId);
                    user.AddNotificationSettings(defaultSettingsForUserType);
                    _genericCommandRepository.Update(user);
                }
                committeeUserModel.UserId = user.Id;
            }
            CommitteeUser committeeUser = new CommitteeUser(committeeUserModel.CommitteeId, committeeUserModel.UserId, (int)((Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), iDMRolesModel.Name)), committeeUserModel.RelatedAgencyCode);
            await _genericCommandRepository.CreateAsync(committeeUser);
            await _genericCommandRepository.SaveAsync();

            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.Email) || !string.IsNullOrEmpty(user.Mobile))
                {
                    await _notificationAppService.SendNotificationByEmailAndSmsForRolesChanged(user.Id, user.Email, user.Mobile);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(userProfile.Email) || !string.IsNullOrEmpty(userProfile.Mobile))
                {
                    await _notificationAppService.SendNotificationByEmailAndSmsForRolesChanged(userProfile.Id, userProfile.Email, userProfile.Mobile);
                }
            }
        }

        public async Task RemoveAssignedUser(int userId, int commiteeId)
        {
            var user = await _committeeQueries.GetAssignedUserByIdAndCommittee(userId, commiteeId);
            Check.ArgumentNotNull(nameof(user), user);
            user.DeActive();
            _genericCommandRepository.Update(user);
            await _genericCommandRepository.SaveAsync();
            if (!string.IsNullOrEmpty(user.UserProfile.Email) || !string.IsNullOrEmpty(user.UserProfile.Mobile))
            {
                await _notificationAppService.SendNotificationByEmailAndSmsForRolesChanged(user.UserProfile.Id, user.UserProfile.Email, user.UserProfile.Mobile);
            }
        }
        #endregion
    }
}
