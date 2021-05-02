using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Branches
{
    public class BranchQueryServiceTest
    {
        private readonly BranchServiceQueries _sutBranchServiceQueries;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;
        public BranchQueryServiceTest()
        {
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfigurationMock.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForTender());
            _sutBranchServiceQueries = new BranchServiceQueries(InitialData.context, _rootConfigurationMock.Object);

        }

        [Fact]
        public async Task ShouldFindBranchBySearchCriteria()
        {
            var branchSearchCriteriaModel = new BranchSearchCriteriaModel();
            var result = await _sutBranchServiceQueries.Find(branchSearchCriteriaModel);
            Assert.NotNull(result);
            Assert.IsType<QueryResult<BranchModel>>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task ShouldFindBranchById(int branchId)
        {
            var result = await _sutBranchServiceQueries.FindBranchById(branchId);
            Assert.NotNull(result);
            Assert.IsType<Branch>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task ShouldFindById(int branchId)
        {
            var result = await _sutBranchServiceQueries.FindById(branchId);
            Assert.NotNull(result);
            Assert.IsType<BranchModel>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async Task ShouldFindAssignedCommitteeByIdAndBranch(int committeeId, int branchId)
        {
            var result = await _sutBranchServiceQueries.GetAssignedCommitteeByIdAndBranch(committeeId, branchId);
            Assert.NotNull(result);
            Assert.IsType<BranchCommittee>(result);
        }


        [Theory]
        [InlineData("branchName", "00220033")]
        public async Task ShouldFindBranchByName(string branchName, string agecyCode)
        {
            var result = await _sutBranchServiceQueries.GetBranchByName(branchName, agecyCode);
            Assert.NotNull(result);
            Assert.IsType<BranchModel>(result);
        }

        [Theory]
        [InlineData("00220033")]
        public async Task ShouldFindBranchByAgencyCode(string agecyCode)
        {
            var result = await _sutBranchServiceQueries.GetBranchByAgency(agecyCode);
            Assert.NotNull(result);
            Assert.IsType<List<BranchModel>>(result);
        }

        [Theory]
        [InlineData(1, 1, "NewMonafasat_DataEntry")]
        public async Task ShouldFindAssignedBranchUsers(int userId, int branchId, string roleName)
        {
            var result = await _sutBranchServiceQueries.GetAssignedBranchUsers(userId, branchId, roleName);
            Assert.NotNull(result);
            Assert.IsType<BranchUser>(result);
        }

        [Fact]
        public async Task ShouldFindBranchUsersBySearchCriteria()
        {
            var branchUserSearchCriteriaModel = new BranchUserSearchCriteriaModel();
            var result = await _sutBranchServiceQueries.GetBranchUsers(branchUserSearchCriteriaModel);
            Assert.NotNull(result);
            Assert.IsType<QueryResult<BranchUserModel>>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task ShouldFindBranchUsersHasEstimateValue(int userId, int branchId)
        {
            var result = await _sutBranchServiceQueries.GetBranchUsersHasEstimateValue(userId, branchId);
            Assert.NotNull(result);
            Assert.IsType<List<BranchUserModel>>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task ShouldFindBranchUsersForAwardingApproval(int userId, int branchId)
        {
            var result = await _sutBranchServiceQueries.GetBranchUsersForAwardingApproval(userId, branchId);
            Assert.NotNull(result);
            Assert.IsType<BranchUser>(result);
        }

        [Theory]
        [InlineData("VROOfficeCode")]
        public async Task ShouldFindVROBranchsByVROOfficeCode(string VROOfficeCode)
        {
            var result = await _sutBranchServiceQueries.GetVROBranchsByVROOfficeCode(VROOfficeCode);
            Assert.NotNull(result);
            Assert.IsType<List<int>>(result);
        }

        [Fact]
        public async Task ShouldFindBranchCommittees()
        {
            var branchCommitteeSearchCriteriaModel = new BranchCommitteeSearchCriteriaModel();
            var result = await _sutBranchServiceQueries.GetBranchCommittees(branchCommitteeSearchCriteriaModel);
            Assert.NotNull(result);
            Assert.IsType<QueryResult<LookupModel>>(result);
        }

        [Theory]
        [InlineData(1, "00220033")]
        public async Task ShouldFindUserDefultBranch(int userId, string agencyCode)
        {
            var result = await _sutBranchServiceQueries.GetUserDefultBranch(userId, agencyCode);
            Assert.IsType<int>(result);
        }

        [Theory]
        [InlineData(1, "00220033")]
        public async Task ShouldFindUserDefultCommittees(int userId, string agencyCode)
        {
            var result = await _sutBranchServiceQueries.GetUserDefultCommittees(userId, agencyCode);
            Assert.IsType<int>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task ShouldFindUserRoleByCommittee(int committeeId, int userId)
        {
            var result = await _sutBranchServiceQueries.GetUserRoleByCommittee(committeeId, userId);
            Assert.NotNull(result);
            Assert.IsType<List<string>>(result);
        }
        [Theory]
        [InlineData(1, 1)]
        public async Task ShouldFindUserRolesByBranch(int userId, int branchId)
        {
            var result = await _sutBranchServiceQueries.GetUserRolesByBranch(userId, branchId);
            Assert.NotNull(result);
            Assert.IsType<List<string>>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task ShouldFindUserRolesByBranchFromCommittees(int userId, int branchId)
        {
            var result = await _sutBranchServiceQueries.GetUserRolesByBranchFromCommittees(userId, branchId);
            Assert.NotNull(result);
            Assert.IsType<List<string>>(result);
        }

        [Theory]
        [InlineData(1, "00220033")]
        public async Task ShouldFindAllUserRolesFromBranches(int userId, string agencyCode)
        {
            var result = await _sutBranchServiceQueries.GetAllUserRolesFromBranches(userId, agencyCode);
            Assert.NotNull(result);
            Assert.IsType<List<UserRolesModel>>(result);
        }

        [Theory]
        [InlineData(1, "00220033")]
        public async Task ShouldFindAllUserRolesFromCommittees(int userId, string agencyCode)
        {
            var result = await _sutBranchServiceQueries.GetAllUserRolesFromCommittees(userId, agencyCode);
            Assert.NotNull(result);
            Assert.IsType<List<UserRolesModel>>(result);
        }

        [Theory]
        [InlineData("00220033")]
        public async Task ShouldFindAllBranchesByAgencyCode(string agencyCode)
        {
            var result = await _sutBranchServiceQueries.GetAllBranchesByAgencyCode(agencyCode);
            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
        }

        [Theory]
        [InlineData(1, 1, "00220033")]
        public async Task ShouldFindAllBranchesNotAssignedToCommittee(int committeeId, int committeeType, string agencyCode)
        {
            var result = await _sutBranchServiceQueries.GetAllBranchesNotAssignedToCommittee(committeeId, committeeType, agencyCode);
            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
        }
        [Theory]
        [InlineData(1)]
        public async Task ShouldFindInsertionTypeAndPurchaseMenthodsById(int Id)
        {
            var result = await _sutBranchServiceQueries.FindInsertionTypeAndPurchaseMenthodsById(Id);
            Assert.NotNull(result);
            Assert.IsType<CreateTenderBasicDataModel>(result);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(100)]

        public async Task ShouldFindHasTendersByBranch(int branchId)
        {
            var result = await _sutBranchServiceQueries.GetHasTendersByBranch(branchId);
            Assert.IsType<bool>(result);
        }
    }
}
