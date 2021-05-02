using COCSubscriptionInquiryService;
using GOSICertificateInquiryService;
using LicenseDetailsService;
using MCICRValidation11Service;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOMRAContractorClassificationService;
using NitaqatInformationInquiryService;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using ZakatCertificateInquiryService;

namespace MOF.Etimad.Monafasat.Integration
{
    public class YasserProxy : ProxyBase, IYasserProxy
    {
        #region Properties
        protected override string Port { get { return _rootConfiguration.EsbSettingsConfiguration.PortValue; } }
        private readonly string _mciCRValidationAddress;
        private readonly string _cocSubscriptionInquiryAddress;
        private readonly string _zakatCertificateInquiryAddress;
        private readonly string _momraContractorClassificationService;
        private readonly string _licenseDetailsServiceAddress;
        private readonly string _gosiCertificateInquiryAddress;
        private readonly string _nitaqatInformationInquiryClientAddress;
        private readonly string _clientCertificateValue;
        private readonly bool _isProduction;
        #endregion

        #region Constructors
        public YasserProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _mciCRValidationAddress = _rootConfiguration.ServicesConfiguration.MCICRValidationService;
            _cocSubscriptionInquiryAddress = _rootConfiguration.ServicesConfiguration.COCSubscriptionInquiryService;
            _zakatCertificateInquiryAddress = _rootConfiguration.ServicesConfiguration.ZakatCertificateInquiryService;
            _momraContractorClassificationService = _rootConfiguration.ServicesConfiguration.MOMRAContractorClassificationService;
            _licenseDetailsServiceAddress = _rootConfiguration.ServicesConfiguration.LicenseDetailsInquiryService;
            _gosiCertificateInquiryAddress = _rootConfiguration.ServicesConfiguration.GOSICertificateInquiryService;
            _nitaqatInformationInquiryClientAddress = _rootConfiguration.ServicesConfiguration.NitaqatInformationInquiryService;
            _isProduction = _rootConfiguration.EsbSettingsConfiguration.IsProduction;
            _clientCertificateValue = _rootConfiguration.EsbSettingsConfiguration.ClientCertificateFindValue;
        }
        #endregion

        #region Methods

