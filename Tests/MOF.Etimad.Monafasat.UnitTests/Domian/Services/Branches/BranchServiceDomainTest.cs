using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Branches
{
    public class BranchServiceDomainTest
    {
        private readonly BranchServiceDomain _sutBranchServiceDomain;
        private readonly Mock<IBranchServiceQueries> _moqBranchServiceQueries;
        private readonly Mock<IBranchServiceCommand> _moqBranchCommands;
        public BranchServiceDomainTest()
        {
            _moqBranchServiceQueries = new Mock<IBranchServiceQueries>();
            _moqBranchCommands = new Mock<IBranchServiceCommand>();
            _sutBranchServiceDomain = new BranchServiceDomain(_moqBranchServiceQueries.Object);
        }

        [Theory]
        [InlineData("branchName", "00220033")]
        public async Task ShouldCheckBranchNotExist(string branchName, string agecyCode)
        {
            var result = await _sutBranchServiceDomain.CheckBranchExist(branchName, agecyCode);
            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task ShouldCheckCommitteeNotExist(int comitteeId, int branchId)
        {
            var result = await _sutBranchServiceDomain.CheckCommitteeExist(comitteeId, branchId);
            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task ShouldCheckTenderExist(int branchId)
        {
            var result = await _sutBranchServiceDomain.CheckTenderExist(branchId);
            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData(1, 1, "NewMonafasat_DataEntry")]
        public async Task ShouldCheckUserExist(int userId, int branchId, string roleName)
        {
            var result = await _sutBranchServiceDomain.CheckUserExist(userId, branchId, roleName);
            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData(5, "NewMonafasat_DataEntry")]
        public void ShouldAssignBranchUserExist(int branchId, string roleName)
        {
            UserProfile userProfile = new UserProfile(1, "1087287072", "hamed", "0533286913", "hsawak@etimad.sa", null);
            BranchUser branchUser = new BranchUser(1, 1, 1, "");
            userProfile.BranchUsers = new System.Collections.Generic.List<BranchUser>();
            userProfile.BranchUsers.Add(branchUser);
            _sutBranchServiceDomain.AssignBranchUserExist(branchId, roleName, userProfile);
            Assert.IsType<bool>(true);
        }

        [Theory]
        [InlineData("test branch", "022001000000")]
        public async Task ShouldCheckBranchNotExistThrowException(string branchName, string agecyCode)
        {
            BranchModel branchModel = new BranchDefaults().GetBranchModel();
            _moqBranchServiceQueries.Setup(x => x.GetBranchByName(It.IsAny<string>(), It.IsAny<string>()))
           .Returns(() =>
           {
               return Task.FromResult<BranchModel>(branchModel);
           });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutBranchServiceDomain.CheckBranchExist(branchName, agecyCode));
        }


        [Theory]
        [InlineData(5, "NewMonafasat_ApproveTenderAward")]
        public void ShouldAssignBranchUserExistThrowException(int branchId, string roleName)
        {
            UserProfile userProfile = new UserProfile(1, "1087287072", "hamed", "0533286913", "hsawak@etimad.sa", null);
            BranchUser branchUser = new BranchUser(5, 1, 25, "");
            userProfile.BranchUsers = new System.Collections.Generic.List<BranchUser>();
            userProfile.BranchUsers.Add(branchUser);

            Action action = () => { _sutBranchServiceDomain.AssignBranchUserExist(branchId, roleName, userProfile); };
            var exception = action.ShouldThrow(typeof(BusinessRuleException));
            exception.Message.ShouldBe(Resources.CommitteeResources.ErrorMessages.UserAlreadyExist);
        }


        [Theory]
        [InlineData(1, 1, "NewMonafasat_DataEntry")]
        public async Task ShouldCheckUserExistThrowException(int userId, int branchId, string roleName)
        {
            _moqBranchServiceQueries.Setup(x => x.GetAssignedBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
           .Returns(() =>
           {
               return Task.FromResult<BranchUser>(new BranchUserDefaults().ReturnBranchUserDefaults());
           });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutBranchServiceDomain.CheckUserExist(userId, branchId, roleName));
        }



        [Theory]
        [InlineData(1)]
        public async Task ShouldGetHasTendersByBranchThrowException(int branchId)
        {
            _moqBranchServiceQueries.Setup(x => x.GetHasTendersByBranch(It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<bool>(true);
           });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutBranchServiceDomain.CheckTenderExist(branchId));
        }
    }
}
