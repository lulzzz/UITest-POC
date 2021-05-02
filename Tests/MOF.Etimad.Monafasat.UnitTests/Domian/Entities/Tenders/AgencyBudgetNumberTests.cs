using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class AgencyBudgetNumberTests
    {
        [Fact]
        public void ConstructDefaultAgencyBudgetNumber()
        {
            AgencyBudgetNumber agencyBudgetNumber = new AgencyBudgetNumber();

            Assert.NotNull(agencyBudgetNumber);
        }

        [Fact]
        public void ConstructAgencyBudgetNumber()
        {
            AgencyBudgetNumber agencyBudgetNumber = new AgencyBudgetNumber(1, "1234", "tender 1", 100, 100, true);

            Assert.Equal(1, agencyBudgetNumber.TenderId);
            Assert.Equal("1234", agencyBudgetNumber.ProjectNumber);
            Assert.Equal("tender 1", agencyBudgetNumber.ProjectName);
            Assert.Equal(100, agencyBudgetNumber.Cost);
            Assert.Equal(100, agencyBudgetNumber.Cache);
            Assert.True(agencyBudgetNumber.IsProject);
            Assert.Equal(ObjectState.Added, agencyBudgetNumber.State);
        }

        [Fact]
        public void DeleteAgencyBudgetNumber()
        {
            AgencyBudgetNumber agencyBudgetNumber = new AgencyBudgetNumber(1, "1234", "tender 1", 100, 100, true);

            agencyBudgetNumber.Delete();

            Assert.Equal(ObjectState.Deleted, agencyBudgetNumber.State);

        }
    }
}
