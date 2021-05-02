using MOF.Etimad.Monafasat.Data;
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
    public class QualificationServiceQueryJobTest
    {
        private readonly QualificationServiceQueryJob _sut;
        public QualificationServiceQueryJobTest()
        {
            _sut = new QualificationServiceQueryJob(InitialData.context);
        }

        [Fact]
        public async Task Should_GetActiveQualificationSuppliers()
        {
            Mock<IAppDbContext> _moqDbContext = new Mock<IAppDbContext>();
            _moqDbContext.Setup(m => m.SupplierPreQualificationDocument)
                .Returns(() => { return InitialData.context.SupplierPreQualificationDocument; });
            var result = _sut.GetActiveQualificationSuppliers();
            Assert.NotNull(result);
        }

    }
}
