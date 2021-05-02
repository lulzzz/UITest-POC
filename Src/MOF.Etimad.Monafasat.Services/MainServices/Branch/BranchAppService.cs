using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class BranchAppService : IBranchAppService
    {

        private readonly IBranchServiceQueries _branchServiceQueries;
        private readonly IBranchServiceCommand _branchServiceCommand;
        private readonly IBranchServiceDomain _branchServiceDomain;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly IIDMAppService _iDMAppService;
        private readonly INotificationAppService _notificationAppService;

        public BranchAppService(IBranchServiceQueries branchServiceQueries, IBranchServiceCommand branchServiceCommand, IBranchServiceDomain branchServiceDomain, IGenericCommandRepository genericCommandRepository, IIDMAppService iDMAppService, INotificationAppService notificationAppService)
        {
            _branchServiceQueries = branchServiceQueries;
            _branchServiceCommand = branchServiceCommand;
            _branchServiceDomain = branchServiceDomain;
            _genericCommandRepository = genericCommandRepository;
            _iDMAppService = iDMAppService;
            _notificationAppService = notificationAppService;
        }


        #region Service Query======================================================

        public async Task<QueryResult<BranchModel>> Find(BranchSearchCriteriaModel criteria)
        {
            Check.ArgumentNotNull(nameof(criteria), criteria);
            return await _branchServiceQueries.Find(criteria);
        }

        public async Task<QueryResult<BranchUserModel>> GetBranchUsers(BranchUserSearchCriteriaModel criteria)
        {
            return await _branchServiceQueries.GetBranchUsers(criteria);
        }

        public async Task<BranchUser> GetBranchUsersForAwardingApproval(int userId, int branchId)
        {
            return await _branchServiceQueries.GetBranchUsersForAwardingApproval(userId, branchId);
        }

        public async Task<BranchCommittee> GetAssignedCommitteeByIdAndBranch(int committeeId, int branchId)
        {
            return await _branchServiceQueries.GetAssignedCommitteeByIdAndBranch(committeeId, branchId);
        }

        public async Task<QueryResult<LookupModel>> GetBranchCommittees(BranchCommitteeSearchCriteriaModel criteria)
        {
            return await _branchServiceQueries.GetBranchCommittees(criteria);
        }

        public async Task<List<LookupModel>> GetAllBranchesByAgencyCode(string agencyCode)
        {
            return await _branchServiceQueries.GetAllBranchesByAgencyCode(agencyCode);
        }

        public async Task<List<LookupModel>> GetAllBranchesNotAssignedToCommittee(int committeeId, int committeeType, string agencyCode)
        {
            return await _branchServiceQueries.GetAllBranchesNotAssignedToCommittee(committeeId, committeeType, agencyCode);
        }

        #endregion

        #region  Service Command =====================================================

        public async Task<BranchModel> AddBranch(BranchModel branchModel)
        {
            List<BranchAddress> branchAddresses = new List<BranchAddress>();
            foreach (OtherBranchAddressModel addressModel in branchModel.AddressesList ?? new List<OtherBranchAddressModel>())
            {
                branchAddresses.Add(new BranchAddress(addressModel.BranchAddressTypeId, addressModel.Position, addressModel.Address, addressModel.Phone, addressModel.Fax, addressModel.Phone2, addressModel.Fax2, addressModel.Email, addressModel.Description, addressModel.AddressName, addressModel.CityCode, addressModel.PostalCode, addressModel.ZipCode, true));
            }
            var mainAddressModel = branchModel.MainBranchAddress;
            branchAddresses.Add(new BranchAddress((int)Enums.BranchAddressType.MainBranchAddress, mainAddressModel.Position, mainAddressModel.Address, mainAddressModel.Phone, mainAddressModel.Fax, mainAddressModel.Phone2, mainAddressModel.Fax2, mainAddressModel.Email, mainAddressModel.Description, mainAddressModel.AddressName, mainAddressModel.CityCode, mainAddressModel.PostalCode, mainAddressModel.ZipCode, true));

            Branch branch;
            branchModel.CommittieeIds.AddRange(branchModel.techcommitteeIds);
            branchModel.CommittieeIds.AddRange(branchModel.checkcommitteeIds);
            branchModel.CommittieeIds.AddRange(branchModel.opencommitteeIds);
            branchModel.CommittieeIds.AddRange(branchModel.purchaseCommitteeIds);
            branchModel.CommittieeIds.AddRange(branchModel.preQualificationCommitteeIds);
            branchModel.CommittieeIds.AddRange(branchModel.vrocommitteeIds);

            if (string.IsNullOrEmpty(branchModel.BranchIdString))
            {
                await _branchServiceDomain.CheckBranchExist(branchModel.BranchName, branchModel.AgencyCode);
                branch = new Branch(branchModel.AgencyCode, branchModel.BranchName, branchAddresses);
                await _branchServiceCommand.CreateBracnhAsync(branch);
                List<BranchCommittee> BranchCommittees = new List<BranchCommittee>();
                BranchCommittee committee;
                foreach (var id in branchModel.CommittieeIds)
                {
                    committee = new BranchCommittee(branch.BranchId, id);
                    BranchCommittees.Add(committee);
                }
                branch.AddCommittees(BranchCommittees);
                await _branchServiceCommand.UpdateAsync(branch);
                return await FindById(branch.BranchId);
            }
            else
            {

                branch = await _branchServiceQueries.FindBranchById(Util.Decrypt(branchModel.BranchIdString));
                branch.Update(branchModel.BranchName, branchAddresses);
                List<BranchCommittee> BranchCommittees = new List<BranchCommittee>();
                BranchCommittee committee;
                foreach (var id in branchModel.CommittieeIds)
                {
                    committee = new BranchCommittee(branch.BranchId, id);
                    BranchCommittees.Add(committee);
                }
                branch.UpdateCommittees(BranchCommittees);
                await _branchServiceCommand.UpdateAsync(branch);
                return await FindById(branch.BranchId);

            }
        }

        public async Task<BranchModel> FindById(int id)
        {
            var result = await _branchServiceQueries.FindById(id);
            result.MainBranchAddress = result.MainBranchAddress ?? new MainBranchAddressModel();
            return result;
        }

        public async Task<CreateTenderBasicDataModel> FindInsertionTypeAndPurchaseMenthodsById(int id)
        {
            var result = await _branchServiceQueries.FindInsertionTypeAndPurchaseMenthodsById(id);
            return result;
        }

        public async Task<Branch> UpdateAsync(BranchModel branchModel)
        {
            Branch branch = await _branchServiceQueries.FindBranchById(branchModel.BranchId);
            if (branch.BranchName != branchModel.BranchName)
                await _branchServiceDomain.CheckBranchExist(branchModel.BranchName, branchModel.AgencyCode);
            Check.ArgumentNotNull(nameof(branch), branch);
            branch.Update(branchModel.BranchName, new List<BranchAddress>());
            return await _branchServiceCommand.UpdateAsync(branch);
        }

        public async Task<bool> DeActiveAsync(int id)
        {
            await _branchServiceDomain.CheckTenderExist(id);
            Branch branch = await _branchServiceQueries.FindBranchById(id);

            return await _branchServiceCommand.DeActiveAsync(branch);
        }
        public async Task<List<int>> GetVROBranchsByVROOfficeCode(string VROOfficeCode)
        {
            var result = await _branchServiceQueries.GetVROBranchsByVROOfficeCode(VROOfficeCode);
            return result;
        }
        public async Task AddUserAsyn(BranchUserModel branchUserModel, string agencyCode)
        {

            Check.ArgumentNotNull(nameof(branchUserModel), branchUserModel);
            var user = await _iDMAppService.FindUserProfileByUserNameAsync(branchUserModel.UserName);
            if (user != null)
                _branchServiceDomain.AssignBranchUserExist(branchUserModel.BranchId, branchUserModel.RoleName, user);
            List<IDMRolesModel> roles = _iDMAppService.GetIDMRoles();
            IDMRolesModel iDMRolesModel = roles.FirstOrDefault(r => r.Name == branchUserModel.RoleName);
            branchUserModel.RoleName = iDMRolesModel.Name;
            branchUserModel.RoleArName = iDMRolesModel.NormalizedName;
            Enums.UserRole userType = (Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), branchUserModel.RoleName, true);
            UserProfile userProfile = new UserProfile();
            if (user == null)
            {
                userProfile = await _iDMAppService.GetUserProfileByEmployeeId(branchUserModel.UserName, agencyCode, userType);
                Check.ArgumentNotNull(nameof(userProfile), userProfile);
                await _genericCommandRepository.CreateAsync(userProfile);
                branchUserModel.UserId = userProfile.Id;
            }
            else
            {
                var defaultSettingsForUserType = await _notificationAppService.GetDefaultSettingByUserType(userType);
                if (user.NotificationSetting.Count(x => x.UserRoleId == (int)userType) < defaultSettingsForUserType.Count)
                {
                    await _branchServiceDomain.CheckUserExist(user.Id, branchUserModel.BranchId, branchUserModel.RoleName);
                    user.AddNotificationSettings(defaultSettingsForUserType);
                    _genericCommandRepository.Update(user);
                }
                branchUserModel.UserId = user.Id;
            }
            var branchUser = new BranchUser(branchUserModel.BranchId, branchUserModel.UserId, (int)((Enums.UserRole)Enum.Parse(typeof(Enums.UserRole), branchUserModel.RoleName)), branchUserModel.RelatedAgencyCode, branchUserModel.EstimatedValueFrom, branchUserModel.EstimatedValueTo);
            await _genericCommandRepository.CreateAsync(branchUser);
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

        public async Task RemoveAssignedUser(int userId, int branchId, int roleId)
        {
            var user = await _branchServiceQueries.GetAssignedUserForMangById(userId, branchId, roleId);
            Check.ArgumentNotNull(nameof(user), user);
            user.DeActive();
            _genericCommandRepository.Update(user);
            await _genericCommandRepository.SaveAsync();

            if (!string.IsNullOrEmpty(user.UserProfile.Email) || !string.IsNullOrEmpty(user.UserProfile.Mobile))
            {
                await _notificationAppService.SendNotificationByEmailAndSmsForRolesChanged(user.UserProfile.Id, user.UserProfile.Email, user.UserProfile.Mobile);
            }
        }

        public async Task RemoveAssignedUserForMang(int userId, int branchId, int roleId)
        {
            var user = await _branchServiceQueries.GetAssignedUserForMangById(userId, branchId, roleId);
            Check.ArgumentNotNull(nameof(user), user);
            user.DeActive();
            _genericCommandRepository.Update(user);
            await _genericCommandRepository.SaveAsync();
            if (!string.IsNullOrEmpty(user.UserProfile.Email) || !string.IsNullOrEmpty(user.UserProfile.Mobile))
            {
                await _notificationAppService.SendNotificationByEmailAndSmsForRolesChanged(user.UserProfile.Id, user.UserProfile.Email, user.UserProfile.Mobile);
            }
        }

        public async Task RemoveAssignedCommittee(int committeeId, int branchId)
        {
            var user = await _branchServiceQueries.GetAssignedCommitteeByIdAndBranch(committeeId, branchId);
            Check.ArgumentNotNull(nameof(user), user);
            user.DeActive();
            _genericCommandRepository.Update(user);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task AddCommittee(BranchCommitteeModel branchCommitteeModel)
        {
            Check.ArgumentNotNull(nameof(branchCommitteeModel), branchCommitteeModel);
            if (string.IsNullOrEmpty(branchCommitteeModel.CommitteeIdsString))
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.ModelDataError);
            }
            branchCommitteeModel.CommitteeIds = (branchCommitteeModel.CommitteeIdsString.Split(',').Where(w => !string.IsNullOrEmpty(w)).Select(d => int.Parse(d)).ToList());
            foreach (var committee in branchCommitteeModel.CommitteeIds)
            {

                await _branchServiceDomain.CheckCommitteeExist(committee, branchCommitteeModel.BranchId);
                var branchUser = new BranchCommittee(branchCommitteeModel.BranchId, committee);
                await _genericCommandRepository.CreateAsync(branchUser);
            }
            await _genericCommandRepository.SaveAsync();
        }

        public async Task<List<RoleModel>> GetUserRolesById(string userName, string agencyCode, int CommitteeTypeId, UsersSearchCriteriaModel searchCriteria)
        {
            searchCriteria.PageSize = 100;
            searchCriteria.NationalIds.Add(userName);
            var result = await _iDMAppService.GetMonafasatUsers(searchCriteria);
            var employeeList = result.Items;
            var employee = employeeList.FirstOrDefault(d => d.nationalId == userName);
            if (CommitteeTypeId == 0 && employee.roles != null && employee.roles.Any())
            {
                return employee.roles.Where(r => r != null && (r.RoleName == RoleNames.PurshaseSpecialist.ToString() || r.RoleName == RoleNames.EtimadOfficer.ToString() || r.RoleName == RoleNames.PrePlanningAuditor.ToString() || r.RoleName == RoleNames.PrePlanningCreator.ToString() || r.RoleName == RoleNames.Auditer.ToString() || r.RoleName == RoleNames.ApproveTenderAward.ToString() || r.RoleName == RoleNames.DataEntry.ToString())).Select(d => new RoleModel { RoleId = d.RoleId, RoleName = d.RoleName, RoleNameAr = d.RoleNameAr }).ToList();
            }
            else if (CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee)
            {
                return employee.roles.Where(r => r != null && (r.RoleName == RoleNames.OffersCheckManager.ToString() || r.RoleName == RoleNames.OffersCheckSecretary.ToString())).Select(r => new RoleModel { RoleId = r.RoleId, RoleName = r.RoleName, RoleNameAr = r.RoleNameAr }).ToList();
            }
            else if (CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee)
            {
                return employee.roles.Where(r => r != null && (r.RoleName == RoleNames.OffersOppeningManager.ToString() || r.RoleName == RoleNames.OffersOppeningSecretary.ToString())).Select(r => new RoleModel { RoleId = r.RoleId, RoleName = r.RoleName, RoleNameAr = r.RoleNameAr }).ToList();
            }
            else if (CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee)
            {
                return employee.roles.Where(r => r != null && r.RoleName == RoleNames.TechnicalCommitteeUser.ToString()).Select(r => new RoleModel { RoleId = r.RoleId, RoleName = r.RoleName, RoleNameAr = r.RoleNameAr }).ToList();
            }
            else if (CommitteeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee)
            {
                return employee.roles.Where(r => r != null && (r.RoleName == RoleNames.PreQualificationCommitteeManager.ToString() || r.RoleName == RoleNames.PreQualificationCommitteeSecretary.ToString())).Select(r => new RoleModel { RoleId = r.RoleId, RoleName = r.RoleName, RoleNameAr = r.RoleNameAr }).ToList();
            }
            else if (CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee)
            {
                return employee.roles.Where(r => r != null && (r.RoleName == RoleNames.OffersPurchaseManager.ToString() || r.RoleName == RoleNames.OffersPurchaseSecretary.ToString())).Select(r => new RoleModel { RoleId = r.RoleId, RoleName = r.RoleName, RoleNameAr = r.RoleNameAr }).ToList();
            }
            else if (CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee)
            {
                return employee.roles.Where(r => r != null && (r.RoleName == RoleNames.OffersOpeningAndCheckManager.ToString() || r.RoleName == RoleNames.OffersOpeningAndCheckSecretary.ToString())).Select(r => new RoleModel { RoleId = r.RoleId, RoleName = r.RoleName, RoleNameAr = r.RoleNameAr }).ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetUserDefultBranch(int userId, string agencyCode)
        {
            var branchId = await _branchServiceQueries.GetUserDefultBranch(userId, agencyCode);
            return branchId;
        }

        public async Task<int> GetUserDefultCommittee(int userId, string agencyCode)
        {
            var committeeId = await _branchServiceQueries.GetUserDefultCommittees(userId, agencyCode);
            return committeeId;
        }

        public async Task<List<string>> GetUserRoleByCommittee(int userId, int committeeId)
        {
            var role = await _branchServiceQueries.GetUserRoleByCommittee(committeeId, userId);
            return role;
        }

        public async Task<List<string>> GetUserRolesByBranch(int userId, int branchId)
        {
            var Roles = await _branchServiceQueries.GetUserRolesByBranch(userId, branchId);
            var committeeRoles = await _branchServiceQueries.GetUserRolesByBranchFromCommittees(userId, branchId);
            Roles.AddRange(committeeRoles);
            return Roles;
        }

        public async Task<List<UserRolesModel>> GetAllUserRoles(int userId, string agencyCode)
        {
            var roles = await _branchServiceQueries.GetAllUserRolesFromBranches(userId, agencyCode);
            var committeeRoles = await _branchServiceQueries.GetAllUserRolesFromCommittees(userId, agencyCode);
            roles.AddRange(committeeRoles);
            return roles;
        }

        public Task<List<BranchModel>> GetBranchByAgency(string agencyCode)
        {
            return _branchServiceQueries.GetBranchByAgency(agencyCode);
        }

        public async Task<List<UserLookupModel>> GetIDMUsers(BranchUserSearchCriteriaModel criteria)
        {
            UsersSearchCriteriaModel userSearchCriteriaModel = new UsersSearchCriteriaModel
            {
                AgencyId = criteria.AgencyId,
                AgencyType = (int)criteria.AgencyType,
                PageSize = 100,
                Name = criteria.UserName,
                Email = criteria.EMail,
                RoleNames = criteria.RoleNames,
                RoleName = criteria.RoleName
            };
            var result = await _iDMAppService.GetMonafasatUsers(userSearchCriteriaModel);
            var employeeList = result.Items;
            List<UserLookupModel> userLookupModels = new List<UserLookupModel>();
            criteria.RoleNames = new List<string> { RoleNames.ApproveTenderAward.ToString(), RoleNames.PurshaseSpecialist.ToString(), RoleNames.EtimadOfficer.ToString(), RoleNames.Auditer.ToString(), RoleNames.DataEntry.ToString(), RoleNames.PrePlanningAuditor.ToString(), RoleNames.PrePlanningCreator.ToString() };
            foreach (ManageUsersAssignationModel employee in employeeList)
            {
                userLookupModels.Add(new UserLookupModel() { Name = employee.fullName + " - " + employee.nationalId + " - " + employee.email, UserName = employee.nationalId, UserId = employee.userId });
            }
            return userLookupModels;
        }

        #endregion
    }
}
