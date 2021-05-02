using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Qualification
{
    public class QualificationQueriesTest
    {
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private QualificationQueries _sut;

        public QualificationQueriesTest()
        {
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _sut = new QualificationQueries(InitialData.context, _moqHttpContextAccessor.Object);
        }



        [Theory]
        [InlineData(1)]
        public async Task GetPrequalificationPartialDetails_WithQualificationId_ReturnsQualificationModel(
            int qualificationId)
        {
            var result = await _sut.GetPrequalificationPartialDetails(qualificationId, tender => tender.TenderActivities);

            Assert.IsType<PreQualificationPartialDetailsModel>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task FindPreQualificationByIdandStatus_WithTenderId_ReturnsTenderEntity(
            int qualificationId)
        {
            var result = await _sut.FindPreQualificationByIdandStatus(qualificationId);

            Assert.IsType<Tender>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetSubscriptionAwardingInformation_WithTenderId_ReturnsTenderEntity(
            int qualificationId)
        {
            var result = await _sut.GetSubscriptionAwardingInformation(qualificationId);

            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetQualificationOffersRegistryReportData_WithTenderId_ReturnsTenderEntity(
            int qualificationId)
        {
            var result = await _sut.GetQualificationOffersRegistryReportData(qualificationId);

            Assert.IsType<RegistryReportForQualificationModel>(result);
        }

        //[Fact]
        //public async Task FindPreQualificationBySearchCriteriaForCheckingStage_WithSearchCriteria_ReturnsListOfCardModely()
        //{

        //    PreQualificationSearchCriteriaModel criteria = new PreQualificationSearchCriteriaModel(){cr = "1010000154",BranchId = 12};
        //    var result = await _sut.FindPreQualificationBySearchCriteriaForCheckingStage(criteria);
           
        //    Assert.IsType<QueryResult<PreQualificationCardModel>>(result);
        //}

        //[Fact]
        //public async Task GetAllSupplierPreQualificationsBySearchCriteria_WithSearchCriteria_ReturnsListOfCardModel()
        //{
        //    PreQualificationSearchCriteriaModel criteria = new PreQualificationSearchCriteriaModel();

        //    var result = await _sut.GetAllSupplierPreQualificationsBySearchCriteria(criteria,new List<SupplierAgencyBlockModel>(),t=>t.PreQualification);
         
        //    Assert.IsType<QueryResult<PreQualificationCardModel>>(result);
        //}

        //[Fact]
        //public async Task GetAllVistorQualificationBySearchCriteria_WithSearchCriteria_ReturnsListOfCardModel()
        //{

        //    PreQualificationSearchCriteriaModel criteria = new PreQualificationSearchCriteriaModel();

        //    var result = await _sut.GetAllVistorQualificationBySearchCriteria(criteria);

        //    Assert.IsType<QueryResult<PreQualificationCardModel>>(result);
        //}


        [Theory]
        [InlineData(1)]
        public async Task GetPreQualificationEditingData_WithId_ReturnsQualificationSavingModel(int qualificationId)
        {
            var result = await _sut.GetPreQualificationEditingData(qualificationId);

            Assert.IsType<PreQualificationSavingModel>(result);
        }

        //[Theory]
        //[InlineData(3239232, 0)]
        //public async Task GetPreQualificationDetailsModelById_WithId_ReturnsQualificationDetailsModel(
        //    int qualificationId, int branchId)
        //{
        //    var result = await _sut.GetPreQualificationDetailsModelById(qualificationId,branchId);

        //    Assert.IsType<PreQualificationDetailsModel>(result);
        //}   
        
        [Theory]
        [InlineData(1)]
        public async Task GetPreQualificationStatus_WithId_ReturnsQualificationDetailsModel(
            int qualificationId)
        {
            var result = await _sut.GetPreQualificationStatus(qualificationId);

            Assert.IsType<int>(result);
        } 
        
        [Theory]
        [InlineData(1)]
        public async Task GetPrequalificationDetails_WithId_ReturnsQualificationDetailsModel(
            int qualificationId)
        {
            var result = await _sut.GetPrequalificationDetails(qualificationId);

            Assert.IsType<QualificationStatusModel>(result);
        }
        
        [Theory]
        [InlineData(1,"1010000154")]
        public async Task GetPrequalificationDetailsForSupplier_WithIdAndCR_ReturnsQualificationDetailsModel(
            int qualificationId,string cr)
        {
            var result = await _sut.GetPrequalificationDetailsForSupplier(qualificationId,cr);

            Assert.IsType<QualificationStatusModel>(result);
        }  

        [Theory]
        [InlineData(1)]
        public async Task GetPostQualificationByTenderId_WithId_ReturnsQualificationDetailsModel(
            int qualificationId)
        {
            var result = await _sut.GetPostQualificationByTenderId(qualificationId);

            Assert.Null(result);
        } 
        
        [Theory]
        [InlineData(1)]
        public async Task GetPreQualificationDetailsForPreQualificationApproval_WithId_ReturnsQualificationApprovalModel(
            int qualificationId)
        {
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() =>
                new List<Claim>() {new Claim(IdentityConfigs.Claims.NameIdentifier.ToLower(), "1121")});
            var result = await _sut.GetPreQualificationDetailsForPreQualificationApproval(qualificationId);

            Assert.IsType<PreQulaificationApprovalModel>(result);
        }
        
        //[Fact]
        //public async Task FindPreQualificationByCriteriaForUnderOperationsStage_WithSearchCriteria_ReturnsQualificationApprovalModel(
        //    )
        //{
        //    _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() =>
        //        new List<Claim>() {new Claim(IdentityConfigs.Claims.NameIdentifier.ToLower(), "1121")});

        //    PreQualificationSearchCriteriaModel criteria = new PreQualificationSearchCriteriaModel();


        //    var result = await _sut.FindPreQualificationByCriteriaForUnderOperationsStage(criteria);

        //    Assert.IsType<PreQulaificationApprovalModel>(result);
        //}

        [Theory]
        [InlineData(7)]
        public async Task getQualificationDataToCreatePostQualification_WithTenderId_ReturnsQualificationApprovalModel(
            int tenderid
        )
        {
            var result = await _sut.getQualificationDataToCreatePostQualification(Util.Encrypt(tenderid), null);

            Assert.IsType<PostQualificationApplyDocumentsModel>(result);
        }

        [Theory]
        [InlineData(7)]
        public async Task GetPostQualificationData_WithId_ReturnsQualificationDocumentModel(int tenderid
        )
        {
            var result = await _sut.GetPostQualificationData(tenderid);

            Assert.IsType<PostQualificationApplyDocumentsModel>(result);
        }
        
        [Theory]
        [InlineData(7)]
        public async Task GetPostQualificationByQualificationId_WithId_ReturnsQualificationApprovalModel(int tenderid
        )
        {
            var result = await _sut.GetPostQualificationByQualificationId(tenderid);

            Assert.IsType<PreQulaificationApprovalModel>(result);
        }

        [Theory]
        [InlineData(7)]
        public async Task FindSubCategoryConfiguration_WithId_ReturnsQualificationSubCategory(int tenderid
        )
        {
            var result = await _sut.FindSubCategoryConfiguration(tenderid);

            Assert.IsType<List<QualificationSubCategoryConfiguration>>(result);
        }

        [Theory]
        [InlineData(7)]
        public async Task FindQualificationItems_WithId_ReturnsQualificationTypeCategory(int tenderid
        )
        {
            var result = await _sut.FindQualificationItems(tenderid);

            Assert.IsType<List<QualificationTypeCategory>>(result);
        }


        [Theory]
        [InlineData(7)]
        public async Task GetTenderWithQualificationConfigurations_WithId_ReturnsTenderEntity(int tenderid
        )
        {
            var result = await _sut.GetTenderWithQualificationConfigurations(tenderid);

            Assert.IsType<Tender>(result);
        }  
        
        
        [Theory]
        [InlineData("1010000154",7)]
        public async Task FindFinalResult_WithId_ReturnsQualificationFinalResult(string cr,int tenderid
        )
        {
            var result = await _sut.FindFinalResult(cr,tenderid);

            Assert.IsType<QualificationFinalResult>(result);
        }


        [Theory]
        [InlineData("1010000154", 7)]
        public async Task GetQualificationSupplierDataYear_WithId_ReturnsQualificationDataYear(string cr, int tenderId
        )
        {
            var result = await _sut.GetQualificationSupplierDataYear(tenderId,cr);

            Assert.IsType<List<QualificationSupplierDataYearly>>(result);
        }


        [Theory]
        [InlineData("1010000154", 7)]
        public async Task GetQualificationSupplierData_WithId_ReturnsQualificationSupplierData(string cr, int tenderId
        )
        {
            var result = await _sut.GetQualificationSupplierData(tenderId, cr);

            Assert.IsType<List<QualificationSupplierData>>(result);
        }


        [Theory]
        [InlineData("1010000154", 7)]
        public async Task GetQualificationSupplierProject_WithId_ReturnsQualificationSupplierProject(string cr, int tenderId
        )
        {
            var result = await _sut.GetQualificationSupplierProject(tenderId, cr);

            Assert.IsType<List<QualificationSupplierProject>>(result);
        }


        //[Theory]
        //[InlineData("1010000154", 7)]
        //public async Task FindPreQualificationsModelBySearchCriteria_WithId_ReturnsTenderEntity(string cr, int tenderId
        //)
        //{
        //    _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() =>
        //        new List<Claim>() { new Claim(IdentityConfigs.Claims.NameIdentifier.ToLower(), "1121") });
        //    PreQualificationSearchCriteriaModel criteria = new PreQualificationSearchCriteriaModel();

        //    var result = await _sut.FindPreQualificationsModelBySearchCriteria(criteria,c => c.ChangeRequests);

        //    Assert.IsType<List<QualificationSupplierProject>>(result);
        //}





    }
}
