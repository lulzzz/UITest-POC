using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.QualificationDocument
{
    public class SupplierQualificationDocumentDomainServiceTest
    {
        private readonly Mock<ISupplierQualificationDocumentQueries> _moqSupplierqualificationDocumentQueries;
        private readonly Mock<ISupplierQualificationDocumentCommands> _moqSupplierqualificationDocumentcommands;
        private readonly Mock<IBlockAppService> _moqBlockAppService;
        private readonly SupplierQualificationDocumentDomainService _SupplierQualificationDocumentDomain;

        public SupplierQualificationDocumentDomainServiceTest()
        {
            _moqBlockAppService = new Mock<IBlockAppService>();
            _moqSupplierqualificationDocumentQueries = new Mock<ISupplierQualificationDocumentQueries>();
            _moqSupplierqualificationDocumentcommands = new Mock<ISupplierQualificationDocumentCommands>();

            _SupplierQualificationDocumentDomain = new SupplierQualificationDocumentDomainService(InitialData.context,
                _moqSupplierqualificationDocumentcommands.Object, _moqSupplierqualificationDocumentQueries.Object,
                _moqBlockAppService.Object);
        }

        [Theory]
        [InlineData(7, "1010052847")]
        public async Task ShouldThrowBusinessExceptionIfQualificationDateHasPassed(int preQualificationId, string supplierNumber)
        {
            var qualification = new QualificationDefault().GetPreQualification();
            qualification.UpdateTenderDates(DateTime.Now, DateTime.Now.AddDays(-5), DateTime.Now, DateTime.Now, "", 1,
                false, null, 1, "", "", "", DateTime.Now);
            _moqSupplierqualificationDocumentQueries.Setup(x => x.GetTenderAndSupplierDocuments(It.IsAny<int>()))
                .Returns(
                    () =>
                    {
                        return Task.FromResult<Tender>(qualification);
                    });
            var e = Assert.ThrowsAsync<BusinessRuleException>(
                async () => await _SupplierQualificationDocumentDomain.CheckPresentationDateValidation(preQualificationId,
                    supplierNumber));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.AfterDate, e.Result.Message);

        }

        [Theory]
        [InlineData(7, "1010052847")]
        public async Task ShouldThrowBusinessExceptionIfQualificationTimeHasPassed(int preQualificationId, string supplierNumber)
        {
            var qualification = new QualificationDefault().GetPreQualification();
            qualification.UpdateTenderDates(DateTime.Now, DateTime.Now.AddHours(-5), DateTime.Now, DateTime.Now, "", 1,
                false, null, 1, "", "", "", DateTime.Now);
            _moqSupplierqualificationDocumentQueries.Setup(x => x.GetTenderAndSupplierDocuments(It.IsAny<int>()))
                .Returns(
                    () =>
                    {
                        return Task.FromResult<Tender>(qualification);
                    });
            var e = Assert.ThrowsAsync<BusinessRuleException>(
                async () => await _SupplierQualificationDocumentDomain.CheckPresentationDateValidation(preQualificationId,
                    supplierNumber));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.AfterDate, e.Result.Message);

        }
    }
}
