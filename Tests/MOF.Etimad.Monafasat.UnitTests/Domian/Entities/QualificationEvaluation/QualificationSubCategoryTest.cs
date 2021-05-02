using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationSubCategoryTest
    {
        [Fact]
        public void Should_Construct_QualificationSubCategory()
        {
            QualificationSubCategory qualificationSubCategory = new QualificationSubCategory();
            _ = qualificationSubCategory.ID;
            _ = qualificationSubCategory.Name;
            _ = qualificationSubCategory.NameEn;
            _ = qualificationSubCategory.QualificationCategoryId;
            _ = qualificationSubCategory.IsConfigure;
            _ = qualificationSubCategory.QualificationItems;
            _ = qualificationSubCategory.QualificationItemCategory;
            _ = qualificationSubCategory.QualificationSubCategoryResults;
            _ = qualificationSubCategory.QualificationTypeCategories;

            qualificationSubCategory.ShouldNotBeNull();

        }
    }
}
