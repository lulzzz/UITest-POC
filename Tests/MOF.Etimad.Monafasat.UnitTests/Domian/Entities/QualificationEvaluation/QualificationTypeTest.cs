using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationTypeTest
    {

        [Fact]
        public void Should_Construct_QualificationType()
        {
            QualificationType qualificationType = new QualificationType();
            _ = qualificationType.ID;
            _ = qualificationType.Name;
            _ = qualificationType.NameEn;
            _ = qualificationType.IsDeleted;
            _ = qualificationType.QualificationTypeCategories;

            qualificationType.ShouldNotBeNull();
        }

    }
}
