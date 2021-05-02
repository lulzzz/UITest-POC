using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationEvaluation
{
    public class QualificationSupplierProjectTest
    {
        private const int id = 1;
        private const int tenderId = 1;
        private const decimal contractValue = 400;
        private const string supplierSelectedCr = "0000";
        private const string contractName = "name";
        private const string description = "name";
        private const string ownerName = "name";
        private const string phoneNumber = "0000";
        private const string emailAddress = "0000";

        [Fact]
        public void Should_Construct_QualificationSupplierProject()
        {
            QualificationSupplierProject qualificationSupplierProject = new QualificationSupplierProject(id, tenderId, contractValue, contractName, description, ownerName,
                phoneNumber, emailAddress, supplierSelectedCr, null, null);
            _ = qualificationSupplierProject.ID;
            _ = qualificationSupplierProject.Tender;
            _ = qualificationSupplierProject.Supplier;
            _ = qualificationSupplierProject.QualificationSupplierData;

            _ = new QualificationSupplierProject();

            qualificationSupplierProject.ShouldNotBeNull();

        }

        [Fact]
        public void Should_DeActive()
        {
            QualificationSupplierProject qualificationSupplierProject = new QualificationSupplierProject(id, tenderId, contractValue, contractName, description, ownerName,
                 phoneNumber, emailAddress, supplierSelectedCr, null, null);
            qualificationSupplierProject.DeActive();
            Assert.False(qualificationSupplierProject.IsActive);

        }
    }
}
