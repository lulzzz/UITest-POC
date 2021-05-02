using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationItemTypeTest
    {
        [Fact]
        public void Should_Construct_QualificationItemType()
        {
            QualificationItemType qualificationItemType = new QualificationItemType();
            _ = qualificationItemType.ID;
            _ = qualificationItemType.Name;
            _ = qualificationItemType.NameEn;

            qualificationItemType.ShouldNotBeNull();

        }
    }
}
