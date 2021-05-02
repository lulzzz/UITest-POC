using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationFinalResultTest
    {
        private const int tenderId = 1;
        private const string supplierSelectedCr = "0000";
        private const decimal resultValue = 20;
        private const int qualificationLookupIdSuccesss = 14;
        private const int qualificationLookupIdFailed = 15;
        private const string failingReason = "reason";

        [Fact]
        public void Should_Construct_QualificationFinalResult()
        {
            QualificationFinalResult qualificationFinalResult = new QualificationFinalResult(tenderId, supplierSelectedCr, resultValue, qualificationLookupIdSuccesss);
            _ = qualificationFinalResult.ID;
            _ = qualificationFinalResult.Tender;
            _ = qualificationFinalResult.Supplier;
            _ = qualificationFinalResult.QualificationLookup;

            qualificationFinalResult.ShouldNotBeNull();

        }

        [Fact]
        public void Should_Create()
        {
            QualificationFinalResult qualificationFinalResult = new QualificationFinalResult(tenderId, supplierSelectedCr, resultValue, qualificationLookupIdSuccesss);
            qualificationFinalResult.Created();
            qualificationFinalResult.ShouldNotBeNull();
        }

        [Fact]
        public void Should_DeActive()
        {
            QualificationFinalResult qualificationFinalResult = new QualificationFinalResult(tenderId, supplierSelectedCr, resultValue, qualificationLookupIdSuccesss);
            qualificationFinalResult.DeActive();
            Assert.False(qualificationFinalResult.IsActive);
        }

        [Fact]
        public void Should_UpdateQualificationLookupId()
        {
            QualificationFinalResult qualificationFinalResultSuccess = new QualificationFinalResult(tenderId, supplierSelectedCr, resultValue, qualificationLookupIdSuccesss);
            QualificationFinalResult qualificationFinalResultFailed = new QualificationFinalResult(tenderId, supplierSelectedCr, resultValue, qualificationLookupIdFailed);

            qualificationFinalResultSuccess.UpdateQualificationLookupId(qualificationLookupIdSuccesss, "");
            qualificationFinalResultFailed.UpdateQualificationLookupId(qualificationLookupIdFailed, failingReason);
            Assert.Equal(failingReason, qualificationFinalResultFailed.FailingReason);
            Assert.Equal("", qualificationFinalResultSuccess.FailingReason);

        }
    }
}
