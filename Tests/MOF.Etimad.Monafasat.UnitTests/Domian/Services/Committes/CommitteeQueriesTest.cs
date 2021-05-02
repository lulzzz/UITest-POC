using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Threading.Tasks;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Services
{
    public class CommitteeQueriesTest
    {
        private readonly Mock<ICommitteeQueries> _moqCommitteeQueries;
        private readonly CommitteeQueries _sut;

        public CommitteeQueriesTest()
        {
            _moqCommitteeQueries = new Mock<ICommitteeQueries>();
            _sut = new CommitteeQueries(InitialData.context);
        }

        [Theory]
        [InlineData("name 1", "11", 1)]
        public async Task CommitteeIsNotExist(string name, string agencyCode, int id)
        {
            var result = await _sut.GetByName(name, agencyCode, id);
            Assert.False(result);

        }

        [Theory]
        [InlineData("committeeName1", "1", 0)]
        public async Task CommitteeIsExist(string name, string agencyCode, int id)
        {
            var result = await _sut.GetByName(name, agencyCode, id);
            Assert.True(result);

        }

        [Theory]
        [InlineData(1)]
        public async Task ShouldHasAssignedUserByCommitteeSucess( int committeeId)
        {
            var result = await _sut.HasAssignedUserByCommittee(committeeId);
            Assert.True(result);
        }

        [Theory]
        [InlineData(5)]
        public async Task ShouldHasAssignedUserByCommitteeReturnFalse(int committeeId)
        {
            var result = await _sut.HasAssignedUserByCommittee(committeeId);
            Assert.False(result);
        }


        [Theory]
        [InlineData("1")]
        public async Task ShouldGetVROAndTechnicalCommitteesSuccess(string agencyCode)
        {
            var result = await _sut.GetVROAndTechnicalCommittees(agencyCode);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("1")]
        public async Task ShouldGetTechincalAndDirectPurchaseCommitteesSuccess(string agencyCode)
        {
            var result = await _sut.GetTechincalAndDirectPurchaseCommittees(agencyCode);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("1")]
        public async Task ShouldGetBasicCommitteesOnBranchSuccess(string agencyCode)
        {
            var result = await _sut.GetBasicCommitteesOnBranch(agencyCode);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(1)]
        public async Task ShoulFindAgencyCodeByCommitteeIdSuccess(int committeId)
        {
            var result = await _sut.FindAgencyCodeByCommitteeId(committeId);
            Assert.NotNull(result);
        }


        
               [Fact]
        public async Task ShoulFindSuccess()
        {
            CommitteeSearchCriteria criteria = new CommitteeSearchCriteria();

            var result = await _sut.Find(criteria, x => x.CommitteeType, d => d.CommitteeType) ;
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1,1)]
        public async Task ShoulGetAssignedUserByIdAndCommitteeSuccess(int userId,int committeId)
        {
            var result = await _sut.GetAssignedUserByIdAndCommittee(userId,committeId);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task ShoulGetCommitteeUsersByCommitteeIdSuccess(int committeId)
        {
            var result = await _sut.GetCommitteeUsersByCommitteeId(committeId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShoulGetCommitteeUsersSuccess()
        {
            CommitteeUserSearchCriteriaModel criteria = new CommitteeUserSearchCriteriaModel();
            criteria.CommitteeTypeId = 1;
            var result = await _sut.GetCommitteeUsers(criteria);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("1",1)]
        public async Task ShoulGetDirectPurchaseCommitteesMembersSuccess(string agencyCode,int branchId)
        {
            var result = await _sut.GetDirectPurchaseCommitteesMembers(agencyCode,branchId);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("1")]
        public async Task ShoulFindAllSuccess(string agencyCode)
        {
            var result = await _sut.FindAll(agencyCode);
            Assert.NotNull(result);
        }

        

              [Theory]
        [InlineData(1, "1")]
        public async Task ShoulFindByCommitteeTypeIdSuccess(int committeId, string agencyCode)
        {
            var result = await _sut.FindByCommitteeTypeId(committeId, agencyCode);
            Assert.NotNull(result);
        }

       
        [Theory]
        [InlineData("1", "1",1)]
        public async Task ShoulGetByNameandTypeIdSuccess(string name, string agencyId, int typeid)
        {
            var result = await _sut.GetByNameandTypeId( name, agencyId,  typeid);
            Assert.IsType<bool>(result);
        }


    }
}
