//using AutoMapper;
//using IdentityModel;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using MOF.Etimad.Monafasat.Core;
//using MOF.Etimad.Monafasat.Core.Entities;
//using MOF.Etimad.Monafasat.Core.Interfaces;
//using MOF.Etimad.Monafasat.Services;
//using MOF.Etimad.Monafasat.Services.Common.NotificationServices;
//using MOF.Etimad.Monafasat.SharedKernel;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace MOF.Etimad.Monafasat.UnitTests.Services.QualificationsAppService
//{
//    public class QualificationApprovementShould
//    {
//        private readonly Mock<IQualificationCommands> _QualificationCommands;
//        private readonly Mock<IQualificationQueries> _QualificationQueres;
//        private readonly Mock<INotificationAppService> _NotificationService;
//        private readonly Mock<IQualificationAppService> _QualificationAppService;
//        private readonly Mock<IQualificationDomainService> _QualificationDomainService;
//        private readonly Mock<IMapper> _mapper;
//        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
//        private readonly Mock<IVerificationService> _Verification;
//        //private readonly Mock<IConfigurationRoot> _configuration;
//        private readonly Mock<IGenericCommandRepository> _genericCommandRepository;
//        private readonly Mock<IBlockAppService> _blockAppService;
//        //   private readonly Mock<ITenderNotification> _tenderNotification;
//        private readonly Mock<ITenderAppService> _moqTenderAppService;
//        private readonly Mock<IFileNetService> _fileNetService;
//        private readonly QualificationAppService _sut;
//        public QualificationApprovementShould()
//        {
//            var configBuilder = new ConfigurationBuilder()
//                                .SetBasePath(@"C:\Projects\Monafasat\Main\Monafasat-V1.0\Src\MOF.Etimad.Monafasat.WebApi")
//                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                                .AddEnvironmentVariables();
//            _QualificationAppService = new Mock<IQualificationAppService>();
//            _QualificationCommands = new Mock<IQualificationCommands>();
//            _QualificationQueres = new Mock<IQualificationQueries>();
//            _NotificationService = new Mock<INotificationAppService>();
//            _Verification = new Mock<IVerificationService>();
//            _QualificationDomainService = new Mock<IQualificationDomainService>();
//            _mapper = new Mock<IMapper>();
//            _httpContextAccessor = new Mock<IHttpContextAccessor>();
//            //  _configuration = new Mock<IConfigurationRoot>();
//            _genericCommandRepository = new Mock<IGenericCommandRepository>();
//            _blockAppService = new Mock<IBlockAppService>();
//            //    _tenderNotification = new Mock<ITenderNotification>();
//            _moqTenderAppService = new Mock<ITenderAppService>();// tenderAppService

//            //private readonly Mock<ITenderAppService> _moqTenderAppService;
//            _fileNetService = new Mock<IFileNetService>();
//            // Arrange
//            _sut = new QualificationAppService(
//                _NotificationService.Object,
//               _QualificationQueres.Object,
//               _QualificationCommands.Object,
//                //    _tenderNotification.Object,
//                _Verification.Object,
//               _QualificationDomainService.Object,
//               _mapper.Object,
//               _httpContextAccessor.Object,
//               _fileNetService.Object,
//               _genericCommandRepository.Object,
//               _blockAppService.Object,
//               configBuilder.Build(),
//               _moqTenderAppService.Object
//                   );
//        }
//        [Theory]
//        [InlineData(4)]
//        public async Task SendQualificationForApprovementAsync(int tenderId)
//        {
//            var context = new Mock<HttpContext>();
//            var claim = new Claim("sub", "15445");
//            var idintity = new GenericIdentity("testUser");
//            idintity.AddClaim(claim);
//            context.SetupGet(x => x.User.Identity).Returns(() =>
//            {
//                return idintity;
//            });
//            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
//            _QualificationQueres.Setup(x => x.GetPreQualificationDetailsByPreQualificationId(It.IsAny<int>()))
//                .Returns(() =>
//              {
//                  return Task.FromResult(new Tender());
//              });
//            _QualificationDomainService.Setup(x => x.IsValidToSendQualificationForApprovement(It.IsAny<Tender>()))
//                 .Returns(() =>
//                 {
//                     return Task.FromResult(false);
//                 });
//            //_tenderNotification.Setup(x => x.SendNotificationForRequestingApproveTenderToAuditor(It.IsAny<Tender>()))
//            //     .Returns(() =>
//            //     {
//            //         return Task.FromResult(false);
//            //     });
//            _QualificationCommands.Setup(x => x.UpdatePreQualificationAsync(It.IsAny<Tender>()))
//                 .Returns((Tender x) =>
//                 {
//                     return Task.FromResult(x);
//                 });
//            var result = await _sut.SendPreQualificationToApprove(tenderId);
//            Assert.NotNull(result);
//            Assert.IsType<Tender>(result);
//            Assert.Equal((int)Enums.TenderStatus.Pending, result.TenderStatusId);
//        }

