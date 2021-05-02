using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationSubCategoryConfigurationTest
    {
        private const int prequalificationID = 1;
        private const int qualificationSubCategoryId = 1;
        private const decimal weight = 50;

        [Fact]
        public void Should_Construct_QualificationSubCategoryConfiguration()
        {
            QualificationSubCategoryConfiguration qualificationSubCategoryConfiguration = new QualificationSubCategoryConfiguration(prequalificationID, qualificationSubCategoryId, weight);
            _ = qualificationSubCategoryConfiguration.ID;
            _ = qualificationSubCategoryConfiguration.QualificationSubCategory;
            _ = qualificationSubCategoryConfiguration.Tender;

            qualificationSubCategoryConfiguration.ShouldNotBeNull();

        }
    }
}
