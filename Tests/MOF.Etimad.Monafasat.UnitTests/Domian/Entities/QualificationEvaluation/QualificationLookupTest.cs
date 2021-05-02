using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationLookupTest
    {
        [Fact]
        public void Should_Construct_QualificationLookup()
        {
            QualificationLookup qualificationLookup = new QualificationLookup();
            _ = qualificationLookup.ID;
            _ = qualificationLookup.QualificationLookupId;
            _ = qualificationLookup.Name;
            _ = qualificationLookup.NameEn;
            _ = qualificationLookup.QualificationLookupsName;

            qualificationLookup.ShouldNotBeNull();

        }
    }
}
