using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.QualificationDocument
{
    public class QualificationDocumentAppServiceTest
    {
        private readonly Mock<ISupplierQualificationDocumentDomainService> _MoqQualificationDomainService;
        private readonly Mock<ISupplierQualificationDocumentAppService> _MoqQualificationAppService;
        //   private readonly Mock<IPreQualificationNotification> _NotificationAppService;
        private readonly Mock<ISupplierQualificationDocumentCommands> _MoqQualificationdocumentcommands;
        private readonly Mock<ISupplierQualificationDocumentQueries> _MoqSupplierqualificationdocumentqueries;
        private readonly Mock<INotificationAppService> _moqNotificationAppService;
        private readonly Mock<ISupplierQualificationDomainService> _moqSupplierQualificationDomainService;

        private readonly Mock<IAppDbContext> _moqAppDbContext;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private readonly Mock<IQualificationQueries> _moqQualificationQueries;
        private readonly Mock<IQualificationCommands> _moqQualificationCommands;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;


        private readonly SupplierQualificationDocumentAppService _sut;

        public QualificationDocumentAppServiceTest()
        {
            //Mock
            _MoqQualificationAppService = new Mock<ISupplierQualificationDocumentAppService>();
            _MoqQualificationDomainService = new Mock<ISupplierQualificationDocumentDomainService>();

            _MoqQualificationdocumentcommands = new Mock<ISupplierQualificationDocumentCommands>();
            _MoqSupplierqualificationdocumentqueries = new Mock<ISupplierQualificationDocumentQueries>();
            _moqNotificationAppService = new Mock<INotificationAppService>();
            _moqAppDbContext = new Mock<IAppDbContext>();
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _moqSupplierQualificationDomainService = new Mock<ISupplierQualificationDomainService>();
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _moqQualificationQueries = new Mock<IQualificationQueries>();
            _moqQualificationCommands = new Mock<IQualificationCommands>();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfigurationMock.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForQualification());


            _sut = new SupplierQualificationDocumentAppService(
                _moqNotificationAppService.Object,
                _MoqQualificationDomainService.Object,
                _MoqQualificationdocumentcommands.Object,
                _MoqSupplierqualificationdocumentqueries.Object,
                _moqSupplierQualificationDomainService.Object,
                _moqHttpContextAccessor.Object,
                _moqQualificationQueries.Object,
                _moqQualificationCommands.Object,
                _rootConfigurationMock.Object
                );

        }

        #region ApplyQualificationDocuments

        [Fact]
        public async Task ShouldEvaluateSupplierSmallQualification()
        {
             MockApplyQualificationData(Enums.PreQualificationType.Small);
             var _subQualificationObj = new QualificationDefault().GetApplyDocumentsModelSmallQulaification();

            var result = await _sut.ApplyDocsforSupplier(_subQualificationObj, false);

            Assert.NotNull(result);
            Assert.IsType<SupplierPreQualificationDocument>(result);
        }
        [Fact]
        public async Task ShouldEvaluateSupplierMediumQualification()
        {
            MockApplyQualificationData(Enums.PreQualificationType.Medium);
            var _subQualificationObj = new QualificationDefault().GetApplyDocumentsModelMediumQulaification();

            var result = await _sut.ApplyDocsforSupplier(_subQualificationObj, false);

            Assert.NotNull(result);
            Assert.IsType<SupplierPreQualificationDocument>(result);
        }

        [Fact]
        public async Task ShouldEvaluateSupplierLargeQualification()
        {
            MockApplyQualificationData(Enums.PreQualificationType.Large);
            var _subQualificationObj = new QualificationDefault().GetApplyDocumentsModelLargeQulaification();

            var result = await _sut.ApplyDocsforSupplier(_subQualificationObj, false);

            Assert.NotNull(result);
            Assert.IsType<SupplierPreQualificationDocument>(result);
        }

        [Fact]
        public async Task ShouldRemovePreviousQualification()
        {
            MockApplyQualificationData(Enums.PreQualificationType.Large);
            MoqRemoverPreviouseQualificationData();

            var _subQualificationObj = new QualificationDefault().GetApplyDocumentsModelLargeQulaification();
            _subQualificationObj.EditMode = true;

            var result = await _sut.ApplyDocsforSupplier(_subQualificationObj, false);

            Assert.NotNull(result);
            Assert.IsType<SupplierPreQualificationDocument>(result);
        }

        private void MoqRemoverPreviouseQualificationData()
        {
            _MoqSupplierqualificationdocumentqueries
                .Setup(x => x.GetSuppierDocument(It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
                {
                    return Task.FromResult(
                        new QualificationDefault().GetSupplierPreQualificationDocument());
                });
            _moqQualificationQueries
                .Setup(x => x.GetQualificationSupplierDataYear(It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
                {
                    return Task.FromResult(
                        new QualificationDefault().GetQualificationSupplierDataYearlyLsit());
                });
            _moqQualificationQueries
                .Setup(x => x.GetQualificationSupplierProject(It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
                {
                    return Task.FromResult(
                        new QualificationDefault().GetQualificationSupplierProjects());
                });
            _moqQualificationQueries
                .Setup(x => x.GetQualificationSupplierData(It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
                {
                    return Task.FromResult(
                        new QualificationDefault().GetQualificationSupplierDataList());
                });

            _moqQualificationQueries
                .Setup(x => x.FindFinalResult(It.IsAny<string>(), It.IsAny<int>())).Returns(() =>
                {
                    return Task.FromResult(
                        new QualificationDefault().GetQualificationFinalResult());
                });
        }

        private void MockApplyQualificationData(Enums.PreQualificationType preQualificationType)
        {
            _MoqQualificationdocumentcommands.Setup(x => x.CreateAsync(It.IsAny<SupplierPreQualificationDocument>()))
                .Returns((SupplierPreQualificationDocument obj) =>
                {
                    return Task.FromResult<SupplierPreQualificationDocument>(obj);
                });

            _moqQualificationQueries.Setup(x => x.GetPreQualificationDetailsById(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(
                    new QualificationDefault().GetGeneralTenderWithQualificationTypeId((int) preQualificationType));
            });
            _MoqSupplierqualificationdocumentqueries.Setup(x => x.GetPreQualificationById(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(
                    new QualificationDefault().GetGeneralTenderWithQualificationTypeId((int) preQualificationType));
            });

            _moqQualificationQueries.Setup(x => x.FindQualificationItems(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<List<QualificationTypeCategory>>(new QualificationDefault()
                    .GetQualificationTypeCategory());
            });

            _moqQualificationQueries.Setup(x => x.FindSubCategoryConfiguration(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<List<QualificationSubCategoryConfiguration>>(new QualificationDefault()
                    .GeQualificationSubCategoryConfigurations());
            });
        }
        #endregion ApplyQualificationDocuments

        [Theory]
        [InlineData(1)]
        public async Task ShouldFindById(int preQualificationId)
        {
            _MoqSupplierqualificationdocumentqueries
                .Setup(x => x.FindSupplierPreQualificationDocumentById(It.IsAny<int>())).Returns(
                    () =>
                    {
                        return Task.FromResult(new QualificationDefault().GetSupplierPreQualificationDocument());
                    });
            MoqUser();

            var result = await _sut.FindById(preQualificationId);

            Assert.NotNull(result);

        }  
        [Theory]
        [InlineData(1)]
        public async Task ShouldGetQualificationSupplierData(int preQualificationId)
        {
            _MoqSupplierqualificationdocumentqueries
                .Setup(x => x.GetQualificationSupplierData(It.IsAny<int>())).Returns(
                    () =>
                    {
                        return Task.FromResult(new List<QualificationSupplierDataModel>());
                    });

            var result = await _sut.GetQualificationSupplierData(preQualificationId);

            Assert.NotNull(result);

        }



        [Theory]
        [InlineData(555, "5956275283")]
        public async Task ShouldGetQualificationSupplierDataForEdit(int PreQualificationId, string supplierId)
        {
            _MoqSupplierqualificationdocumentqueries.Setup(x => x.GetQualificationSupplierDataForEdit(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int x, string y) =>
                {
                    return Task.FromResult(new List<QualificationSupplierDataModel>());
                });

            var result = await _sut.GetQualificationSupplierDataForEdit(PreQualificationId, supplierId);

            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(555, "5956275283", false)]
        public async Task ShouldCheckSuppierDocument(int PreQualificationId, string supplierId, bool editMode)
        {
            _MoqQualificationDomainService
                .Setup(x => x.CheckApplyingDuplicationValidation(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int x, string y) => { return Task.CompletedTask; });

            var result = await _sut.CheckSuppierDocument(PreQualificationId, supplierId, editMode);

            Assert.Null(result);
        }


        [Fact]
        public async Task ShouldUpdateSupplierPreQualificationDocumentStatus()
        {
            MoqUser();
            _MoqSupplierqualificationdocumentqueries
                .Setup(x => x.FindSupplierPreQualificationDocumentById(It.IsAny<int>())).Returns(
                    () =>
                    {
                        return Task.FromResult(new QualificationDefault().GetSupplierPreQualificationDocument());
                    });
            _MoqQualificationDomainService
                .Setup(x => x.CheckApplyingDuplicationValidation(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int x, string y) => { return Task.CompletedTask; });
            SupplierPreQualificationDocumentModel supplierPreQualificationDocumentModel =
                new SupplierPreQualificationDocumentModel();
            supplierPreQualificationDocumentModel.SupplierPreQualificationDocumentId = 1;
            supplierPreQualificationDocumentModel.PreQualificationResult = 14;

            var result = await _sut.UpdateSupplierPreQualificationDocumentStatus(supplierPreQualificationDocumentModel);

            Assert.Null(result);
        }


        [Fact]
        public async Task ShouldPostQualificationChecking()
        {
            MoqUser();
            _MoqSupplierqualificationdocumentqueries
                .Setup(x => x.FindSupplierPreQualificationDocumentById(It.IsAny<int>())).Returns(
                    () =>
                    {
                        return Task.FromResult(new QualificationDefault().GetSupplierPreQualificationDocument());
                    });
            _MoqQualificationDomainService
                .Setup(x => x.CheckApplyingDuplicationValidation(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int x, string y) => { return Task.CompletedTask; });
            SupplierPreQualificationDocumentModel supplierPreQualificationDocumentModel =
                new SupplierPreQualificationDocumentModel();
            supplierPreQualificationDocumentModel.SupplierPreQualificationDocumentId = 1;
            supplierPreQualificationDocumentModel.PreQualificationResult = 14;

            var result = await _sut.PostQualificationChecking(supplierPreQualificationDocumentModel);

            Assert.Null(result);
        }

        private void MoqUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var committeeId = new Claim(IdentityConfigs.Claims.CommitteeId, "1");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            idintity.AddClaim(committeeId);

            context.SetupGet(x => x.User.Identity).Returns(() => { return idintity; });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
        }
    }
}
