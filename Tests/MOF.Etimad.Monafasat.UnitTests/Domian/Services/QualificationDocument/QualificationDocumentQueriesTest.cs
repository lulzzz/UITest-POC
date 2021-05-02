using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services.MainServices.SupplierQualificationDocument.Abstract;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.QualificationDocument
{
    public class QualificationDocumentQueriesTest
    {
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;

        private readonly SupplierQualificationDocumentQueries _QualificationDocumentQuereies;

        public QualificationDocumentQueriesTest()
        {
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _QualificationDocumentQuereies =
                new SupplierQualificationDocumentQueries(InitialData.context, _moqHttpContextAccessor.Object);
        }

        [Theory]
        [InlineData(1)]
        public async Task ShouldFindSupplierPreQualificationDocumentById(int supplierQualificationDoumentId)
        {

          var result =  await _QualificationDocumentQuereies.FindSupplierPreQualificationDocumentById(supplierQualificationDoumentId);

          Assert.NotNull(result);
          Assert.IsType<SupplierPreQualificationDocument>(result);
        }


        [Theory]
        [InlineData(1)]
        public async Task ShouldGetTenderAndSupplierDocuments(int preQualificationId)
        {

            var result = await _QualificationDocumentQuereies.GetTenderAndSupplierDocuments(preQualificationId);

            Assert.NotNull(result);
            Assert.IsType<Tender>(result);

        }

        [Theory]
        [InlineData(1)]
        public async Task ShouldGetPreQualificationById(int preQualificationId)
        {

            var result = await _QualificationDocumentQuereies.GetPreQualificationById(preQualificationId);

            Assert.NotNull(result);
            Assert.IsType<Tender>(result);

        } 
        
        [Theory]
        [InlineData(1)]
        public async Task ShouldGetQualificationSupplierData(int preQualificationId)
        {
            MoqUser();
            var result = await _QualificationDocumentQuereies.GetQualificationSupplierData(preQualificationId);

            Assert.NotNull(result);
            Assert.IsType<List<QualificationSupplierDataModel>>(result);

        }
        
        [Theory]
        [InlineData(7, "1010000154")]
        public async Task ShouldGetSupplierDataReviewModel(int preQualificationId, string supplierNumber)
        {
            var result = await _QualificationDocumentQuereies.GetSupplierDataReviewModel(preQualificationId, supplierNumber);

            Assert.NotNull(result);
            Assert.IsType<QualificationSupplierDataReviewViewModel>(result);
        }

        private void MoqUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var usernum = new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(usernum);
            idintity.AddClaim(claim);

            context.SetupGet(x => x.User.Identity).Returns(() => { return idintity; });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
        }

    }
}