//        [Theory]
//        [InlineData(4, "4444")]
//        public async Task ApproveQualificationAsync(int tenderId, string verficationCode)
//        {

//            var context = new Mock<HttpContext>();
//            var claim = new Claim("sub", "15445");
//            var idintity = new GenericIdentity("testUser");
//            idintity.AddClaim(claim);

//            context.SetupGet(x => x.User.Identity).Returns(() =>
//            {
//                return idintity;
//            });

//            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);

//            _QualificationQueres.Setup(x => x.GetPreQualificationDetailsByPreQualificationId(It.IsAny<int>()))
//                .Returns(() =>
//                {
//                    return Task.FromResult(new Tender());
//                });
//            _QualificationDomainService.Setup(x => x.IsValidToApproveQualification(It.IsAny<Tender>()))
//                 .Returns(() =>
//                 {
//                     return Task.FromResult(true);
//                 });
//            //_tenderNotification.Setup(x => x.SendNotificationForAcceptingApprovingQualificationToDataEntry(It.IsAny<Tender>()))
//            //     .Returns(() =>
//            //     {
//            //         return Task.FromResult(false);
//            //     });
//            _QualificationCommands.Setup(x => x.UpdatePreQualificationAsync(It.IsAny<Tender>()))
//                 .Returns((Tender x) =>
//                 {
//                     return Task.FromResult(x);
//                 });
//            var result = await                                 _sut.ApprovePreQualification(tenderId, verficationCode,1);
//            Assert.NotNull(result);
//            Assert.IsType<Tender>(result);
//            Assert.Equal((int)Enums.TenderStatus.Approved, result.TenderStatusId);
//        }
//        [Theory]
//        [InlineData(4, "4444")]
//        public async Task RejectApproveQualificationAsync(int tenderId, string rejectionReason)
//        {

//            var context = new Mock<HttpContext>();
//            var claim = new Claim("sub", "15445");
//            var idintity = new GenericIdentity("testUser");
//            idintity.AddClaim(claim);

//            context.SetupGet(x => x.User.Identity).Returns(() =>
//            {
//                return idintity;
//            });

//            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);

//            _QualificationQueres.Setup(x => x.GetPreQualificationDetailsByPreQualificationId(It.IsAny<int>()))
//                .Returns(() =>
//                {
//                    return Task.FromResult(new Tender());
//                });
//            _QualificationDomainService.Setup(x => x.IsValidToRejectQualification(It.IsAny<Tender>()))
//                 .Returns(() =>
//                 {
//                     return Task.FromResult(true);
//                 });
//            //_tenderNotification.Setup(x => x.SendNotificationFoRejectingApprovingQualificationToDataEntry(It.IsAny<Tender>()))
//            //     .Returns(() =>
//            //     {
//            //         return Task.FromResult(false);
//            //     });
//            _QualificationCommands.Setup(x => x.UpdatePreQualificationAsync(It.IsAny<Tender>()))
//                 .Returns((Tender x) =>
//                 {
//                     return Task.FromResult(x);
//                 });
//            var result = await _sut.RejectApprovePreQualification(tenderId, rejectionReason);
//            Assert.NotNull(result);
//            Assert.IsType<Tender>(result);
//            Assert.Equal((int)Enums.TenderStatus.Rejected, result.TenderStatusId);
//        }
//        [Theory]
//        [InlineData(4)]
//        public async Task ReopenPreQualificationAsync(int tenderId)
//        {

//            var context = new Mock<HttpContext>();
//            var claim = new Claim("sub", "15445");
//            var idintity = new GenericIdentity("testUser");
//            idintity.AddClaim(claim);

//            context.SetupGet(x => x.User.Identity).Returns(() =>
//            {
//                return idintity;
//            });

//            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);

//            _QualificationQueres.Setup(x => x.GetPreQualificationDetailsByPreQualificationId(It.IsAny<int>()))
//                .Returns(() =>
//                {
//                    return Task.FromResult(new Tender());
//                });
//            _QualificationDomainService.Setup(x => x.IsValidToReopenQualification(It.IsAny<Tender>()))
//                 .Returns(() =>
//                 {
//                     return Task.FromResult(true);
//                 });
//            _QualificationCommands.Setup(x => x.UpdatePreQualificationAsync(It.IsAny<Tender>()))
//                 .Returns((Tender x) =>
//                 {
//                     return Task.FromResult(x);
//                 });
//            var result = await _sut.ReopenPreQualification(tenderId);
//            Assert.NotNull(result);
//            Assert.IsType<Tender>(result);
//            Assert.Equal((int)Enums.TenderStatus.Established, result.TenderStatusId);
//        }


//    }
//}
