using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class YesserProxyTest
    {
        private readonly YasserProxy _YesserProxy;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly RootConfigurations _configuration;

        public YesserProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());

            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _YesserProxy = new YasserProxy(_rootConfiguration.Object);
        }

        [Fact]
        public async Task ShouldValidateMCICRAndGetInfo()
        {
            //Arrange
            var model = new MCICRInfoModelRequest { CommercialRegistrationNumber = "1010274503" };
            //Act
            var result = await _YesserProxy.ValidateMCICRAndGetInfo(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldValidateMCICRAndGetInfoWithOwners()
        {
            //Arrange
            var model = new MCICRInfoModelRequest { CommercialRegistrationNumber = "1010274503" };
            //Act
            var result = await _YesserProxy.ValidateMCICRAndGetInfoWithOwners(model);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetCOCSubscriptionBySijilNumber()
        {
            //Arrange
            var model = new COCSubscriptionInquiryRequestModel { CityCode = "101", LicenseNumber = "777777" };
            //Act
            var result = await _YesserProxy.GetCOCSubscriptionBySijilNumber(model);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldZakatCertificateInquiryByCR()
        {
            //Arrange
            var model = new ZakatCertificateInquiryRequestModel { CommercialRegistrationNumber = "1010274503" };
            //Act
            var result = await _YesserProxy.ZakatCertificateInquiryByCR(model);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldGOSICertificateInquiry()
        {
            //Arrange
            var model = new GOSICertificateInquiryRequestModel
            {
                GOSIId = "13253331",
                GOSIIdType = GOSIIdType.GosiRegistrationID
                ,
                GOSIOwnerPersonIdType = GOSIOwnerPersonIdType.NationalID
            };
            //Act
            var result = await _YesserProxy.GOSICertificateInquiry(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldContractorDetailsInquiry()
        {
            //Arrange
            var model = new ContractorDetailsRequestModel { PartyId = new PartyIdModel { PartyIdNumber = "1010274503", PartyIdType = PartyType.NationalId } };
            //Act
            var result = await _YesserProxy.ContractorDetailsInquiry(model);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldNitaqatInformationInquiry()
        {
            //Arrange
            var model = new NitaqatInformationRequestModel { EstablishmentLaborOfficeId = "1", EstablishmentSequenceNumber = "290901" };
            //Act
            var result = await _YesserProxy.NitaqatInformationInquiry(model);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldLicenseStatusInquiry()
        {
            //Arrange
            var model = new LicenseDetailsRequestModel { LicenseNumber = "777777" };
            //Act
            var result = await _YesserProxy.LicenseStatusInquiry(model);
            //Assert
            Assert.NotNull(result);
        }


        // to do dont remove below code

        //public class YesserProxyserviceTest
        //{
        //    #region ----------------Private Member ------------------

        //    private readonly IYasserProxy _yesserProxy = ProxyResolver.Resolve<IYasserProxy>();

        //    protected string Port { get { return _configuration.GetSection("EsbSettings:PortValue").Value; } }

        //    protected readonly string ChannelID = "MNFST";
        //    protected readonly string Version = "1.0";

        //    protected virtual string ServiceID { get; }
        //    protected virtual string FunctionID { get; }

        //    protected string RequestID { get => GetNewRequestID(); }
        //    protected string ClientDate { get => GetCurrentDate(); }
        //    private readonly string _mciCRValidationAddress;
        //    private readonly string _cocSubscriptionInquiryAddress;
        //    private readonly string _zakatCertificateInquiryAddress;
        //    private readonly string _gosiCertificateInquiryAddress;
        //    private readonly string _nitaqatInformationInquiryClientAddress;
        //    private readonly string _momraContractorClassificationService;
        //    private readonly string _licenseDetailsServiceAddress;

        //    private readonly string _clientCertificateValue;
        //    private readonly bool _isProduction;

        //    private IConfigurationRoot _configuration;

        //    #endregion

        //    #region constructor
        //    public YesserProxyserviceTest()
        //    {
        //        _configuration = new ConfigurationBuilder()
        //           .SetBasePath(Directory.GetCurrentDirectory())
        //           .AddJsonFile("appsettings.json")
        //           .Build();

        //        _mciCRValidationAddress = _configuration.GetSection("Services:MCICRValidationService").Value;
        //        _cocSubscriptionInquiryAddress = _configuration.GetSection("Services:COCSubscriptionInquiryService").Value;
        //        _zakatCertificateInquiryAddress = _configuration.GetSection("Services:ZakatCertificateInquiryService").Value;
        //        _gosiCertificateInquiryAddress = _configuration.GetSection("Services:GOSICertificateInquiryService").Value;
        //        _nitaqatInformationInquiryClientAddress = _configuration.GetSection("Services:NitaqatInformationInquiryService").Value;
        //        _momraContractorClassificationService = _configuration.GetSection("Services:MOMRAContractorClassificationService").Value;
        //        _licenseDetailsServiceAddress = _configuration.GetSection("Services:LicenseDetailsInquiryService").Value;

        //        _clientCertificateValue = _configuration.GetSection("EsbSettings:ClientCertificateFindValue").Value;
        //        _isProduction = bool.Parse(_configuration.GetSection("EsbSettings:IsProduction").Value);
        //    }
        //    #endregion

        //    #region FACTS 

        //    [Fact]
        //    public async Task ShouldGetZakatCertificateInquiryByCrAsync()
        //    {
        //        var service = GetZakatCertificateInquiryServiceClient();
        //        var request = new ZakatCertificateInquiryService.ZakatCertificateInquiryRq_Type
        //        {
        //            MsgRqHdr = ZakatCertificateInquiryByCrFillHeader()
        //        };

        //        request.Body = new ZakatCertificateInquiryService.ZakatCertificateInquiryRqBody_Type()
        //        {
        //            ItemElementName = ZakatCertificateInquiryService.ItemChoiceType.CommercialRegistrationNumber,
        //            Item = "5852004964"
        //        };
        //        Logger.LogToFile(request, "");

        //        var result = (await service.ZakatCertificateInquiryAsync(request))?.ZakatCertificateInquiryRs;
        //        Logger.LogToFile(request, result);

        //        Assert.Equal(ServiceStatusCodes.Success.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        if (result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
        //        {
        //            if (result.Body != null)
        //            {
        //                Assert.Equal("3270392435", result.Body.ZakatCertificate.CertificateNumber.ToLower());
        //            }


        //        }

        //        // throw new WebServiceException(Logger.GetJsonString(request, result));
        //    }

        //    [Fact]
        //    public async Task ShouldValidateCRAndGetInfoAsync()
        //    {
        //        var service = GetMCICRValidationServiceClient();
        //        var request = new MCICRValidation11Service.MCICRValidationRq_Type
        //        {
        //            MsgRqHdr = MCICRValidationCRFillHeader()
        //        };

        //        request.Body = new MCICRValidation11MCICRValidationRqBody_Type()
        //        {
        //            CommercialRegistrationNumber = "4030595543"
        //        };
        //        Logger.LogToFile(request, "");

        //        var result = (await service.MCICRValidationAsync(request))?.MCICRValidationRs;
        //        Logger.LogToFile(request, result);

        //        Assert.Equal(ServiceStatusCodes.Success.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        if (result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
        //        {
        //            if (result.Body != null)
        //            {
        //                Logger.LogToFile(request, result);
        //                Assert.Equal("مؤسسة أحمد صلاح محمد باعاصم للتجارة", result.Body.Name.ToLower());

        //            }
        //        }
        //        //throw new WebServiceException(Logger.GetJsonString(request, result));
        //    }


        //    [Fact]
        //    //NO data found
        //    //TODO :: chekc data source :: sadeg :: ther
        //    public async Task ShouldGetCOCSubscriptionBySigiliNumberAsync()
        //    {
        //        var service = GetCOCSubscriptionInquiryServiceClient();
        //        var request = new COCSubscriptionInquiryRq_Type
        //        {
        //            MsgRqHdr = COCSubscriptionBySijilNumberFillHeader()
        //        };

        //        request.Body = new COCSubscriptionInquiryService.COCSubscriptionInquiryRqBody_Type()
        //        {
        //            LicenseNumber = "4030595543",
        //            CityCode = "201"
        //        };
        //        Logger.LogToFile(request, "");

        //        var result = (await service.COCSubscriptionInquiryAsync(request))?.COCSubscriptionInquiryRs;
        //        Logger.LogToFile(request, result);

        //        //Assert.Equal(ServiceStatusCodes.Success.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        Assert.Equal(ServiceStatusCodes.NoRecordsFoundCOCSubsciption.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        if (result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
        //        {
        //            if (result.Body != null)
        //            {
        //                Assert.Equal("105000000102", result.Body.MembershipDetails.MembershipId.ToLower());
        //            }
        //        }
        //        //throw new WebServiceException(Logger.GetJsonString(request, result));
        //    }


        //    [Fact]
        //    public async Task ShouldGetGOSICertificateInquiryAsync()
        //    {
        //        var service = GetGOSICertificateInquiryServiceClient();
        //        var request = new GOSICertificateInquiryService.GOSICertificateInquiryRq_Type
        //        {
        //            MsgRqHdr = GOSICertificateInquiryFillHeader()
        //        };

        //        request.Body = new GOSICertificateInquiryService.GOSICertificateInquiryRqBody_Type()
        //        {
        //            GOSIId = "506769353",
        //            GOSIIdType = GOSIIdType_Type.GosiRegistrationID,
        //            GOSIOwnerPersonIdType = GOSIOwnerPersonIdType_Type.NationalID

        //        };
        //        Logger.LogToFile(request, "");

        //        var result = (await service.GOSICertificateInquiryAsync(request))?.GOSICertificateInquiryRs;
        //        Logger.LogToFile(request, result);

        //        Assert.Equal(ServiceStatusCodes.Success.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        if (result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
        //        {
        //            if (result.Body != null)
        //            {
        //                Assert.Equal("506769353", result.Body.CompanyInformationList[0].GOSIRegistrationId.ToLower());
        //                Assert.Equal("شركه بوابه الغداء التجاريه", result.Body.CompanyInformationList[0].BusinessNameAr.ToLower());
        //            }
        //        }

        //        //throw new WebServiceException(Logger.GetJsonString(request, result));
        //    }

        //    [Fact]
        //    public async Task ShouldGetNitaqatInformationInquiryAsync()
        //    {
        //        var service = GetNitaqatInformationInquiryServiceClient();
        //        var request = new NitaqatInformationInquiryService.NitaqatInformationInquiryRq_Type
        //        {
        //            MsgRqHdr = NitaqatInformationInquiryFillHeader()
        //        };

        //        request.Body = new NitaqatInformationInquiryService.NitaqatInformationInquiryRqBody_Type()
        //        {
        //            EstablishmentLaborOfficeId = "9",
        //            EstablishmentSequenceNumber = "1626191"
        //        };
        //        Logger.LogToFile(request, "");

        //        var result = (await service.NitaqatInformationInquiryAsync(request))?.NitaqatInformationInquiryRs;
        //        Logger.LogToFile(request, result);

        //        Assert.Equal(ServiceStatusCodes.Success.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        if (result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
        //        {
        //            if (result.Body != null)
        //            {
        //                Assert.Equal("مؤسسة أحمد صلاح محمد باعاصم لتجارة الجملة و التجزئة", result.Body.EstablishmentName.ToLower());
        //            }
        //        }

        //        //throw new WebServiceException(Logger.GetJsonString(request, result));
        //    }

        //    [Fact]
        //    public async Task ShouldGetContractorDetailsInquiryAsync()
        //    {
        //        var service = GetContractorDetailsInquiryClient();
        //        var request = new ContractorDetailsInquiryRq_Type
        //        {
        //            MsgRqHdr = ContractorDetailsInquiryFillHeader()
        //        };

        //        request.Body = new ContractorDetailsInquiryRqBody_Type()
        //        {
        //            PartyId = new MOMRAContractorClassificationService.PartyId_Type()
        //            {
        //                PartyIdNum = "5852004964",
        //                PartyIdType = MOMRAContractorClassificationService.PartyIdType_Type.CommercialRegistration,
        //                PartyIdTypeSpecified = true
        //            }
        //        };
        //        Logger.LogToFile(request, "");

        //        var result = (await service.ContractorDetailsInquiryAsync(request))?.ContractorDetailsInquiryRs;
        //        Logger.LogToFile(request, result);
        //        Assert.Equal(ServiceStatusCodes.NoRecordsFoundContractorDetailsE006299.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        if (result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
        //        {
        //            if (result.Body != null)
        //            {
        //                Assert.Equal("على", result.Body.ContractorInfo.ContractorNameAR.ToLower());
        //            }
        //        }
        //    }
        //    [Fact]
        //    //No data found
        //    //TODO :: chekc data source :: sadeg :: thaer
        //    public async Task ShouldGetLicenseDetailsInquiryAsync()
        //    {
        //        var service = GetLicenseDetailsInquiryServiceClient();
        //        var request = new LicenseDetailsService.LicenseDetailsInquiryRq_Type
        //        {
        //            MsgRqHdr = LicenseDetailsServiceInquiryFillHeader()
        //        };

        //        request.Body = new LicenseDetailsService.LicenseDetailsInquiryRqBody_Type()
        //        {
        //            ItemElementName = LicenseDetailsService.ItemChoiceType.LicenseNumber,
        //            Item = "4030595543",//check
        //        };
        //        Logger.LogToFile(request, "");

        //        var result = (await service.LicenseDetailsInquiryAsync(request))?.LicenseDetailsInquiryRs;
        //        Logger.LogToFile(request, result);

        //        //Assert.Equal(ServiceStatusCodes.Success.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        Assert.Equal(ServiceStatusCodes.NoRecordsFoundLicenseDetails.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());

        //        if (result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
        //        {
        //            if (result.Body != null)
        //            {
        //                Assert.Equal("Invalid".ToLower(), result.Body.LicenseInfo.LicenseStatus.ToLower());
        //            }
        //        }

        //        //throw new WebServiceException(Logger.GetJsonString(request, result));
        //    }

        //    [Fact]
        //    public async Task ShouldGetLicenseStatusInquiryAsync()
        //    {
        //        var service = GetLicenseDetailsInquiryServiceClient();
        //        var request = new LicenseDetailsService.LicenseDetailsInquiryRq_Type
        //        {
        //            MsgRqHdr = LicenseStatusServiceInquiryFillHeader()
        //        };

        //        request.Body = new LicenseDetailsService.LicenseDetailsInquiryRqBody_Type()
        //        {
        //            ItemElementName = LicenseDetailsService.ItemChoiceType.CompanyId,
        //            Item = "357915"
        //        };
        //        Logger.LogToFile(request, "");

        //        var result = (await service.LicenseDetailsInquiryAsync(request))?.LicenseDetailsInquiryRs;
        //        Logger.LogToFile(request, result);

        //        Assert.Equal(ServiceStatusCodes.Success.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        //Assert.Equal(ServiceStatusCodes.NorecordsfoundforCompanyIdLicenseNumber.ToLower(), result.MsgRsHdr.ResponseStatus.StatusCode.ToLower());
        //        if (result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
        //        {
        //            if (result.Body != null)
        //            {
        //                Assert.Equal("Invalid".ToLower(), result.Body.LicenseInfo.LicenseStatus.ToLower());
        //            }
        //        }

        //        //throw new WebServiceException(Logger.GetJsonString(request, result));
        //    }
        //    #endregion

        //    #region Setup Headers
        //    /// <summary>
        //    /// function for fill Header
        //    /// </summary>
        //    /// <returns></returns>
        //    private MCICRValidation11MsgRqHdr_Type MCICRValidationCRFillHeader()
        //    {
        //        string mciCRValidationCRServiceID = _configuration.GetSection("YesserServiceID:MCICRValidationCRServiceID").Value;
        //        string mciCRValidationCRFunctionID = _configuration.GetSection("YesserFunctionID:MCICRValidationCRFunctionID").Value;
        //        string mciValidationChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;

        //        return new MCICRValidation11MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = mciValidationChannelID,
        //            ServiceId = mciCRValidationCRServiceID,
        //            FuncId = mciCRValidationCRFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    /// <summary>
        //    /// function for fill Header
        //    /// </summary>
        //    /// <returns></returns>
        //    private MCICRValidation11MsgRqHdr_Type MCICRValidationCROwnersFillHeader()
        //    {
        //        string mciCRValidationCRServiceID = _configuration.GetSection("YesserServiceID:MCICRValidationCRServiceID").Value;
        //        string mciCRValidationCROwnersFunctionID = _configuration.GetSection("YesserFunctionID:MCICRValidationCROwnersFunctionID").Value;
        //        string mciValidationChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;

        //        return new MCICRValidation11MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = mciValidationChannelID,
        //            ServiceId = mciCRValidationCRServiceID,
        //            FuncId = mciCRValidationCROwnersFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    private COCSubscriptionInquiryService.MsgRqHdr_Type COCSubscriptionByMemberIdFillHeader()
        //    {
        //        string cOCSubscriptionChannelID = _configuration.GetSection("YesserChannelID:COCSubscriptionChannelID").Value;
        //        string cOCSubscriptionInquiryByMemberIdFunctionID = _configuration.GetSection("YesserFunctionID:COCSubscriptionInquiryByMemberIdFunctionID").Value;
        //        string cOCSubscriptionInquiryServiceID = _configuration.GetSection("YesserServiceID:COCSubscriptionInquiryServiceID").Value;

        //        return new COCSubscriptionInquiryService.MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = cOCSubscriptionChannelID,
        //            ServiceId = cOCSubscriptionInquiryServiceID,
        //            FuncId = cOCSubscriptionInquiryByMemberIdFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    private COCSubscriptionInquiryService.MsgRqHdr_Type COCSubscriptionBySijilNumberFillHeader()
        //    {
        //        string cOCSubscriptionChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;
        //        string cOCSubscriptionInquiryBySijilNumberFunctionID = _configuration.GetSection("YesserFunctionID:COCSubscriptionInquiryBySijilNumberFunctionID").Value;
        //        string cOCSubscriptionInquiryServiceID = _configuration.GetSection("YesserServiceID:COCSubscriptionInquiryServiceID").Value;

        //        return new COCSubscriptionInquiryService.MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = cOCSubscriptionChannelID,
        //            ServiceId = cOCSubscriptionInquiryServiceID,
        //            FuncId = cOCSubscriptionInquiryBySijilNumberFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    private ZakatCertificateInquiryService.MsgRqHdr_Type ZakatCertificateInquiryByCrFillHeader()
        //    {
        //        string zakatCertificateInquiryChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;
        //        string zakatCertificateInquiryByCrFunctionID = _configuration.GetSection("YesserFunctionID:ZakatCertificateInquiryByCrFunctionID").Value;
        //        string zakatCertificateInquiryServiceID = _configuration.GetSection("YesserServiceID:ZakatCertificateInquiryServiceID").Value;

        //        return new ZakatCertificateInquiryService.MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = zakatCertificateInquiryChannelID,
        //            ServiceId = zakatCertificateInquiryServiceID,
        //            FuncId = zakatCertificateInquiryByCrFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    private ZakatCertificateInquiryService.MsgRqHdr_Type ZakatCertificateInquiryBySevenHandredCodeFillHeader()
        //    {
        //        string zakatCertificateInquiryChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;
        //        string zakatCertificateInquiryBySevenHandredCodeFunctionID = _configuration.GetSection("YesserFunctionID:ZakatCertificateInquiryBySevenHandredCodeFunctionID").Value;
        //        string zakatCertificateInquiryServiceID = _configuration.GetSection("YesserServiceID:ZakatCertificateInquiryServiceID").Value;

        //        return new ZakatCertificateInquiryService.MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = zakatCertificateInquiryChannelID,
        //            ServiceId = zakatCertificateInquiryServiceID,
        //            FuncId = zakatCertificateInquiryBySevenHandredCodeFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    private GOSICertificateInquiryService.MsgRqHdr_Type GOSICertificateInquiryFillHeader()
        //    {
        //        string gOSICertificateInquiryChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;
        //        string gOSICertificateInquiryFunctionID = _configuration.GetSection("YesserFunctionID:GOSICertificateInquiryFunctionID").Value;
        //        string gOSICertificateInquiryServiceID = _configuration.GetSection("YesserServiceID:GOSICertificateInquiryServiceID").Value;

        //        return new GOSICertificateInquiryService.MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = gOSICertificateInquiryChannelID,
        //            ServiceId = gOSICertificateInquiryServiceID,
        //            FuncId = gOSICertificateInquiryFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    private MOMRAContractorClassificationService.MsgRqHdr_Type ContractorDetailsInquiryFillHeader()
        //    {
        //        string gOSICertificateInquiryChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;
        //        string gOSICertificateInquiryFunctionID = _configuration.GetSection("YesserFunctionID:ContractorDetailsInquiryFunctionID").Value;
        //        string gOSICertificateInquiryServiceID = _configuration.GetSection("YesserServiceID:ContractorDetailsInquiryServiceID").Value;

        //        return new MOMRAContractorClassificationService.MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = gOSICertificateInquiryChannelID,
        //            ServiceId = gOSICertificateInquiryServiceID,
        //            FuncId = gOSICertificateInquiryFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    private NitaqatInformationInquiryService.MsgRqHdr_Type NitaqatInformationInquiryFillHeader()
        //    {
        //        string nitaqatInformationInquiryChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;
        //        string nitaqatInformationInquiryFunctionID = _configuration.GetSection("YesserFunctionID:NitaqatInformationInquiryFunctionID").Value;
        //        string nitaqatInformationInquiryServiceID = _configuration.GetSection("YesserServiceID:NitaqatInformationInquiryServiceID").Value;

        //        return new NitaqatInformationInquiryService.MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = nitaqatInformationInquiryChannelID,
        //            ServiceId = nitaqatInformationInquiryServiceID,
        //            FuncId = nitaqatInformationInquiryFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    private LicenseDetailsService.MsgRqHdr_Type LicenseDetailsServiceInquiryFillHeader()
        //    {
        //        string licenseDetailsInquiryChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;
        //        string licenseDetailsInquiryFunctionID = _configuration.GetSection("YesserFunctionID:LicenseDetailsInquiryFunctionID").Value;
        //        string licenseDetailsInquiryServiceID = _configuration.GetSection("YesserServiceID:LicenseDetailsInquiryServiceID").Value;

        //        return new LicenseDetailsService.MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = licenseDetailsInquiryChannelID,
        //            ServiceId = licenseDetailsInquiryServiceID,
        //            FuncId = licenseDetailsInquiryFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,
        //        };
        //    }
        //    private LicenseDetailsService.MsgRqHdr_Type LicenseStatusServiceInquiryFillHeader()
        //    {
        //        string licenseDetailsInquiryChannelID = _configuration.GetSection("YesserChannelID:YesserMainChannelID").Value;
        //        string licensestatusInquiryFunctionID = _configuration.GetSection("YesserFunctionID:LicensestatusInquiryFunctionID").Value;
        //        string licenseDetailsInquiryServiceID = _configuration.GetSection("YesserServiceID:LicenseDetailsInquiryServiceID").Value;

        //        return new LicenseDetailsService.MsgRqHdr_Type()
        //        {
        //            RqUID = RequestID,
        //            SCId = licenseDetailsInquiryChannelID,
        //            ServiceId = licenseDetailsInquiryServiceID,
        //            FuncId = licensestatusInquiryFunctionID,
        //            ClientDt = ClientDate,
        //            Version = Version,

        //        };
        //    }
        //    #endregion

        //    #region Setup ServiceClient
        //    private MCICRValidationClient GetMCICRValidationServiceClient()
        //    {
        //        var service = new MCICRValidationClient(MCICRValidationClient.EndpointConfiguration.MCICRValidationSOAP11, _mciCRValidationAddress);
        //        service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());

        //        if (_isProduction)
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
        //            service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
        //        }
        //        else
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
        //            service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
        //            new X509ServiceCertificateAuthentication()
        //            {
        //                CertificateValidationMode = X509CertificateValidationMode.None,
        //                RevocationMode = X509RevocationMode.NoCheck
        //            };
        //        }
        //        return service;
        //    }
        //    private COCSubscriptionInquiryClient GetCOCSubscriptionInquiryServiceClient()
        //    {
        //        var service = new COCSubscriptionInquiryClient(COCSubscriptionInquiryClient.EndpointConfiguration.COCSubscriptionInquirySOAP11, _cocSubscriptionInquiryAddress);
        //        service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());

        //        if (_isProduction)
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
        //            service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
        //        }
        //        else
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
        //            service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
        //            new X509ServiceCertificateAuthentication()
        //            {
        //                CertificateValidationMode = X509CertificateValidationMode.None,
        //                RevocationMode = X509RevocationMode.NoCheck
        //            };
        //        }
        //        return service;
        //    }
        //    private ZakatCertificateInquiryClient GetZakatCertificateInquiryServiceClient()
        //    {
        //        var service = new ZakatCertificateInquiryClient(ZakatCertificateInquiryClient.EndpointConfiguration.ZakatCertificateInquirySOAP11, _zakatCertificateInquiryAddress);
        //        service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());

        //        if (_isProduction)
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
        //            service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
        //        }
        //        else
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
        //            service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
        //            new X509ServiceCertificateAuthentication()
        //            {
        //                CertificateValidationMode = X509CertificateValidationMode.None,
        //                RevocationMode = X509RevocationMode.NoCheck
        //            };
        //        }
        //        return service;
        //    }
        //    private ContractorDetailsInquiryClient GetContractorDetailsInquiryClient()
        //    {
        //        var service = new ContractorDetailsInquiryClient(ContractorDetailsInquiryClient.EndpointConfiguration.ContractorDetailsInquirySOAP11, _momraContractorClassificationService);
        //        service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());

        //        if (_isProduction)
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
        //            service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
        //        }
        //        else
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
        //            service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
        //            new X509ServiceCertificateAuthentication()
        //            {
        //                CertificateValidationMode = X509CertificateValidationMode.None,
        //                RevocationMode = X509RevocationMode.NoCheck
        //            };
        //        }
        //        return service;
        //    }
        //    private GOSICertificateInquiryClient GetGOSICertificateInquiryServiceClient()
        //    {
        //        var service = new GOSICertificateInquiryClient(GOSICertificateInquiryClient.EndpointConfiguration.GOSICertificateInquirySOAP11, _gosiCertificateInquiryAddress);
        //        service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());

        //        if (_isProduction)
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
        //            service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
        //        }
        //        else
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
        //            service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
        //            new X509ServiceCertificateAuthentication()
        //            {
        //                CertificateValidationMode = X509CertificateValidationMode.None,
        //                RevocationMode = X509RevocationMode.NoCheck
        //            };
        //        }
        //        return service;
        //    }
        //    private NitaqatInformationInquiryClient GetNitaqatInformationInquiryServiceClient()
        //    {
        //        var service = new NitaqatInformationInquiryClient(NitaqatInformationInquiryClient.EndpointConfiguration.NitaqatInformationInquirySOAP11, _nitaqatInformationInquiryClientAddress);
        //        service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());

        //        if (_isProduction)
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
        //            service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
        //        }
        //        else
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
        //            service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
        //            new X509ServiceCertificateAuthentication()
        //            {
        //                CertificateValidationMode = X509CertificateValidationMode.None,
        //                RevocationMode = X509RevocationMode.NoCheck
        //            };
        //        }
        //        return service;
        //    }
        //    private LicenseDetailsInquiryClient GetLicenseDetailsInquiryServiceClient()
        //    {
        //        var service = new LicenseDetailsInquiryClient(LicenseDetailsInquiryClient.EndpointConfiguration.LicenseDetailsInquirySOAP11, _licenseDetailsServiceAddress);
        //        service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());

        //        if (_isProduction)
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
        //            service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
        //        }
        //        else
        //        {
        //            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
        //            service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
        //            new X509ServiceCertificateAuthentication()
        //            {
        //                CertificateValidationMode = X509CertificateValidationMode.None,
        //                RevocationMode = X509RevocationMode.NoCheck
        //            };
        //        }
        //        return service;
        //    }
        //    #endregion

        //    #region GetNewRequestID
        //    internal string GetNewRequestID()
        //    {
        //        return ChannelID + String.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
        //    }

        //    internal string GetCurrentDate()
        //    {
        //        return String.Format("{0:yyyy-MM-ddTHH:mm:ss}", DateTime.Now);
        //    }
        //    #endregion
        //}


    }
}
