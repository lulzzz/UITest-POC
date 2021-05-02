using AutoMapper;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.User;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class IDMAppService : IIDMAppService
    {
        private readonly IIDMQueries _iDMQueries;
        private readonly ICommandService _commandService;
        private readonly IIDMProxy _idmProxy; private readonly IMapper _mapper;
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationAppService _notificationAppService;
        private readonly INotificationQueries _notificationQueries;

        public IDMAppService(IIDMProxy iDMProxy, IIDMQueries iDMQueries, IMapper mapper, ICommandService commandService, IHttpContextAccessor httpContextAccessor, INotificationQueries notificationQueries, INotificationAppService notificationAppService, IAppDbContext context)
        {
            _commandService = commandService;
            _iDMQueries = iDMQueries;
            _mapper = mapper;
            _idmProxy = iDMProxy;
            _httpContextAccessor = httpContextAccessor;
            _notificationAppService = notificationAppService;
            _notificationQueries = notificationQueries;
            _context = context;
        }

        #region Queries =========================================

        public async Task<GovAgency> FindGovAgencyByIdAsync(string agencyCode)
        {
            return await _iDMQueries.FindGovAgencyByIdAsync(agencyCode);
        }

        public async Task<UserProfile> FindUserProfileByUserNameAsync(string UserName)
        {
            return await _iDMQueries.FindUserProfileByUserNameAsync(UserName);
        }
        public async Task SetUserDefaultRole(string userRole)
        {
            Check.ArgumentNotNullOrEmpty(userRole, userRole);
            UserProfile user = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(_httpContextAccessor.HttpContext.User.UserId());
            user.UpdateDefaultUserRole(userRole);
            await _notificationAppService.UpdateUser(user);
        }
        public async Task<List<UserLookupModel>> FindUsersByRoleNameAndAgencyCodeForCommitteeAssignAsync(string roleName, string agencyCode)
        {
            return await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAsync(roleName, agencyCode);
        }

        public async Task<List<UserLookupModel>> GetAllDataEntryAndAuditorUsers(string roleName, string agencyCode)
        {
            return await _iDMQueries.GetAllDataEntryAndAuditorUsers(roleName, agencyCode);
        }

        public async Task<List<UserLookupModel>> FindUsersByRoleNameAndAgencyCodeNotAssignedToCommitteeAssignAsync(string roleName, string agencyCode, int committeeId, AgencyType agencyType = (int)AgencyType.None)
        {
            List<string> RoleNames = new List<string>();
            if (!String.IsNullOrEmpty(roleName))
                RoleNames.Add(roleName);

            UsersSearchCriteriaModel userSearchCriteriaModel = new UsersSearchCriteriaModel
            {
                AgencyId = agencyCode,
                AgencyType = (int)agencyType,
                RoleNames = RoleNames,
                PageSize = 100
            };
            List<EmployeeIntegrationModel> employeeList = (await _idmProxy.GetMonafasatUsersByAgencyType(userSearchCriteriaModel)).Items.ToList();
            List<UserLookupModel> userLookupModels = new List<UserLookupModel>();

            List<int> assignedUsers = await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(roleName, agencyCode, committeeId);
            foreach (EmployeeIntegrationModel employee in employeeList)
            {
                if (employee.roles != null && employee.roles.Count > 0 && employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && (employee.roles.Where(r => r.Name == roleName)).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                    userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
            }
            return userLookupModels;
        }
        public async Task<List<UserLookupModel>> GetUsersbyCommitteeTypeId(string agencyCode, int committeeId, int committeeTypeId, string email = "", string roleName = "", int agencyType = (int)AgencyType.None)
        {
            List<string> roleNames = new List<string>();
            if (!String.IsNullOrEmpty(roleName))
                roleNames.Add(roleName);

            List<int> assignedUsers = new List<int>();
            UsersSearchCriteriaModel userSearchCriteriaModel = new UsersSearchCriteriaModel
            {
                AgencyId = agencyCode,
                AgencyType = agencyType,
                Email = email,
                RoleNames = roleNames,
                PageSize = 100
            };
            var result = await _idmProxy.GetMonafasatUsersByAgencyType(userSearchCriteriaModel);
            var employeeList = result.Items;
            List<UserLookupModel> userLookupModels = new List<UserLookupModel>();
            if (committeeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersOppeningManager.ToString(), agencyCode, committeeId));
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersOppeningSecretary.ToString(), agencyCode, committeeId));

                foreach (EmployeeIntegrationModel employee in employeeList)
                {
                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && (employee.roles.Where(r => r.Name == RoleNames.OffersOppeningManager.ToString() || r.Name == RoleNames.OffersOppeningSecretary.ToString())).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                    {
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                    }
                }
            }
            else if (committeeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersCheckManager.ToString(), agencyCode, committeeId));
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersCheckSecretary.ToString(), agencyCode, committeeId));

                foreach (EmployeeIntegrationModel employee in employeeList)
                {
                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && (employee.roles.Where(r => r.Name == RoleNames.OffersCheckManager.ToString() || r.Name == RoleNames.OffersCheckSecretary.ToString())).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                }

            }
            else if (committeeTypeId == (int)Enums.CommitteeType.TechincalCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.TechnicalCommitteeUser.ToString(), agencyCode, committeeId));

                foreach (EmployeeIntegrationModel employee in employeeList)
                {
                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && employee.roles.Where(r => r.Name == RoleNames.TechnicalCommitteeUser.ToString()).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                }
            }
            else if (committeeTypeId == (int)Enums.CommitteeType.VROCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersOpeningAndCheckManager.ToString(), agencyCode, committeeId));
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersOpeningAndCheckSecretary.ToString(), agencyCode, committeeId));
                foreach (EmployeeIntegrationModel employee in employeeList)
                {
                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && (employee.roles.Where(r => r.Name == RoleNames.OffersOpeningAndCheckManager.ToString() || r.Name == RoleNames.OffersOpeningAndCheckSecretary.ToString())).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                }
            }
            return userLookupModels;
        }

        public async Task<List<UserLookupModel>> GetUsersbyCommitteeTypeId(CommitteeUserSearchCriteriaModel CriteriaModel)
        {
            List<string> roleNames = new List<string>();
            if (!String.IsNullOrEmpty(CriteriaModel.RoleName))
                roleNames.Add(CriteriaModel.RoleName);

            if (String.IsNullOrEmpty(CriteriaModel.RoleName) && CriteriaModel.RoleNames.Count > 0)
            {
                roleNames = CriteriaModel.RoleNames;
            }

            List<int> assignedUsers = new List<int>();
            UsersSearchCriteriaModel userSearchCriteriaModel = new UsersSearchCriteriaModel
            {
                AgencyId = CriteriaModel.AgencyId,
                AgencyType = (int)CriteriaModel.AgencyType,
                Email = CriteriaModel.EMail,
                RoleNames = roleNames,
                PageSize = 100,
                Name = CriteriaModel.UserName

            };

            var result = await _idmProxy.GetMonafasatUsersByAgencyType(userSearchCriteriaModel);
            var employeeList = result.Items;
            List<UserLookupModel> userLookupModels = new List<UserLookupModel>();
            if (CriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersOppeningManager.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersOppeningSecretary.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));

                foreach (EmployeeIntegrationModel employee in employeeList)
                {

                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && (employee.roles.Where(r => r.Name == RoleNames.OffersOppeningManager.ToString() || r.Name == RoleNames.OffersOppeningSecretary.ToString())).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                }
            }
            else if (CriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersCheckManager.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersCheckSecretary.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));

                foreach (EmployeeIntegrationModel employee in employeeList)
                {
                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && (employee.roles.Where(r => r.Name == RoleNames.OffersCheckManager.ToString() || r.Name == RoleNames.OffersCheckSecretary.ToString())).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                }
            }
            else if (CriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.TechnicalCommitteeUser.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));

                foreach (EmployeeIntegrationModel employee in employeeList)
                {
                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && employee.roles.Where(r => r.Name == RoleNames.TechnicalCommitteeUser.ToString()).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                }
            }

            else if (CriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.PreQualificationCommitteeManager.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.PreQualificationCommitteeSecretary.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));

                foreach (EmployeeIntegrationModel employee in employeeList)
                {
                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && (employee.roles.Where(r => r.Name == RoleNames.PreQualificationCommitteeManager.ToString() || r.Name == RoleNames.PreQualificationCommitteeSecretary.ToString())).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                }
            }
            else if (CriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersPurchaseSecretary.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersPurchaseManager.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));

                foreach (EmployeeIntegrationModel employee in employeeList)
                {
                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && (employee.roles.Where(r => r.Name == RoleNames.OffersPurchaseManager.ToString() || r.Name == RoleNames.OffersPurchaseSecretary.ToString())).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                }
            }
            else if (CriteriaModel.CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee)
            {
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersOpeningAndCheckSecretary.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));
                assignedUsers.AddRange(await _iDMQueries.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(RoleNames.OffersOpeningAndCheckManager.ToString(), CriteriaModel.AgencyId, CriteriaModel.CommitteeId));

                foreach (EmployeeIntegrationModel employee in employeeList)
                {
                    if (employee.roles != null && employee.roles.Count > 0 && employee.roles[0] != null && (employee.roles.Where(r => r.Name == RoleNames.OffersOpeningAndCheckManager.ToString() || r.Name == RoleNames.OffersOpeningAndCheckSecretary.ToString())).FirstOrDefault() != null && !assignedUsers.Contains(employee.userId))
                        userLookupModels.Add(new UserLookupModel() { Name = employee.fullName, UserName = employee.nationalId, UserId = employee.userId });
                }
            }
            return userLookupModels;
        }


        public async Task<List<GovAgencyModel>> GetALlAgencies()
        {
            return await _iDMQueries.GetAllAgencies();
        }
        public async Task<List<string>> GetAllSupplierOnTender(int tenderId)
        {
            List<string> suppliers = await _iDMQueries.GetAllSupplierOnTender(tenderId);
            return suppliers;
        }

        public async Task<List<string>> GetAllSupplierOnQualification(int tenderId)
        {
            List<string> suppliers = await _iDMQueries.GetAllSupplierOnQualification(tenderId);
            return suppliers;
        }

        public async Task<List<string>> QualificationToSendInvitation(int tenderId)
        {
            List<string> suppliers = await _iDMQueries.GetAllSupplierQualificationToSendInvitation(tenderId);
            return suppliers;
        }

        public async Task<List<int>> GetTechnicalCommitteeMembersOnTender(int committeeId)
        {
            var users = await _iDMQueries.GetTechnicalCommitteeMembersOnTender(committeeId);
            return users;
        }

        public async Task<SupplierFullDataModel> GetSupplierInfoByCR(string CR)
        {
            return (await _idmProxy.GetSupplierInfoByCR(CR));
        }

        public async Task<List<ContactOfficerModel>> GetContactOfficersByCR(List<string> CRs)
        {
            var result = await _idmProxy.GetContactOfficersByCR(CRs);
            return result.Items.ToList();
        }

        public async Task<QueryResult<SupplierIntegrationModel>> GetSupplierDetailsBySearchCriteria(SupplierIntegrationSearchCriteria searchCriteria)
        {
            return await _idmProxy.GetSuppliersBySearchCriteria(searchCriteria);
        }

        public async Task<List<UserLookupModel>> GetUserBasedOnlistOfRoleIds(List<int> lstOfuserRoles, string agencyCode)
        {
            return await _iDMQueries.GetUserBasedOnlistOfRoleName(lstOfuserRoles, agencyCode);
        }

        public async Task<List<BranchModel>> FindBranchesNotAssignedByUserAndRole(string agencyCode, int userId, string roleName)
        {
            return await _iDMQueries.FindBranchesNotAssignedByUserAndRole(agencyCode, userId, roleName);
        }
        public async Task<ManageUsersAssignationModel> GetMonafastatEmployeeDetailById(string agencyCode, string nationalId, string roleName = "", int agencyType = (int)AgencyType.None)
        {
            List<string> RoleNames = new List<string>();
            if (!String.IsNullOrEmpty(roleName))
                RoleNames.Add(roleName);
            UsersSearchCriteriaModel userSearchCriteriaModel = new UsersSearchCriteriaModel
            {
                AgencyId = agencyCode,
                RoleNames = RoleNames,
                NationalIds = new List<string>() { nationalId },
                AgencyType = agencyType
            };
            var result = await _idmProxy.GetMonafasatUsersByAgencyType(userSearchCriteriaModel);
            var employee = result.Items.SingleOrDefault();
            var model = _mapper.Map<ManageUsersAssignationModel>(employee);
            model.BranchRoles = GetBranchRoles(model.roles);
            model.AllCommitteeRoles = GetCommitteeRoles(model.roles);

            model.CommitteeOpenOfferRoles = GetRolesByCommitteeType(model.roles, Enums.CommitteeType.OpenOfferCommittee);
            model.CommitteeAuditOfferRoles = GetRolesByCommitteeType(model.roles, Enums.CommitteeType.CheckOfferCommittee);
            model.CommitteeTechnicalRoles = GetRolesByCommitteeType(model.roles, Enums.CommitteeType.TechincalCommittee);
            model.CommitteeOpenAndCheckRoles = GetRolesByCommitteeType(model.roles, Enums.CommitteeType.VROCommittee);
            model.CommitteePreQualificationRoles = GetRolesByCommitteeType(model.roles, Enums.CommitteeType.PreQualificationCommittee);
            model.CommitteePurchaseRoles = GetRolesByCommitteeType(model.roles, Enums.CommitteeType.PurchaseCommittee);
            return model;
        }

        public async Task<QueryResult<ManageUsersAssignationModel>> GetMonafasatUsers(UsersSearchCriteriaModel userSearchCriteriaModel)
        {
            bool filterUnAssigned = !String.IsNullOrEmpty(userSearchCriteriaModel.NotAssignedOnlyUserFlag) && (userSearchCriteriaModel.NotAssignedOnlyUserFlag.ToLower() == "1" || userSearchCriteriaModel.NotAssignedOnlyUserFlag.ToLower() == "true");

            if (!string.IsNullOrEmpty(userSearchCriteriaModel.RoleName))
            {
                userSearchCriteriaModel.RoleNames = new List<string>
                {
                    userSearchCriteriaModel.RoleName
                };
            }
            if (!string.IsNullOrEmpty(userSearchCriteriaModel.BranchId))
            {
                userSearchCriteriaModel.NationalIds = await _iDMQueries.FindUsersAssignedToBranch(userSearchCriteriaModel);
            }
            else if (!string.IsNullOrEmpty(userSearchCriteriaModel.NationalId))
            {
                userSearchCriteriaModel.NationalIds.Add(userSearchCriteriaModel.NationalId.Trim());
            }

            var unassignedListcount = 0;
            if (filterUnAssigned)
            {
                userSearchCriteriaModel.AssignedUserIds = await _iDMQueries.GetAssignedUsersByAgency(userSearchCriteriaModel.AgencyId);
                unassignedListcount = userSearchCriteriaModel.AssignedUserIds.Count;
            }

            QueryResult<EmployeeIntegrationModel> result = await _idmProxy.GetMonafasatUsersByAgencyType(userSearchCriteriaModel);
            List<EmployeeIntegrationModel>  employeeIntegrationModellist = result.Items.ToList();
            unassignedListcount = result.TotalCount;
            var usersList = _mapper.Map<List<ManageUsersAssignationModel>>(employeeIntegrationModellist);
            QueryResult<ManageUsersAssignationModel> employeeListQueryResult = new QueryResult<ManageUsersAssignationModel>(usersList.AsEnumerable(), unassignedListcount, userSearchCriteriaModel.PageNumber, userSearchCriteriaModel.PageSize);
            return employeeListQueryResult;

        }

        public async Task<QueryResult<EmployeeIntegrationModel>> GetMonafasatUsersForBlockUserList(UsersSearchCriteriaModel userSearchCriteriaModel)
        {
            return await _idmProxy.GetMonafasatUsersByAgencyType(userSearchCriteriaModel);
        }
        public async Task<AgencyInfoModel> GetAgencyDetailsRelatedToSadad(string agencyCode, int agencyType)
        {
            return await _idmProxy.GetAgencyDetailsRelatedToSadad(agencyCode, agencyType);
        }

        public async Task<Dictionary<string, string>> GetAgencyLogos(List<string> agencyCode)
        {
            return await _idmProxy.GetAgencyLogos(agencyCode);
        }

        public List<IDMRolesModel> GetIDMRoles()
        {
            List<IDMRolesModel> list = new List<IDMRolesModel>();
            RoleNames.GetRolesWithCaptions().ToList().ForEach(x =>
           {
               list.Add(new IDMRolesModel() { Name = x.Key, NormalizedName = x.Value });
           });
            return list;
        }

        #endregion

        #region methods to filter roles per Committee

        private List<RoleModel> GetRolesByCommitteeType(List<RoleModel> roles, Enums.CommitteeType CommitteeType)
        {
            List<string> allowedRoles = new List<string>();
            switch (CommitteeType)
            {
                case Enums.CommitteeType.TechincalCommittee:
                    allowedRoles.AddRange(new string[] { Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_TechnicalCommitteeUser) });
                    break;
                case Enums.CommitteeType.OpenOfferCommittee:
                    allowedRoles.AddRange(new string[] { Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningManager), Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningSecretary) });
                    break;
                case Enums.CommitteeType.CheckOfferCommittee:
                    allowedRoles.AddRange(new string[] { Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersCheckManager), Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersCheckSecretary) });
                    break;
                case Enums.CommitteeType.VROCommittee:
                    allowedRoles.AddRange(new string[] { Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningAndCheckManager), Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_OffersOpeningAndCheckSecretary) });
                    break;
                case Enums.CommitteeType.BlockCommittee:
                    break;
                case Enums.CommitteeType.PurchaseCommittee:
                    allowedRoles.AddRange(new string[] { Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee), Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee) });
                    break;
                case Enums.CommitteeType.PreQualificationCommittee:
                    allowedRoles.AddRange(new string[] { Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_PreQualificationCommitteeSecretary), Enum.GetName(typeof(Enums.UserRole), Enums.UserRole.NewMonafasat_PreQualificationCommitteeManager) });
                    break;
                default:
                    break;
            }
            var result = roles.Where(r => allowedRoles.Contains(r.RoleName)).ToList();
            return result;
        }

        private static List<RoleModel> GetBranchRoles(List<RoleModel> roles)
        {
            string[] allowedRoles = {
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_Auditer),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_DataEntry),
                Enum.GetName(typeof(Enums.UserRole),  Enums.UserRole.NewMonafasat_PlanningApprover),
                Enum.GetName(typeof(Enums.UserRole),  Enums.UserRole.NewMonafasat_PlanningOfficer),
                Enum.GetName(typeof(Enums.UserRole),   Enums.UserRole.NewMonafasat_EtimadOfficer),
                Enum.GetName(typeof(Enums.UserRole),    Enums.UserRole.NewMonafasat_PurshaseSpecialist),
                Enum.GetName(typeof(Enums.UserRole),  Enums.UserRole.NewMonafasat_ApproveTenderAward) };
            var result = roles.Where(r => allowedRoles.Contains(r.RoleName)).ToList();
            return result;
        }
        private static List<RoleModel> GetCommitteeRoles(List<RoleModel> roles)
        {
            string[] allowedRoles = {
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_TechnicalCommitteeUser),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_OffersOpeningSecretary),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_OffersOpeningManager),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_OffersCheckSecretary),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_OffersCheckManager),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_PreQualificationCommitteeSecretary),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_PreQualificationCommitteeManager),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_OffersOpeningAndCheckManager),
                Enum.GetName(typeof(Enums.UserRole),Enums.UserRole.NewMonafasat_OffersOpeningAndCheckSecretary),
            };
            var result = roles.Where(r => allowedRoles.Contains(r.RoleName)).ToList();
            return result;
        }

        #endregion

        #region Commands======================================

        public async Task<LoggedUserModel> SyncUserInfo(int userId, string userName, string fullName, string mobile, string email, string supplierNumber, string supplierName, string agencyCode, List<string> userRoles, string relatedAgencyCode, List<string> vroUserRoles)
        {
            if (userRoles.Count == 0) { return null; }
            UserProfile user = await syncUser(userId, userName, fullName, mobile, email, userRoles, agencyCode, vroUserRoles);
            await syncGovAgency();
            await syncSupplier(supplierNumber, supplierName, userId);
            await _commandService.SaveAsync();
            LoggedUserModel loggedUserModel = prepaireUserRoles(agencyCode, userRoles, user, vroUserRoles);
            return loggedUserModel;
        }
        private LoggedUserModel prepaireUserRoles(string agencyCode, List<string> userRoles, UserProfile user, List<string> vroUserRoles)
        {
            System.Security.Claims.ClaimsPrincipal identity = _httpContextAccessor.HttpContext.User;
            LoggedUserModel loggedUserModel = new LoggedUserModel();

            if (user.BranchUsers != null && user.BranchUsers.Any(b => b.IsActive == true && b.Branch.AgencyCode == agencyCode))
            {

                loggedUserModel.AssignedRoleLevelTypeModels.AddRange(user.BranchUsers.Where(b => b.IsActive == true && b.Branch.AgencyCode == agencyCode && b.Branch.IsActive == true).Select(b => new AssignedRoleLevelTypeModel
                {
                    AssignedRoleLevel = AssignedRoleLevelType.Branch,
                    Id = b.BranchId,
                    RoleName = b.UserRole.Name,
                    DisplayedRoleName = b.UserRole.DisplayNameAr + " - " + b.Branch.BranchName
                }));
            }
            if (user.CommitteeUsers != null && user.CommitteeUsers.Any(c => c.IsActive == true && c.Committee.AgencyCode == agencyCode))
            {
                loggedUserModel.AssignedRoleLevelTypeModels.AddRange(user.CommitteeUsers.Where(c => c.IsActive == true
                && c.Committee.AgencyCode == agencyCode
                && c.Committee.IsActive == true).Select(b => new AssignedRoleLevelTypeModel
                {
                    AssignedRoleLevel = AssignedRoleLevelType.Committee,
                    Id = b.CommitteeId,
                    RoleName = b.UserRole.Name,
                    DisplayedRoleName = b.UserRole.DisplayNameAr + " - " + b.Committee.CommitteeName
                }));
            }

            var nonAssignedRoles = RoleNames.GetNonAssignedRoles();
            if (!identity.UserIsVRO())
            {
                if (userRoles.Any(c => nonAssignedRoles.Contains(c)))
                {
                    loggedUserModel.AssignedRoleLevelTypeModels.AddRange(userRoles.Where(c => nonAssignedRoles.Contains(c)).ToList().Select(s =>
                                       new AssignedRoleLevelTypeModel
                                       {
                                           AssignedRoleLevel = AssignedRoleLevelType.NotAssigned,
                                           Id = 0,
                                           RoleName = s.Split(',')[0],
                                           DisplayedRoleName = GetRoleDisplayNameByValue(s)
                                       }));
                }
                loggedUserModel.DefaultRoleDetails = user.DefaultUserRole;
                if (loggedUserModel.AssignedRoleLevelTypeModels.Count() == 0)
                {
                    userRoles.Add(RoleNames.UnAssingedUser);
                    loggedUserModel.AssignedRoleLevelTypeModels.AddRange(userRoles.Where(c => nonAssignedRoles.Contains(c)).ToList().Select(s =>
                                     new AssignedRoleLevelTypeModel
                                     {
                                         AssignedRoleLevel = AssignedRoleLevelType.NotAssigned,
                                         Id = 0,
                                         RoleName = RoleNames.UnAssingedUser,
                                         DisplayedRoleName = GetRoleDisplayNameByValue(s)
                                     }));

                }

            }
            else
            {
                if (vroUserRoles.Any(c => nonAssignedRoles.Contains(c)))
                {
                    loggedUserModel.AssignedRoleLevelTypeModels.AddRange(vroUserRoles.Where(c => nonAssignedRoles.Contains(c)).ToList().Select(s =>
                                       new AssignedRoleLevelTypeModel
                                       {
                                           AssignedRoleLevel = AssignedRoleLevelType.NotAssigned,
                                           Id = 0,
                                           RoleName = s.Split(',')[0],
                                           DisplayedRoleName = GetRoleDisplayNameByValue(s)
                                       }));
                }
                loggedUserModel.DefaultRoleDetails = user.DefaultUserRole;
                if (loggedUserModel.AssignedRoleLevelTypeModels.Count() == 0)
                {
                    vroUserRoles.Add(RoleNames.UnAssingedUser);
                    loggedUserModel.AssignedRoleLevelTypeModels.AddRange(vroUserRoles.Where(c => nonAssignedRoles.Contains(c)).ToList().Select(s =>
                                     new AssignedRoleLevelTypeModel
                                     {
                                         AssignedRoleLevel = AssignedRoleLevelType.NotAssigned,
                                         Id = 0,
                                         RoleName = RoleNames.UnAssingedUser,
                                         DisplayedRoleName = GetRoleDisplayNameByValue(s)
                                     }));

                }

            }
            loggedUserModel.AssignedRoleLevelTypeModels = loggedUserModel.AssignedRoleLevelTypeModels.Distinct().ToList();



            return loggedUserModel;
        }
        private async Task<UserProfile> syncUser(int userId, string userName, string fullName, string mobile, string email, List<string> userRoles, string agencyCode, List<string> vroUserRoles)
        {
            System.Security.Claims.ClaimsPrincipal identity = _httpContextAccessor.HttpContext.User;
            UserProfile user = await _iDMQueries.FindUserProfileByIdAsync(userId);
            List<int> userRoleIds = new List<int>();

            if (identity.UserIsVRO())
            {
                vroUserRoles.Where(c => RoleNames.GetRolesWithCaptions().ContainsKey(c)).ToList().ForEach(role =>
                {
                    userRoleIds.Add((int)Enum.Parse(typeof(Enums.UserRole), role));
                });
            }
            else
            {
                userRoles.Where(c => RoleNames.GetRolesWithCaptions().ContainsKey(c)).ToList().ForEach(role =>
                {
                    userRoleIds.Add((int)Enum.Parse(typeof(Enums.UserRole), role));
                });

            }

            if (user == null)
            {
                user = new UserProfile(userId, userName, fullName, mobile, email, await _notificationAppService.GetDefaultSettingByUserTypes(userRoleIds));
                await _commandService.CreateAsync(user);
            }
            else
            {

                var relatedAgencyCode = identity.UserRelatedAgencyCode();
                var defaultSettingsForUserType = await _notificationAppService.GetDefaultSettingByUserTypes(userRoleIds);
                var currentSetting = await _notificationQueries.FindUserNotificationSettingbyUserProfileId(user.Id);
                var notassignedroles = defaultSettingsForUserType.Where(w => !currentSetting.Any(d => d.NotificationCodeId == w.NotificationCodeId)).ToList();
                user.AddMissingNotificationSettings(notassignedroles);
                user.Update(fullName, userName, email, mobile);

                if (!identity.UserIsVRO())
                {
                    user.CheckExistRoleForUser(userRoles, agencyCode);
                }
                else
                {
                    user.CheckExistRoleForUser(vroUserRoles, agencyCode, relatedAgencyCode);
                }
                foreach (var item in user.BranchUsers)
                {
                    item.Branch = null;
                    item.UserRole = null;
                }
                foreach (var item in user.CommitteeUsers)
                {
                    item.Committee = null;
                    item.UserRole = null;
                }
                _commandService.UpdateAsync(user);
                user = await _iDMQueries.FindUserProfileByIdAsync(userId);
            }
            return user;
        }
        private static string GetRoleDisplayNameByValue(string item)
        {
            var fieldinfo = (typeof(RoleNames).GetFields().FirstOrDefault(r => (r.GetValue(null) as string) == item));
            return ((PermissionCaptionAttribute)fieldinfo.GetCustomAttributes(typeof(PermissionCaptionAttribute), false).FirstOrDefault()).Caption;
        }

        private async Task syncSupplier(string selectedCrNumber, string selectedCrName, int userId)
        {
            if (!string.IsNullOrEmpty(selectedCrNumber))
            {
                Supplier supplier = await _iDMQueries.FindSupplierObjectByUserIdAsync(selectedCrNumber);
                SupplierUserProfile userSupplier = new SupplierUserProfile(selectedCrNumber, userId);
                if (supplier == null)
                {
                    Supplier supplierNew = new Supplier(selectedCrNumber, selectedCrName, await _notificationAppService.GetDefaultSettingByCr());
                    await _commandService.CreateAsync(supplierNew);
                    await _commandService.CreateAsync(userSupplier);
                }
                else
                {
                    List<UserNotificationSetting> defaultNotificationSettings = await _notificationAppService.GetDefaultSettingByCr();

                    supplier.AddNotificationSettings(defaultNotificationSettings);
                    _commandService.UpdateAsync(supplier);
                    SupplierUserProfile supplierUserObject = await _iDMQueries.FindSupplierUserProfileByUserProfileIdAndCrAsync(userId, selectedCrNumber);
                    if (supplierUserObject == null)
                        await _commandService.CreateAsync(userSupplier);
                }
            }
        }

        private async Task syncGovAgency()
        {
            System.Security.Claims.ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            string agencyCode;
            string UserAgencyName = user.UserAgencyName();
            UserAgencyName = UserAgencyName.Replace(Environment.NewLine, "");

            if ((user.UserIsSemiGovAgency() && user.UserRoles().Contains(RoleNames.supplier)) || user.UserCategory() == (int)Enums.IDMUserCategory.GovVendor && user.UserRoles().Contains(RoleNames.supplier))
                agencyCode = user.SupplierAgency();
            else
                agencyCode = user.UserAgency();
            if (string.IsNullOrEmpty(agencyCode))
                return;

            GovAgency agency = await _iDMQueries.FindGovAgencyByIdAsync(agencyCode);
            GovAgency agencyVRO = await _iDMQueries.FindGovAgencyByIdAsync(user.RelatedVRoCode());
            if (agency == null)
            {
                if (agencyVRO == null && !string.IsNullOrEmpty(user.RelatedVRoCode()))
                {
                    agencyVRO = new GovAgency(user.RelatedVRoCode(), UserAgencyName, 3, true, (int)Enums.AgencyType.VRO, user.Mobile());
                    await _commandService.CreateAsync(agencyVRO);
                }
                agency = new GovAgency(agencyCode, UserAgencyName, 3, user.UserIsVRO(), int.Parse(user.UserCategoryID()), user.Mobile(), string.IsNullOrEmpty(user.RelatedVRoCode()) ? null : user.RelatedVRoCode());
                await _commandService.CreateAsync(agency);
            }
            else
            {
                if (agencyVRO == null && !string.IsNullOrEmpty(user.RelatedVRoCode()))
                {
                    agencyVRO = new GovAgency(user.RelatedVRoCode(), UserAgencyName, 3, true, (int)Enums.AgencyType.VRO, user.Mobile());
                    await _commandService.CreateAsync(agencyVRO);
                }
                agency.GovAgencyUpdated(agencyCode, string.IsNullOrEmpty(user.RelatedVRoCode()) ? null : user.RelatedVRoCode(), user.UserAgencyName(), int.Parse(user.UserCategoryID()), user.Mobile());
                _commandService.UpdateAsync(agency);
            }
        }

        public async Task<List<CommitteeModel>> FindCommitteesNotAssignedToBranch(string agencyCode, int branchId)
        {
            return await _iDMQueries.FindCommitteesNotAssignedToBranch(agencyCode, branchId);
        }

        public async Task<List<CommitteeModel>> FindCommitteeNotAssignedToUser(string agencyCode, int userId)
        {
            return await _iDMQueries.FindCommitteeNotAssignedToUser(agencyCode, userId);
        }

        public async Task<List<BranchModel>> FindBranchesNotAssignedToUser(string agencyCode, int userId)
        {
            return await _iDMQueries.FindBranchesNotAssignedToUser(agencyCode, userId);
        }

        public async Task<List<CommitteeUserAssignModel>> FindCommitteeAssignedToUser(string agencyCode, int userId)
        {
            List<CommitteeUserAssignModel> CommitteesAssignedToUserList = await _iDMQueries.FindCommitteeAssignedToUser(agencyCode, userId);
            List<IDMRolesModel> roles = GetIDMRoles();
            foreach (var item in CommitteesAssignedToUserList)
            {
                IDMRolesModel iDMRolesModel = roles.FirstOrDefault(r => r.Name == item.RoleName);
                if (iDMRolesModel != null)
                {
                    item.RoleId = iDMRolesModel.Id;
                    item.RoleNameAr = iDMRolesModel.NormalizedName;
                }
            }
            return CommitteesAssignedToUserList;
        }

        public async Task<List<BranchUserAssignModel>> FindBranchesAssignedToUser(string agencyCode, int userId)
        {
            List<BranchUserAssignModel> branchesAssignedToUserList = await _iDMQueries.FindBranchesAssignedToUser(agencyCode, userId);
            List<IDMRolesModel> roles = GetIDMRoles();
            foreach (var item in branchesAssignedToUserList)
            {
                IDMRolesModel iDMRolesModel = roles.FirstOrDefault(r => r.Name == item.RoleName);
                if (iDMRolesModel != null)
                {
                    item.RoleId = iDMRolesModel.Id;
                    item.RoleNameAr = iDMRolesModel.NormalizedName;
                }
            }
            return branchesAssignedToUserList;
        }

        public async Task<QueryResult<UserCommitteeBranchesModel>> GetUserCommitteeBranchesModel(string agencyCode, BranchCommitteeUserSearchCriteriaModel searchCriteria)
        {
            int userId = Util.Decrypt(searchCriteria.UserIdString);
            List<CommitteeUserAssignModel> committeeUserAssignModelList = await FindCommitteeAssignedToUser(agencyCode, userId);
            List<BranchUserAssignModel> branchesAssignedToUserList = await FindBranchesAssignedToUser(agencyCode, userId);
            List<UserCommitteeBranchesModel> viewModelList = new List<UserCommitteeBranchesModel>();
            foreach (var item in committeeUserAssignModelList)
            {
                viewModelList.Add(new UserCommitteeBranchesModel() { CommitteeUserId = item.CommitteeUserId, CommitteeId = item.CommitteeId, CommitteeName = item.CommitteeName, RoleId = item.RoleId, RoleName = item.RoleName, RoleNameAr = item.RoleNameAr });
            }
            foreach (var item in branchesAssignedToUserList)
            {
                viewModelList.Add(new UserCommitteeBranchesModel() { BranchUserId = item.BranchUserId, BranchId = item.BranchId, BranchName = item.BranchName, RoleId = item.RoleId, RoleName = item.RoleName, RoleNameAr = item.RoleNameAr, EstimatedValueFrom = item.EstimatedValueFrom, EstimatedValueTo = item.EstimatedValueTo });
            }
            int count = viewModelList.Count;
            viewModelList = viewModelList.Skip((searchCriteria.PageNumber - 1) * searchCriteria.PageSize).Take(searchCriteria.PageSize).ToList();
            QueryResult<UserCommitteeBranchesModel> viewModelListQueryResult =
                            new QueryResult<UserCommitteeBranchesModel>(viewModelList.AsEnumerable(), count, searchCriteria.PageNumber, searchCriteria.PageSize);
            return viewModelListQueryResult;
        }

        #endregion

        public async Task<UserProfile> GetUserProfileByEmployeeId(string userName, string agencyCode, Enums.UserRole userType)
        {
            ManageUsersAssignationModel result = await GetMonafastatEmployeeDetailById(agencyCode, userName, userType.ToString());
            if (result != null)
                return new UserProfile(result.userId, result.nationalId, result.fullName, result.mobileNumber, result.email, await _notificationAppService.GetDefaultSettingByUserType(userType));
            return null;
        }
        public async Task<List<EmployeeIntegrationModel>> GetEmployeeDetailsByRoleName(string roleName)
        {
            return await _idmProxy.GetEmployeeDetailsByRoleName(roleName);
        }
        public async Task<QueryResult<AgencyExceptedModel>> GetAgencyExceptedModel(BlockSearchCriteriaModel blockSearchCriteriaModel)
        {
            return await _iDMQueries.GetAgencyExceptedModel(blockSearchCriteriaModel);
        }

        public async Task<AgencyExceptedModel> GetAgencyExceptedById(string agencyId)
        {

            return await _iDMQueries.GetAgencyExceptedById(agencyId);
        }
        public async Task UpdateAgencyStatus(string agencyId, bool isExcepted)
        {
            GovAgency govAgency = await _context.GovAgencies.FindAsync(agencyId);
            govAgency.SetExcepted(isExcepted);
            _context.GovAgencies.Update(govAgency);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAgencyIsOld(string agencyId, bool isOld)
        {
            GovAgency govAgency = await _context.GovAgencies.FindAsync(agencyId);
            govAgency.SetIsOld(isOld);
            _context.GovAgencies.Update(govAgency);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> SaveAgency(AgencyExceptedModel agencyExceptedModel)
        {
            string sb = "";
            if (agencyExceptedModel.PurchaseMethods.Contains((int)Enums.TenderType.CurrentTender) && agencyExceptedModel.PurchaseMethods.Contains((int)Enums.TenderType.NewTender))
                throw new BusinessRuleException("عفوا لا يمكن اختيار منافسه عامه حالى وجديد معا");
            if (agencyExceptedModel.PurchaseMethods.Contains((int)Enums.TenderType.NewDirectPurchase) && agencyExceptedModel.PurchaseMethods.Contains((int)Enums.TenderType.CurrentDirectPurchase))
                throw new BusinessRuleException("عفوا لا يمكن اختيار منافسه شراء مباشر حالى وجديد معا");
            GovAgency govAgency = await _context.GovAgencies.FindAsync(agencyExceptedModel.AgencyIdString);
            if (agencyExceptedModel.PurchaseMethods.Count() != 0)
            {
                foreach (int pr in agencyExceptedModel.PurchaseMethods)
                {
                    sb = string.Concat(pr.ToString() + ",", sb);
                }
                sb = sb.Substring(0, sb.Length - 1);
            }

            govAgency.SetPurchaseMethod(sb.ToString());
            govAgency.SetIsOld(agencyExceptedModel.IsOldSystem);
            _context.GovAgencies.Update(govAgency);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<List<LookupModel>> GetAllAgenciesList()
        {
            return _iDMQueries.GetAllAgenciesList();
        }
    }
}
