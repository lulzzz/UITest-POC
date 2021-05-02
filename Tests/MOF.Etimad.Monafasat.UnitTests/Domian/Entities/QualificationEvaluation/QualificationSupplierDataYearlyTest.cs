using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationSupplierDataYearlyTest
    {
        private const int id = 1;
        private const int qualificationYearId = 1;
        private const int tenderId = 1;
        private const int qualificationItemId = 1;
        private const decimal supplierValue = 1;
        private const string supplierSelectedCr = "0000";


        [Fact]
        public void Should_Construct_QualificationSupplierDataYearly()
        {
            QualificationSupplierDataYearly qualificationSupplierDataYearly = new QualificationSupplierDataYearly(id, qualificationItemId, qualificationYearId, supplierSelectedCr, supplierValue, tenderId);
            _ = qualificationSupplierDataYearly.ID;
            _ = qualificationSupplierDataYearly.Tender;
            _ = qualificationSupplierDataYearly.Supplier;
            _ = qualificationSupplierDataYearly.QualificationYear;
            _ = qualificationSupplierDataYearly.QualificationItem;

            _ = new QualificationSupplierDataYearly();

            qualificationSupplierDataYearly.ShouldNotBeNull();

        }

        [Fact]
        public void Should_DeActive()
        {
            QualificationSupplierDataYearly qualificationSupplierDataYearly = new QualificationSupplierDataYearly(id, qualificationItemId, qualificationYearId, supplierSelectedCr, supplierValue, tenderId);

            qualificationSupplierDataYearly.DeActive();
            Assert.False(qualificationSupplierDataYearly.IsActive);

        }
    }
}
