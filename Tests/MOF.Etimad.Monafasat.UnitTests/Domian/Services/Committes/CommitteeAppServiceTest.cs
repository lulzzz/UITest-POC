using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.CommitteeDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services
{

    public class CommitteeAppServiceTest
    {
        private readonly Mock<ICommitteeDomainService> _moqCommitteeDomain;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<ICommitteeQueries> _moqCommitteeQueries;
        private readonly Mock<IIDMAppService> _iDMAppService;
        private readonly Mock<IAppDbContext> _context;
        private readonly CommitteeAppService _sut;
        private readonly Mock<INotificationAppService> _notificationAppService;
        private readonly CommitteeDefaults committeeDefaults = new CommitteeDefaults();


        public CommitteeAppServiceTest()
        {
            _moqCommitteeDomain = new Mock<ICommitteeDomainService>();
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _moqCommitteeQueries = new Mock<ICommitteeQueries>();
            _iDMAppService = new Mock<IIDMAppService>();
            _context = new Mock<IAppDbContext>();
            _notificationAppService = new Mock<INotificationAppService>();
            _sut = new CommitteeAppService(_moqCommitteeDomain.Object, _moqCommandRepository.Object, _moqCommitteeQueries.Object, _iDMAppService.Object, _notificationAppService.Object);
        }


        [Fact]
        public async Task ShouldCreateCommitteeSuccess()
        {
            Committee committee = committeeDefaults.ReturnCommitteeDefaults();
            _moqCommitteeDomain.Setup(x => x.CheckNameExist(It.IsAny<string>(), It.IsAny<string>(), 0))
                               .Returns(() =>
                               {
                                   return Task.FromResult<bool>(false);
                               });

            _moqCommandRepository.Setup(x => x.CreateAsync(It.IsAny<Committee>()))
                          .Returns((Committee obj) =>
                          {
                              return Task.FromResult(obj);
                          });
            _moqCommandRepository.Setup(x => x.SaveAsync())
                         .Returns(Task.CompletedTask);


            var result = await _sut.CreateAsync(committee);
            Assert.NotNull(result);
        }



        [Fact]
        public async Task ShouldThrowExceptionWhenCreatingModelIsNull()
        {
            _moqCommitteeDomain.Setup(x => x.CheckNameExist(It.IsAny<string>(), It.IsAny<string>(), 0))
                               .Returns(() =>
                               {
                                   return Task.FromResult<bool>(false);
                               });

            _moqCommandRepository.Setup(x => x.CreateAsync(It.IsAny<Committee>()))
                          .Returns((Committee obj) =>
                          {
                              return Task.FromResult(obj);
                          });
            _moqCommandRepository.Setup(x => x.SaveAsync())
                         .Returns(Task.CompletedTask);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.CreateAsync(null));
        }


        [Theory]
        [InlineData(1)]
        public async Task ShouldGetCommitteByIdSuccess(int committeeId)
        {
            _moqCommitteeQueries.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Committee>(committeeDefaults.GetCommitteeData());
                });
            var result = await _sut.GetById(committeeId);
            Assert.NotNull(result);
        }



        [Fact]
        public async Task ShouldUpdateCommitteeSuccess()
        {
            CommitteeModel committeeModel = committeeDefaults.ReturnCommitteeModelData();
            _moqCommitteeDomain.Setup(x => x.CheckNameExist(It.IsAny<string>(), It.IsAny<string>(), 0))
                               .Returns(() =>
                               {
                                   return Task.FromResult<bool>(false);
                               });
            _moqCommitteeQueries.Setup(x => x.GetById(It.IsAny<int>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<Committee>(committeeDefaults.GetCommitteeData());
                           });
            await _sut.UpdateAsync(committeeModel);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenUpdateCommitteAndModelIsNull()
        {
            _moqCommitteeDomain.Setup(x => x.CheckNameExist(It.IsAny<string>(), It.IsAny<string>(), 0))
                               .Returns(() =>
                               {
                                   return Task.FromResult<bool>(false);
                               });
            _moqCommitteeQueries.Setup(x => x.GetById(It.IsAny<int>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<Committee>(committeeDefaults.GetCommitteeData());
                           });
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.UpdateAsync(null));
        }



        [Theory]
        [InlineData(1)]
        public async Task ShouldDeActiveAsyncSuccess(int committeeId)
        {
            _moqCommitteeDomain.Setup(x => x.CheckIfHasUsers(It.IsAny<int>()))
                               .Returns(() =>
                               {
                                   return Task.FromResult<bool>(false);
                               });

            _moqCommitteeDomain.Setup(x => x.CheckIfHasTenders(It.IsAny<int>()))
                         .Returns(() =>
                         {
                             return Task.FromResult<bool>(false);
                         });


            _moqCommitteeDomain.Setup(x => x.CheckIfHasEnqiryReplies(It.IsAny<int>()))
                         .Returns(() =>
                         {
                             return Task.FromResult<bool>(false);
                         });

            _moqCommitteeQueries.Setup(x => x.GetById(It.IsAny<int>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<Committee>(committeeDefaults.GetCommitteeData());
                           });
            await _sut.DeActiveAsync(committeeId);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Theory]
        [InlineData(2)]
        public async Task ShouldDeActiveAsyncThrowException(int committeeId)
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _sut.DeActiveAsync(committeeId));
        }



        [Theory]
        [InlineData("022001000000")]
        public async Task ShouldGetAllCommittesByAgencyCodeSuccess(string agencyCode)
        {
            _moqCommitteeQueries.Setup(x => x.FindAll(It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Committee>>(committeeDefaults.GetCommitteesData());
                });
            var result = await _sut.FindAll(agencyCode);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1,"022001000000")]
        public async Task ShouldGetAllCommittesByCommitteeTypeIdSuccess(int committeeTypeId ,string agencyCode)
        {
            _moqCommitteeQueries.Setup(x => x.FindByCommitteeTypeId(It.IsAny<int>(),It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Committee>>(committeeDefaults.GetCommitteesData());
                });
            var result = await _sut.FindByCommitteeTypeId(committeeTypeId,agencyCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldAddUserAsynThrowExceptionModelIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.AddUserAsyn(null, "022001000000"));
        }


        [Theory]
        [InlineData(1, 1)]
        public async Task ShouldRemoveAssignedUserSuccess(int userId, int commiteeId)
        {
            _moqCommitteeQueries.Setup(x => x.GetAssignedUserByIdAndCommittee(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<CommitteeUser>(committeeDefaults.GetCommitteeUserData());
                });
            await _sut.RemoveAssignedUser(userId, commiteeId);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldRemoveAssignedUserThrowException()
        {
            _moqCommitteeQueries.Setup(x => x.GetAssignedUserByIdAndCommittee(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<CommitteeUser>(null);
                });
              await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.RemoveAssignedUser(1, 1));

        }


        [Fact]
        public async Task ShouldUpdateUserSuccess()
        {
            CommitteeUserModel committeeUserModel = new CommitteeDefaults().GetCommitteeUserModel();
            string agencyCode = "";
            _iDMAppService.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
           .Returns(() =>
           {
               return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
           });
            _iDMAppService.Setup(m => m.GetIDMRoles())
              .Returns(() =>
              {
                  return new List<IDMRolesModel>(new CommitteeDefaults().GetIDMRolesModel());
              });

            _iDMAppService.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
       .Returns(() =>
       {
           return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
       });

            _notificationAppService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new CommitteeDefaults().GetUserNotificationSettings());
            });

            await _sut.AddUserAsyn(committeeUserModel, agencyCode);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);

        }


        [Fact]
        public async Task ShouldAddSuccess()
        {
            CommitteeUserModel committeeUserModel = new CommitteeDefaults().GetCommitteeUserModel();
            string agencyCode = "";
            _iDMAppService.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
           .Returns(() =>
           {
               return Task.FromResult<UserProfile>(null);
           });
            _iDMAppService.Setup(m => m.GetIDMRoles())
              .Returns(() =>
              {
                  return new List<IDMRolesModel>(new CommitteeDefaults().GetIDMRolesModel());
              });

            _iDMAppService.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
       .Returns(() =>
       {
           return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
       });

            _notificationAppService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new CommitteeDefaults().GetUserNotificationSettings());
            });

            await _sut.AddUserAsyn(committeeUserModel, agencyCode);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.Once);

        }
        [Fact]
        public async Task ShouldAddUsersToCommittesThrowException()
        {
            string agencyCode = "022001000000";
            CommitteeUserModel committeeUserModel = new CommitteeDefaults().GetCommitteeUserModel();
            _iDMAppService.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<UserProfile>(null);
            });
            _iDMAppService.Setup(m => m.GetIDMRoles())
              .Returns(() =>
              {
                  return new List<IDMRolesModel>(new CommitteeDefaults().GetIDMRolesModel());
              });
            _iDMAppService.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
           .Returns(() =>
           {
               return Task.FromResult<UserProfile>(null);
           });

            _notificationAppService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new CommitteeDefaults().GetUserNotificationSettings());
            });
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.AddUserAsyn(committeeUserModel, agencyCode));
        }

        [Fact]
        public async Task ShouldUpdateUserSuccessWithoutNotificationSetting()
        {
            CommitteeUserModel committeeUserModel = new CommitteeDefaults().GetCommitteeUserModel();
            string agencyCode = "";
            _iDMAppService.Setup(m => m.FindUserProfileByUserNameAsync(It.IsAny<string>()))
           .Returns(() =>
           {
               return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaultWithoutNotificationSettings());
           });
            _iDMAppService.Setup(m => m.GetIDMRoles())
              .Returns(() =>
              {
                  return new List<IDMRolesModel>(new CommitteeDefaults().GetIDMRolesModel());
              });

            _iDMAppService.Setup(m => m.GetUserProfileByEmployeeId(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Enums.UserRole>()))
       .Returns(() =>
       {
           return Task.FromResult<UserProfile>(new CommitteeDefaults().ReturnUserProfileDefaults());
       });

            _notificationAppService.Setup(m => m.GetDefaultSettingByUserType(It.IsAny<Enums.UserRole>()))
            .Returns(() =>
            {
                return Task.FromResult<List<UserNotificationSetting>>(new CommitteeDefaults().GetUserNotificationSettings());
            });

            await _sut.AddUserAsyn(committeeUserModel, agencyCode);
            _moqCommandRepository.Verify(m => m.SaveAsync(), Times.AtLeastOnce);

        }

    }
}
