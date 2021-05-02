using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.IDMDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Block
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class BlockAppServiceTest
    {
        private readonly AppDbContext _moqAppDbContext;
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;

        private readonly Mock<INotificationAppService> _moqnotificationappService;
        private readonly Mock<ISupplierCommands> _moqSupplierCommands;
        private readonly Mock<IYasserProxy> _yesserProxy;
        private readonly Mock<ISupplierQueries> _moqSupplierQueries;
        private readonly Mock<IIDMAppService> _moqIDMApp;
        private readonly Mock<IMapper> _moqMappCommands;
        private readonly Mock<IIDMQueries> _moqIDMQueries;
        private readonly Mock<IVerificationService> _moqVerificationCodeService;
        private readonly Mock<INotificationQueries> _moqNotificationsQuery;
        private readonly BlockAppService _sut;
        private readonly Mock<IBlockQueries> _moqBlockQueries;
        private readonly Mock<IBlockCommands> _moqBlockCommands;
        public BlockAppServiceTest()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase(databaseName: "TestingDB")
                 .Options;
            _httpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _httpContext.Object);


            //Mock

            _moqIDMApp = new Mock<IIDMAppService>();
            _moqMappCommands = new Mock<IMapper>();
            _yesserProxy = new Mock<IYasserProxy>();
            _moqnotificationappService = new Mock<INotificationAppService>();
            _moqSupplierCommands = new Mock<ISupplierCommands>();
            _moqSupplierCommands = new Mock<ISupplierCommands>();
            _moqSupplierQueries = new Mock<ISupplierQueries>();
            _moqIDMQueries = new Mock<IIDMQueries>();
            _moqVerificationCodeService = new Mock<IVerificationService>();
            _moqNotificationsQuery = new Mock<INotificationQueries>();
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _moqBlockQueries = new Mock<IBlockQueries>();
            _moqBlockCommands = new Mock<IBlockCommands>();
            _sut = new BlockAppService(
                 _moqAppDbContext,
                _moqnotificationappService.Object,
                _moqMappCommands.Object,
                _moqIDMApp.Object,
                _moqBlockQueries.Object,
                _moqBlockCommands.Object,
                _moqCommandRepository.Object,
                _yesserProxy.Object,
                _moqSupplierQueries.Object,
                _moqIDMQueries.Object,
                _moqVerificationCodeService.Object,
                _moqNotificationsQuery.Object
                );
        }

        #region Commands======================================
        [Fact]
        public async Task ShouldAddBlockAsyn()
        {
            //=== arrange=========
            var _blockObj = new SupplierBlockDefaults().ReturnSupplierBlockDefaults();


            //============Return Result===========
            _moqCommandRepository.Setup(m => m.CreateAsync(_blockObj)).Returns<SupplierBlock>(r =>
            {
                return Task.FromResult(_blockObj);
            });
            var result = await _sut.AddBlockAsyn(_blockObj);
            _moqCommandRepository.Verify(m => m.CreateAsync(_blockObj), Times.Once);

            //_moqno
            //====== Assert =====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
            //Assert.
        }

        [Fact]
        public async Task ShouldUpdateBlockAsync()
        {
            ////Mock
            _moqBlockCommands.Setup(x => x.UpdateAsync(It.IsAny<SupplierBlock>()))
                             .Returns((SupplierBlock obj) =>
                             {
                                 return Task.FromResult<SupplierBlock>(obj);
                             });

            _moqBlockQueries.Setup(x => x.FindBlockByIdAsync(It.IsAny<int>()))
                          .Returns(() =>
                          {
                              return Task.FromResult<SupplierBlock>(new SupplierBlock());
                          });


            //=== arrange=========
            var _blockObj = new SupplierBlockModel()
            {
                SupplierBlockId = 5,
                CommercialRegistrationNo = "SelectedCr1",
                CommercialSupplierName = "SelectedCrName",
                AgencyCode = "5",
                BlockDetails = "BlockDetails",
                FileName = "FileName",
                BlockEndDate = DateTime.Now,
                BlockStartDate = DateTime.Now,
                BlockTypeId = 1,
                Punishment = "",
                ResolutionNumber = "456",
                Violation = "Violation"
            };
            var result = await _sut.UpdateBlockAsync(_blockObj);



            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public async Task ShouldDeactivateBlockAsyn(int supplierBlockId)
        {
            //Mock
            _moqBlockCommands.Setup(x => x.UpdateAsync(It.IsAny<SupplierBlock>()))
                             .Returns((SupplierBlock obj) =>
                             {
                                 return Task.FromResult<SupplierBlock>(obj);
                             });

            _moqBlockQueries.Setup(x => x.FindBlockByIdAsync(It.IsAny<int>()))
                          .Returns(() =>
                          {
                              return Task.FromResult<SupplierBlock>(new SupplierBlock() { Supplier = new Supplier() });
                          });

            //============Return Result===========
            var result = await _sut.DeactivateBlockAsyn(supplierBlockId);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }
        [Fact]
        public async Task ShouldGetManagerBlockList()
        {
            //Mock
            _moqBlockQueries.Setup(x => x.FindManagerBlockList(It.IsAny<BlockSearchCriteria>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult(new QueryResult<SupplierBlockModel>(new List<SupplierBlockModel> { new SupplierBlockModel() }, 1, 1, 1));
                             });

            //=== arrange=========
            var _blockObj = new BlockSearchCriteria();

            //============Return Result===========
            var result = await _sut.GetManagerBlockList(_blockObj);


            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
        }

        #endregion ===========================================

        #region Commands New ======================================
        [Fact]
        public async Task ShouldGetAllSuppliers()
        {
            //Mock
            _moqIDMApp.Setup(x => x.GetSupplierDetailsBySearchCriteria(It.IsAny<SupplierIntegrationSearchCriteria>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult(new QueryResult<SupplierIntegrationModel>(new List<SupplierIntegrationModel> { new SupplierIntegrationModel() }, 1, 1, 1));
                             });

            //=== arrange=========
            var _blockObj = new SupplierIntegrationSearchCriteria();

            //============Return Result===========
            var result = await _sut.GetAllSuppliers(_blockObj);


            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierIntegrationModel>>(result);
        }

        [Fact]
        public async Task ShouldGetAdminBlockList()
        {
            //Mock
            _moqBlockQueries.Setup(x => x.FindAdminBlockList(It.IsAny<BlockSearchCriteria>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult(new QueryResult<SupplierBlockModel>(new List<SupplierBlockModel> { new SupplierBlockModel() }, 1, 1, 1));
                             });

            //=== arrange=========
            var _blockObj = new BlockSearchCriteria();

            //============Return Result===========
            var result = await _sut.GetAdminBlockList(_blockObj);


            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ShouldAddAdminBlock(bool isSecretaryNotify)
        {            
            _moqBlockQueries.Setup(q => q.FindBlockedUser(It.IsAny<BlockSearchCriteria>())).Returns(() =>
            {
                return Task.FromResult(new QueryResult<SupplierBlockModel>(new List<SupplierBlockModel> { }, 0, 1, 10));

            });

            var _blockObj = new SupplierBlockModel() { CommercialRegistrationNo = "789645", CommercialSupplierName = "789645312", AdminBlockReason = "8746512",IsSecretaryNotify= isSecretaryNotify };
            _moqCommandRepository.Setup(m => m.CreateAsync(It.IsAny<SupplierBlock>())).Returns<SupplierBlock>(r =>
            {
                return Task.FromResult(new SupplierBlockDefaults().ReturnSupplierBlockDefaults());
            });

            var result = await _sut.AddAdminBlock(_blockObj);

            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
            GeneralFields.supplierBlockId = result.SupplierBlockId;
        }



        [Fact]
        public async Task ShouldAddAdminBlockWithUserNotificationSettings()
        {

            _moqBlockQueries.Setup(q => q.FindBlockedUser(It.IsAny<BlockSearchCriteria>())).Returns(() =>
            {
                return Task.FromResult(new QueryResult<SupplierBlockModel>(new List<SupplierBlockModel> { }, 0, 1, 10));

            });

            _moqSupplierQueries.Setup(q => q.FindSupplierByCRNumber(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult<Supplier>(new IDMDefaults().GetSupplierData());
            });

            _moqnotificationappService.Setup(m => m.GetDefaultSettingByCr())
                .Returns(() =>
                {
                    return Task.FromResult<List<UserNotificationSetting>>(new BranchUserDefaults().GetUserNotificationSettings());
                });

            var _blockObj = new SupplierBlockModel() { CommercialRegistrationNo = "789645", CommercialSupplierName = "789645312", AdminBlockReason = "8746512"};

            _moqCommandRepository.Setup(m => m.CreateAsync(It.IsAny<SupplierBlock>())).Returns<SupplierBlock>(r =>
            {
                return Task.FromResult(new SupplierBlockDefaults().ReturnSupplierBlockDefaults());
            });

            var result = await _sut.AddAdminBlock(_blockObj);

            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
            GeneralFields.supplierBlockId = result.SupplierBlockId;
        }

        [Fact, Priority(-9)]

        public async Task ShouldSecretaryApproval()
        {
            _moqBlockQueries.Setup(x => x.FindSupplierBlockById(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return new SupplierBlock();
                             });

            _moqBlockCommands.Setup(x => x.UpdateAsync(It.IsAny<SupplierBlock>()))
                       .Returns((SupplierBlock obj) =>
                       {
                           return Task.FromResult<SupplierBlock>(obj);
                       });
            //============Return Result===========
            var result = await _sut.SecretaryApproval(GeneralFields.supplierBlockId);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }


        [Fact, Priority(-8)]
        public async Task ShouldAddSecretaryBlock()
        {
            _moqBlockQueries.Setup(x => x.FindBlockById(It.IsAny<int>()))
                           .Returns(() =>
                           {
                               SupplierBlockModel obj = new SupplierBlockModel();
                               return Task.FromResult<SupplierBlockModel>(obj);
                           });
            _moqBlockCommands.Setup(x => x.UpdateAsync(It.IsAny<SupplierBlock>()))
                       .Returns((SupplierBlock obj) =>
                       {
                           return Task.FromResult<SupplierBlock>(obj);
                       });
            _moqBlockQueries.Setup(x => x.FindBlockByIdAsync(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult<SupplierBlock>(new SupplierBlock());
                             });
            //=== arrange=========
            var _blockObj = new SupplierBlockModel() { SupplierBlockId = GeneralFields.supplierBlockId };

            //============Return Result===========
            var result = await _sut.AddSecretaryBlock(_blockObj);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }

        [Fact]
        public async Task ShouldManagerRejectionReason()
        {
            //Mock
            _moqBlockQueries.Setup(x => x.FindSupplierBlockById(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return new SupplierBlock();
                             });
            _moqBlockCommands.Setup(x => x.UpdateAsync(It.IsAny<SupplierBlock>()))
                       .Returns((SupplierBlock obj) =>
                       {
                           return Task.FromResult<SupplierBlock>(obj);
                       });
            _moqBlockQueries.Setup(x => x.GetAgencyById(It.IsAny<string>()))
                          .Returns(() =>
                          {
                              return Task.FromResult(new GovAgency());
                          });
            _moqIDMApp.Setup(x => x.GetEmployeeDetailsByRoleName(It.IsAny<string>()))
                         .Returns(() =>
                         {
                             return Task.FromResult<List<EmployeeIntegrationModel>>(new List<EmployeeIntegrationModel>());
                         });

            //Param
            int supplierBlockId = 0; string reason = "";

            //============Return Result===========
            var result = await _sut.ManagerRejectionReason(supplierBlockId, reason);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }

        [Fact]
        public async Task ShouldSecretaryRejectionReason()
        {
            //Mock
            _moqBlockQueries.Setup(x => x.FindSupplierBlockById(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return new SupplierBlock();
                             });
            _moqBlockCommands.Setup(x => x.UpdateAsync(It.IsAny<SupplierBlock>()))
                         .Returns((SupplierBlock obj) =>
                         {
                             return Task.FromResult<SupplierBlock>(obj);
                         });
            //Param
            int supplierBlockId = 0; string reason = "";

            //============Return Result===========
            var result = await _sut.SecretaryRejectionReason(supplierBlockId, reason);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }

        [Fact, Priority(-7)]
        public async Task ShouldManagerApproval()
        {
            _moqVerificationCodeService.Setup(x => x.CheckForVerificationCode(It.IsAny<int>(), It.IsAny<string>(),It.IsAny<int>()))
                          .Returns(() =>
                          {
                              return Task.FromResult<bool>(true);
                          });
            _moqBlockQueries.Setup(x => x.FindSupplierBlockById(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return new SupplierBlockDefaults().ReturnSupplierBlockDefaults();
                             });

            _moqBlockQueries.Setup(x => x.GetAgencyById(It.IsAny<string>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult(new GovAgency());
                             });
            _moqBlockCommands.Setup(x => x.UpdateAsync(It.IsAny<SupplierBlock>()))
                          .Returns((SupplierBlock obj) =>
                          {
                              return Task.FromResult<SupplierBlock>(obj);
                          });
            _moqIDMApp.Setup(x => x.GetEmployeeDetailsByRoleName(It.IsAny<string>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<EmployeeIntegrationModel>>(new List<EmployeeIntegrationModel>());
               });

            var result = await _sut.ManagerApproval(GeneralFields.supplierBlockId, "768");

            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }


        #endregion

        #region Queries =========================================

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public async Task ShouldFindBlockByIdAsync(int supplierBlockId)
        {
            //Mock
            _moqBlockQueries.Setup(x => x.FindBlockByIdAsync(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult<SupplierBlock>(new SupplierBlock());
                             });

            //============Return Result===========
            var result = await _sut.FindBlockByIdAsync(supplierBlockId);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }

        [Fact]
        public async Task ShouldFindAsync()
        {
            //Mock
            _moqBlockQueries.Setup(x => x.Find(It.IsAny<BlockSearchCriteria>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult(new QueryResult<SupplierBlockModel>(new List<SupplierBlockModel> { new SupplierBlockModel() }, 1, 1, 1));
                             });

            //=== arrange=========
            var _blockObj = new BlockSearchCriteria();

            //============Return Result===========
            var result = await _sut.FindAsyn(_blockObj);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
        }

        [Fact]
        public async Task ShouldGetAllSupplierBlocksByAgencyANdCrs()
        {
            string agencyCode = ""; List<string> CRs = new List<string>();

            //Mock
            _moqBlockQueries.Setup(x => x.GetAllCurrentBlockedSuppliers(It.IsAny<string>(), It.IsAny<List<string>>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult(new List<SupplierAgencyBlockModel>(new List<SupplierAgencyBlockModel> { new SupplierAgencyBlockModel() }));

                             });

            //============Return Result===========
            var result = await _sut.GetAllSupplierBlocks(agencyCode, CRs);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<List<SupplierAgencyBlockModel>>(result);
        }

        [Fact]
        public async Task ShouldGetAllBlockedSuppliersByAgencyANdCrs()
        {
            string agencyCode = ""; List<string> CRs = new List<string>();

            //Mock
            _moqBlockQueries.Setup(x => x.GetAllBlockedSuppliers(It.IsAny<string>(), It.IsAny<List<string>>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult(new List<SupplierAgencyBlockModel>(new List<SupplierAgencyBlockModel> { new SupplierAgencyBlockModel() }));

                             });

            //============Return Result===========
            var result = await _sut.GetAllBlockedSuppliers(agencyCode, CRs);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<List<SupplierAgencyBlockModel>>(result);
        }

        [Theory]
        [InlineData("4")]
        [InlineData("5")]
        [InlineData("6")]
        [InlineData("7")]
        public async Task ShouldCheckifSupplierBlockedByCrNo(string commericalRegisterNo)
        {
            //Mock
            _moqBlockQueries.Setup(x => x.CheckifSupplierBlockedByCrNo(It.IsAny<string>(), It.IsAny<string>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult<bool>(true);
                             });

            //============Return Result===========
            var result = await _sut.CheckifSupplierBlockedByCrNo(commericalRegisterNo);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
        }



        #endregion
        #region Queries New ==============================================






        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public async Task ShouldFindBlockById(int supplierBlockId)
        {

            //Mock
            _moqBlockQueries.Setup(x => x.FindBlockById(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult<SupplierBlockModel>(new SupplierBlockModel());
                             });

            //============Return Result===========
            var result = await _sut.FindBlockById(supplierBlockId);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlockModel>(result);
        }





        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public async Task ShouldFindSupplierBlockById(int supplierBlockId)
        {

            //Mock
            _moqBlockQueries.Setup(x => x.FindSupplierBlockById(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return new SupplierBlock();
                             });

            //============Return Result===========
            var result = _sut.FindSupplierBlockById(supplierBlockId);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }

        [Fact]
        public async Task ShouldGetAllSupplierBlocks()
        {
            //Mock
            _moqBlockQueries.Setup(x => x.Find(It.IsAny<BlockSearchCriteria>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult(new QueryResult<SupplierBlockModel>(new List<SupplierBlockModel> { new SupplierBlockModel() }, 1, 1, 1));

                             });

            //=== arrange=========
            var _blockObj = new BlockSearchCriteria();

            //============Return Result===========
            var result = await _sut.FindAsyn(_blockObj);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
        }



        #endregion ==============================================
    }
}
