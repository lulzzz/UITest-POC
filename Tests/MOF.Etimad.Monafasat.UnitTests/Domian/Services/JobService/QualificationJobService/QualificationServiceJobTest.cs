using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.Services.JobServices.Qualification;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.JobService.QualificationJobService
{
    public class QualificationServiceJobTest
    {
        private readonly Mock<IQualificationServiceQueryJob> _moqQualificationServiceQuery;
        private readonly Mock<ISupplierQualificationDocumentAppService> _moqSupplierQualificationDocumentsAppService;
        private readonly QualificationServiceJob _sut;
        public QualificationServiceJobTest()
        {
            _moqQualificationServiceQuery = new Mock<IQualificationServiceQueryJob>();
            _moqSupplierQualificationDocumentsAppService = new Mock<ISupplierQualificationDocumentAppService>();
            _sut = new QualificationServiceJob(_moqQualificationServiceQuery.Object, _moqSupplierQualificationDocumentsAppService.Object);
        }

        [Fact]
        public async Task Should_RecalculateSupplierPoint()
        {
            List<QualificationSupplierDto> qualificationSupplierDtos = new List<QualificationSupplierDto>()
            {
                new QualificationSupplierDto(){ QualificationId = 6, SupplierCr = "5956275283" }
            };
            _moqQualificationServiceQuery.Setup(q => q.GetActiveQualificationSuppliers())
                .Returns(() => { return Task.FromResult(qualificationSupplierDtos); });
            _moqQualificationServiceQuery.Setup(q => q.GetQualificationSupplierData(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult(new QualificationDefault().GetApplyDocumentsModelLargeQulaification()); });
            _moqSupplierQualificationDocumentsAppService.Setup(q => q.GetSuppierDocument(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult(new QualificationDefault().GetSupplierPreQualificationDocument()); });

            await _sut.RecalculateSupplierPoint();
            _moqSupplierQualificationDocumentsAppService.Verify(s => s.ApplyDocsforSupplier(It.IsAny<PreQualificationApplyDocumentsModel>(), It.IsAny<bool>()));

        }
    }
}