        #region ValidateMCICRAndGetInfo
        public async Task<MCICRInfoModel> ValidateMCICRAndGetInfo(MCICRInfoModelRequest model)
        {
            var isMCICRValidationWork = _rootConfiguration.isMCICRValidationConfiguration.isMCICRValidationWork;
            if (isMCICRValidationWork)
            {
                var service = GetMCICRValidationServiceClient();
                var request = new MCICRValidation11Service.MCICRValidationRq_Type
                {
                    MsgRqHdr = MCICRValidationCRFillHeader()
                };
                request.Body = new MCICRValidation11Service.MCICRValidationRqBody_Type()
                {
                    CommercialRegistrationNumber = model.CommercialRegistrationNumber
                };
                Logger.LogToFile(request, "");
                try
                {
                    var result = (await service.MCICRValidationAsync(request))?.MCICRValidationRs;
                    Logger.LogToFile(request, result);
                    if (result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                    {
                        if (result.Body != null)
                        {
                            var commercialRegistrationType = result.Body.CommercialRegistrationType;
                            return new MCICRInfoModel
                            {
                                Name = result.Body.Name,
                                CommercialRegistrationNameAr = result.Body.Name,
                                AddressDescription = new AddressDescription(result.Body.AddrDesc.AddrLine1),
                                Capital = result.Body.Capital,
                                CityNameAr = result.Body.CityNameAr,
                                CommercialRegistrationStatusInfo = new CommercialRegistrationStatusInfo((int)result.Body.CommercialRegistrationStatusInfo.CommercialRegistrationStatus),
                                CommercialRegistrationType = commercialRegistrationType == CommercialRegistrationType_Type.Main ? CommercialRegistrationType.Main : CommercialRegistrationType.Branch,
                                IssueDateHjri = DateTimeExtensions.ParseHijriDate(result.Body.IssueDtHjr).Value,
                                ExpiryDateHjri = DateTimeExtensions.ParseHijriDate(result.Body.ExpiryDtHjr).Value,
                                LegalTypeAr = result.Body.LegalTypeAr,
                                LegalTypeEn = result.Body.LegalTypeEn,
                                PhoneNumber = result.Body.PhoneNum,
                                FaxNumber = result.Body.FaxNum,
                                ResponseCode = result.MsgRsHdr.ResponseStatus.StatusCode,
                                CommercialRegistrationActivities = new CommercialRegistrationActivities(result.Body.CommercialRegistrationActivities.Item.ToString()/*CommercialRegistrationActivity*/),

                            };
                        }
                        else if (result.MsgRsHdr.ResponseStatus.StatusCode == "E001199")
                        {
                            return new MCICRInfoModel
                            {
                                statusName = Resources.SharedResources.ErrorMessages.CrExpired,
                                ResponseCode = result.MsgRsHdr.ResponseStatus.StatusCode
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else if (result != null && result.MsgRsHdr.ResponseStatus.StatusCode == "E001199")
                    {
                        return new MCICRInfoModel
                        {
                            statusName = Resources.SharedResources.ErrorMessages.CrExpired,
                            ResponseCode = result.MsgRsHdr.ResponseStatus.StatusCode
                        };
                    }
                    else if (result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.NoRecordsFoundValidateMCICR)
                    {
                        return null;
                    }
                }
                catch
                {
                    throw new BusinessRuleException("خطأ أثناء جلب البيانات لهذا السجل ");
                }
                throw new BusinessRuleException("خطأ أثناء جلب البيانات لهذا السجل ");
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region ValidateMCICRAndGetInfoWithOwners
        public async Task<MCICRInfoModel> ValidateMCICRAndGetInfoWithOwners(MCICRInfoModelRequest model)
        {
            var isMCICRValidationWork = _rootConfiguration.isMCICRValidationConfiguration.isMCICRValidationWork;
            if (isMCICRValidationWork)
            {
                var service = GetMCICRValidationServiceClient();
                var request = new MCICRValidation11Service.MCICRValidationRq_Type
                {
                    MsgRqHdr = MCICRValidationCRFillHeader()
                };
                request.Body = new MCICRValidation11Service.MCICRValidationRqBody_Type()
                {
                    CommercialRegistrationNumber = model.CommercialRegistrationNumber
                };
                Logger.LogToFile(request, "");
                var result = (await service.MCICRValidationAsync(request))?.MCICRValidationRs;
                Logger.LogToFile(request, result);
                if (result != null && result.Body != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                {
                    var commercialRegistrationType = result.Body.CommercialRegistrationType;
                    return new MCICRInfoModel
                    {
                        Name = result.Body.Name,
                        CommercialRegistrationNameAr = result.Body.Name,
                        AddressDescription = new AddressDescription(result.Body.AddrDesc.AddrLine1),
                        Capital = result.Body.Capital,
                        CityNameAr = result.Body.CityNameAr,
                        CommercialRegistrationStatusInfo = new CommercialRegistrationStatusInfo((int)result.Body.CommercialRegistrationStatusInfo.CommercialRegistrationStatus),
                        CommercialRegistrationType = commercialRegistrationType == CommercialRegistrationType_Type.Main ? CommercialRegistrationType.Main : CommercialRegistrationType.Branch,
                        IssueDateHjri = DateTimeExtensions.ParseHijriDate(result.Body.IssueDtHjr).Value,
                        ExpiryDateHjri = DateTimeExtensions.ParseHijriDate(result.Body.ExpiryDtHjr).Value,
                        LegalTypeAr = result.Body.LegalTypeAr,
                        LegalTypeEn = result.Body.LegalTypeEn,
                        PhoneNumber = result.Body.PhoneNum,
                        FaxNumber = result.Body.FaxNum,
                        CommercialRegistrationActivities = new CommercialRegistrationActivities(result.Body.CommercialRegistrationActivities.Item.ToString()/*CommercialRegistrationActivity*/),
                        CROwnersList = FillCrOwners(result.Body.CROwnersList)
                    };
                }
                throw new WebServiceException(Logger.GetJsonString(request, result));
            }
            else
            {
                return null;
            }
        }
        private static List<CROwner> FillCrOwners(CROwnersList cROwnersList)
        {
            List<CROwner> owners = new List<CROwner>();
            if (cROwnersList != null && cROwnersList.CROwner != null && cROwnersList.CROwner.Length > 0)
                foreach (var item in cROwnersList.CROwner)
                {
                    owners.Add(new CROwner(item.Name, item.NationalityEn, item.NationalityAr, item.Relation));
                }
            return owners;
        }
        #endregion
        #region GetCOCSubscription
        /// <summary>
        /// اشتراك الغرفة التجارية
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<COCSubscriptionInquiryModel> GetCOCSubscriptionBySijilNumber(COCSubscriptionInquiryRequestModel model)
        {
            var isCOCSubscriptionWork = _rootConfiguration.isCOCSubscriptionConfiguration.isCOCSubscriptionWork;
            if (isCOCSubscriptionWork)
            {
                var service = GetCOCSubscriptionInquiryServiceClient();
                var request = new COCSubscriptionInquiryRq_Type
                {
                    MsgRqHdr = COCSubscriptionBySijilNumberFillHeader()
                };

                request.Body = new COCSubscriptionInquiryService.COCSubscriptionInquiryRqBody_Type()
                {
                    LicenseNumber = model.LicenseNumber,
                    CityCode = model.CityCode

                };
                Logger.LogToFile(request, "");
                var result = (await service.COCSubscriptionInquiryAsync(request))?.COCSubscriptionInquiryRs;
                Logger.LogToFile(request, result);
                if (result != null && result.Body != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                {
                    return new COCSubscriptionInquiryModel
                    {
                        MembershipActivity = new MembershipActivity(result.Body.MembershipActivity.MainActivity, result.Body.MembershipActivity.SecondActivity),
                        MembershipAddress = new MembershipAddress(result.Body.MembershipAddress.CityName, result.Body.MembershipAddress.POBox, result.Body.MembershipAddress.Region, result.Body.MembershipAddress.LocationInfo, result.Body.MembershipAddress.PostalCode),
                        MembershipCapital = new MembershipCapital(result.Body.MembershipCapital.AllowedShares, result.Body.MembershipCapital.PaidShares, result.Body.MembershipCapital.AnnouncedCapital),
                        MembershipDetails = new MembershipDetails(result.Body.MembershipDetails.MembershipCity, result.Body.MembershipDetails.MembershipClass, result.Body.MembershipDetails.MembershipId, result.Body.MembershipDetails.MembershipFromDt, result.Body.MembershipDetails.MembershipToDt, result.Body.MembershipDetails.MembershipFromDtHjr, result.Body.MembershipDetails.MembershipToDtHjr),
                        MembershipEmail = result.Body.MembershipEmail,
                        MembershipEstablishment = new MembershipEstablishment(result.Body.MembershipEstablishment.EstJuridicalForm, result.Body.MembershipEstablishment.EstName, result.Body.MembershipEstablishment.EstNationality, result.Body.MembershipEstablishment.EstNumOfBranches),
                        MembershipFax = result.Body.MembershipFax,
                        MembershipMobile = result.Body.MembershipMobile,
                        MembershipOwner = new MembershipOwner(result.Body.MembershipOwner.OwnerGender, result.Body.MembershipOwner.OwnerName, result.Body.MembershipOwner.OwnerNationality, result.Body.MembershipOwner.OwnerPrefix, result.Body.MembershipOwner.OwnerPosition),
                        MembershipPhone = result.Body.MembershipPhone,
                        MembershipSijil = new MembershipSijil(result.Body.MembershipSijil.SijilFromDt, result.Body.MembershipSijil.SijilFromDtHjr, result.Body.MembershipSijil.SijilNum, result.Body.MembershipSijil.SijilSource, result.Body.MembershipSijil.SijilToDt, result.Body.MembershipSijil.SijilToDtHjr),
                        MembershipWebsite = result.Body.MembershipWebsite
                    };
                }
                else if (result != null && (result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.NoRecordsFoundCOCSubsciption
                    || result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.UnknownCOCErrorCOCSubsciption))
                {
                    return null;
                }
                throw new WebServiceException(Logger.GetJsonString(request, result));
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region ZakatCertificateInquiry
        public async Task<ZakatCertificateInquiryResponseModel> ZakatCertificateInquiryByCR(ZakatCertificateInquiryRequestModel model)
        {
            var isZakatCertificateWork = _rootConfiguration.isZakatCertificateConfiguration.isZakatCertificateWork;
            if (isZakatCertificateWork)
            {
                var service = GetZakatCertificateInquiryServiceClient();
                var request = new ZakatCertificateInquiryService.ZakatCertificateInquiryRq_Type
                {
                    MsgRqHdr = ZakatCertificateInquiryByCrFillHeader()
                };
                request.Body = new ZakatCertificateInquiryService.ZakatCertificateInquiryRqBody_Type()
                {
                    ItemElementName = ZakatCertificateInquiryService.ItemChoiceType.CommercialRegistrationNumber,
                    Item = model.CommercialRegistrationNumber
                };
                Logger.LogToFile(request, "");
                var result = (await service.ZakatCertificateInquiryAsync(request))?.ZakatCertificateInquiryRs;
                Logger.LogToFile(request, result);
                if (result != null && result.Body != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                {
                    var branches = new List<IntegrationBranchModel>();
                    if (result.Body.Party != null && ((OrganizationRec_Type)result.Body.Party.Item).Branches != null && ((OrganizationRec_Type)result.Body.Party.Item).Branches.Length > 0)
                        foreach (var item in ((OrganizationRec_Type)result.Body.Party.Item).Branches)
                        {
                            branches.Add(new IntegrationBranchModel(item.BranchName, item.BranchSerial, new PartyIdModel(item.BranchIdentity.PartyIdNum, (PartyType)(int)item.BranchIdentity.PartyIdType), item.LicenseNumber, new IntegrationCityModel(item.City.CityName, item.City.CityCode)));
                        }
                    return new ZakatCertificateInquiryResponseModel
                    {
                        ZakatCertificate = new ZakatCertificateModel(result.Body.ZakatCertificate.CertificateNumber, result.Body.ZakatCertificate.CertificateTIN, result.Body.ZakatCertificate.ExpiryDt, result.Body.ZakatCertificate.ExpiryDtHjr, result.Body.ZakatCertificate.IssueDt, result.Body.ZakatCertificate.IssueDtHjr, (CerificateType)((int)result.Body.ZakatCertificate.CerificateType)),
                        Party = new PartyModel(result.Body.Party?.FullNameAr, new PartyIdModel(result.Body?.Party?.PartyId.PartyIdNum, (PartyType)Convert.ToInt32(result.Body.Party?.PartyId.PartyIdType)), new OrganizationModel(branches))
                    };
                }
                else
                {
                    return null;
                }
                throw new WebServiceException(Logger.GetJsonString(request, result));
            }
            else
            {
                return null;
            }
        }


        #endregion
        #region GOSICertificateInquiry
        /// <summary>
        /// التأمينات الاجتماعية
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<GOSICertificateInquiryResponseModel> GOSICertificateInquiry(GOSICertificateInquiryRequestModel model)
        {
            var isGOSICertificateWork = _rootConfiguration.isGOSICertificateConfiguration.isGOSICertificateWork;
            if (isGOSICertificateWork)
            {
                var service = GetGOSICertificateInquiryServiceClient();
                var request = new GOSICertificateInquiryService.GOSICertificateInquiryRq_Type
                {
                    MsgRqHdr = GOSICertificateInquiryFillHeader()
                };
                request.Body = new GOSICertificateInquiryService.GOSICertificateInquiryRqBody_Type()
                {
                    GOSIId = model.GOSIId,
                    GOSIIdType = (GOSIIdType_Type)(int)model.GOSIIdType,
                    GOSIOwnerPersonIdType = (GOSIOwnerPersonIdType_Type)(int)model.GOSIOwnerPersonIdType
                };
                Logger.LogToFile(request, "");
                var result = (await service.GOSICertificateInquiryAsync(request))?.GOSICertificateInquiryRs;
                Logger.LogToFile(request, result);
                if (result != null && result.Body != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                {
                    List<CompanyInformationModel> CompanyInformationModellist = new List<CompanyInformationModel>();
                    foreach (var item in result.Body.CompanyInformationList)
                    {
                        CompanyInformationModellist.Add(new CompanyInformationModel(item.BusinessNameAr, item.GOSIRegistrationId, item.BusinessNameEn));
                    }
                    return new GOSICertificateInquiryResponseModel(CompanyInformationModellist);

                }
                return null;
            }
            else
            {
                List<CompanyInformationModel> CompanyInformationModellist = new List<CompanyInformationModel>();
                CompanyInformationModellist.Add(new CompanyInformationModel("شركة الضمان لتجارة مواد البناء", "13253331", ""));
                return new GOSICertificateInquiryResponseModel(CompanyInformationModellist);
            }
        }

        #endregion
        #region ContractorDetailsInquiry
        public async Task<ContractorDetailsResponseModel> ContractorDetailsInquiry(ContractorDetailsRequestModel model)
        {
            var isMOMRAContractorClassificationWork = _rootConfiguration.isMOMRAContractorClassificationConfiguration.isMOMRAContractorClassificationWork;
            if (isMOMRAContractorClassificationWork)
            {
                var service = GetContractorDetailsInquiryClient();
                var request = new ContractorDetailsInquiryRq_Type
                {
                    MsgRqHdr = ContractorDetailsInquiryFillHeader()
                };
                request.Body = new ContractorDetailsInquiryRqBody_Type()
                {
                    PartyId = new MOMRAContractorClassificationService.PartyId_Type()
                    {
                        PartyIdNum = model.PartyId.PartyIdNumber,
                        PartyIdType = (MOMRAContractorClassificationService.PartyIdType_Type.CommercialRegistration)
                    }

                };
                Logger.LogToFile(request, "");
                var result = (await service.ContractorDetailsInquiryAsync(request))?.ContractorDetailsInquiryRs;
                Logger.LogToFile(request, result);
                if (result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                {
                    if (result.Body != null)
                    {
                        ContractorInfoModel ContractorInfo = new ContractorInfoModel()
                        {
                            ContractorNameAR = result.Body.ContractorInfo.ContractorNameAR,
                            ContractorNameEn = result.Body.ContractorInfo.ContractorNameEn,
                            ClassificationDt = result.Body.ContractorInfo.ClassificationDt,
                            ClassificationDtHjr = result.Body.ContractorInfo.ClassificationDtHjr,
                            ContractorCompany = result.Body.ContractorInfo.ContractorCompany,
                        };
                        ContractorInfo.ContractorContactInfo.Region = result.Body.ContractorInfo.ContractorContactInfo.Region;
                        ContractorInfo.ContractorContactInfo.ZipCode = result.Body.ContractorInfo.ContractorContactInfo.ZipCode;
                        ContractorInfo.ContractorContactInfo.POBox = result.Body.ContractorInfo.ContractorContactInfo.POBox;
                        ContractorInfo.ContractorContactInfo.EmailInfo = new EmailInfoModel()
                        {
                            Email = result.Body.ContractorInfo.ContractorContactInfo.EmailInfo.Email,
                            EmailUsage = result.Body.ContractorInfo.ContractorContactInfo.EmailInfo.EmailUsage,
                            PreferEmailFlag = result.Body.ContractorInfo.ContractorContactInfo.EmailInfo.PreferEmailFlag == MOMRAContractorClassificationService.Flg_Type.Y ? true : false,
                            PreferEmailFlagSpecified = result.Body.ContractorInfo.ContractorContactInfo.EmailInfo.PreferEmailFlagSpecified
                        };
                        foreach (var item in result.Body.ContractorInfo.ContractorClassificationInfo)
                        {
                            ContractorInfo.ContractorClassificationInfo.Add(new ContractorClassificationInfoModel()
                            {
                                ClassificationDt = item.ClassificationDt,
                                ClassificationField = item.ClassificationField,
                                ClassificationGrade = item.ClassificationGrade,
                                ClassificationDtHjr = item.ClassificationDtHjr,
                                ClassificationDtSpecified = item.ClassificationDtSpecified
                            });
                        }
                        return new ContractorDetailsResponseModel()
                        {
                            ContractorInfo = ContractorInfo
                        };
                    }
                }
                else if (result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.NoRecordsFoundContractorDetails)
                {
                    return null;
                }
                throw new WebServiceException(Logger.GetJsonString(request, result));
            }
            else
            {
                return null;
            }
        }

        #endregion
        #region NitaqatInformationInquiry
        /// <summary>
        /// النطاق فى وزارة العمل
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<NitaqatInformationResponseModel> NitaqatInformationInquiry(NitaqatInformationRequestModel model)
        {

            var isNitaqatInformationWork = _rootConfiguration.isNitaqatInformationConfiguration.isNitaqatInformationWork;
            if (isNitaqatInformationWork)
            {
                var service = GetNitaqatInformationInquiryServiceClient();
                var request = new NitaqatInformationInquiryService.NitaqatInformationInquiryRq_Type
                {
                    MsgRqHdr = NitaqatInformationInquiryFillHeader()
                };
                request.Body = new NitaqatInformationInquiryService.NitaqatInformationInquiryRqBody_Type()
                {
                    EstablishmentLaborOfficeId = model.EstablishmentLaborOfficeId,
                    EstablishmentSequenceNumber = model.EstablishmentSequenceNumber
                };
                Logger.LogToFile(request, "");
                var result = (await service.NitaqatInformationInquiryAsync(request))?.NitaqatInformationInquiryRs;
                Logger.LogToFile(request, result);
                if (result != null && result.Body != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                {
                    return new NitaqatInformationResponseModel()
                    {
                        EstablishmentName = result.Body.EstablishmentName,
                        EntityColor = new EntityColorModel()
                        {
                            EntityColorId = result.Body.EntityColor.EntityColorId,
                            EntityColorName = result.Body.EntityColor.EntityColorName
                        },
                        EconomicActivityName = result.Body.EconomicActivityName,
                        EstablishmentSize = result.Body.EstablishmentSize,
                        SaudiPercentage = result.Body.SaudiPercentage
                    };
                }
                return null;
            }
            else
            {
                return new NitaqatInformationResponseModel()
                {
                    EstablishmentName = "مؤسسة جديد الشمال للمقاولات المعمارية",
                    EntityColor = new EntityColorModel()
                    {
                        EntityColorId = "300",
                        EntityColorName = "اخضر منخفض"
                    },
                    EconomicActivityName = "كيانات مجمعة",
                    EstablishmentSize = "Small-2",
                    SaudiPercentage = "14.29"
                };
            }
        }
        #endregion
        #endregion
        #region Setup Headers
        /// <summary>
        /// function for fill Header
        /// </summary>
        /// <returns></returns>
        private MCICRValidation11Service.MsgRqHdr_Type MCICRValidationCRFillHeader()
        {
            string mciCRValidationCRServiceID = _rootConfiguration.YesserServiceIDConfiguration.MCICRValidationCRServiceID;
            string mciCRValidationCRFunctionID = _rootConfiguration.YesserFunctionIDConfiguration.MCICRValidationCRFunctionID;
            string mciValidationChannelID = _rootConfiguration.YesserChannelIDConfiguration.MCIValidationChannelID;
            return new MCICRValidation11Service.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = mciValidationChannelID,
                ServiceId = mciCRValidationCRServiceID,
                FuncId = mciCRValidationCRFunctionID,
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        private COCSubscriptionInquiryService.MsgRqHdr_Type COCSubscriptionBySijilNumberFillHeader()
        {
            string cOCSubscriptionChannelID = _rootConfiguration.YesserChannelIDConfiguration.COCSubscriptionChannelID;
            string cOCSubscriptionInquiryBySijilNumberFunctionID = _rootConfiguration.YesserFunctionIDConfiguration.COCSubscriptionInquiryBySijilNumberFunctionID;
            string cOCSubscriptionInquiryServiceID = _rootConfiguration.YesserServiceIDConfiguration.COCSubscriptionInquiryServiceID;

            return new COCSubscriptionInquiryService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = cOCSubscriptionChannelID,
                ServiceId = cOCSubscriptionInquiryServiceID,
                FuncId = cOCSubscriptionInquiryBySijilNumberFunctionID,
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        private ZakatCertificateInquiryService.MsgRqHdr_Type ZakatCertificateInquiryByCrFillHeader()
        {
            string zakatCertificateInquiryChannelID = _rootConfiguration.YesserChannelIDConfiguration.ZakatCertificateInquiryChannelID;
            string zakatCertificateInquiryByCrFunctionID = _rootConfiguration.YesserFunctionIDConfiguration.ZakatCertificateInquiryByCrFunctionID;
            string zakatCertificateInquiryServiceID = _rootConfiguration.YesserServiceIDConfiguration.ZakatCertificateInquiryServiceID;

            return new ZakatCertificateInquiryService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = zakatCertificateInquiryChannelID,
                ServiceId = zakatCertificateInquiryServiceID,
                FuncId = zakatCertificateInquiryByCrFunctionID,
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        private GOSICertificateInquiryService.MsgRqHdr_Type GOSICertificateInquiryFillHeader()
        {
            string gOSICertificateInquiryChannelID = _rootConfiguration.YesserChannelIDConfiguration.GOSICertificateInquiryChannelID;
            string gOSICertificateInquiryFunctionID = _rootConfiguration.YesserFunctionIDConfiguration.GOSICertificateInquiryFunctionID;
            string gOSICertificateInquiryServiceID = _rootConfiguration.YesserServiceIDConfiguration.GOSICertificateInquiryServiceID;

            return new GOSICertificateInquiryService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = gOSICertificateInquiryChannelID,
                ServiceId = gOSICertificateInquiryServiceID,
                FuncId = gOSICertificateInquiryFunctionID,
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        private MOMRAContractorClassificationService.MsgRqHdr_Type ContractorDetailsInquiryFillHeader()
        {
            string gOSICertificateInquiryChannelID = _rootConfiguration.YesserChannelIDConfiguration.ContractorDetailsInquiryChannelID;
            string gOSICertificateInquiryFunctionID = _rootConfiguration.YesserFunctionIDConfiguration.ContractorDetailsInquiryFunctionID;
            string gOSICertificateInquiryServiceID = _rootConfiguration.YesserServiceIDConfiguration.ContractorDetailsInquiryServiceID;

            return new MOMRAContractorClassificationService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = gOSICertificateInquiryChannelID,
                ServiceId = gOSICertificateInquiryServiceID,
                FuncId = gOSICertificateInquiryFunctionID,
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        private NitaqatInformationInquiryService.MsgRqHdr_Type NitaqatInformationInquiryFillHeader()
        {
            string nitaqatInformationInquiryChannelID = _rootConfiguration.YesserChannelIDConfiguration.NitaqatInformationInquiryChannelID;
            string nitaqatInformationInquiryFunctionID = _rootConfiguration.YesserFunctionIDConfiguration.NitaqatInformationInquiryFunctionID;
            string nitaqatInformationInquiryServiceID = _rootConfiguration.YesserServiceIDConfiguration.NitaqatInformationInquiryServiceID;

            return new NitaqatInformationInquiryService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = nitaqatInformationInquiryChannelID,
                ServiceId = nitaqatInformationInquiryServiceID,
                FuncId = nitaqatInformationInquiryFunctionID,
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        private LicenseDetailsService.MsgRqHdr_Type LicenseDetailsServiceInquiryFillHeader()
        {
            string licenseDetailsInquiryChannelID = _rootConfiguration.YesserChannelIDConfiguration.LicenseDetailsInquiryChannelID;
            string licenseDetailsInquiryFunctionID = _rootConfiguration.YesserFunctionIDConfiguration.LicenseDetailsInquiryFunctionID;
            string licenseDetailsInquiryServiceID = _rootConfiguration.YesserServiceIDConfiguration.LicenseDetailsInquiryServiceID;

            return new LicenseDetailsService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = licenseDetailsInquiryChannelID,
                ServiceId = licenseDetailsInquiryServiceID,
                FuncId = licenseDetailsInquiryFunctionID,
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        #endregion
        #region Setup ServiceClient
        private MCICRValidationClient GetMCICRValidationServiceClient()
        {
            var service = new MCICRValidationClient(MCICRValidationClient.EndpointConfiguration.MCICRValidationSOAP11, _mciCRValidationAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }
        private COCSubscriptionInquiryClient GetCOCSubscriptionInquiryServiceClient()
        {
            var service = new COCSubscriptionInquiryClient(COCSubscriptionInquiryClient.EndpointConfiguration.COCSubscriptionInquirySOAP11, _cocSubscriptionInquiryAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }
        private ZakatCertificateInquiryClient GetZakatCertificateInquiryServiceClient()
        {
            var service = new ZakatCertificateInquiryClient(ZakatCertificateInquiryClient.EndpointConfiguration.ZakatCertificateInquirySOAP11, _zakatCertificateInquiryAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }
        private ContractorDetailsInquiryClient GetContractorDetailsInquiryClient()
        {
            var service = new ContractorDetailsInquiryClient(ContractorDetailsInquiryClient.EndpointConfiguration.ContractorDetailsInquirySOAP11, _momraContractorClassificationService);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }
        private GOSICertificateInquiryClient GetGOSICertificateInquiryServiceClient()
        {
            var service = new GOSICertificateInquiryClient(GOSICertificateInquiryClient.EndpointConfiguration.GOSICertificateInquirySOAP11, _gosiCertificateInquiryAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }
        private NitaqatInformationInquiryClient GetNitaqatInformationInquiryServiceClient()
        {
            var service = new NitaqatInformationInquiryClient(NitaqatInformationInquiryClient.EndpointConfiguration.NitaqatInformationInquirySOAP11, _nitaqatInformationInquiryClientAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }
        private LicenseDetailsInquiryClient GetLicenseDetailsInquiryServiceClient()
        {
            var service = new LicenseDetailsInquiryClient(LicenseDetailsInquiryClient.EndpointConfiguration.LicenseDetailsInquirySOAP11, _licenseDetailsServiceAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }
        #endregion

        public async Task<LicenseDetailsResponseModel> LicenseStatusInquiry(LicenseDetailsRequestModel model)
        {
            var _licenseDetailsResponseModel = new LicenseDetailsResponseModel();
            var isLicenseDetailsWork = _rootConfiguration.isLicenseDetailsConfiguration.isLicenseDetailsWork;
            if (isLicenseDetailsWork)
            {
                var service = GetLicenseDetailsInquiryServiceClient();
                var request = new LicenseDetailsService.LicenseDetailsInquiryRq_Type
                {
                    MsgRqHdr = LicenseDetailsServiceInquiryFillHeader()
                };
                request.Body = new LicenseDetailsService.LicenseDetailsInquiryRqBody_Type()
                {
                    ItemElementName = LicenseDetailsService.ItemChoiceType.LicenseNumber,
                    Item = model.LicenseNumber
                };
                var result = (await service.LicenseDetailsInquiryAsync(request))?.LicenseDetailsInquiryRs;
                Logger.LogToFile(request, result);
                if (result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                {
                    if (result.Body?.LicenseInfo != null)
                    {
                        _licenseDetailsResponseModel.hasLicense = true;
                        _licenseDetailsResponseModel.LicenseInfo = new LicenseInfoModel()
                        {
                            ExpiryDt = result.Body.LicenseInfo.ExpiryDt,
                            IssueDt = result.Body.LicenseInfo.IssueDt,
                            LicenseNumber = result.Body.LicenseInfo.LicenseNumber,
                            LicenseStatus = result.Body?.LicenseInfo?.LicenseStatus,
                        };
                    }
                    else
                    {
                        _licenseDetailsResponseModel.hasLicense = false;
                    }
                }
                else if (result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.NorecordsfoundforCompanyIdLicenseNumber)
                {
                    _licenseDetailsResponseModel.hasLicense = false;
                }
                return _licenseDetailsResponseModel;
                throw new WebServiceException(Logger.GetJsonString(request, result));
            }
            else
            {
                _licenseDetailsResponseModel.hasLicense = true;
                _licenseDetailsResponseModel.LicenseInfo = new LicenseInfoModel()
                {
                    ExpiryDt = DateTime.Now,//23/2/36
                    IssueDt = DateTime.Now, // 24-2-26
                    LicenseNumber = "11203002999",
                    LicenseStatus = "Suspended",
                };
                return _licenseDetailsResponseModel;
            }
        }
    }
}
