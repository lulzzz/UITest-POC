using AutoMapper;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.AgencyDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.CommitteeDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.IDMDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.IDM
{
    public class IDMAppServiceTest
    {
        #region Mock
        private readonly Mock<IIDMQueries> _moqIDMQueries;
        private readonly Mock<ICommandService> _moqCommandService;
        private readonly Mock<IIDMProxy> _moqIdmProxy;
        private readonly Mock<IMapper> _moqMapper;
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private readonly Mock<INotificationAppService> _moqNotificationAppService;
        private readonly Mock<INotificationQueries> _moqNotificationQueries;
        private readonly IDMAppService _sut;

        public IDMAppServiceTest()
        {
            _moqIDMQueries = new Mock<IIDMQueries>();
            _moqCommandService = new Mock<ICommandService>();
            _moqIdmProxy = new Mock<IIDMProxy>();
            _moqMapper = new Mock<IMapper>();
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _moqNotificationAppService = new Mock<INotificationAppService>();
            _moqNotificationQueries = new Mock<INotificationQueries>();
            _sut = new IDMAppService(_moqIdmProxy.Object, _moqIDMQueries.Object, _moqMapper.Object, _moqCommandService.Object, _moqHttpContextAccessor.Object,
                _moqNotificationQueries.Object, _moqNotificationAppService.Object, InitialData.context);
        }
        #endregion Mock

        [Fact]
        public async Task Should_FindGovAgencyByIdAsync()
        {
            _moqIDMQueries.Setup(i => i.FindGovAgencyByIdAsync(It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<GovAgency>(new AgencyDefaults().GetGovAgency());
                });

            var result = await _sut.FindGovAgencyByIdAsync(It.IsAny<string>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindUserProfileByUserNameAsync()
        {
            _moqIDMQueries.Setup(i => i.FindUserProfileByUserNameAsync(It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<UserProfile>(new AgencyDefaults().GetUserProfileData());
                });

            var result = await _sut.FindUserProfileByUserNameAsync(It.IsAny<string>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_SetUserDefaultRole()
        {
            MoqUser();
            _moqIDMQueries.Setup(i => i.FindUserProfileByIdWithoutIncludesAsync(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<UserProfile>(new AgencyDefaults().GetUserProfileData());
                });

            await _sut.SetUserDefaultRole(RoleNames.MonafasatAdmin);

            _moqNotificationAppService.Verify(n => n.UpdateUser(It.IsAny<UserProfile>()), Times.Once);
        }

        [Fact]
        public async Task Should_FindUsersByRoleNameAndAgencyCodeForCommitteeAssignAsync()
        {
            _moqIDMQueries.Setup(i => i.FindUsersByRoleNameAndAgencyCodeAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<UserLookupModel>>(new AgencyDefaults().GetUserLookupModels());
                });

            var result = await _sut.FindUsersByRoleNameAndAgencyCodeForCommitteeAssignAsync(It.IsAny<string>(), It.IsAny<string>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllDataEntryAndAuditorUsers()
        {
            _moqIDMQueries.Setup(i => i.GetAllDataEntryAndAuditorUsers(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<UserLookupModel>>(new AgencyDefaults().GetUserLookupModels());
                });

            var result = await _sut.GetAllDataEntryAndAuditorUsers(It.IsAny<string>(), It.IsAny<string>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllSupplierOnTender()
        {
            List<string> crs = new List<string>() { "1299659801", "1299659802" };
            _moqIDMQueries.Setup(i => i.GetAllSupplierOnTender(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<string>>(crs);
                });

            var result = await _sut.GetAllSupplierOnTender(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllSupplierOnQualification()
        {
            List<string> crs = new List<string>() { "1299659801", "1299659802" };
            _moqIDMQueries.Setup(i => i.GetAllSupplierOnQualification(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<string>>(crs);
                });

            var result = await _sut.GetAllSupplierOnQualification(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_QualificationToSendInvitation()
        {
            List<string> crs = new List<string>() { "1299659801", "1299659802" };
            _moqIDMQueries.Setup(i => i.GetAllSupplierQualificationToSendInvitation(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<string>>(crs);
                });

            var result = await _sut.QualificationToSendInvitation(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTechnicalCommitteeMembersOnTender()
        {
            List<int> userIds = new List<int>() { 1, 2 };
            _moqIDMQueries.Setup(i => i.GetTechnicalCommitteeMembersOnTender(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<int>>(userIds);
                });

            var result = await _sut.GetTechnicalCommitteeMembersOnTender(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindBranchesNotAssignedByUserAndRole()
        {
            _moqIDMQueries.Setup(i => i.FindBranchesNotAssignedByUserAndRole(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<BranchModel>>(new BranchDefaults().GetBranchModels());
                });

            var result = await _sut.FindBranchesNotAssignedByUserAndRole(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindCommitteeNotAssignedToUser()
        {
            _moqIDMQueries.Setup(i => i.FindCommitteeNotAssignedToUser(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<CommitteeModel>>(new CommitteeDefaults().ReturnCommitteeModels());
                });

            var result = await _sut.FindCommitteeNotAssignedToUser(It.IsAny<string>(), It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindBranchesNotAssignedToUser()
        {
            _moqIDMQueries.Setup(i => i.FindBranchesNotAssignedToUser(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<BranchModel>>(new BranchDefaults().GetBranchModels());
                });

            var result = await _sut.FindBranchesNotAssignedToUser(It.IsAny<string>(), It.IsAny<int>());

            Assert.NotNull(result);
        }

        private void MoqUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var usernum = new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(usernum);
            idintity.AddClaim(claim);

            context.SetupGet(x => x.User.Identity).Returns(() => { return idintity; });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
        }

        [Theory]
        [InlineData("022001000000", "1", 1)]
        public async Task ShouldGetMonafastatEmployeeDetailByIdSuccess(string agencyCode, string nationalId, int agencyType)
        {
            _moqIdmProxy.Setup(i => i.GetMonafasatUsersByAgencyType(It.IsAny<UsersSearchCriteriaModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<EmployeeIntegrationModel>>(new BranchDefaults().GetEmployeeIntegrationModel());
                });
            _moqMapper.Setup(x => x.Map<ManageUsersAssignationModel>(It.IsAny<EmployeeIntegrationModel>())).Returns(() =>
            {
                return new BranchDefaults().GetUsersAssignation()[0];
            });
            var result = await _sut.GetMonafastatEmployeeDetailById(agencyCode, nationalId, "", agencyType);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("022001000000", 1)]
        public async Task ShouldFindCommitteeAssignedToUserSuccess(string agencyCode, int userId)
        {
            _moqIDMQueries.Setup(i => i.FindCommitteeAssignedToUser(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<CommitteeUserAssignModel>>(new CommitteeDefaults().GetAllCommitteeUserAssignModel());
                });

            var result = await _sut.FindCommitteeAssignedToUser(agencyCode, userId);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData("022001000000", 1)]
        public async Task ShouldFindBranchesAssignedToUserSuccess(string agencyCode, int userId)
        {
            _moqIDMQueries.Setup(i => i.FindBranchesAssignedToUser(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<BranchUserAssignModel>>(new BranchDefaults().GetAllBranchesUserAssignModel());
                });

            var result = await _sut.FindBranchesAssignedToUser(agencyCode, userId);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData("022001000000")]
        public async Task ShouldGetUserCommitteeBranchesModelSuccess(string agencyCode)
        {
            BranchCommitteeUserSearchCriteriaModel searchCriteria = new BranchCommitteeUserSearchCriteriaModel();
            _moqIDMQueries.Setup(i => i.FindCommitteeAssignedToUser(It.IsAny<string>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<List<CommitteeUserAssignModel>>(new CommitteeDefaults().GetAllCommitteeUserAssignModel());
              });


            _moqIDMQueries.Setup(i => i.FindBranchesAssignedToUser(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<BranchUserAssignModel>>(new BranchDefaults().GetAllBranchesUserAssignModel());
                });

            var result = await _sut.GetUserCommitteeBranchesModel(agencyCode, searchCriteria);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1, "NewMonafasat_TechnicalCommitteeUser")]
        [InlineData(2, "NewMonafasat_OffersOpeningManager")]
        [InlineData(3, "NewMonafasat_OffersCheckSecretary")]
        [InlineData(5, "NewMonafasat_ManagerDirtectPurshasingCommittee")]
        [InlineData(6, "NewMonafasat_PreQualificationCommitteeManager")]
        [InlineData(7, "NewMonafasat_OffersOpeningAndCheckSecretary")]
        public async Task ShouldGetUsersbyCommitteeTypeIdSuccess(int committeeTypeId, string rolename)
        {
            CommitteeUserSearchCriteriaModel searchCriteria = new CommitteeUserSearchCriteriaModel()
            {
                CommitteeTypeId = committeeTypeId,
                AgencyId = "1",
                RoleName = rolename,
                RoleNames=new List<string>() { "NewMonafasat_TechnicalCommitteeUser", "NewMonafasat_OffersOpeningManager" }
             
            };
            _moqIdmProxy.Setup(i => i.GetMonafasatUsersByAgencyType(It.IsAny<UsersSearchCriteriaModel>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<QueryResult<EmployeeIntegrationModel>>(new BranchDefaults().GetEmployeeIntegrationModel());
                 });

            _moqIDMQueries.Setup(i => i.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<int>>(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
                });

            var result = await _sut.GetUsersbyCommitteeTypeId(searchCriteria);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(1, "NewMonafasat_TechnicalCommitteeUser")]
        [InlineData(2, "NewMonafasat_OffersOpeningManager")]
        [InlineData(3, "NewMonafasat_OffersCheckSecretary")]
        [InlineData(7, "NewMonafasat_OffersOpeningAndCheckSecretary")]
        public async Task ShouldFindUsersbyCommitteeTypeIdSuccess(int committeeTypeId, string rolename)
        {
            string agencyCode = "022001000000";
            _moqIdmProxy.Setup(i => i.GetMonafasatUsersByAgencyType(It.IsAny<UsersSearchCriteriaModel>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<QueryResult<EmployeeIntegrationModel>>(new BranchDefaults().GetEmployeeIntegrationModel());
                 });

            _moqIDMQueries.Setup(i => i.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<int>>(new List<int>() { 1, 2, 3, 4, 5, 6, 7 });
                });

            var result = await _sut.GetUsersbyCommitteeTypeId(agencyCode, 1, committeeTypeId, "", rolename, 1);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("NewMonafasat_OffersCheckSecretary", "022001000000", 1, AgencyType.None)]
        public async Task ShouldFindUsersByRoleNameAndAgencyCodeNotAssignedToCommitteeAssignAsyncSuccess(string roleName, string agencyCode, int committeeId, AgencyType agencyType)
        {
            _moqIdmProxy.Setup(i => i.GetMonafasatUsersByAgencyType(It.IsAny<UsersSearchCriteriaModel>()))
                  .Returns(() =>
                  {
                      return Task.FromResult<QueryResult<EmployeeIntegrationModel>>(new BranchDefaults().GetEmployeeIntegrationModel());
                  });
            _moqIDMQueries.Setup(i => i.FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<List<int>>(new List<int>() { 1 });
            });
            var result = await _sut.FindUsersByRoleNameAndAgencyCodeNotAssignedToCommitteeAssignAsync(roleName, agencyCode, committeeId, agencyType);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldSyncUserInfoReturnNull()
        {
            List<string> userRoles = new List<string>();
            List<string> vroUserRoles = new List<string>();
            var result = await _sut.SyncUserInfo(1, "username", "fullname", "mobile", "email", "supplierNumber", "supplierName", new BranchDefaults()._agencyCode, userRoles, "relatedAgencyCode", vroUserRoles);
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldSyncUserInfoUpdatedSuccess()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);

            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            List<string> userRoles = new List<string>() { "NewMonafasat_TechnicalCommitteeUser" };
            List<string> vroUserRoles = new List<string>();

            _moqIDMQueries.Setup(i => i.FindUserProfileByIdAsync(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(new BranchUserDefaults().ReturnUserProfileDefaults());
            });

            _moqNotificationAppService.Setup(m => m.GetDefaultSettingByUserTypes(It.IsAny<List<int>>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });
            _moqNotificationAppService.Setup(m => m.GetDefaultSettingByCr())
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });

            _moqNotificationQueries.Setup(m => m.FindUserNotificationSettingbyUserProfileId(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });

            _moqIDMQueries.Setup(i => i.FindSupplierObjectByUserIdAsync(It.IsAny<string>()))
        .Returns(() =>
        {
            return Task.FromResult<Supplier>(null);
        });

            var result = await _sut.SyncUserInfo(1, "username", "fullname", "mobile", "email", "supplierNumber", "supplierName", new BranchDefaults()._agencyCode, userRoles, "relatedAgencyCode", vroUserRoles);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldSyncUserInfoSuccess()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);

            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            List<string> userRoles = new List<string>() { "NewMonafasat_TechnicalCommitteeUser" };
            List<string> vroUserRoles = new List<string>();

            _moqIDMQueries.Setup(i => i.FindUserProfileByIdAsync(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(null);
            });

            _moqNotificationAppService.Setup(m => m.GetDefaultSettingByUserTypes(It.IsAny<List<int>>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });

            var result = await _sut.SyncUserInfo(1, "username", "fullname", "mobile", "email", "supplierNumber", "supplierName", new BranchDefaults()._agencyCode, userRoles, "relatedAgencyCode", vroUserRoles);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldSyncUserInfoUpdatedWithContainsSupplierDataSuccess()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);

            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            List<string> userRoles = new List<string>() { "NewMonafasat_UnitManager" };
            List<string> vroUserRoles = new List<string>();

            _moqIDMQueries.Setup(i => i.FindUserProfileByIdAsync(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(new BranchUserDefaults().ReturnUserProfileDefaults());
            });

            _moqNotificationAppService.Setup(m => m.GetDefaultSettingByUserTypes(It.IsAny<List<int>>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });
            _moqNotificationAppService.Setup(m => m.GetDefaultSettingByCr())
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });

            _moqNotificationQueries.Setup(m => m.FindUserNotificationSettingbyUserProfileId(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });

            _moqIDMQueries.Setup(i => i.FindSupplierObjectByUserIdAsync(It.IsAny<string>()))
        .Returns(() =>
        {
            return Task.FromResult<Supplier>(new IDMDefaults().GetSupplierData());
        });

        _moqIDMQueries.Setup(i => i.FindSupplierUserProfileByUserProfileIdAndCrAsync(It.IsAny<int>(), It.IsAny<string>()))
        .Returns(() =>
        {
            return Task.FromResult<SupplierUserProfile>(null);
        });

            var result = await _sut.SyncUserInfo(1, "username", "fullname", "mobile", "email", "supplierNumber", "supplierName", new BranchDefaults()._agencyCode, userRoles, "relatedAgencyCode", vroUserRoles);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetUserProfileByEmployeeIdReturnSuccess()
        {
            _moqIdmProxy.Setup(i => i.GetMonafasatUsersByAgencyType(It.IsAny<UsersSearchCriteriaModel>()))
                          .Returns(() =>
                          {
                              return Task.FromResult<QueryResult<EmployeeIntegrationModel>>(new BranchDefaults().GetEmployeeIntegrationModel());
                          });
            _moqMapper.Setup(x => x.Map<ManageUsersAssignationModel>(It.IsAny<EmployeeIntegrationModel>())).Returns(() =>
            {
                return new BranchDefaults().GetUsersAssignation()[0];
            });

            var result = await _sut.GetUserProfileByEmployeeId("userName", "agencyCode", Enums.UserRole.NewMonafasat_TechnicalCommitteeUser);
            Assert.NotNull(result);
        }


        

        [Fact]
        public async Task ShouldGetContactOfficersByCRSuccess()
        {
            _moqIdmProxy.Setup(i => i.GetContactOfficersByCR(It.IsAny<List<string>>()))
                          .Returns(() =>
                          {
                              return Task.FromResult<QueryResult<ContactOfficerModel>>(new BranchDefaults().GetContactOfficerModel());
                          });
 
            var result = await _sut.GetContactOfficersByCR(new List<string>() { "cr"});
            Assert.NotNull(result);
        }
    }
}
