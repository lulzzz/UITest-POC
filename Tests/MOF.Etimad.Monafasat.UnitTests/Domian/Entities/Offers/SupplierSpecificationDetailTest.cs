using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class SupplierSpecificationDetailTest
    {
        private const int id = 1;
        private const int compinedDetailId = 1;
        private const bool isForApplier = true;
        private const int degree = 1;
        private const int constructionWorkId = 1;
        private const int maintenanceRunningWorkId = 1;

        [Fact]
        public void Should_Construct_SupplierSpecificationDetail()
        {
            _ = new SupplierSpecificationDetail();
            SupplierSpecificationDetail supplierSpecificationDetail = new SupplierSpecificationDetail(id, compinedDetailId, isForApplier, degree, constructionWorkId, maintenanceRunningWorkId);
            _ = supplierSpecificationDetail.ConstructionWork;
            _ = supplierSpecificationDetail.MaintenanceRunningWork;
            _ = supplierSpecificationDetail.SpecificationId;

            supplierSpecificationDetail.ShouldNotBeNull();

        }

        [Fact]
        public void Should_Construct_SupplierSpecificationDetail_create()
        {
            SupplierSpecificationDetail supplierSpecificationDetail = new SupplierSpecificationDetail(0, compinedDetailId, isForApplier, degree, constructionWorkId, maintenanceRunningWorkId);

            supplierSpecificationDetail.ShouldNotBeNull();
        }
    }
}
