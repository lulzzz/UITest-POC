using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class SupplierSpecificationAttachmentTest
    {
        private const int attachmentId = 1;
        private const int offerId = 1;
        private const bool isForApplier = true;
        private const int degree = 1;
        private const int constructionWorkId = 1;
        private const int maintenanceRunningWorkId = 1;

        [Fact]
        public void Should_Construct_SupplierBankGuaranteeDetail()
        {
            _ = new SupplierSpecificationAttachment();
            SupplierSpecificationAttachment supplierSpecificationAttachment = new SupplierSpecificationAttachment(attachmentId, offerId, isForApplier, degree, constructionWorkId, maintenanceRunningWorkId);
            _ = supplierSpecificationAttachment.ConstructionWork;
            _ = supplierSpecificationAttachment.MaintenanceRunningWork;

            supplierSpecificationAttachment.ShouldNotBeNull();

        }

        [Fact]
        public void Should_Construct_SupplierBankGuaranteeDetail_create()
        {
            SupplierSpecificationAttachment supplierSpecificationAttachment = new SupplierSpecificationAttachment(0, offerId, isForApplier, degree, constructionWorkId, maintenanceRunningWorkId);

            supplierSpecificationAttachment.ShouldNotBeNull();
        }
    }
}
