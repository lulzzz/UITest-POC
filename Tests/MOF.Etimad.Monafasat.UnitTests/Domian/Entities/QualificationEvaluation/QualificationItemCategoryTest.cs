using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationItemCategoryTest
    {
        [Fact]
        public void Should_Construct_QualificationItemCategory()
        {
            QualificationItemCategory qualificationItemCategory = new QualificationItemCategory();
            _ = qualificationItemCategory.ID;
            _ = qualificationItemCategory.Name;
            _ = qualificationItemCategory.NameEn;

            qualificationItemCategory.ShouldNotBeNull();

        }
    }
}
