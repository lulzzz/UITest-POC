using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationTypeCategoryTest
    {
        [Fact]
        public void Should_Construct_QualificationTypeCategory()
        {
            QualificationTypeCategory qualificationTypeCategory = new QualificationTypeCategory();
            _ = qualificationTypeCategory.ID;
            _ = qualificationTypeCategory.QualificationTypeId;
            _ = qualificationTypeCategory.QualificationSubCategoryId;
            _ = qualificationTypeCategory.QualificationSubCategory;
            _ = qualificationTypeCategory.QualificationType;

            qualificationTypeCategory.ShouldNotBeNull();

        }
    }
}
