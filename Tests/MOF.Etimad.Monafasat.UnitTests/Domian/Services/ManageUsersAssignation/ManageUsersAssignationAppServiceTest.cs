using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.CommitteeDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.ManageUsersAssignation
{
    public class ManageUsersAssignationAppServiceTest
    {
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<IIDMQueries> _IIDMQueries;
        private readonly Mock<IIDMAppService> _moqIDMApp;
        private readonly Mock<INotificationAppService> _moqnotificationappService;
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly Mock<IBranchServiceDomain> _moqBranchServiceDomain;
        private readonly Mock<ICommitteeDomainService> _moqCommitteeServiceDomain;
        private readonly AppDbContext _moqAppDbContext;
        private readonly ManageUsersAssignationAppService _sutManageUsersAssignationAppService;

        public ManageUsersAssignationAppServiceTest()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestingDB")
            .Options;
            _httpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _httpContext.Object);
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _moqIDMApp = new Mock<IIDMAppService>();
            _moqnotificationappService = new Mock<INotificationAppService>();
            _IIDMQueries = new Mock<IIDMQueries>();
            _moqBranchServiceDomain = new Mock<IBranchServiceDomain>();
            _moqCommitteeServiceDomain = new Mock<ICommitteeDomainService>();
            _sutManageUsersAssignationAppService = new ManageUsersAssignationAppService(
                _moqBranchServiceDomain.Object,
                _moqCommitteeServiceDomain.Object,
                _moqCommandRepository.Object,
                _moqIDMApp.Object,
                _IIDMQueries.Object,
                _moqnotificationappService.Object,
                _httpContext.Object);
        }

        [Fact]
        public async Task ShouldUpdateUserDataWhenToAssignBranchSuccess()
        {
            string agencyCode = "022001000000";
            BranchUserAssignModel branchUserModel = new BranchDefaults().GetBranchUserAssignModel();
            _moqIDMApp.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(new BranchUserDefaults().ReturnUserProfileDefaults());
            });

            _moqIDMApp.Setup(m => m.GetMonafastatEmployeeDetailById(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<ManageUsersAssignationModel>(new BranchUserDefaults().ReturnManageUsersAssignationDataModel());
           });

            _moqnotificationappService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });

            await _sutManageUsersAssignationAppService.AddUserToBranchAsyn(branchUserModel, agencyCode);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldAddUsersToBranchThrowException_WhenModelIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sutManageUsersAssignationAppService.AddUserToBranchAsyn(null, "022001000000"));
        }


        [Fact]
        public async Task ShouldAddUsersToBranchThrowException()
        {
            string agencyCode = "022001000000";
            BranchUserAssignModel branchUserModel = new BranchDefaults().GetBranchUserAssignModel();
            _moqIDMApp.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(null);
            });

            _moqIDMApp.Setup(m => m.GetMonafastatEmployeeDetailById(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<ManageUsersAssignationModel>(new BranchUserDefaults().ReturnManageUsersAssignationDataModel());
           });

            _moqnotificationappService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _sutManageUsersAssignationAppService.AddUserToBranchAsyn(branchUserModel, agencyCode));
        }


        [Fact]
        public async Task ShouldUpdateUserDataWhenToAssignCommitteThrowException()
        {
            string agencyCode = "022001000000";
            CommitteeUserAssignModel committeeUserAssignModel = new CommitteeDefaults().GetCommitteeUserAssignModel();
            _moqIDMApp.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(null);
            });

            _moqIDMApp.Setup(m => m.GetIDMRoles())
          .Returns(() =>
          {
              return new List<IDMRolesModel>(new CommitteeDefaults().GetIDMRolesModel());
          });


            _moqIDMApp.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
          .Returns(() =>
          {
              return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
          });


            _moqnotificationappService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new CommitteeDefaults().GetUserNotificationSettings());
            });

            await Assert.ThrowsAsync<NullReferenceException>(async () => await _sutManageUsersAssignationAppService.AddUserToCommitteeAsyn(committeeUserAssignModel, agencyCode));

        }


        [Fact]
        public async Task ShouldAddUsersToCommitteeThrowException_WhenModelIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sutManageUsersAssignationAppService.AddUserToCommitteeAsyn(null, "022001000000"));
        }

        [Fact]
        public async Task ShouldUpdateUserDataWhenToAssignCommitteSuccess()
        {
            string agencyCode = "022001000000";
            CommitteeUserAssignModel committeeUserAssignModel = new CommitteeDefaults().GetCommitteeUserAssignModel();
            _moqIDMApp.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
            });

            _moqIDMApp.Setup(m => m.GetIDMRoles())
          .Returns(() =>
          {
              return new List<IDMRolesModel>(new CommitteeDefaults().GetIDMRolesModel());
          });


            _moqIDMApp.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
          .Returns(() =>
          {
              return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
          });


            _moqnotificationappService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new CommitteeDefaults().GetUserNotificationSettings());
            });

            await _sutManageUsersAssignationAppService.AddUserToCommitteeAsyn(committeeUserAssignModel, agencyCode);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateUserDataWhenToAssignCommitteSuccessWithoutNotificationSetting()
        {
            string agencyCode = "022001000000";
            CommitteeUserAssignModel committeeUserAssignModel = new CommitteeDefaults().GetCommitteeUserAssignModel();
            _moqIDMApp.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaultWithoutNotificationSettings());
            });

            _moqIDMApp.Setup(m => m.GetIDMRoles())
          .Returns(() =>
          {
              return new List<IDMRolesModel>(new CommitteeDefaults().GetIDMRolesModel());
          });


            _moqIDMApp.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
          .Returns(() =>
          {
              return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
          });


            _moqnotificationappService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new CommitteeDefaults().GetUserNotificationSettings());
            });

            await _sutManageUsersAssignationAppService.AddUserToCommitteeAsyn(committeeUserAssignModel, agencyCode);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }



        [Fact]
        public async Task ShouldUpdateUserDataWhenToAssignBranchSuccessWithoutNotificationSetting()
        {
            string agencyCode = "022001000000";
            BranchUserAssignModel branchUserModel = new BranchDefaults().GetBranchUserAssignModel();
            _moqIDMApp.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(new BranchUserDefaults().ReturnUserProfileDefaultWithoutNotificationSettings());
            });

            _moqIDMApp.Setup(m => m.GetMonafastatEmployeeDetailById(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<ManageUsersAssignationModel>(new BranchUserDefaults().ReturnManageUsersAssignationDataModel());
           });

            _moqnotificationappService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });

            await _sutManageUsersAssignationAppService.AddUserToBranchAsyn(branchUserModel, agencyCode);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldUserPermissionRestSuccess()
        {
            _IIDMQueries.Setup(m => m.FindUserProfileByIdAsync(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(new BranchUserDefaults().ReturnUserProfileData());
            });

            await _sutManageUsersAssignationAppService.UserPermissionRest(100);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);

        }


        [Theory]
        [InlineData("sub", "15445")]
        [InlineData("userCategory", "10")]
        public void ShouldGetIDMRolesSuccess(string key , string value)
        {
            var claim = new Claim(key, value);
            var idintity = new GenericIdentity(IdentityConfigs.Claims.VRORole, "VRORole");
            idintity.AddClaim(claim);
            _httpContext.SetupGet(x => x.HttpContext.User.Identity).Returns(() => { return idintity; });
            _httpContext.SetupGet(x => x.HttpContext.User.Claims).Returns(() => { return idintity.Claims; });
            _moqIDMApp.Setup(m => m.GetIDMRoles())
          .Returns(() =>
          {
              return new List<IDMRolesModel>(new CommitteeDefaults().GetIDMRolesModel());
          });

            var result = _sutManageUsersAssignationAppService.GetIDMRoles();
            Assert.NotNull(result);
        }


    

    }
}
