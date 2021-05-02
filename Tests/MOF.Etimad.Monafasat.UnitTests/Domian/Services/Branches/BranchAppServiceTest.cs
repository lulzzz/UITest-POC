using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Branches
{
    public class BranchAppServiceTest
    {
        private readonly Mock<IBranchServiceCommand> _moqBranchCommands;
        private readonly Mock<IBranchServiceQueries> _moqBranchServiceQueries;
        private readonly AppDbContext _moqAppDbContext;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<IIDMQueries> _IIDMQueries;
        private readonly Mock<IIDMAppService> _moqIDMApp;
        private readonly Mock<INotificationAppService> _moqnotificationappService;
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly BranchAppService _sutBranchAppService;
        private readonly Mock<IBranchServiceDomain> _moqBranchServiceDomain;

        public BranchAppServiceTest()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestingDB")
            .Options;
            _httpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _httpContext.Object);
            _moqBranchCommands = new Mock<IBranchServiceCommand>();
            _moqBranchServiceQueries = new Mock<IBranchServiceQueries>();
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _moqIDMApp = new Mock<IIDMAppService>();
            _moqnotificationappService = new Mock<INotificationAppService>();
            _IIDMQueries = new Mock<IIDMQueries>();
            _moqBranchServiceDomain = new Mock<IBranchServiceDomain>();
            _sutBranchAppService = new BranchAppService(_moqBranchServiceQueries.Object, _moqBranchCommands.Object, _moqBranchServiceDomain.Object, _moqCommandRepository.Object, _moqIDMApp.Object, _moqnotificationappService.Object);
        }

        [Fact]
        public async Task ShouldCreateBranchSuccess()
        {
            BranchModel branchModel = new BranchDefaults().GetBranchModel();
            _moqBranchServiceQueries.Setup(m => m.FindById(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<BranchModel>(new BranchDefaults().GetBranchModel());
            });
            var result = await _sutBranchAppService.AddBranch(branchModel);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task ShouldUpdateBranchSuccess()
        {
            BranchModel branchModel = new BranchDefaults().GetBranchModelForUpdate();
            _moqBranchServiceQueries.Setup(m => m.FindBranchById(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Branch>(new BranchDefaults().ReturnBranchDefaults());
            });

            _moqBranchServiceQueries.Setup(m => m.FindById(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<BranchModel>(new BranchDefaults().GetBranchModelForUpdate());
            });

            var result = await _sutBranchAppService.AddBranch(branchModel);
            Assert.NotNull(result);
            Assert.NotNull(branchModel.BranchIdString);
        }

        [Fact]
        public async Task ShouldAssignCommittessToBranchSuccess()
        {
            BranchCommitteeModel branchCommitteeModel = new BranchDefaults().GetBranchCommitteeModel();
            await _sutBranchAppService.AddCommittee(branchCommitteeModel);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldRemoveAssignCommittessToBranchSuccess()
        {
            int committeeId = 1; int branchId = 1;
            _moqBranchServiceQueries.Setup(m => m.GetAssignedCommitteeByIdAndBranch(It.IsAny<int>(), It.IsAny<int>()))
          .Returns(() =>
          {
              return Task.FromResult<BranchCommittee>(new BranchCommitteeDefaults().ReturnBranchCommitteeDefaults());
          });
            await _sutBranchAppService.RemoveAssignedCommittee(committeeId, branchId);
            _moqCommandRepository.Verify(m => m.Update(It.IsAny<BranchCommittee>()), Times.Once);
        }

        [Fact]
        public async Task ShouldRemoveAssignedUserForMangSuccess()
        {
            int roleId = 1; int branchId = 1; int userId = 1;
            _moqBranchServiceQueries.Setup(m => m.GetAssignedUserForMangById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
          .Returns(() =>
          {
              return Task.FromResult<BranchUser>(new BranchUserDefaults().ReturnBranchUserDefaults());
          });
            await _sutBranchAppService.RemoveAssignedUserForMang(userId, branchId, roleId);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }


        [Fact]
        public async Task ShouldAssignUsersToBranchSuccess()
        {
            string agencyCode = "022001000000";
            BranchUserModel branchUserModel = new BranchDefaults().GetBranchUserModel();
            _moqIDMApp.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(new BranchUserDefaults().ReturnUserProfileDefaults());
            });
            _moqIDMApp.Setup(m => m.GetIDMRoles())
              .Returns(() =>
              {
                  return new List<IDMRolesModel>(new BranchUserDefaults().GetIDMRolesModel());
              });
            _moqIDMApp.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
           .Returns(() =>
               {
                   return Task.FromResult<UserProfile>(new BranchUserDefaults().ReturnUserProfileDefaults());
               });

            _moqnotificationappService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });

            await _sutBranchAppService.AddUserAsyn(branchUserModel, agencyCode);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldRemoveAssignUsersToBranchSuccess()
        {
            int userId = 1; int branchId = 1; int roleName = 1;
            _moqBranchServiceQueries.Setup(m => m.GetAssignedUserForMangById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
          .Returns(() =>
          {
              return Task.FromResult<BranchUser>(new BranchUserDefaults().ReturnBranchUserDefaults());
          });
            await _sutBranchAppService.RemoveAssignedUser(userId, branchId, roleName);
            _moqCommandRepository.Verify(m => m.Update(It.IsAny<BranchUser>()), Times.Once);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public async Task ShouldDeActiveBranchSuccess(int branchId)
        {
            _moqBranchServiceQueries.Setup(x => x.FindBranchById(It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<Branch>(new BranchDefaults().ReturnBranchDefaults());
                     });
            var result = await _sutBranchAppService.DeActiveAsync(branchId);
            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData(1, "00220033")]
        public async Task ShouldFindUserDefultCommitteeSuccess(int userId, string agencyCode)
        {
            _moqBranchServiceQueries.Setup(x => x.GetUserDefultCommittees(It.IsAny<int>(), It.IsAny<string>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<int>(1);
                     });
            var result = await _sutBranchAppService.GetUserDefultCommittee(userId, agencyCode);
            Assert.IsType<int>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task ShouldFindUserRolesByBranchSuccess(int userId, int branchId)
        {
            List<string> rolesFromBranch = new List<string>();
            List<string> rolesFromCommittees = new List<string>();

            _moqBranchServiceQueries.Setup(x => x.GetUserRolesByBranch(It.IsAny<int>(), It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<List<string>>(rolesFromBranch);
                     });

            _moqBranchServiceQueries.Setup(x => x.GetUserRolesByBranchFromCommittees(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<string>>(rolesFromCommittees);
                });
            var result = await _sutBranchAppService.GetUserRolesByBranch(userId, branchId);
            Assert.IsType<List<string>>(result);
        }


        [Theory]
        [InlineData(1, 1)]
        public async Task ShouldFindUserRoleByCommitteeSuccess(int userId, int committeeId)
        {
            List<string> rolesFromCommittees = new List<string>();


            _moqBranchServiceQueries.Setup(x => x.GetUserRoleByCommittee(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<string>>(rolesFromCommittees);
                });
            var result = await _sutBranchAppService.GetUserRoleByCommittee(userId, committeeId);
            Assert.IsType<List<string>>(result);
        }

        [Theory]
        [InlineData(1, "00220033")]
        public async Task ShouldFindUserDefultBranchSuccess(int userId, string agencyCode)
        {
            _moqBranchServiceQueries.Setup(x => x.GetUserDefultBranch(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<int>(1);
                });
            var result = await _sutBranchAppService.GetUserDefultBranch(userId, agencyCode);
            Assert.IsType<int>(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public async Task ShouldGetUserRolesByIdSuccess(int committeeTypeId)
        {
            UsersSearchCriteriaModel usersSearchCriteriaModel = new UsersSearchCriteriaModel() { PageSize = 100, PageNumber = 1 };
            IEnumerable<ManageUsersAssignationModel> manageUsersAssignationModel = new BranchDefaults().GetUsersAssignation();
            _moqIDMApp.Setup(m => m.GetMonafasatUsers(It.IsAny<UsersSearchCriteriaModel>()))
              .Returns(() =>
              {
                  return Task.FromResult<QueryResult<ManageUsersAssignationModel>>(manageUsersAssignationModel.ToQueryResult().Result);

              });

            var result = await _sutBranchAppService.GetUserRolesById(new BranchDefaults()._nationalId, new BranchDefaults()._agencyCode, committeeTypeId, usersSearchCriteriaModel);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(9)]
        public async Task ShouldGetUserRolesByIdReturnNull(int committeeTypeId)
        {
            UsersSearchCriteriaModel usersSearchCriteriaModel = new UsersSearchCriteriaModel() { PageSize = 100, PageNumber = 1 };
            IEnumerable<ManageUsersAssignationModel> manageUsersAssignationModel = new BranchDefaults().GetUsersAssignation();
            _moqIDMApp.Setup(m => m.GetMonafasatUsers(It.IsAny<UsersSearchCriteriaModel>()))
              .Returns(() =>
              {
                  return Task.FromResult<QueryResult<ManageUsersAssignationModel>>(manageUsersAssignationModel.ToQueryResult().Result);

              });

            var result = await _sutBranchAppService.GetUserRolesById(new BranchDefaults()._nationalId, new BranchDefaults()._agencyCode, committeeTypeId, usersSearchCriteriaModel);
            Assert.Null(result);
        }




        [Fact]
        public async Task ShouldGetAllUserRolesSuccess()
        { 
            _moqBranchServiceQueries.Setup(x => x.GetAllUserRolesFromBranches(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() =>
                {
            return Task.FromResult<List<UserRolesModel>>(new List<UserRolesModel> ());
        });

            _moqBranchServiceQueries.Setup(x => x.GetAllUserRolesFromCommittees(It.IsAny<int>(), It.IsAny<string>()))
             .Returns(() =>
             {
                 return Task.FromResult<List<UserRolesModel>>(new List<UserRolesModel>());
             });

            var result = await _sutBranchAppService.GetAllUserRoles(1, new BranchDefaults()._agencyCode);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShoulAddCommitteeThrowException()
        {
            BranchCommitteeModel branchCommitteeModel = new BranchCommitteeModel();
            branchCommitteeModel.CommitteeIdsString = "";
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutBranchAppService.AddCommittee(branchCommitteeModel));
        }


        
        [Fact]
        public async Task ShouldGetBranchUsersForAwardingApprovalSuccess()
        {
            _moqBranchServiceQueries.Setup(x => x.GetBranchUsersForAwardingApproval(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<BranchUser>(new BranchUser());
                });

            var result = await _sutBranchAppService.GetBranchUsersForAwardingApproval(1, 1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetVROBranchsByVROOfficeCodeSuccess()
        {
            string _vroOfficeCode = "vroOfficeCode";
            _moqBranchServiceQueries.Setup(x => x.GetVROBranchsByVROOfficeCode(It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<int>>(new List<int>());
                });
            var result = await _sutBranchAppService.GetVROBranchsByVROOfficeCode(_vroOfficeCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldAddUsersToBranchSuccess()
        {
            string agencyCode = "022001000000";
            BranchUserModel branchUserModel = new BranchDefaults().GetBranchUserModel();
            _moqIDMApp.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(null);
            });
            _moqIDMApp.Setup(m => m.GetIDMRoles())
              .Returns(() =>
              {
                  return new List<IDMRolesModel>(new BranchUserDefaults().GetIDMRolesModel());
              });
            _moqIDMApp.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
           .Returns(() =>
           {
               return Task.FromResult<UserProfile>(new BranchUserDefaults().ReturnUserProfileDefaults());
           });

            _moqnotificationappService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });

            await _sutBranchAppService.AddUserAsyn(branchUserModel, agencyCode);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }



        [Fact]
        public async Task ShouldAddUsersToBranchThrowException()
        {
            string agencyCode = "022001000000";
            BranchUserModel branchUserModel = new BranchDefaults().GetBranchUserModel();
            _moqIDMApp.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(null);
            });
            _moqIDMApp.Setup(m => m.GetIDMRoles())
              .Returns(() =>
              {
                  return new List<IDMRolesModel>(new BranchUserDefaults().GetIDMRolesModel());
              });
            _moqIDMApp.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
           .Returns(() =>
           {
               return Task.FromResult<UserProfile>(null);
           });

            _moqnotificationappService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
            });
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sutBranchAppService.AddUserAsyn(branchUserModel, agencyCode));
        }
    }
}
