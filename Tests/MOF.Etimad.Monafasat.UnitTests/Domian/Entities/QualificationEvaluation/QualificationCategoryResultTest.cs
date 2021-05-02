using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationCategoryResultTest
    {
        private const int id = 1;
        private const int qualificationItemCategoryId = 1;
        private const int tenderId = 1;
        private const string supplierSelectedCr = "0000";
        private const decimal resultValue = 20;
        private const decimal percentage = 10;
        private const decimal weight = 50;


        [Fact]
        public void Should_Construct_QualificationCategoryResult()
        {
            QualificationCategoryResult qualificationCategoryResult = new QualificationCategoryResult(id, qualificationItemCategoryId, tenderId, supplierSelectedCr, resultValue
                , percentage, weight);
            _ = qualificationCategoryResult.QualificationItemCategory;
            _ = qualificationCategoryResult.Tender;
            _ = qualificationCategoryResult.Supplier;

            qualificationCategoryResult.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Created()
        {
            QualificationCategoryResult qualificationCategoryResult = new QualificationCategoryResult(id, qualificationItemCategoryId, tenderId, supplierSelectedCr, resultValue
                , percentage, weight);
            qualificationCategoryResult.Created();
            Assert.Equal(ObjectState.Added, qualificationCategoryResult.State);

        }
    }
}
