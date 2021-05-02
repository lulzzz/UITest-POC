using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationYearTest
    {
        [Fact]
        public void Should_Construct_QualificationYear()
        {
            QualificationYear qualificationYear = new QualificationYear();
            _ = qualificationYear.ID;
            _ = qualificationYear.Name;

            qualificationYear.ShouldNotBeNull();
        }
    }
}
