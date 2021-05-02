using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.PrePlanning
{
    public class PrePlanningQueriesTest
    {

        private readonly Mock<IPrePlanningQueries> _moqCommitteeQueries;
        private readonly PrePlanningQueries _sut;

        public PrePlanningQueriesTest()
        {
            _moqCommitteeQueries = new Mock<IPrePlanningQueries>();
            _sut = new PrePlanningQueries(InitialData.context);
        }

        [Fact]
        public async Task ShoulFindPrePlanningBySearchCriteriaSuccess()
        {
            PrePlanningSearchCriteria criteria = new PrePlanningSearchCriteria();
            var result = await _sut.FindPrePlanningBySearchCriteria(criteria);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("no project", 1, "0410010000000002000", 1)]
        public async Task ShoulGetByNameReturnFalse(string projectName, int branchId, string agencyCode, int prePlanningId)
        {
            var result = await _sut.GetByName(projectName, branchId, agencyCode, prePlanningId);
            Assert.False(result);
        }

        [Theory]
        [InlineData(1,0)]
        public async Task ShoulFindByIdForEditSuccess(int preplanningId, int? statusId)
        {
            var result = await _sut.FindByIdForEdit(preplanningId,statusId);
            Assert.NotNull(result);
        }

    }
}
