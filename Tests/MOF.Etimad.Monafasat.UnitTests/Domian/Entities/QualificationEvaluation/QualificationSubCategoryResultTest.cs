using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationSubCategoryResultTest
    {
        private const int qualificationCategoryId = 1;
        private const int qualificationSubCategoryId = 1;
        private const int tenderId = 1;
        private const string supplierSelectedCr = "0000";
        private const decimal resultValue = 1;
        private const decimal percentage = 1;


        [Fact]
        public void Should_Construct_QualificationSubCategoryResult()
        {
            QualificationSubCategoryResult qualificationSubCategoryResult = new QualificationSubCategoryResult(qualificationSubCategoryId, qualificationCategoryId, tenderId,
                supplierSelectedCr, resultValue, percentage);
            _ = qualificationSubCategoryResult.ID;
            _ = qualificationSubCategoryResult.QualificationSubCategory;
            _ = qualificationSubCategoryResult.Tender;
            _ = qualificationSubCategoryResult.Supplier;
            _ = new QualificationSubCategoryResult();

            qualificationSubCategoryResult.ShouldNotBeNull();

        }

        [Fact]
        public void Should_Create()
        {
            QualificationSubCategoryResult qualificationSubCategoryResult = new QualificationSubCategoryResult(qualificationSubCategoryId, qualificationCategoryId, tenderId,
                supplierSelectedCr, resultValue, percentage);

            qualificationSubCategoryResult.Created();
            Assert.Equal(ObjectState.Added, qualificationSubCategoryResult.State);

        }
    }
}
