using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationLookupsNameTest
    {
        [Fact]
        public void Should_Construct_QualificationLookupsName()
        {
            QualificationLookupsName qualificationLookupsName = new QualificationLookupsName();
            _ = qualificationLookupsName.ID;
            _ = qualificationLookupsName.Name;
            _ = qualificationLookupsName.NameEn;
            _ = qualificationLookupsName.QualificationLookups;

            qualificationLookupsName.ShouldNotBeNull();

        }
    }
}
